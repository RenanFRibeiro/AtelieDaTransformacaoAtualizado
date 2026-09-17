using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.Services;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.Infrastructure.Schema;
using AtelieDaTransformacao.Infrastructure.Repositories;
using AtelieDaTransformacao.UI.Hubs;
using AtelieDaTransformacao.UI.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Adicionar logging console para diagnóstico
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            var logger = LoggerFactory
                .Create(config => config.AddConsole())
                .CreateLogger("StartupLogger");

            logger.LogInformation("🚀 Iniciando aplicação em ambiente: {Environment}",
                builder.Environment.EnvironmentName);

            var connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                logger.LogError("❌ CRÍTICO: Connection string 'DefaultConnection' não foi configurada!");
                logger.LogError("Variáveis de ambiente disponíveis: {Env}",
                    string.Join(", ", Environment.GetEnvironmentVariables()
                        .Keys.Cast<string>()
                        .Where(k => k.Contains("Connection", StringComparison.OrdinalIgnoreCase))));
                throw new InvalidOperationException(
                    "Connection string 'DefaultConnection' not found. Configure ConnectionStrings__DefaultConnection in Azure App Service or appsettings.json");
            }

            logger.LogInformation("✅ Connection String carregada com sucesso");

            builder.Services.AddDbContext<
                AtelieDaTransformacaoDbContext>(
                options =>
                    options.UseSqlServer(
                        connectionString));

            builder.Services
                .AddIdentity<IdentityUser, IdentityRole>(
                    options =>
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 8;
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireLowercase = true;
                        options.Lockout.MaxFailedAccessAttempts = 5;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                        options.Lockout.AllowedForNewUsers = true;

                        options.User.RequireUniqueEmail = true;
                        // A conta só pode ser usada depois que o cliente comprovar
                        // que controla o endereço de e-mail informado no cadastro.
                        // Isso vale também em desenvolvimento para que o fluxo testado
                        // seja o mesmo que irá para produção.
                        options.SignIn.RequireConfirmedEmail = true;
                        options.Password.RequiredUniqueChars = 1;
                    })
                .AddEntityFrameworkStores<
                    AtelieDaTransformacaoDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(
                options =>
                {
                    options.LoginPath =
                        "/Account/Login";

                    options.AccessDeniedPath =
                        "/Account/AccessDenied";

                    options.ExpireTimeSpan =
                        TimeSpan.FromHours(8);

                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = builder.Environment.IsProduction() ? CookieSecurePolicy.Always : CookieSecurePolicy.SameAsRequest;
                });

            builder.Services.AddScoped<
                IProductRepository,
                ProductRepository>();

            builder.Services.AddScoped<
                IProductCategoryRepository,
                ProductCategoryRepository>();

            builder.Services.AddScoped<
                IOrderRepository,
                OrderRepository>();

            builder.Services.AddScoped<
                IFeedbackRepository,
                FeedbackRepository>();

            builder.Services.AddScoped<
                IOrderService,
                OrderService>();

            builder.Services.AddScoped<
                IProductService,
                ProductService>();

            builder.Services.AddScoped<
                IProductCategoryService,
                ProductCategoryService>();

            builder.Services.AddHttpClient<
                ICepService,
                CepService>();

            builder.Services.AddScoped<
                IFreteService,
                FreteService>();

            builder.Services.AddScoped<
                IWhatsAppService,
                WhatsAppService>();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(
                options =>
                {
                    options.IdleTimeout =
                        TimeSpan.FromHours(2);

                    options.Cookie.HttpOnly =
                        true;

                    options.Cookie.IsEssential = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = builder.Environment.IsProduction() ? CookieSecurePolicy.Always : CookieSecurePolicy.SameAsRequest;
                });

            builder.Services.AddHttpClient("EmailDns", client =>
            {
                client.BaseAddress = new Uri("https://cloudflare-dns.com/");
                client.Timeout = TimeSpan.FromSeconds(8);
                client.DefaultRequestHeaders.Accept.ParseAdd("application/dns-json");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("AtelieDaTransformacao/1.0");
            });

            builder.Services.AddHttpClient("BrevoEmail", client =>
            {
                client.BaseAddress = new Uri("https://api.brevo.com/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.UserAgent.ParseAdd("AtelieDaTransformacao/1.0");
            });

            builder.Services.Configure<EmailOptions>(
                builder.Configuration.GetSection("Email"));
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                // Links de confirmação e recuperação expiram após 24 horas.
                options.TokenLifespan = TimeSpan.FromHours(24);
            });
            builder.Services.Configure<OrderAutomationOptions>(
                builder.Configuration.GetSection("OrderAutomation"));

            builder.Services.AddSingleton<IEmailAddressVerifier, EmailAddressVerifier>();
            builder.Services.AddSingleton<IEmailService, BrevoEmailService>();

            builder.Services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 300,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0,
                            AutoReplenishment = true
                        }));

                options.AddFixedWindowLimiter("auth", limiter =>
                {
                    limiter.PermitLimit = 10;
                    limiter.Window = TimeSpan.FromMinutes(1);
                    limiter.QueueLimit = 0;
                    limiter.AutoReplenishment = true;
                });
            });

            builder.Services.AddControllersWithViews(options =>
            {
                // Os campos não obrigatórios não devem receber [Required] implicitamente
                // apenas por serem strings não anuláveis. As validações obrigatórias
                // continuam sendo definidas explicitamente por [Required] e pelo domínio.
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });

            builder.Services.AddSignalR();

            builder.Services.AddHostedService<OrderAutomationWorker>();

            var app = builder.Build();

            logger.LogInformation("⚙️ Configurando banco de dados...");

            try
            {
                await using (
                    var scope =
                        app.Services.CreateAsyncScope())
                {
                    var db =
                        scope.ServiceProvider
                            .GetRequiredService<
                                AtelieDaTransformacaoDbContext>();

                    logger.LogInformation("📊 Executando migrations...");
                    await db.Database.MigrateAsync();
                    logger.LogInformation("✅ Migrations executadas com sucesso");

                    logger.LogInformation("📋 Inicializando schema de pedidos...");
                    await OrderSchemaInitializer
                        .EnsureAsync(db);
                    logger.LogInformation("✅ Schema de pedidos inicializado");
                }
            }
            catch (Exception dbEx)
            {
                logger.LogError(dbEx, "❌ Erro ao configurar banco de dados: {Message}", dbEx.Message);
                throw;
            }

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(
                    "/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.Use(async (context, next) =>
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["X-Frame-Options"] = "DENY";
                context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
                context.Response.Headers["Permissions-Policy"] = "camera=(self), microphone=(), geolocation=()";
                await next();
            });

            var staticFileContentTypeProvider = new FileExtensionContentTypeProvider();
            staticFileContentTypeProvider.Mappings[".webmanifest"] = "application/manifest+json";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = staticFileContentTypeProvider,
                OnPrepareResponse = context =>
                {
                    // O service worker precisa ser sempre revalidado pelo navegador,
                    // senão atualizações do app shell podem demorar a chegar aos usuários.
                    if (context.File.Name.Equals("sw.js", StringComparison.OrdinalIgnoreCase))
                    {
                        context.Context.Response.Headers["Cache-Control"] = "no-cache";
                    }
                }
            });

            app.UseRouting();

            app.UseSession();

            app.UseRateLimiter();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapHub<OrderStatusHub>(
                "/hubs/orders");

            app.MapControllerRoute(
                name: "default",
                pattern:
                    "{controller=Home}/{action=Index}/{id?}");

            logger.LogInformation("🎉 Aplicação iniciada com sucesso! Ambiente: {Environment}",
                app.Environment.EnvironmentName);

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            var logger = LoggerFactory
                .Create(config => config.AddConsole())
                .CreateLogger("StartupLogger");

            logger.LogError(ex, "❌ Erro crítico durante inicialização da aplicação: {Message}\n{StackTrace}",
                ex.Message, ex.StackTrace);
            throw;
        }
    }
}
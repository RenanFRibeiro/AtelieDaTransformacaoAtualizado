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
using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.UI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder =
            WebApplication.CreateBuilder(args);

        var connectionString =
            builder.Configuration.GetConnectionString(
                "DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");

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
                    options.SignIn.RequireConfirmedEmail = builder.Environment.IsProduction();
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

        builder.Services.Configure<EmailOptions>(
            builder.Configuration.GetSection("Email"));
        builder.Services.Configure<OrderAutomationOptions>(
            builder.Configuration.GetSection("OrderAutomation"));

        builder.Services.AddSingleton<IEmailService, SmtpEmailService>();

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

        var app =
            builder.Build();

        await using (
            var scope =
                app.Services.CreateAsyncScope())
        {
            var db =
                scope.ServiceProvider
                    .GetRequiredService<
                        AtelieDaTransformacaoDbContext>();

            await db.Database.MigrateAsync();

            await OrderSchemaInitializer
                .EnsureAsync(db);
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

        app.UseStaticFiles();

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

        await app.RunAsync();
    }
}
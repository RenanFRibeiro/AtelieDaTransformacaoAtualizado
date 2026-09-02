using AtelieDaTransformacao.Application.Interfaces;
using AtelieDaTransformacao.Application.Services;
using AtelieDaTransformacao.Domain.Interfaces;
using AtelieDaTransformacao.Infrastructure.Context;
using AtelieDaTransformacao.Infrastructure.Schema;
using AtelieDaTransformacao.Infrastructure.Repositories;
using AtelieDaTransformacao.UI.Hubs;

using Microsoft.AspNetCore.Identity;
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
                    options.Password.RequireDigit =
                        false;

                    options.Password.RequiredLength =
                        6;

                    options.Password.RequireNonAlphanumeric =
                        false;

                    options.Password.RequireUppercase =
                        false;

                    options.Password.RequireLowercase =
                        false;

                    options.User.RequireUniqueEmail =
                        true;
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

                options.SlidingExpiration =
                    true;
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

                options.Cookie.IsEssential =
                    true;
            });

        builder.Services.AddControllersWithViews();

        builder.Services.AddSignalR();

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

        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

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
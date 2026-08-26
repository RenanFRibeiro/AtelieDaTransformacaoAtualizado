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
        var builder = WebApplication.CreateBuilder(args);

        // =========================================================
        // CONNECTION STRING
        // =========================================================

        var connectionString =
            builder.Configuration.GetConnectionString(
                "DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");


        // =========================================================
        // DATABASE
        // =========================================================

        builder.Services.AddDbContext<
            AtelieDaTransformacaoDbContext>(
            options =>
                options.UseSqlServer(
                    connectionString));


        // =========================================================
        // IDENTITY
        // =========================================================

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


        // =========================================================
        // COOKIE DE AUTENTICAÇÃO
        // =========================================================

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


        // =========================================================
        // PRODUCT REPOSITORIES
        // =========================================================

        builder.Services.AddScoped<
            IProductRepository,
            ProductRepository>();

        builder.Services.AddScoped<
            IProductCategoryRepository,
            ProductCategoryRepository>();


        // =========================================================
        // ORDER REPOSITORY
        // =========================================================

        builder.Services.AddScoped<
    IOrderRepository,
    OrderRepository>();

        builder.Services.AddScoped<
            IOrderService,
            OrderService>();


        // =========================================================
        // PRODUCT SERVICES
        // =========================================================

        builder.Services.AddScoped<
            IProductService,
            ProductService>();

        builder.Services.AddScoped<
            IProductCategoryService,
            ProductCategoryService>();


        // =========================================================
        // ORDER SERVICE
        // =========================================================

        builder.Services.AddScoped<
            IOrderService,
            OrderService>();


        // =========================================================
        // WHATSAPP
        // =========================================================

        builder.Services.AddScoped<
            IWhatsAppService,
            WhatsAppService>();


        // =========================================================
        // SESSION
        // =========================================================

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


        // =========================================================
        // MVC
        // =========================================================

        builder.Services.AddControllersWithViews();


        // =========================================================
        // SIGNALR
        // =========================================================

        builder.Services.AddSignalR();


        // =========================================================
        // BUILD
        // =========================================================

        var app =
            builder.Build();


        // =========================================================
        // DATABASE MIGRATIONS
        // =========================================================

        await using (
            var scope =
                app.Services.CreateAsyncScope())
        {
            var db =
                scope.ServiceProvider
                    .GetRequiredService<
                        AtelieDaTransformacaoDbContext>();

            await db.Database.MigrateAsync();
            await OrderSchemaInitializer.EnsureAsync(db);
        }


        // =========================================================
        // ERROR HANDLING
        // =========================================================

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler(
                "/Home/Error");

            app.UseHsts();
        }


        // =========================================================
        // HTTP PIPELINE
        // =========================================================

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

        app.UseAuthentication();

        app.UseAuthorization();


        // =========================================================
        // SIGNALR HUB
        // =========================================================

        app.MapHub<OrderStatusHub>(
            "/hubs/orders");


        // =========================================================
        // MVC ROUTE
        // =========================================================

        app.MapControllerRoute(
            name: "default",
            pattern:
                "{controller=Home}/{action=Index}/{id?}");


        // =========================================================
        // START
        // =========================================================

        await app.RunAsync();
    }
}
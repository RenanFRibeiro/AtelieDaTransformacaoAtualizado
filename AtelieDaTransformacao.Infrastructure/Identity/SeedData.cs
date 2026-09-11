using System.Security.Claims;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AtelieDaTransformacao.Infrastructure.Identity;

public static class SeedData
{
    public static async Task InitializeAsync(
        IServiceProvider services,
        string? adminEmail,
        string? adminPassword)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var db = services.GetRequiredService<AtelieDaTransformacaoDbContext>();
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            var roleResult = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!roleResult.Succeeded)
                throw new InvalidOperationException(string.Join(" | ", roleResult.Errors.Select(e => e.Description)));
        }

        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
        {
            adminEmail = adminEmail.Trim().ToLowerInvariant();
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin is null)
            {
                admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    LockoutEnabled = true
                };

                var result = await userManager.CreateAsync(admin, adminPassword);
                if (!result.Succeeded)
                    throw new InvalidOperationException(string.Join(" | ", result.Errors.Select(e => e.Description)));
            }

            if (!await userManager.IsInRoleAsync(admin, "Admin"))
            {
                var roleResult = await userManager.AddToRoleAsync(admin, "Admin");
                if (!roleResult.Succeeded)
                    throw new InvalidOperationException(string.Join(" | ", roleResult.Errors.Select(e => e.Description)));
            }

            var desktopClaim = new Claim("created_by", "desktop");
            var existingClaims = await userManager.GetClaimsAsync(admin);
            if (!existingClaims.Any(c => c.Type == desktopClaim.Type &&
                                         string.Equals(c.Value, desktopClaim.Value, StringComparison.OrdinalIgnoreCase)))
                await userManager.AddClaimAsync(admin, desktopClaim);
        }
        else
        {
            logger.LogWarning("Nenhuma conta administrativa inicial foi criada. Configure AdminSettings:Email e AdminSettings:Password via User Secrets ou variáveis de ambiente antes de usar o painel administrativo.");
        }

        if (!await db.ProductCategories.AnyAsync())
        {
            db.ProductCategories.AddRange(
                new ProductCategory { Name = "Artesanato", Description = "Peças artesanais." },
                new ProductCategory { Name = "Decoração", Description = "Itens decorativos." },
                new ProductCategory { Name = "Presentes", Description = "Produtos para presente." });
            await db.SaveChangesAsync();
        }
    }
}

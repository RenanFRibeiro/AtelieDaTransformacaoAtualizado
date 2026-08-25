using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AtelieDaTransformacao.Infrastructure.Identity;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services, string adminEmail, string adminPassword)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var db = services.GetRequiredService<AtelieDaTransformacaoDbContext>();

        if (!await roleManager.RoleExistsAsync("Admin"))
            await roleManager.CreateAsync(new IdentityRole("Admin"));

        var admin = await userManager.FindByEmailAsync(adminEmail);
        if (admin is null)
        {
            admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(admin, adminPassword);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }
        if (!await userManager.IsInRoleAsync(admin, "Admin"))
            await userManager.AddToRoleAsync(admin, "Admin");

        // O administrador inicial também possui acesso ao Desktop.
        var desktopClaim = new System.Security.Claims.Claim("created_by", "desktop");
        var existingClaims = await userManager.GetClaimsAsync(admin);
        if (!existingClaims.Any(c => c.Type == desktopClaim.Type &&
                                     string.Equals(c.Value, desktopClaim.Value, StringComparison.OrdinalIgnoreCase)))
            await userManager.AddClaimAsync(admin, desktopClaim);

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

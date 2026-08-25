using AtelieDaTransformacao.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AtelieDaTransformacao.Infrastructure.Context;

public class AtelieDaTransformacaoDbContext : IdentityDbContext
{
    public AtelieDaTransformacaoDbContext(
        DbContextOptions<AtelieDaTransformacaoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<ProductCategory> ProductCategories { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AtelieDaTransformacaoDbContext).Assembly);
    }
}
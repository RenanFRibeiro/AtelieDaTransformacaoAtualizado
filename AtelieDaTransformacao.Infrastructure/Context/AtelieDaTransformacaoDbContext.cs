using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AtelieDaTransformacao.Domain.Entities;

namespace AtelieDaTransformacao.Infrastructure.Context
{
    public class AtelieDaTransformacaoDbContext : IdentityDbContext //•	Declara a classe DbContext da aplicação que herda de IdentityDbContext para incluir tabelas/funcionalidades de Identity.
    {
        public AtelieDaTransformacaoDbContext(DbContextOptions<AtelieDaTransformacaoDbContext> options) : base(options) //•	Construtor que recebe opções do EF (conexão, provider, etc.) e as repassa para o construtor base (IdentityDbContext) para inicialização.
        {
        }

        public DbSet<Product> Products { get; set; } //•	Define um conjunto (DbSet) para a entidade Product; representa a tabela Products no banco.
        public DbSet<ProductCategory> ProductCategories { get; set; } //•	Define o DbSet para ProductCategory; representa a tabela ProductCategories.
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; } //•	Define o DbSet para Customer; representa a tabela Customers.
        public DbSet<ProductImage> ProductImages { get; set; }   // NOVO

        protected override void OnModelCreating(ModelBuilder modelBuilder) //•	Método override onde se configura o modelo EF (mapeamentos, constraints, relacionamentos).
        {
            base.OnModelCreating(modelBuilder);//•	Chama a implementação base para configurar entidades do Identity (usuários, roles, etc.).

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AtelieDaTransformacaoDbContext).Assembly);//•	Procura e aplica automaticamente todas as classes que implementam IEntityTypeConfiguration<T> no mesmo assembly (ex.: ProductConfiguration, ProductCategoryConfiguration), centralizando configurações de mapeamento.

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.UserId).HasMaxLength(450).IsRequired();
                entity.Property(x => x.Status).HasConversion<int>().IsRequired();
                entity.Property(x => x.Total).HasColumnType("decimal(18,2)");
                entity.Property(x => x.CustomerName).HasMaxLength(150).IsRequired();
                entity.Property(x => x.CustomerEmail).HasMaxLength(256).IsRequired();
                entity.Property(x => x.CustomerPhone).HasMaxLength(30);
                entity.Property(x => x.ShippingAddress).HasMaxLength(500);
                entity.Property(x => x.PaymentMethod).HasMaxLength(100);
                entity.Property(x => x.Notes).HasMaxLength(1000);
                entity.HasIndex(x => x.UserId);

                entity.HasMany(x => x.Items)
                    .WithOne(x => x.Order)
                    .HasForeignKey(x => x.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItems");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.ProductTitle).HasMaxLength(200).IsRequired();
                entity.Property(x => x.ProductImage).HasMaxLength(1000);
                entity.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
                entity.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");
                entity.HasIndex(x => new { x.OrderId, x.ProductId });
            });
        }
    }
    
}
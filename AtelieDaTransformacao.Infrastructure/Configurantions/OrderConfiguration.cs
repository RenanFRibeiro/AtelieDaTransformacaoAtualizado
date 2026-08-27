using AtelieDaTransformacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AtelieDaTransformacao.Infrastructure.Configurations;

public sealed class OrderConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(
        EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNumber)
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasMaxLength(450)
            .IsRequired();

        builder.Property(x => x.UserEmail)
            .HasMaxLength(256)
            .IsRequired();

        // Mapeia as colunas de checkout mantidas em bancos já existentes.
        builder.Property(x => x.CustomerName)
            .HasMaxLength(150)
            .IsRequired(false);

        builder.Property(x => x.CustomerEmail)
            .HasMaxLength(256)
            .IsRequired(false);

        builder.Property(x => x.CustomerPhone)
            .HasMaxLength(30)
            .IsRequired(false);

        builder.Property(x => x.ShippingAddress)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.Property(x => x.PaymentMethod)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(x => x.Notes)
            .HasMaxLength(1000)
            .IsRequired(false);

        builder.Property(x => x.ItemsJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x => x.CheckoutJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x => x.Total)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.AutoAdvance)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.StatusChangedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.HasIndex(x => x.OrderNumber)
            .IsUnique();

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Status);
    }
}
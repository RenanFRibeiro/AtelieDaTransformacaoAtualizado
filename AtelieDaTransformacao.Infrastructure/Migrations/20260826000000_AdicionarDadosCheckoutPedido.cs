using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtelieDaTransformacao.Infrastructure.Migrations;

public partial class AdicionarDadosCheckoutPedido : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("IF COL_LENGTH('Orders','CustomerName') IS NULL ALTER TABLE [Orders] ADD [CustomerName] nvarchar(150) NULL;");
        migrationBuilder.Sql("IF COL_LENGTH('Orders','CustomerPhone') IS NULL ALTER TABLE [Orders] ADD [CustomerPhone] nvarchar(30) NULL;");
        migrationBuilder.Sql("IF COL_LENGTH('Orders','ShippingAddress') IS NULL ALTER TABLE [Orders] ADD [ShippingAddress] nvarchar(500) NULL;");
        migrationBuilder.Sql("IF COL_LENGTH('Orders','PaymentMethod') IS NULL ALTER TABLE [Orders] ADD [PaymentMethod] nvarchar(100) NULL;");
        migrationBuilder.Sql("IF COL_LENGTH('Orders','Notes') IS NULL ALTER TABLE [Orders] ADD [Notes] nvarchar(1000) NULL;");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // As colunas podem pertencer a versões legadas do banco; não removê-las.
    }
}

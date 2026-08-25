using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtelieDaTransformacao.Infrastructure.Migrations;

public partial class AddCheckoutJsonToOrders : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            IF COL_LENGTH('Orders', 'CheckoutJson') IS NULL
            BEGIN
                ALTER TABLE [Orders]
                ADD [CheckoutJson] nvarchar(max) NOT NULL
                CONSTRAINT [DF_Orders_CheckoutJson] DEFAULT ('{}');
            END
            """);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("""
            IF COL_LENGTH('Orders', 'CheckoutJson') IS NOT NULL
            BEGIN
                ALTER TABLE [Orders]
                DROP CONSTRAINT IF EXISTS [DF_Orders_CheckoutJson];
                ALTER TABLE [Orders]
                DROP COLUMN [CheckoutJson];
            END
            """);
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtelieDaTransformacao.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Esta migration pode encontrar um banco que já recebeu as tabelas
            // Orders/OrderItems pelo inicializador anterior. Por isso a criação
            // é idempotente: só cria o objeto quando ele ainda não existe.
            migrationBuilder.Sql("""
IF OBJECT_ID(N'dbo.Orders', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Orders
    (
        Id INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_Orders PRIMARY KEY,
        UserId NVARCHAR(450) NOT NULL,
        Status INT NOT NULL,
        Total DECIMAL(18,2) NOT NULL,
        CustomerName NVARCHAR(150) NOT NULL,
        CustomerEmail NVARCHAR(256) NOT NULL,
        CustomerPhone NVARCHAR(30) NOT NULL,
        ShippingAddress NVARCHAR(500) NOT NULL,
        PaymentMethod NVARCHAR(100) NOT NULL,
        Notes NVARCHAR(1000) NOT NULL,
        CreatedAt DATETIME2 NOT NULL,
        UpdatedAt DATETIME2 NOT NULL
    );
END;

IF OBJECT_ID(N'dbo.OrderItems', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.OrderItems
    (
        Id INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_OrderItems PRIMARY KEY,
        OrderId INT NOT NULL,
        ProductId INT NOT NULL,
        ProductTitle NVARCHAR(200) NOT NULL,
        ProductImage NVARCHAR(1000) NOT NULL,
        UnitPrice DECIMAL(18,2) NOT NULL,
        Quantity INT NOT NULL,
        Subtotal DECIMAL(18,2) NOT NULL,
        CONSTRAINT FK_OrderItems_Orders_OrderId
            FOREIGN KEY (OrderId)
            REFERENCES dbo.Orders(Id)
            ON DELETE CASCADE
    );
END;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_Orders_UserId'
      AND object_id = OBJECT_ID(N'dbo.Orders')
)
BEGIN
    CREATE INDEX IX_Orders_UserId
        ON dbo.Orders(UserId);
END;

IF NOT EXISTS
(
    SELECT 1
    FROM sys.indexes
    WHERE name = N'IX_OrderItems_OrderId_ProductId'
      AND object_id = OBJECT_ID(N'dbo.OrderItems')
)
BEGIN
    CREATE INDEX IX_OrderItems_OrderId_ProductId
        ON dbo.OrderItems(OrderId, ProductId);
END;

IF OBJECT_ID(N'dbo.OrderItems', N'U') IS NOT NULL
   AND OBJECT_ID(N'dbo.Orders', N'U') IS NOT NULL
   AND NOT EXISTS
(
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_OrderItems_Orders_OrderId'
)
BEGIN
    ALTER TABLE dbo.OrderItems
    ADD CONSTRAINT FK_OrderItems_Orders_OrderId
        FOREIGN KEY (OrderId)
        REFERENCES dbo.Orders(Id)
        ON DELETE CASCADE;
END;
""");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
IF OBJECT_ID(N'dbo.OrderItems', N'U') IS NOT NULL
    DROP TABLE dbo.OrderItems;

IF OBJECT_ID(N'dbo.Orders', N'U') IS NOT NULL
    DROP TABLE dbo.Orders;
""");
        }
    }
}

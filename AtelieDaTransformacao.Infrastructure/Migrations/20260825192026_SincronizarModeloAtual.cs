using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtelieDaTransformacao.Infrastructure.Migrations;

public partial class SincronizarModeloAtual : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // =========================================================
        // ORDERNUMBER
        // =========================================================

        migrationBuilder.Sql(
            """
            IF COL_LENGTH('Orders', 'OrderNumber') IS NULL
            BEGIN
                ALTER TABLE [Orders]
                ADD [OrderNumber] nvarchar(30) NULL;
            END
            """);

        // Preenche números para pedidos antigos
        migrationBuilder.Sql(
            """
            UPDATE [Orders]
            SET [OrderNumber] =
                CONCAT('AT-', FORMAT([Id], '000000'))
            WHERE
                [OrderNumber] IS NULL
                OR LTRIM(RTRIM([OrderNumber])) = '';
            """);

        migrationBuilder.Sql(
            """
            ALTER TABLE [Orders]
            ALTER COLUMN [OrderNumber] nvarchar(30) NOT NULL;
            """);


        // =========================================================
        // USEREMAIL
        // =========================================================

        migrationBuilder.Sql(
            """
            IF COL_LENGTH('Orders', 'UserEmail') IS NULL
            BEGIN
                ALTER TABLE [Orders]
                ADD [UserEmail] nvarchar(256) NULL;
            END
            """);

        // Como CustomerEmail já não existe no banco,
        // não tentamos copiar dados dessa coluna.

        migrationBuilder.Sql(
            """
            UPDATE [Orders]
            SET [UserEmail] = 'cliente'
            WHERE
                [UserEmail] IS NULL
                OR LTRIM(RTRIM([UserEmail])) = '';
            """);

        migrationBuilder.Sql(
            """
            ALTER TABLE [Orders]
            ALTER COLUMN [UserEmail] nvarchar(256) NOT NULL;
            """);


        // =========================================================
        // ITEMSJSON
        // =========================================================

        migrationBuilder.Sql(
            """
            IF COL_LENGTH('Orders', 'ItemsJson') IS NULL
            BEGIN
                ALTER TABLE [Orders]
                ADD [ItemsJson] nvarchar(max) NULL;
            END
            """);

        migrationBuilder.Sql(
            """
            UPDATE [Orders]
            SET [ItemsJson] = '[]'
            WHERE
                [ItemsJson] IS NULL
                OR LTRIM(RTRIM([ItemsJson])) = '';
            """);

        migrationBuilder.Sql(
            """
            ALTER TABLE [Orders]
            ALTER COLUMN [ItemsJson] nvarchar(max) NOT NULL;
            """);


        // =========================================================
        // AUTOADVANCE
        // =========================================================

        migrationBuilder.Sql(
            """
            IF COL_LENGTH('Orders', 'AutoAdvance') IS NULL
            BEGIN
                ALTER TABLE [Orders]
                ADD [AutoAdvance] bit NOT NULL
                CONSTRAINT [DF_Orders_AutoAdvance]
                DEFAULT (0);
            END
            """);


        // =========================================================
        // STATUSCHANGEDAT
        // =========================================================

        migrationBuilder.Sql(
            """
            IF COL_LENGTH('Orders', 'StatusChangedAt') IS NULL
            BEGIN
                ALTER TABLE [Orders]
                ADD [StatusChangedAt] datetime2 NULL;
            END
            """);

        migrationBuilder.Sql(
            """
            UPDATE [Orders]
            SET [StatusChangedAt] =
                COALESCE([UpdatedAt], [CreatedAt], GETUTCDATE())
            WHERE [StatusChangedAt] IS NULL;
            """);

        migrationBuilder.Sql(
            """
            ALTER TABLE [Orders]
            ALTER COLUMN [StatusChangedAt] datetime2 NOT NULL;
            """);


        // =========================================================
        // ÍNDICE ORDERNUMBER
        // =========================================================

        migrationBuilder.Sql(
            """
            IF NOT EXISTS
            (
                SELECT 1
                FROM sys.indexes
                WHERE
                    name = 'IX_Orders_OrderNumber'
                    AND object_id = OBJECT_ID('Orders')
            )
            BEGIN
                CREATE UNIQUE INDEX [IX_Orders_OrderNumber]
                ON [Orders] ([OrderNumber]);
            END
            """);


        // =========================================================
        // ÍNDICE STATUS
        // =========================================================

        migrationBuilder.Sql(
            """
            IF NOT EXISTS
            (
                SELECT 1
                FROM sys.indexes
                WHERE
                    name = 'IX_Orders_Status'
                    AND object_id = OBJECT_ID('Orders')
            )
            BEGIN
                CREATE INDEX [IX_Orders_Status]
                ON [Orders] ([Status]);
            END
            """);

        // =========================================================
        // IMPORTANTE
        //
        // Não removemos:
        // - ProductImages
        // - OrderItems
        // - CustomerName
        // - CustomerEmail
        // - CustomerPhone
        // - ShippingAddress
        // - PaymentMethod
        // - Notes
        //
        // O banco pode possuir estruturas antigas que não fazem
        // parte do modelo atual. Mantemos tudo para evitar perda.
        // =========================================================
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(
            """
            IF EXISTS
            (
                SELECT 1
                FROM sys.indexes
                WHERE
                    name = 'IX_Orders_OrderNumber'
                    AND object_id = OBJECT_ID('Orders')
            )
            BEGIN
                DROP INDEX [IX_Orders_OrderNumber]
                ON [Orders];
            END
            """);

        migrationBuilder.Sql(
            """
            IF EXISTS
            (
                SELECT 1
                FROM sys.indexes
                WHERE
                    name = 'IX_Orders_Status'
                    AND object_id = OBJECT_ID('Orders')
            )
            BEGIN
                DROP INDEX [IX_Orders_Status]
                ON [Orders];
            END
            """);

        // Não removemos colunas ou tabelas no Down
        // para evitar perda acidental de dados.
    }
}
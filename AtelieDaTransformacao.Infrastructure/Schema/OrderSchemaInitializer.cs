using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using AtelieDaTransformacao.Infrastructure.Context;

namespace AtelieDaTransformacao.Infrastructure.Schema;

public static class OrderSchemaInitializer
{
    public static async Task EnsureAsync(AtelieDaTransformacaoDbContext db)
    {
        var connection = db.Database.GetDbConnection();
        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        await using var command = connection.CreateCommand();
        command.CommandText = @"
IF OBJECT_ID(N'[dbo].[Orders]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[Orders]
    (
        [Id] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Orders] PRIMARY KEY,
        [OrderNumber] NVARCHAR(30) NOT NULL,
        [UserId] NVARCHAR(450) NOT NULL,
        [UserEmail] NVARCHAR(256) NOT NULL,
        [ItemsJson] NVARCHAR(MAX) NOT NULL,
        [Total] DECIMAL(18,2) NOT NULL,
        [Status] INT NOT NULL,
        [AutoAdvance] BIT NOT NULL CONSTRAINT [DF_Orders_AutoAdvance] DEFAULT (0),
        [CreatedAt] DATETIME2 NOT NULL,
        [UpdatedAt] DATETIME2 NOT NULL,
        [StatusChangedAt] DATETIME2 NOT NULL
    );

    CREATE UNIQUE INDEX [IX_Orders_OrderNumber] ON [dbo].[Orders]([OrderNumber]);
    CREATE INDEX [IX_Orders_UserId] ON [dbo].[Orders]([UserId]);
    CREATE INDEX [IX_Orders_Status] ON [dbo].[Orders]([Status]);
END";

        await command.ExecuteNonQueryAsync();
    }
}

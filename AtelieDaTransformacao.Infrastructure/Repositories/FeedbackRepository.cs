using System.Data;
using AtelieDaTransformacao.Domain.Entities;
using AtelieDaTransformacao.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AtelieDaTransformacao.Infrastructure.Repositories;

public sealed class FeedbackRepository : IFeedbackRepository
{
    private const string GivenNameClaim =
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";

    private const string SurnameClaim =
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";

    private readonly string _connectionString;

    private static readonly SemaphoreSlim SchemaLock = new(1, 1);

    private static bool _schemaReady;

    public FeedbackRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'DefaultConnection' not found.");
    }

    public async Task<Feedback?> GetByUserOrderProductAsync(
        string usuarioId,
        int pedidoId,
        int produtoId)
    {
        await EnsureSchemaAsync();

        const string sql = """
            SELECT TOP (1)
                Id,
                UsuarioId,
                ProdutoId,
                PedidoId,
                Nota,
                Comentario,
                ImagemUrl,
                IsAnonimo,
                DataCriacao
            FROM dbo.Feedbacks
            WHERE UsuarioId = @UsuarioId
              AND PedidoId = @PedidoId
              AND ProdutoId = @ProdutoId;
            """;

        await using var connection =
            new SqlConnection(_connectionString);

        await connection.OpenAsync();

        await using var command =
            new SqlCommand(sql, connection);

        command.Parameters.Add(
            "@UsuarioId",
            SqlDbType.NVarChar,
            450).Value = usuarioId;

        command.Parameters.Add(
            "@PedidoId",
            SqlDbType.Int).Value = pedidoId;

        command.Parameters.Add(
            "@ProdutoId",
            SqlDbType.Int).Value = produtoId;

        await using var reader =
            await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;

        return Map(reader);
    }

    public async Task<IReadOnlyList<Feedback>> GetForOrderAsync(
        string usuarioId,
        int pedidoId)
    {
        await EnsureSchemaAsync();

        const string sql = """
            SELECT
                Id,
                UsuarioId,
                ProdutoId,
                PedidoId,
                Nota,
                Comentario,
                ImagemUrl,
                IsAnonimo,
                DataCriacao
            FROM dbo.Feedbacks
            WHERE UsuarioId = @UsuarioId
              AND PedidoId = @PedidoId
            ORDER BY DataCriacao DESC, Id DESC;
            """;

        var result = new List<Feedback>();

        await using var connection =
            new SqlConnection(_connectionString);

        await connection.OpenAsync();

        await using var command =
            new SqlCommand(sql, connection);

        command.Parameters.Add(
            "@UsuarioId",
            SqlDbType.NVarChar,
            450).Value = usuarioId;

        command.Parameters.Add(
            "@PedidoId",
            SqlDbType.Int).Value = pedidoId;

        await using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(Map(reader));
        }

        return result;
    }

    public async Task<IReadOnlyList<Feedback>> GetPublishedAsync(
        int limit = 12)
    {
        await EnsureSchemaAsync();

        limit = Math.Clamp(limit, 1, 50);

        var sql = $"""
            SELECT TOP ({limit})
                f.Id,
                f.UsuarioId,
                f.ProdutoId,
                f.PedidoId,
                f.Nota,
                f.Comentario,
                f.ImagemUrl,
                f.IsAnonimo,
                f.DataCriacao,

                CASE
                    WHEN f.IsAnonimo = 1
                        THEN N'Cliente Anônimo'

                    WHEN NULLIF(
                        LTRIM(RTRIM(
                            COALESCE(givenClaim.ClaimValue, N'') +
                            CASE
                                WHEN surnameClaim.ClaimValue IS NULL
                                  OR LTRIM(RTRIM(surnameClaim.ClaimValue)) = N''
                                THEN N''
                                ELSE N' ' +
                                     LTRIM(RTRIM(surnameClaim.ClaimValue))
                            END
                        )),
                        N''
                    ) IS NOT NULL
                    THEN
                        LTRIM(RTRIM(
                            COALESCE(givenClaim.ClaimValue, N'') +
                            CASE
                                WHEN surnameClaim.ClaimValue IS NULL
                                  OR LTRIM(RTRIM(surnameClaim.ClaimValue)) = N''
                                THEN N''
                                ELSE N' ' +
                                     LTRIM(RTRIM(surnameClaim.ClaimValue))
                            END
                        ))

                    ELSE COALESCE(
                        u.UserName,
                        N'Cliente'
                    )
                END AS PublicName

            FROM dbo.Feedbacks f

            INNER JOIN dbo.Products p
                ON p.Id = f.ProdutoId

            LEFT JOIN dbo.AspNetUsers u
                ON u.Id = f.UsuarioId

            OUTER APPLY
            (
                SELECT TOP (1)
                    Claim.ClaimValue AS ClaimValue
                FROM dbo.AspNetUserClaims Claim
                WHERE Claim.UserId = f.UsuarioId
                  AND Claim.ClaimType = @GivenNameClaim
            ) givenClaim

            OUTER APPLY
            (
                SELECT TOP (1)
                    Claim.ClaimValue AS ClaimValue
                FROM dbo.AspNetUserClaims Claim
                WHERE Claim.UserId = f.UsuarioId
                  AND Claim.ClaimType = @SurnameClaim
            ) surnameClaim

            ORDER BY
                f.DataCriacao DESC,
                f.Id DESC;
            """;

        var result = new List<Feedback>();

        await using var connection =
            new SqlConnection(_connectionString);

        await connection.OpenAsync();

        await using var command =
            new SqlCommand(sql, connection);

        command.Parameters.Add(
            "@GivenNameClaim",
            SqlDbType.NVarChar,
            256).Value = GivenNameClaim;

        command.Parameters.Add(
            "@SurnameClaim",
            SqlDbType.NVarChar,
            256).Value = SurnameClaim;

        await using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var feedback = Map(reader);

            feedback.PublicName =
                reader["PublicName"] == DBNull.Value
                    ? "Cliente"
                    : Convert.ToString(
                        reader["PublicName"])
                        ?? "Cliente";

            if (string.IsNullOrWhiteSpace(
                    feedback.PublicName))
            {
                feedback.PublicName = "Cliente";
            }

            result.Add(feedback);
        }

        return result;
    }

    public async Task AddAsync(Feedback feedback)
    {
        await EnsureSchemaAsync();

        const string sql = """
            INSERT INTO dbo.Feedbacks
            (
                UsuarioId,
                ProdutoId,
                PedidoId,
                Nota,
                Comentario,
                ImagemUrl,
                IsAnonimo,
                DataCriacao
            )
            VALUES
            (
                @UsuarioId,
                @ProdutoId,
                @PedidoId,
                @Nota,
                @Comentario,
                @ImagemUrl,
                @IsAnonimo,
                @DataCriacao
            );
            """;

        await using var connection =
            new SqlConnection(_connectionString);

        await connection.OpenAsync();

        await using var command =
            new SqlCommand(sql, connection);

        command.Parameters.Add(
            "@UsuarioId",
            SqlDbType.NVarChar,
            450).Value = feedback.UsuarioId;

        command.Parameters.Add(
            "@ProdutoId",
            SqlDbType.Int).Value = feedback.ProdutoId;

        command.Parameters.Add(
            "@PedidoId",
            SqlDbType.Int).Value = feedback.PedidoId;

        command.Parameters.Add(
            "@Nota",
            SqlDbType.TinyInt).Value = feedback.Nota;

        command.Parameters.Add(
            "@Comentario",
            SqlDbType.NVarChar,
            2000).Value = feedback.Comentario;

        command.Parameters.Add(
            "@ImagemUrl",
            SqlDbType.NVarChar,
            500).Value =
            (object?)feedback.ImagemUrl
            ?? DBNull.Value;

        command.Parameters.Add(
            "@IsAnonimo",
            SqlDbType.Bit).Value =
            feedback.IsAnonimo;

        command.Parameters.Add(
            "@DataCriacao",
            SqlDbType.DateTime2).Value =
            feedback.DataCriacao;

        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SqlException ex)
            when (ex.Number is 2601 or 2627)
        {
            throw new InvalidOperationException(
                "Este produto já foi avaliado neste pedido.",
                ex);
        }
    }

    private async Task EnsureSchemaAsync()
    {
        if (_schemaReady)
            return;

        await SchemaLock.WaitAsync();

        try
        {
            if (_schemaReady)
                return;

            const string sql = """
                IF OBJECT_ID(N'dbo.Feedbacks', N'U') IS NULL
                BEGIN

                    CREATE TABLE dbo.Feedbacks
                    (
                        Id INT IDENTITY(1,1) NOT NULL
                            CONSTRAINT PK_Feedbacks
                            PRIMARY KEY,

                        UsuarioId NVARCHAR(450) NOT NULL,

                        ProdutoId INT NOT NULL,

                        PedidoId INT NOT NULL,

                        Nota TINYINT NOT NULL,

                        Comentario NVARCHAR(2000) NOT NULL,

                        ImagemUrl NVARCHAR(500) NULL,

                        IsAnonimo BIT NOT NULL
                            CONSTRAINT DF_Feedbacks_IsAnonimo
                            DEFAULT (0),

                        DataCriacao DATETIME2 NOT NULL
                            CONSTRAINT DF_Feedbacks_DataCriacao
                            DEFAULT (SYSUTCDATETIME()),

                        CONSTRAINT CK_Feedbacks_Nota
                            CHECK (Nota BETWEEN 1 AND 5),

                        CONSTRAINT FK_Feedbacks_Users
                            FOREIGN KEY (UsuarioId)
                            REFERENCES dbo.AspNetUsers(Id),

                        CONSTRAINT FK_Feedbacks_Products
                            FOREIGN KEY (ProdutoId)
                            REFERENCES dbo.Products(Id),

                        CONSTRAINT FK_Feedbacks_Orders
                            FOREIGN KEY (PedidoId)
                            REFERENCES dbo.Orders(Id)
                    );

                    CREATE UNIQUE INDEX
                        UX_Feedbacks_User_Order_Product
                    ON dbo.Feedbacks
                    (
                        UsuarioId,
                        PedidoId,
                        ProdutoId
                    );

                    CREATE INDEX
                        IX_Feedbacks_ProdutoId_DataCriacao
                    ON dbo.Feedbacks
                    (
                        ProdutoId,
                        DataCriacao DESC
                    );

                    CREATE INDEX
                        IX_Feedbacks_PedidoId
                    ON dbo.Feedbacks
                    (
                        PedidoId
                    );

                END;
                """;

            await using var connection =
                new SqlConnection(_connectionString);

            await connection.OpenAsync();

            await using var command =
                new SqlCommand(sql, connection);

            await command.ExecuteNonQueryAsync();

            _schemaReady = true;
        }
        finally
        {
            SchemaLock.Release();
        }
    }

    private static Feedback Map(
        SqlDataReader reader)
    {
        return new Feedback
        {
            Id =
                reader.GetInt32(
                    reader.GetOrdinal("Id")),

            UsuarioId =
                reader.GetString(
                    reader.GetOrdinal("UsuarioId")),

            ProdutoId =
                reader.GetInt32(
                    reader.GetOrdinal("ProdutoId")),

            PedidoId =
                reader.GetInt32(
                    reader.GetOrdinal("PedidoId")),

            Nota =
                reader.GetByte(
                    reader.GetOrdinal("Nota")),

            Comentario =
                reader.GetString(
                    reader.GetOrdinal("Comentario")),

            ImagemUrl =
                reader.IsDBNull(
                    reader.GetOrdinal("ImagemUrl"))
                    ? null
                    : reader.GetString(
                        reader.GetOrdinal("ImagemUrl")),

            IsAnonimo =
                reader.GetBoolean(
                    reader.GetOrdinal("IsAnonimo")),

            DataCriacao =
                reader.GetDateTime(
                    reader.GetOrdinal("DataCriacao"))
        };
    }
}
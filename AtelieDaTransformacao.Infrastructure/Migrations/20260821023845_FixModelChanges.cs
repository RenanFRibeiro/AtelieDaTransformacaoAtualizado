using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtelieDaTransformacao.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // A migration 20260821023810_UpdateDatabaseSchema já é responsável
            // por criar Orders e OrderItems. Esta migration havia sido criada
            // duplicando essas tabelas e causava o erro "There is already an
            // object named 'Orders'". Ela permanece no histórico para não
            // quebrar bancos que já registraram esta migration, mas não executa
            // nenhuma operação de banco.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intencionalmente vazio. As tabelas pertencem à migration anterior.
        }
    }
}

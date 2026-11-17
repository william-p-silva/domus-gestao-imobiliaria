using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddGrupoCaracteristica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GrupoCaracteristica_ID",
                table: "Caracteristicas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GruposCaracteristica",
                columns: table => new
                {
                    GrupoCaracteristica_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposCaracteristica", x => x.GrupoCaracteristica_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caracteristicas_GrupoCaracteristica_ID",
                table: "Caracteristicas",
                column: "GrupoCaracteristica_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GruposCaracteristica_Codigo",
                table: "GruposCaracteristica",
                column: "Codigo",
                unique: true);

            // Grupo padrão: as características já existentes passam a apontar para ele
            // (o GUID vazio é o default da coluna adicionada acima).
            migrationBuilder.Sql(@"
                INSERT INTO [GruposCaracteristica]
                    ([GrupoCaracteristica_ID], [Nome], [Codigo], [Estado], [CriadoEm])
                VALUES
                    ('00000000-0000-0000-0000-000000000000', N'Geral', N'GERAL', N'Ativa', GETUTCDATE());
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_Caracteristicas_GruposCaracteristica_GrupoCaracteristica_ID",
                table: "Caracteristicas",
                column: "GrupoCaracteristica_ID",
                principalTable: "GruposCaracteristica",
                principalColumn: "GrupoCaracteristica_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caracteristicas_GruposCaracteristica_GrupoCaracteristica_ID",
                table: "Caracteristicas");

            migrationBuilder.DropTable(
                name: "GruposCaracteristica");

            migrationBuilder.DropIndex(
                name: "IX_Caracteristicas_GrupoCaracteristica_ID",
                table: "Caracteristicas");

            migrationBuilder.DropColumn(
                name: "GrupoCaracteristica_ID",
                table: "Caracteristicas");
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCaracteristicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caracteristicas",
                columns: table => new
                {
                    Caracteristica_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caracteristicas", x => x.Caracteristica_ID);
                });

            migrationBuilder.CreateTable(
                name: "CaracteristicasImovel",
                columns: table => new
                {
                    CaracteristicaImovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Caracteristica_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaracteristicasImovel", x => x.CaracteristicaImovel_ID);
                    table.ForeignKey(
                        name: "FK_CaracteristicasImovel_Caracteristicas_Caracteristica_ID",
                        column: x => x.Caracteristica_ID,
                        principalTable: "Caracteristicas",
                        principalColumn: "Caracteristica_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaracteristicasImovel_Imoveis_Imovel_ID",
                        column: x => x.Imovel_ID,
                        principalTable: "Imoveis",
                        principalColumn: "Imovel_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristicasImovel_Caracteristica_ID",
                table: "CaracteristicasImovel",
                column: "Caracteristica_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CaracteristicasImovel_Imovel_ID",
                table: "CaracteristicasImovel",
                column: "Imovel_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaracteristicasImovel");

            migrationBuilder.DropTable(
                name: "Caracteristicas");
        }
    }
}

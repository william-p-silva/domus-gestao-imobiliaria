using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AssinaturaLocador",
                table: "Contratos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AssinaturaLocatario",
                table: "Contratos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssinaturaLocador",
                table: "Contratos");

            migrationBuilder.DropColumn(
                name: "AssinaturaLocatario",
                table: "Contratos");
        }
    }
}

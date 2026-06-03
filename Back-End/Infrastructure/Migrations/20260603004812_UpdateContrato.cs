using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContrato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "Contratos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "Contratos");
        }
    }
}

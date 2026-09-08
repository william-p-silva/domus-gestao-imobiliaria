using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AutualizandoMensagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MensagensChat_Usuarios_Usuario_ID",
                table: "MensagensChat");

            migrationBuilder.RenameColumn(
                name: "Usuario_ID",
                table: "MensagensChat",
                newName: "UsuarioChat_ID");

            migrationBuilder.RenameIndex(
                name: "IX_MensagensChat_Usuario_ID",
                table: "MensagensChat",
                newName: "IX_MensagensChat_UsuarioChat_ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataEnvio",
                table: "MensagensChat",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletadaEm",
                table: "MensagensChat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "MensagensChat",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_MensagensChat_UsuarioChat_UsuarioChat_ID",
                table: "MensagensChat",
                column: "UsuarioChat_ID",
                principalTable: "UsuarioChat",
                principalColumn: "UsuarioChat_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MensagensChat_UsuarioChat_UsuarioChat_ID",
                table: "MensagensChat");

            migrationBuilder.DropColumn(
                name: "DeletadaEm",
                table: "MensagensChat");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "MensagensChat");

            migrationBuilder.RenameColumn(
                name: "UsuarioChat_ID",
                table: "MensagensChat",
                newName: "Usuario_ID");

            migrationBuilder.RenameIndex(
                name: "IX_MensagensChat_UsuarioChat_ID",
                table: "MensagensChat",
                newName: "IX_MensagensChat_Usuario_ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataEnvio",
                table: "MensagensChat",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "FK_MensagensChat_Usuarios_Usuario_ID",
                table: "MensagensChat",
                column: "Usuario_ID",
                principalTable: "Usuarios",
                principalColumn: "Usuario_ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AutualizandoUserChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioChat_Chats_Chat_ID",
                table: "UsuarioChat");

            migrationBuilder.AddColumn<DateTime>(
                name: "CriadoEm",
                table: "UsuarioChat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletadoEm",
                table: "UsuarioChat",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "UsuarioChat",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Funcao",
                table: "UsuarioChat",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeChat",
                table: "UsuarioChat",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioChat_Chats_Chat_ID",
                table: "UsuarioChat",
                column: "Chat_ID",
                principalTable: "Chats",
                principalColumn: "Chat_ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioChat_Chats_Chat_ID",
                table: "UsuarioChat");

            migrationBuilder.DropColumn(
                name: "CriadoEm",
                table: "UsuarioChat");

            migrationBuilder.DropColumn(
                name: "DeletadoEm",
                table: "UsuarioChat");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "UsuarioChat");

            migrationBuilder.DropColumn(
                name: "Funcao",
                table: "UsuarioChat");

            migrationBuilder.DropColumn(
                name: "NomeChat",
                table: "UsuarioChat");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioChat_Chats_Chat_ID",
                table: "UsuarioChat",
                column: "Chat_ID",
                principalTable: "Chats",
                principalColumn: "Chat_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Endereco_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CEP = table.Column<string>(type: "char(8)", nullable: false),
                    UF = table.Column<string>(type: "char(2)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Endereco_ID);
                });

            migrationBuilder.CreateTable(
                name: "Funcoes",
                columns: table => new
                {
                    Funcao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcoes", x => x.Funcao_ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TokenConfirmaEmail = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TokenEmailExpire = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailAConfirmar = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EmailConfirmado = table.Column<bool>(type: "bit", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usuario_ID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Enderecos_Endereco_ID",
                        column: x => x.Endereco_ID,
                        principalTable: "Enderecos",
                        principalColumn: "Endereco_ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Imoveis",
                columns: table => new
                {
                    Imovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Endereco_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetrosQuadrados = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comodos = table.Column<int>(type: "int", nullable: false),
                    Banheiros = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorAluguel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Aprovado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Avaliado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imoveis", x => x.Imovel_ID);
                    table.ForeignKey(
                        name: "FK_Imoveis_Enderecos_Endereco_ID",
                        column: x => x.Endereco_ID,
                        principalTable: "Enderecos",
                        principalColumn: "Endereco_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Imoveis_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Notificacao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Lida = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Notificacao_ID);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioFuncoes",
                columns: table => new
                {
                    UsuarioFuncao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Funcao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioFuncoes", x => x.UsuarioFuncao_ID);
                    table.ForeignKey(
                        name: "FK_UsuarioFuncoes_Funcoes_Funcao_ID",
                        column: x => x.Funcao_ID,
                        principalTable: "Funcoes",
                        principalColumn: "Funcao_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsuarioFuncoes_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Chat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Chat_ID);
                    table.ForeignKey(
                        name: "FK_Chats_Imoveis_Imovel_ID",
                        column: x => x.Imovel_ID,
                        principalTable: "Imoveis",
                        principalColumn: "Imovel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contratos",
                columns: table => new
                {
                    Contrato_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Locador_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Locatario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrlContrato = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataTermino = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AssinaturaLocador = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AssinaturaLocatario = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratos", x => x.Contrato_ID);
                    table.ForeignKey(
                        name: "FK_Contratos_Imoveis_Imovel_ID",
                        column: x => x.Imovel_ID,
                        principalTable: "Imoveis",
                        principalColumn: "Imovel_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contratos_Usuarios_Locador_ID",
                        column: x => x.Locador_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contratos_Usuarios_Locatario_ID",
                        column: x => x.Locatario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagensImovel",
                columns: table => new
                {
                    ImagemImovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UrlImagem = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagensImovel", x => x.ImagemImovel_ID);
                    table.ForeignKey(
                        name: "FK_ImagensImovel_Imoveis_Imovel_ID",
                        column: x => x.Imovel_ID,
                        principalTable: "Imoveis",
                        principalColumn: "Imovel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reclamacoes",
                columns: table => new
                {
                    Reclamacao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    DataResolucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reclamacoes", x => x.Reclamacao_ID);
                    table.ForeignKey(
                        name: "FK_Reclamacoes_Imoveis_Imovel_ID",
                        column: x => x.Imovel_ID,
                        principalTable: "Imoveis",
                        principalColumn: "Imovel_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reclamacoes_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensagensChat",
                columns: table => new
                {
                    MensagemChat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    DataEnvio = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagensChat", x => x.MensagemChat_ID);
                    table.ForeignKey(
                        name: "FK_MensagensChat_Chats_Chat_ID",
                        column: x => x.Chat_ID,
                        principalTable: "Chats",
                        principalColumn: "Chat_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensagensChat_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioChat",
                columns: table => new
                {
                    UsuarioChat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioChat", x => x.UsuarioChat_ID);
                    table.ForeignKey(
                        name: "FK_UsuarioChat_Chats_Chat_ID",
                        column: x => x.Chat_ID,
                        principalTable: "Chats",
                        principalColumn: "Chat_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioChat_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Avaliacoes",
                columns: table => new
                {
                    Avaliacao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Usuario_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Imovel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contrato_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    PublicadoEm = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avaliacoes", x => x.Avaliacao_ID);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Contratos_Contrato_ID",
                        column: x => x.Contrato_ID,
                        principalTable: "Contratos",
                        principalColumn: "Contrato_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Imoveis_Imovel_ID",
                        column: x => x.Imovel_ID,
                        principalTable: "Imoveis",
                        principalColumn: "Imovel_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avaliacoes_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParcelasAluguel",
                columns: table => new
                {
                    ParcelaAluguel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Contrato_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorParcela = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusPagamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PixCopiaCola = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UrlParcelaAluguel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParcelasAluguel", x => x.ParcelaAluguel_ID);
                    table.ForeignKey(
                        name: "FK_ParcelasAluguel_Contratos_Contrato_ID",
                        column: x => x.Contrato_ID,
                        principalTable: "Contratos",
                        principalColumn: "Contrato_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensagensReclamacao",
                columns: table => new
                {
                    MensagemReclamacao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Reclamacao_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Emissor_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Texto = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagensReclamacao", x => x.MensagemReclamacao_ID);
                    table.ForeignKey(
                        name: "FK_MensagensReclamacao_Reclamacoes_Reclamacao_ID",
                        column: x => x.Reclamacao_ID,
                        principalTable: "Reclamacoes",
                        principalColumn: "Reclamacao_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensagensReclamacao_Usuarios_Emissor_ID",
                        column: x => x.Emissor_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Usuario_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RecibosPagamento",
                columns: table => new
                {
                    ReciboPagamento_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParcelaAluguel_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValorParcela = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrlRecibo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataEmissao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecibosPagamento", x => x.ReciboPagamento_ID);
                    table.ForeignKey(
                        name: "FK_RecibosPagamento_ParcelasAluguel_ParcelaAluguel_ID",
                        column: x => x.ParcelaAluguel_ID,
                        principalTable: "ParcelasAluguel",
                        principalColumn: "ParcelaAluguel_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Funcoes",
                columns: new[] { "Funcao_ID", "Nome" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Administrador" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Locador" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Locatario" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_Contrato_ID",
                table: "Avaliacoes",
                column: "Contrato_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_Imovel_ID",
                table: "Avaliacoes",
                column: "Imovel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_Usuario_ID",
                table: "Avaliacoes",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_Imovel_ID",
                table: "Chats",
                column: "Imovel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_Imovel_ID",
                table: "Contratos",
                column: "Imovel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_Locador_ID",
                table: "Contratos",
                column: "Locador_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Contratos_Locatario_ID",
                table: "Contratos",
                column: "Locatario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ImagensImovel_Imovel_ID",
                table: "ImagensImovel",
                column: "Imovel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Imoveis_Endereco_ID",
                table: "Imoveis",
                column: "Endereco_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Imoveis_Usuario_ID",
                table: "Imoveis",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MensagensChat_Chat_ID",
                table: "MensagensChat",
                column: "Chat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MensagensChat_Usuario_ID",
                table: "MensagensChat",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MensagensReclamacao_Emissor_ID",
                table: "MensagensReclamacao",
                column: "Emissor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MensagensReclamacao_Reclamacao_ID",
                table: "MensagensReclamacao",
                column: "Reclamacao_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_Usuario_ID",
                table: "Notificacoes",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ParcelasAluguel_Contrato_ID",
                table: "ParcelasAluguel",
                column: "Contrato_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RecibosPagamento_ParcelaAluguel_ID",
                table: "RecibosPagamento",
                column: "ParcelaAluguel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reclamacoes_Imovel_ID",
                table: "Reclamacoes",
                column: "Imovel_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Reclamacoes_Usuario_ID",
                table: "Reclamacoes",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioChat_Chat_ID",
                table: "UsuarioChat",
                column: "Chat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioChat_Usuario_ID",
                table: "UsuarioChat",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioFuncoes_Funcao_ID",
                table: "UsuarioFuncoes",
                column: "Funcao_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioFuncoes_Usuario_ID",
                table: "UsuarioFuncoes",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Celular",
                table: "Usuarios",
                column: "Celular",
                unique: true,
                filter: "[Celular] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CPF",
                table: "Usuarios",
                column: "CPF",
                unique: true,
                filter: "[CPF] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmailAConfirmar",
                table: "Usuarios",
                column: "EmailAConfirmar",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Endereco_ID",
                table: "Usuarios",
                column: "Endereco_ID",
                unique: true,
                filter: "[Endereco_ID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avaliacoes");

            migrationBuilder.DropTable(
                name: "ImagensImovel");

            migrationBuilder.DropTable(
                name: "MensagensChat");

            migrationBuilder.DropTable(
                name: "MensagensReclamacao");

            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "RecibosPagamento");

            migrationBuilder.DropTable(
                name: "UsuarioChat");

            migrationBuilder.DropTable(
                name: "UsuarioFuncoes");

            migrationBuilder.DropTable(
                name: "Reclamacoes");

            migrationBuilder.DropTable(
                name: "ParcelasAluguel");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Funcoes");

            migrationBuilder.DropTable(
                name: "Contratos");

            migrationBuilder.DropTable(
                name: "Imoveis");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}

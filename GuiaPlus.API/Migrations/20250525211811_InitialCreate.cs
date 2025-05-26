using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuiaPlus.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbClientes",
                columns: table => new
                {
                    tbClientes_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tbClientes_CPFCNPJ = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    tbClientes_NomeCompleto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tbClientes_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tbClientes_Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tbClientes_Status = table.Column<int>(type: "int", nullable: false),
                    tbClientes_Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbClientes", x => x.tbClientes_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbServicos",
                columns: table => new
                {
                    tbServicos_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tbServicos_Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tbServicos_Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbServicos", x => x.tbServicos_Id);
                });

            migrationBuilder.CreateTable(
                name: "tbEnderecosClientes",
                columns: table => new
                {
                    tbEnderecosClientes_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tbClientes_Id = table.Column<int>(type: "int", nullable: false),
                    tbEnderecosClientes_CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    tbEnderecosClientes_Logradouro = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tbEnderecosClientes_Bairro = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    tbEnderecosClientes_Cidade = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    tbEnderecosClientes_Complemento = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tbEnderecosClientes_Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEnderecosClientes", x => x.tbEnderecosClientes_Id);
                    table.ForeignKey(
                        name: "FK_tbEnderecosClientes_tbClientes_tbClientes_Id",
                        column: x => x.tbClientes_Id,
                        principalTable: "tbClientes",
                        principalColumn: "tbClientes_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbGuias",
                columns: table => new
                {
                    tbGuias_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tbClientes_Id = table.Column<int>(type: "int", nullable: false),
                    tbServicos_Id = table.Column<int>(type: "int", nullable: false),
                    tbEnderecosClientes_Id = table.Column<int>(type: "int", nullable: false),
                    tbGuias_NumeroGuia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    tbGuias_Status = table.Column<int>(type: "int", nullable: false),
                    tbGuias_DataHoraRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tbGuias_DataHoraIniciouColeta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    tbGuias_DataHoraConfirmouRetirada = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbGuias", x => x.tbGuias_Id);
                    table.ForeignKey(
                        name: "FK_tbGuias_tbClientes_tbClientes_Id",
                        column: x => x.tbClientes_Id,
                        principalTable: "tbClientes",
                        principalColumn: "tbClientes_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbGuias_tbEnderecosClientes_tbEnderecosClientes_Id",
                        column: x => x.tbEnderecosClientes_Id,
                        principalTable: "tbEnderecosClientes",
                        principalColumn: "tbEnderecosClientes_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbGuias_tbServicos_tbServicos_Id",
                        column: x => x.tbServicos_Id,
                        principalTable: "tbServicos",
                        principalColumn: "tbServicos_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbClientes_tbClientes_CPFCNPJ",
                table: "tbClientes",
                column: "tbClientes_CPFCNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbEnderecosClientes_tbClientes_Id",
                table: "tbEnderecosClientes",
                column: "tbClientes_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbGuias_tbClientes_Id",
                table: "tbGuias",
                column: "tbClientes_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbGuias_tbEnderecosClientes_Id",
                table: "tbGuias",
                column: "tbEnderecosClientes_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbGuias_tbServicos_Id",
                table: "tbGuias",
                column: "tbServicos_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbGuias");

            migrationBuilder.DropTable(
                name: "tbEnderecosClientes");

            migrationBuilder.DropTable(
                name: "tbServicos");

            migrationBuilder.DropTable(
                name: "tbClientes");
        }
    }
}

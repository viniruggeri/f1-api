using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814

namespace f1.api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corridas",
                columns: table => new
                {
                    CorridaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Local = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corridas", x => x.CorridaId);
                });

            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    EquipeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Pais = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    AnoFundacao = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipes", x => x.EquipeId);
                });

            migrationBuilder.CreateTable(
                name: "Pilotos",
                columns: table => new
                {
                    PilotoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Nacionalidade = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    EquipeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilotos", x => x.PilotoId);
                    table.ForeignKey(
                        name: "FK_Pilotos_Equipes_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "EquipeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resultados",
                columns: table => new
                {
                    ResultadoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PilotoId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CorridaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Posicao = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Pontos = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultados", x => x.ResultadoId);
                    table.ForeignKey(
                        name: "FK_Resultados_Corridas_CorridaId",
                        column: x => x.CorridaId,
                        principalTable: "Corridas",
                        principalColumn: "CorridaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resultados_Pilotos_PilotoId",
                        column: x => x.PilotoId,
                        principalTable: "Pilotos",
                        principalColumn: "PilotoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Equipes",
                columns: new[] { "EquipeId", "AnoFundacao", "Nome", "Pais" },
                values: new object[,]
                {
                    { 1, 2005, "Red Bull Racing", "Áustria" },
                    { 2, 2010, "Mercedes-AMG Petronas", "Alemanha" },
                    { 3, 1950, "Scuderia Ferrari", "Itália" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pilotos_EquipeId",
                table: "Pilotos",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resultados_CorridaId",
                table: "Resultados",
                column: "CorridaId");

            migrationBuilder.CreateIndex(
                name: "IX_Resultados_PilotoId",
                table: "Resultados",
                column: "PilotoId");
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resultados");

            migrationBuilder.DropTable(
                name: "Corridas");

            migrationBuilder.DropTable(
                name: "Pilotos");

            migrationBuilder.DropTable(
                name: "Equipes");
        }
    }
}

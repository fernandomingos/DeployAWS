using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeployAWS.Infrastructure.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sobrenome = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataCadastro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsAtivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    IsDisponivel = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "DataCadastro", "Email", "IsAtivo", "Nome", "Sobrenome" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 15, 7, 25, 7, 203, DateTimeKind.Local).AddTicks(1648), "cliente1@teste.com", true, "Cliente 1", "Teste 1" },
                    { 2, new DateTime(2023, 1, 15, 7, 25, 7, 203, DateTimeKind.Local).AddTicks(2200), "cliente2@teste.com", true, "Cliente 2", "Teste 2" },
                    { 3, new DateTime(2023, 1, 15, 7, 25, 7, 203, DateTimeKind.Local).AddTicks(2206), "cliente3@teste.com", true, "Cliente 3", "Teste 3" },
                    { 4, new DateTime(2023, 1, 15, 7, 25, 7, 203, DateTimeKind.Local).AddTicks(2207), "cliente4@teste.com", true, "Cliente 4", "Teste 4" },
                    { 5, new DateTime(2023, 1, 15, 7, 25, 7, 203, DateTimeKind.Local).AddTicks(2209), "cliente5@teste.com", true, "Cliente 5", "Teste 5" }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "IsDisponivel", "Nome", "Valor" },
                values: new object[,]
                {
                    { 1, true, "Lápis", 0m },
                    { 2, true, "Caderno", 0m },
                    { 3, true, "Borracha", 0m },
                    { 4, true, "Caneta", 0m },
                    { 5, true, "Apontador", 0m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}

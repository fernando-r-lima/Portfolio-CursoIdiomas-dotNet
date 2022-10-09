using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Curso_Idiomas.Migrations
{
    public partial class InscricaoUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInscricao",
                table: "Inscricao",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NotaFinal",
                table: "Inscricao",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInscricao",
                table: "Inscricao");

            migrationBuilder.DropColumn(
                name: "NotaFinal",
                table: "Inscricao");
        }
    }
}

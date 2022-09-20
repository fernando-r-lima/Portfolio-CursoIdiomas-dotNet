using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Curso_Idiomas.Migrations
{
    public partial class ProfessorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataContratacao",
                table: "Professor",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Professor",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataContratacao",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Professor");
        }
    }
}

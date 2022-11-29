using Microsoft.EntityFrameworkCore.Migrations;

namespace Curso_Idiomas.Migrations
{
    public partial class NotaFinalFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "NotaFinal",
                table: "Inscricao",
                type: "real",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NotaFinal",
                table: "Inscricao",
                type: "int",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true);
        }
    }
}

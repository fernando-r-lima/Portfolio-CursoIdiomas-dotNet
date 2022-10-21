using Microsoft.EntityFrameworkCore.Migrations;

namespace Curso_Idiomas.Migrations
{
    public partial class InscricaoCompositeKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricao",
                table: "Inscricao");

            migrationBuilder.DropIndex(
                name: "IX_Inscricao_AlunoId",
                table: "Inscricao");

            migrationBuilder.DropColumn(
                name: "InscricaoId",
                table: "Inscricao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricao",
                table: "Inscricao",
                columns: new[] { "AlunoId", "TurmaId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Inscricao",
                table: "Inscricao");

            migrationBuilder.AddColumn<int>(
                name: "InscricaoId",
                table: "Inscricao",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inscricao",
                table: "Inscricao",
                column: "InscricaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_AlunoId",
                table: "Inscricao",
                column: "AlunoId");
        }
    }
}

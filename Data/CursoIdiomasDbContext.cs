using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;

namespace Curso_Idiomas.Data
{
    public class CursoIdiomasDbContext : DbContext
    {
        public CursoIdiomasDbContext(DbContextOptions<CursoIdiomasDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Turma> Turma { get; set; }
        public DbSet<Inscricao> Inscricao { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }

        

    }
}

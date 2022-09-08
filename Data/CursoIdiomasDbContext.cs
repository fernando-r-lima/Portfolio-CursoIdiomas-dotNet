using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;

namespace Curso_Idiomas.Data
{
    public class CursoIdiomasDbContext : DbContext
    {
        public CursoIdiomasDbContext(DbContextOptions<CursoIdiomasDbContext> options) : base(options)
        {
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().ToTable("Aluno");
            modelBuilder.Entity<Turma>().ToTable("Turma");
            modelBuilder.Entity<Inscricao>().ToTable("Inscricao");
            modelBuilder.Entity<Professor>().ToTable("Professor");
            modelBuilder.Entity<Disciplina>().ToTable("Disciplina");
        }

    }
}

﻿// <auto-generated />
using System;
using Curso_Idiomas.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Curso_Idiomas.Migrations
{
    [DbContext(typeof(CursoIdiomasDbContext))]
    partial class CursoIdiomasDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Curso_Idiomas.Models.Aluno", b =>
                {
                    b.Property<int>("AlunoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataMatricula")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AlunoId");

                    b.ToTable("Aluno");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Disciplina", b =>
                {
                    b.Property<int>("DisciplinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DisciplinaId");

                    b.ToTable("Disciplina");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Inscricao", b =>
                {
                    b.Property<int>("AlunoId")
                        .HasColumnType("int");

                    b.Property<int>("TurmaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataInscricao")
                        .HasColumnType("datetime2");

                    b.Property<float?>("NotaFinal")
                        .HasColumnType("real");

                    b.HasKey("AlunoId", "TurmaId");

                    b.HasIndex("TurmaId");

                    b.ToTable("Inscricao");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Professor", b =>
                {
                    b.Property<int>("ProfessorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataContratacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Sobrenome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProfessorId");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Turma", b =>
                {
                    b.Property<int>("TurmaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisciplinaId")
                        .HasColumnType("int");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("int");

                    b.Property<string>("Semestre")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("TurmaId");

                    b.HasIndex("DisciplinaId");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Inscricao", b =>
                {
                    b.HasOne("Curso_Idiomas.Models.Aluno", "Aluno")
                        .WithMany("Inscricoes")
                        .HasForeignKey("AlunoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Curso_Idiomas.Models.Turma", "Turma")
                        .WithMany("Inscricoes")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Aluno");

                    b.Navigation("Turma");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Turma", b =>
                {
                    b.HasOne("Curso_Idiomas.Models.Disciplina", "Disciplina")
                        .WithMany("Turmas")
                        .HasForeignKey("DisciplinaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Curso_Idiomas.Models.Professor", "Professor")
                        .WithMany("Turmas")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disciplina");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Aluno", b =>
                {
                    b.Navigation("Inscricoes");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Disciplina", b =>
                {
                    b.Navigation("Turmas");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Professor", b =>
                {
                    b.Navigation("Turmas");
                });

            modelBuilder.Entity("Curso_Idiomas.Models.Turma", b =>
                {
                    b.Navigation("Inscricoes");
                });
#pragma warning restore 612, 618
        }
    }
}

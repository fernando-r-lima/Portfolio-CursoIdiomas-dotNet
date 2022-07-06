using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Curso_Idiomas.Models.ViewModels;

namespace Curso_Idiomas.Controllers
{
    public class TurmaController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public TurmaController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string ordem, string conteudoFiltro, string filtroEscolhido)
        {
            var viewModel = new TurmaViewModel();

            IQueryable<Turma> turmas = _context.Turma
                .Include(t => t.Inscricoes)
                .Include(t => t.Professor)
                .Include(t => t.Disciplina);

            if (!String.IsNullOrEmpty(conteudoFiltro))
            {
                switch (filtroEscolhido)
                {
                    case "disciplina":
                        viewModel.FiltroEscolhido = "disciplina";
                        turmas = turmas.Where(t => t.Disciplina.Nome.Contains(conteudoFiltro));
                        break;
                    case "horario":
                        viewModel.FiltroEscolhido = "horario";
                        turmas = turmas.Where(t => t.Horario.Contains(conteudoFiltro));
                        break;
                    case "professor":
                        viewModel.FiltroEscolhido = "professor";
                        turmas = turmas.Where(t => t.Professor.Nome.Contains(conteudoFiltro)
                                                || t.Professor.Sobrenome.Contains(conteudoFiltro));
                        break;
                }
            }

            switch (ordem)
            {
                case "disciplina_desc":
                    turmas = turmas.OrderByDescending(t => t.Disciplina.Nome).ThenBy(t => t.Horario).ThenBy(t => t.Professor.Nome);
                    break;
                case "horario":
                    turmas = turmas.OrderBy(t => t.Horario).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Professor.Nome);
                    break;
                case "horario_desc":
                    turmas = turmas.OrderByDescending(t => t.Horario).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Professor.Nome);
                    break;
                case "professor":
                    turmas = turmas.OrderBy(t => t.Professor.Nome).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Horario);
                    break;
                case "professor_desc":
                    turmas = turmas.OrderByDescending(t => t.Professor.Nome).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Horario);
                    break;
                default:
                    turmas = turmas.OrderBy(t => t.Disciplina.Nome).ThenBy(t => t.Horario).ThenBy(t => t.Professor.Nome);
                    break;
            }

            viewModel.Turmas = turmas;

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemDisciplina = String.IsNullOrEmpty(ordem) ? "disciplina_desc" : "";
            viewModel.OrdemHorario = ordem == "horario" ? "horario_desc" : "horario";
            viewModel.OrdemProfessor = ordem == "professor" ? "professor_desc" : "professor";

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            Turma turma = _context.Turma
                            .Include(t => t.Inscricoes)
                            .ThenInclude(i => i.Aluno)
                            .Include(t => t.Professor)
                            .Include(t => t.Disciplina)
                            .FirstOrDefault(t => t.TurmaId == id);
            return View(turma);
        }
    }
}

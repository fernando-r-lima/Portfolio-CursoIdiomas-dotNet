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
    public class ProfessorController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public ProfessorController(CursoIdiomasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string ordem, string conteudoFiltro, string filtroEscolhido)
        {
            var viewModel = new ProfessorViewModel();

            IQueryable<Professor> professores = _context.Professor.Include(p => p.Turmas);

            if (!String.IsNullOrEmpty(conteudoFiltro))
            {
                switch (filtroEscolhido)
                {
                    case "nome":
                        viewModel.FiltroEscolhido = "nome";
                        professores = professores.Where(p => p.Nome.Contains(conteudoFiltro));
                        break;
                    case "sobrenome":
                        viewModel.FiltroEscolhido = "sobrenome";
                        professores = professores.Where(p => p.Sobrenome.Contains(conteudoFiltro));
                        break;
                }
            }


            switch (ordem)
            {
                case "nome_desc":
                    professores = professores.OrderByDescending(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
                case "sobrenome":
                    professores = professores.OrderBy(p => p.Sobrenome).ThenBy(p => p.Nome);
                    break;
                case "sobrenome_desc":
                    professores = professores.OrderByDescending(p => p.Sobrenome).ThenBy(p => p.Nome);
                    break;
                default:
                    professores = professores.OrderBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
            }

            viewModel.Professores = professores;

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemNome = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            viewModel.OrdemSobrenome = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            Professor professor = _context.Professor
                                    .Include(p => p.Turmas)
                                    .ThenInclude(t => t.Disciplina)
                                    .FirstOrDefault(p => p.ProfessorId == id);
            return View(professor);
        }
    }
}

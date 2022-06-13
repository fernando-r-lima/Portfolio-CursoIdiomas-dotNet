using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Curso_Idiomas.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public ProfessorController(CursoIdiomasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string ordem)
        {
            IEnumerable<Professor> professores = _context.Professor.Include(p => p.Turmas);

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

            ViewData["BotaoNome"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["BotaoSobrenome"] = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";

            return View(professores);
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

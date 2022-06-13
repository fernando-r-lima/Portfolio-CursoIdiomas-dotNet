using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Curso_Idiomas.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public DisciplinaController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string ordem)
        {
            IEnumerable<Disciplina> disciplinas = _context.Disciplina.Include(d => d.Turmas);

            switch (ordem)
            {
                case "nome_desc":
                    disciplinas = disciplinas.OrderByDescending(d => d.Nome);
                    break;
                default:
                    disciplinas = disciplinas.OrderBy(d => d.Nome);
                    break;
            }

            ViewData["BotaoNome"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            
            return View(disciplinas);
        }
        public IActionResult Details(int id)
        {
            Disciplina disciplina = _context.Disciplina
                                               .Include(d => d.Turmas)
                                               .ThenInclude(t => t.Professor)
                                               .FirstOrDefault(d => d.DisciplinaId == id);
            return View(disciplina);
        }
    }
}

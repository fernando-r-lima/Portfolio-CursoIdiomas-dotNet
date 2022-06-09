using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Curso_Idiomas.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public DisciplinaController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Disciplina> disciplinas = _context.Disciplina.Include(d => d.Turmas);
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

using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Curso_Idiomas.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public ProfessorController(CursoIdiomasDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Professor> professores = _context.Professor.Include(p => p.Turmas);
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

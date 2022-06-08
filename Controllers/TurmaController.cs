using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;

namespace Curso_Idiomas.Controllers
{
    public class TurmaController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public TurmaController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Turma> turmas = _context.Turma;
            return View(turmas);
        }
    }
}

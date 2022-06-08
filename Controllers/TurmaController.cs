using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;

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
            IEnumerable<Turma> turmas = _context.Turma
                .Include(t => t.Professor)
                .Include(t => t.Disciplina);
                                
            return View(turmas);
        }
    }
}

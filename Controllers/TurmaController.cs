using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
                .Include(t => t.Inscricoes)
                .Include(t => t.Professor)
                .Include(t => t.Disciplina);

            return View(turmas);
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

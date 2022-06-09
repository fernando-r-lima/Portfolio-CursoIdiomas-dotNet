using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Curso_Idiomas.Controllers
{
    public class AlunoController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public AlunoController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            IEnumerable<Aluno> alunos = _context.Aluno.Include(a => a.Inscricoes);
            return View(alunos);
        }

        public IActionResult Details(int id)
        {
            Aluno aluno = _context.Aluno
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Disciplina)
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Professor)
                                    .FirstOrDefault(a => a.AlunoId == id);

            return View(aluno);
        }
    }
}

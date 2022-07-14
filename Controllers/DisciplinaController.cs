using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Curso_Idiomas.Models.ViewModels;
using System.Threading.Tasks;

namespace Curso_Idiomas.Controllers
{
    public class DisciplinaController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public DisciplinaController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string ordem, string conteudoFiltro)
        {
            var viewModel = new DisciplinaViewModel();

            IQueryable<Disciplina> disciplinas = _context.Disciplina.Include(d => d.Turmas);

            if (!String.IsNullOrEmpty(conteudoFiltro))
                disciplinas = disciplinas.Where(a => a.Nome.Contains(conteudoFiltro));

            switch (ordem)
            {
                case "nome_desc":
                    disciplinas = disciplinas.OrderByDescending(d => d.Nome);
                    break;
                default:
                    disciplinas = disciplinas.OrderBy(d => d.Nome);
                    break;
            }

            viewModel.Disciplinas = await disciplinas.ToListAsync();

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemNome = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";

            return View(viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            Disciplina disciplina = await _context.Disciplina
                                               .Include(d => d.Turmas)
                                               .ThenInclude(t => t.Professor)
                                               .FirstOrDefaultAsync(d => d.DisciplinaId == id);
            return View(disciplina);
        }
    }
}

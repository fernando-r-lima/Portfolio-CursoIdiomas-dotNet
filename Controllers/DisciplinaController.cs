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

            IQueryable<Disciplina> disciplinas = _context.Disciplinas.Include(d => d.Turmas);

            if (!String.IsNullOrEmpty(conteudoFiltro))
                disciplinas = disciplinas.Where(a => a.Nome.Contains(conteudoFiltro));

            switch (ordem)
            {
                case "nome_desc":
                    disciplinas = disciplinas.OrderByDescending(d => d.Nome);
                    break;
                case "turmas":
                    disciplinas = disciplinas.OrderBy(d => d.Turmas.Count).ThenBy(d => d.Nome);
                    break;
                case "turmas_desc":
                    disciplinas = disciplinas.OrderByDescending(d => d.Turmas.Count).ThenBy(d => d.Nome);
                    break;

                default:
                    disciplinas = disciplinas.OrderBy(d => d.Nome);
                    break;
            }

            viewModel.Disciplinas = await disciplinas.ToListAsync();

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemNome = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            viewModel.OrdemTurmas = ordem == "turmas" ? "turmas_desc" : "turmas";

            return View(viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            Disciplina disciplina = await _context.Disciplinas
                                               .Include(d => d.Turmas)
                                               .ThenInclude(t => t.Professor)
                                               .FirstOrDefaultAsync(d => d.DisciplinaId == id);
            return View(disciplina);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disciplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disciplina);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplinaAtualizando = await _context.Disciplinas.FirstOrDefaultAsync(d => d.DisciplinaId == id);

            if (await TryUpdateModelAsync<Disciplina>(disciplinaAtualizando, "", d => d.Nome))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(disciplinaAtualizando);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disciplina = await _context.Disciplinas
                                    .Include(d => d.Turmas).ThenInclude(t => t.Professor)
                                    .FirstOrDefaultAsync(d => d.DisciplinaId == id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

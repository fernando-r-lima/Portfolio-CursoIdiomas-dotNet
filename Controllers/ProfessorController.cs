using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Curso_Idiomas.Models.ViewModels;
using System.Threading.Tasks;

namespace Curso_Idiomas.Controllers
{
    public class ProfessorController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public ProfessorController(CursoIdiomasDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string ordem, string conteudoFiltro, string filtroEscolhido)
        {
            var viewModel = new ProfessorViewModel();

            IQueryable<Professor> professores = _context.Professores.Include(p => p.Turmas);

            if (!String.IsNullOrEmpty(conteudoFiltro))
            {
                switch (filtroEscolhido)
                {
                    case "nome":
                        viewModel.FiltroEscolhido = "nome";
                        professores = professores.Where(p => p.Nome.Contains(conteudoFiltro));
                        break;
                    case "sobrenome":
                        viewModel.FiltroEscolhido = "sobrenome";
                        professores = professores.Where(p => p.Sobrenome.Contains(conteudoFiltro));
                        break;
                }
            }


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

            viewModel.Professores = await professores.ToListAsync();

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemNome = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            viewModel.OrdemSobrenome = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Professor professor = await _context.Professores
                                    .Include(p => p.Turmas)
                                    .ThenInclude(t => t.Disciplina)
                                    .FirstOrDefaultAsync(p => p.ProfessorId == id);
            return View(professor);
        }
        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sobrenome")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professores.FindAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professorAtualizando = await _context.Professores.FirstOrDefaultAsync(a => a.ProfessorId == id);

            if (await TryUpdateModelAsync<Professor>(professorAtualizando, "", p => p.Nome, p => p.Sobrenome))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(professorAtualizando);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professores
                                    .Include(p => p.Turmas).ThenInclude(t => t.Disciplina)
                                    .FirstOrDefaultAsync(p => p.ProfessorId == id);

            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professores.FindAsync(id);

            if (professor == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Professores.Remove(professor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

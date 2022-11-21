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

                    case "email":
                        viewModel.FiltroEscolhido = "email";
                        professores = professores.Where(p => p.Email.Contains(conteudoFiltro));
                        break;
                    case "dataContratacao":
                        viewModel.FiltroEscolhido = "dataContratacao";
                        professores = professores.Where(p => p.DataContratacao.ToString().Contains(conteudoFiltro));
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
                case "matricula":
                    professores = professores.OrderBy(p => p.ProfessorId);
                    break;
                case "matricula_desc":
                    professores = professores.OrderByDescending(p => p.ProfessorId);
                    break;
                case "email":
                    professores = professores.OrderBy(p => p.Email).ThenBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
                case "email_desc":
                    professores = professores.OrderByDescending(p => p.Email).ThenBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
                case "dataContratacao":
                    professores = professores.OrderBy(p => p.DataContratacao).ThenBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
                case "dataContratacao_desc":
                    professores = professores.OrderByDescending(p => p.DataContratacao).ThenBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
                case "turmas":
                    professores = professores.OrderBy(p => p.Turmas.Count).ThenBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
                case "turmas_desc":
                    professores = professores.OrderByDescending(p => p.Turmas.Count).ThenBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;

                default:
                    professores = professores.OrderBy(p => p.Nome).ThenBy(p => p.Sobrenome);
                    break;
            }

            viewModel.Professores = await professores.ToListAsync();

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemNome = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            viewModel.OrdemSobrenome = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";
            viewModel.OrdemMatricula = ordem == "matricula" ? "matricula_desc" : "matricula";
            viewModel.OrdemEmail = ordem == "email" ? "email_desc" : "email";
            viewModel.OrdemDataContratacao = ordem == "dataContratacao" ? "dataContratacao_desc" : "dataContratacao";
            viewModel.OrdemTurmas = ordem == "turmas" ? "turmas_desc" : "turmas";

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
        public async Task<IActionResult> Create([Bind("DataContratacao,Nome,Sobrenome,Email")] Professor professor)
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

            if (await TryUpdateModelAsync<Professor>(professorAtualizando, "", p => p.Email, p => p.Nome, p => p.Sobrenome, p => p.DataContratacao))
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

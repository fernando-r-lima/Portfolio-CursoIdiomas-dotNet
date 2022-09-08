using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using Curso_Idiomas.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Curso_Idiomas.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curso_Idiomas.Controllers
{
    public class AlunoController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public AlunoController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string ordem, string conteudoFiltro, string filtroEscolhido)
        {

            var viewModel = new AlunoViewModel();

            IQueryable<Aluno> alunos = _context.Aluno.Include(a => a.Inscricoes);

            //Codigo com IEnumerable nao faz busca com case-insensitive, precisa usar ToUpper()
            //IEnumerable<Aluno> alunos = await _context.Aluno.Include(a => a.Inscricoes).ToListAsync();

            if (!String.IsNullOrEmpty(conteudoFiltro))
            {
                switch (filtroEscolhido)
                {
                    case "nome":
                        viewModel.FiltroEscolhido = "nome";
                        //alunos = alunos.Where(a => a.Nome.ToUpper().Contains(conteudoFiltro.ToUpper()));
                        alunos = alunos.Where(a => a.Nome.Contains(conteudoFiltro));
                        break;
                    case "sobrenome":
                        viewModel.FiltroEscolhido = "sobrenome";
                        alunos = alunos.Where(a => a.Sobrenome.Contains(conteudoFiltro));
                        break;
                }
            }

            switch (ordem)
            {
                case "nome_desc":
                    alunos = alunos.OrderByDescending(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
                case "sobrenome":
                    alunos = alunos.OrderBy(a => a.Sobrenome).ThenBy(a => a.Nome);
                    break;
                case "sobrenome_desc":
                    alunos = alunos.OrderByDescending(a => a.Sobrenome).ThenBy(a => a.Nome);
                    break;
                default:
                    alunos = alunos.OrderBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
            }

            viewModel.Alunos = await alunos.ToListAsync();

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemNome = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            viewModel.OrdemSobrenome = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Aluno aluno = await _context.Aluno
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Disciplina)
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Professor)
                                    .FirstOrDefaultAsync(a => a.AlunoId == id);

            return View(aluno);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Sobrenome")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alunoAtualizando = await _context.Aluno.FirstOrDefaultAsync(a => a.AlunoId == id);

            if (await TryUpdateModelAsync<Aluno>(alunoAtualizando, "", a => a.Nome, a => a.Sobrenome))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(alunoAtualizando);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var aluno = await _context.Aluno.FirstOrDefaultAsync(a => a.AlunoId == id);

            var aluno = await _context.Aluno
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Disciplina)
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Professor)
                                    .FirstOrDefaultAsync(a => a.AlunoId == id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);

            if (aluno == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

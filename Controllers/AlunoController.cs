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

            IQueryable<Aluno> alunos = _context.Alunos.Include(a => a.Inscricoes);

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
                    case "email":
                        viewModel.FiltroEscolhido = "email";
                        alunos = alunos.Where(a => a.Email.Contains(conteudoFiltro));
                        break;
                    case "dataMatricula":
                        viewModel.FiltroEscolhido = "dataMatricula";
                        alunos = alunos.Where(a => a.DataMatricula.ToString().Contains(conteudoFiltro));
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
                case "matricula":
                    alunos = alunos.OrderBy(a => a.AlunoId);
                    break;
                case "matricula_desc":
                    alunos = alunos.OrderByDescending(a => a.AlunoId);
                    break;
                case "email":
                    alunos = alunos.OrderBy(a => a.Email).ThenBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
                case "email_desc":
                    alunos = alunos.OrderByDescending(a => a.Email).ThenBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
                case "dataMatricula":
                    alunos = alunos.OrderBy(a => a.DataMatricula).ThenBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
                case "dataMatricula_desc":
                    alunos = alunos.OrderByDescending(a => a.DataMatricula).ThenBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
                case "turmas":
                    alunos = alunos.OrderBy(a => a.Inscricoes.Count).ThenBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
                case "turmas_desc":
                    alunos = alunos.OrderByDescending(a => a.Inscricoes.Count).ThenBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;

                default:
                    alunos = alunos.OrderBy(a => a.Nome).ThenBy(a => a.Sobrenome);
                    break;
            }

            viewModel.Alunos = await alunos.ToListAsync();

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemNome = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            viewModel.OrdemSobrenome = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";
            viewModel.OrdemMatricula = ordem == "matricula" ? "matricula_desc" : "matricula";
            viewModel.OrdemEmail = ordem == "email" ? "email_desc" : "email";
            viewModel.OrdemDataMatricula = ordem == "dataMatricula" ? "dataMatricula_desc" : "dataMatricula";
            viewModel.OrdemTurmas = ordem == "turmas" ? "turmas_desc" : "turmas";

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Aluno aluno = await _context.Alunos
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
        public async Task<IActionResult> Create([Bind("DataMatricula,Nome,Sobrenome,Email")] Aluno aluno)
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

            var aluno = await _context.Alunos.FindAsync(id);

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

            var alunoAtualizando = await _context.Alunos.FirstOrDefaultAsync(a => a.AlunoId == id);

            if (await TryUpdateModelAsync<Aluno>(alunoAtualizando, "", a => a.Email, a => a.Nome, a => a.Sobrenome, a => a.DataMatricula))
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

            var aluno = await _context.Alunos
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
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

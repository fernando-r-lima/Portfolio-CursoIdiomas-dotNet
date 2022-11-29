using Curso_Idiomas.Data;
using Curso_Idiomas.Models;
using Curso_Idiomas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso_Idiomas.Controllers
{
    public class InscricaoController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public InscricaoController(CursoIdiomasDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(int alunoId)
        {
            Aluno aluno = await _context.Alunos
                .Include(a => a.Inscricoes).ThenInclude(i => i.Turma)
                .FirstOrDefaultAsync(a => a.AlunoId == alunoId);

            IQueryable<Turma> turmas = _context.Turmas
                .Include(t => t.Professor)
                .Include(t => t.Disciplina);

            List<Turma> turmasInscritas = new List<Turma>();
            foreach (Inscricao i in aluno.Inscricoes)
            {
                turmasInscritas.Add(i.Turma);
            }

            var viewModel = new InscricaoViewModel();
            viewModel.Aluno = aluno;
            viewModel.Turmas = turmas;
            viewModel.TurmasInscritas = turmasInscritas;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoId,TurmaId")] Inscricao inscricao, string operacao)
        {
            switch (operacao)
            {
                case "criar":
                    inscricao.DataInscricao = DateTime.Now;
                    _context.Add(inscricao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", new { alunoId = inscricao.AlunoId });

                case "excluir":
                    _context.Inscricoes.Remove(inscricao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Create", new { alunoId = inscricao.AlunoId });
            }
            return View(inscricao);
        }

        public async Task<IActionResult> Edit(int? alunoId, int? turmaId)
        {
            if (alunoId == null || turmaId == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricoes.Include(i => i.Aluno)
                                                    .Include(i => i.Turma)
                                                    .ThenInclude(t => t.Professor)
                                                    .Include(i => i.Turma)
                                                    .ThenInclude(t => t.Disciplina)
                                                    .FirstOrDefaultAsync(i => i.AlunoId == alunoId && i.TurmaId == turmaId);
            if (inscricao == null)
            {
                return NotFound();
            }

            return View(inscricao);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? alunoId, int? turmaId)
        {
            if (alunoId == null || turmaId == null)
            {
                return NotFound();
            }

            var inscricaoAtualizando = await _context.Inscricoes
                                                    .Include(i => i.Aluno)
                                                    .Include(i => i.Turma)
                                                    .ThenInclude(t => t.Professor)
                                                    .Include(i => i.Turma)
                                                    .ThenInclude(t => t.Disciplina)
                                                    .FirstOrDefaultAsync(i => i.AlunoId == alunoId && i.TurmaId == turmaId);

            float? notaAtual = inscricaoAtualizando.NotaFinal;

            if (await TryUpdateModelAsync<Inscricao>(inscricaoAtualizando, "", i => i.NotaFinal))
            {
                await _context.SaveChangesAsync();
                return View(inscricaoAtualizando);
            }

            inscricaoAtualizando.NotaFinal = notaAtual;
            return View(inscricaoAtualizando);
        }
    }
}

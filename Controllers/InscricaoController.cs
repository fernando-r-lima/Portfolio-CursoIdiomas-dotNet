using Curso_Idiomas.Data;
using Curso_Idiomas.Models;
using Curso_Idiomas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .Include(a => a.Inscricoes)
                .FirstOrDefaultAsync(a => a.AlunoId == alunoId);

            IQueryable<Turma> turmas = _context.Turmas
                .Include(t => t.Professor)
                .Include(t => t.Disciplina);

            List<int> turmasInscritas = new List<int>();
            foreach (Inscricao i in aluno.Inscricoes)
            {
                turmasInscritas.Add(i.TurmaId);
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


    }
}

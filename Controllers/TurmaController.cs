using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Curso_Idiomas.Models.ViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Curso_Idiomas.Controllers
{
    public class TurmaController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public TurmaController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string ordem, string conteudoFiltro, string filtroEscolhido)
        {
            var viewModel = new TurmaViewModel();

            IQueryable<Turma> turmas = _context.Turmas
                .Include(t => t.Inscricoes)
                .Include(t => t.Professor)
                .Include(t => t.Disciplina);

            if (!String.IsNullOrEmpty(conteudoFiltro))
            {
                switch (filtroEscolhido)
                {
                    case "disciplina":
                        viewModel.FiltroEscolhido = "disciplina";
                        turmas = turmas.Where(t => t.Disciplina.Nome.Contains(conteudoFiltro));
                        break;
                    case "horario":
                        viewModel.FiltroEscolhido = "horario";
                        turmas = turmas.Where(t => t.Horario.Contains(conteudoFiltro));
                        break;
                    case "professor":
                        viewModel.FiltroEscolhido = "professor";
                        turmas = turmas.Where(t => t.Professor.Nome.Contains(conteudoFiltro)
                                                || t.Professor.Sobrenome.Contains(conteudoFiltro));
                        break;
                }
            }

            switch (ordem)
            {
                case "disciplina_desc":
                    turmas = turmas.OrderByDescending(t => t.Disciplina.Nome).ThenBy(t => t.Horario).ThenBy(t => t.Professor.Nome);
                    break;
                case "horario":
                    turmas = turmas.OrderBy(t => t.Horario).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Professor.Nome);
                    break;
                case "horario_desc":
                    turmas = turmas.OrderByDescending(t => t.Horario).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Professor.Nome);
                    break;
                case "professor":
                    turmas = turmas.OrderBy(t => t.Professor.Nome).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Horario);
                    break;
                case "professor_desc":
                    turmas = turmas.OrderByDescending(t => t.Professor.Nome).ThenBy(t => t.Disciplina.Nome).ThenBy(t => t.Horario);
                    break;
                default:
                    turmas = turmas.OrderBy(t => t.Disciplina.Nome).ThenBy(t => t.Horario).ThenBy(t => t.Professor.Nome);
                    break;
            }

            viewModel.Turmas = await turmas.ToListAsync();

            viewModel.ConteudoFiltro = conteudoFiltro;

            viewModel.OrdemDisciplina = String.IsNullOrEmpty(ordem) ? "disciplina_desc" : "";
            viewModel.OrdemHorario = ordem == "horario" ? "horario_desc" : "horario";
            viewModel.OrdemProfessor = ordem == "professor" ? "professor_desc" : "professor";

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Turma turma = await _context.Turmas
                            .Include(t => t.Inscricoes)
                            .ThenInclude(i => i.Aluno)
                            .Include(t => t.Professor)
                            .Include(t => t.Disciplina)
                            .FirstOrDefaultAsync(t => t.TurmaId == id);
            return View(turma);
        }

        public IActionResult Create()
        {
            PopularListasDeSelecao();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Horario,Semestre,DisciplinaId,ProfessorId")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopularListasDeSelecao(turma.DisciplinaId, turma.ProfessorId);
            return View(turma);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas.Include(t => t.Disciplina).FirstOrDefaultAsync(t => t.TurmaId == id);

            if (turma == null)
            {
                return NotFound();
            }

            PopularListasDeSelecao(turma.DisciplinaId, turma.ProfessorId);
            return View(turma);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turmaAtualizando = await _context.Turmas.FirstOrDefaultAsync(t => t.TurmaId == id);

            if (await TryUpdateModelAsync<Turma>(
                turmaAtualizando, "", t => t.Horario, t => t.Semestre, t => t.DisciplinaId, t => t.ProfessorId))
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopularListasDeSelecao(turmaAtualizando.DisciplinaId, turmaAtualizando.ProfessorId);
            return View(turmaAtualizando);
        }

        private void PopularListasDeSelecao(object disciplinaSelecionada = null, object professorSelecionado = null)
        {
            var listaProfessores = _context.Professores.OrderBy(p => p.Nome).ThenBy(p => p.Sobrenome);
            ViewBag.ListaProfessores = new SelectList(listaProfessores, "ProfessorId", "NomeCompleto", professorSelecionado);

            var listaDisciplinas = _context.Disciplinas.OrderBy(d => d.Nome);
            ViewBag.ListaDisciplinas = new SelectList(listaDisciplinas, "DisciplinaId", "Nome", disciplinaSelecionada);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas
                                    .Include(t => t.Professor)
                                    .Include(t => t.Disciplina)
                                    .Include(t => t.Inscricoes).ThenInclude(i => i.Aluno)
                                    .FirstOrDefaultAsync(a => a.TurmaId == id);

            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Curso_Idiomas.Controllers
{
    public class TurmaController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public TurmaController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string ordem)
        {
            IEnumerable<Turma> turmas = _context.Turma
                .Include(t => t.Inscricoes)
                .Include(t => t.Professor)
                .Include(t => t.Disciplina);

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

            ViewData["BotaoDisciplina"] = String.IsNullOrEmpty(ordem) ? "disciplina_desc" : "";
            ViewData["BotaoHorario"] = ordem == "horario" ? "horario_desc" : "horario";
            ViewData["BotaoProfessor"] = ordem == "professor" ? "professor_desc" : "professor";

            return View(turmas);
        }

        public IActionResult Details(int id)
        {
            Turma turma = _context.Turma
                            .Include(t => t.Inscricoes)
                            .ThenInclude(i => i.Aluno)
                            .Include(t => t.Professor)
                            .Include(t => t.Disciplina)
                            .FirstOrDefault(t => t.TurmaId == id);
            return View(turma);
        }
    }
}

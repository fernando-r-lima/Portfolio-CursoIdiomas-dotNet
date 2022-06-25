using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using System.Collections.Generic;
using Curso_Idiomas.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Curso_Idiomas.Controllers
{
    public class AlunoController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public AlunoController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string ordem, string filtro)
        {
            //Codigo abaixo com IEnumerable so fazia busca com case-sensitive
            //IEnumerable<Aluno> alunos = _context.Aluno.Include(a => a.Inscricoes);
            
            IQueryable<Aluno> alunos = _context.Aluno.Include(a => a.Inscricoes);

            if (!String.IsNullOrEmpty(filtro))
                alunos = alunos.Where(a => a.Nome.Contains(filtro) || a.Sobrenome.Contains(filtro));

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

            /*
            if (String.IsNullOrEmpty(ordem))
                ViewData["BotaoNome"] = "nome_desc";
            else
                ViewData["BotaoNome"] = "";

            if (ordem == "sobrenome")
                ViewData["BotaoSobrenome"] = "sobrenome_desc";
            else
                ViewData["BotaoSobrenome"] = "sobrenome";
            */

            ViewData["BotaoNome"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["BotaoSobrenome"] = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";
            ViewData["FiltroAtual"] = filtro;

            return View(alunos);
        }

        public IActionResult Details(int id)
        {
            Aluno aluno = _context.Aluno
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Disciplina)
                                    .Include(a => a.Inscricoes).ThenInclude(i => i.Turma).ThenInclude(t => t.Professor)
                                    .FirstOrDefault(a => a.AlunoId == id);

            return View(aluno);
        }
    }
}

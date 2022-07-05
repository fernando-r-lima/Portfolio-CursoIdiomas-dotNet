using Microsoft.AspNetCore.Mvc;
using Curso_Idiomas.Data;
using Curso_Idiomas.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Curso_Idiomas.Models.ViewModels;

namespace Curso_Idiomas.Controllers
{
    public class AlunoController : Controller
    {
        private readonly CursoIdiomasDbContext _context;
        public AlunoController(CursoIdiomasDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string ordem, string conteudoFiltro, string filtroEscolhido)
        {

            var viewModel = new AlunoViewModel();

            //Codigo usando IEnumerable so fazia busca com case-sensitive
            IQueryable<Aluno> alunos = _context.Aluno.Include(a => a.Inscricoes);

            if (!String.IsNullOrEmpty(conteudoFiltro))
            {
                switch (filtroEscolhido)
                {
                    case "nome":
                        viewModel.FiltroEscolhido = "nome";
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

            viewModel.Alunos = alunos;
            viewModel.ConteudoFiltro = conteudoFiltro;

            ViewData["BotaoNome"] = String.IsNullOrEmpty(ordem) ? "nome_desc" : "";
            ViewData["BotaoSobrenome"] = ordem == "sobrenome" ? "sobrenome_desc" : "sobrenome";
            
            return View(viewModel);
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

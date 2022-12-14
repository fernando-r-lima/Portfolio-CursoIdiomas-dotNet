using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Curso_Idiomas.Models.ViewModels
{
    public class AlunoViewModel
    {
        public IEnumerable<Aluno> Alunos { get; set; }


        public string OrdemNome { get; set; }
        public string OrdemSobrenome { get; set; }
        public string OrdemMatricula { get; set; }
        public string OrdemEmail { get; set; }
        public string OrdemDataMatricula { get; set; }
        public string OrdemTurmas { get; set; }
        

        public string ConteudoFiltro { get; set; }
        public string FiltroEscolhido { get; set; }
        public List<SelectListItem> OpcoesFiltro { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "nome", Text = "Nome" },
            new SelectListItem { Value = "sobrenome", Text = "Sobrenome"},
            new SelectListItem { Value = "email", Text = "Email"},
            new SelectListItem { Value = "dataMatricula", Text = "DataMatricula"},
        };
    }
}

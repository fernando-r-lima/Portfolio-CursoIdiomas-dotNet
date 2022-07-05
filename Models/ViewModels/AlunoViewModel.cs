using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Curso_Idiomas.Models.ViewModels
{
    public class AlunoViewModel
    {
        public IEnumerable<Aluno> Alunos { get; set; }

        public string ConteudoFiltro { get; set; }

        public string FiltroEscolhido { get; set; }

        public List<SelectListItem> OpcoesFiltro { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "nome", Text = "Nome" },
            new SelectListItem { Value = "sobrenome", Text = "Sobrenome"},
        };
    }
}

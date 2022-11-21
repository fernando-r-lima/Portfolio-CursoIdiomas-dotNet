using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Curso_Idiomas.Models.ViewModels
{
    public class ProfessorViewModel
    {
        public IEnumerable<Professor> Professores { get; set; }

        public string OrdemNome { get; set; }
        public string OrdemSobrenome { get; set; }
        public string OrdemMatricula { get; set; }
        public string OrdemEmail { get; set; }
        public string OrdemDataContratacao { get; set; }
        public string OrdemTurmas { get; set; }


        public string ConteudoFiltro { get; set; }
        public string FiltroEscolhido { get; set; }
        public List<SelectListItem> OpcoesFiltro { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "nome", Text = "Nome" },
            new SelectListItem { Value = "sobrenome", Text = "Sobrenome"},
            new SelectListItem { Value = "email", Text = "Email"},
            new SelectListItem { Value = "dataContratacao", Text = "Data de Contratação"},
        };
    }
}

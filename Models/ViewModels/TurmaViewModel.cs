using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Curso_Idiomas.Models.ViewModels
{
    public class TurmaViewModel
    {
        public IEnumerable<Turma> Turmas { get; set; }

        public string OrdemDisciplina { get; set; }
        public string OrdemHorario { get; set; }
        public string OrdemProfessor { get; set; }
        public string OrdemCodigo { get; set; }
        public string OrdemSemestre { get; set; }
        public string OrdemAlunos { get; set; }


        public string ConteudoFiltro { get; set; }
        public string FiltroEscolhido { get; set; }
        public List<SelectListItem> OpcoesFiltro { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "disciplina", Text = "Disciplina" },
            new SelectListItem { Value = "horario", Text = "Horario" },
            new SelectListItem { Value = "professor", Text = "Professor" },
            new SelectListItem { Value = "semestre", Text = "Semestre" },
            
        };
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Curso_Idiomas.Models.ViewModels
{
    public class DisciplinaViewModel
    {
        public IEnumerable<Disciplina> Disciplinas { get; set; }
        public string OrdemNome { get; set; }
        public string ConteudoFiltro { get; set; }
    }
}

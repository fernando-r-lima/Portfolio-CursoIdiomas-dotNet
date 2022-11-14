using System.Collections.Generic;

namespace Curso_Idiomas.Models.ViewModels
{
    public class InscricaoViewModel
    {
        public IEnumerable<Turma> Turmas { get; set; }

        public Aluno Aluno { get; set; }

        public IEnumerable<Turma> TurmasInscritas { get; set; }
    }
}

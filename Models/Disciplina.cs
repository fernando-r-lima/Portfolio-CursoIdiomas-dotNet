using System.Collections.Generic;

namespace Curso_Idiomas.Models
{
    public class Disciplina
    {
        public int DisciplinaId { get; set; }
        public string Nome { get; set; }
        //exemplo "Alemão A2.1"
        public ICollection<Turma> Turmas { get; set; }
    }
}

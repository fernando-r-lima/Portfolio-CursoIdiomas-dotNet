using System.Collections.Generic;

namespace Curso_Idiomas.Models
{
    public class Professor
    {
        public int ProfessorId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public ICollection<Turma> Turmas { get; set; }
    }
}

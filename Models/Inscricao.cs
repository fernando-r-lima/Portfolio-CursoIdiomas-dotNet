using System.Collections.Generic;

namespace Curso_Idiomas.Models
{
    public class Inscricao
    {
        public int InscricaoId { get; set; }
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }

        public Aluno Aluno { get; set; }
        public Turma Turma { get; set; }
    }
}

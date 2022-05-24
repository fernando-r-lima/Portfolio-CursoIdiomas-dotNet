using System.Collections.Generic;

namespace Curso_Idiomas.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public List<Inscricao> Inscricoes { get; set; }

    }
}

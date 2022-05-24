using System.Collections.Generic;

namespace Curso_Idiomas.Models
{
    public class Turma
    {
        public int TurmaId { get; set; }
        public string Horario { get; set; }
        //exemplo "Seg e Qua - 15:00 às 17:00"
        public string Semestre { get; set; }
        //exemplo "2022.1"
        public int DisciplinaId { get; set; }
        public int ProfessorId { get; set; }


        public Disciplina Disciplina { get; set; }
        public Professor Professor { get; set; }
        public List<Inscricao> Inscricoes { get; set; }



    }
}

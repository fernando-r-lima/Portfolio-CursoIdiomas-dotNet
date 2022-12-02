using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Curso_Idiomas.Models
{
    public class Turma
    {
        [Display(Name = "Código")]
        [DisplayFormat(DataFormatString = "{0:000}")]
        public int TurmaId { get; set; }

        [Display(Name = "Horário")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O tamanho permitido desse campo é de 2 a 50 caracteres")]
        public string Horario { get; set; }
        //exemplo "Seg e Qua - 15:00 às 17:00"

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "O tamanho permitido desse campo é de 2 a 10 caracteres")]
        public string Semestre { get; set; }
        //exemplo "2022.1"

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int DisciplinaId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int ProfessorId { get; set; }


        public Disciplina Disciplina { get; set; }
        public Professor Professor { get; set; }
        public List<Inscricao> Inscricoes { get; set; }



    }
}

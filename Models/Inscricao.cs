using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Curso_Idiomas.Models
{
    public class Inscricao
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }

        [Display(Name = "Nota final")]
        [DisplayFormat(NullDisplayText = "-")]
        [Range(0,10, ErrorMessage = "Insira um valor entre {1} e {2}.")]
        public float? NotaFinal { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:G}")]
        [Display(Name = "Data da inscrição")]
        public DateTime DataInscricao { get; set; }

        public Aluno Aluno { get; set; }
        public Turma Turma { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Curso_Idiomas.Models
{
    public class Aluno
    {
        [Display(Name = "Matrícula")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O tamanho permitido desse campo é de 2 a 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O tamanho permitido desse campo é de 2 a 50 caracteres")]
        public string Sobrenome { get; set; }

        [EmailAddress(ErrorMessage = "Insira um email válido")]
        [DataType(DataType.EmailAddress)]
        [DisplayFormat(NullDisplayText = "- sem email -")]
        public string Email { get; set; }

        //ErrorMessage nao funciona    [Required(ErrorMessage = "Este campo é obrigatório")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Matrícula")]
        public DateTime DataMatricula { get; set; }

        [Display(Name = "Nome Completo")]
        public string NomeCompleto
        {
            get
            {
                return $"{Nome} {Sobrenome}";
            }
        }


        public List<Inscricao> Inscricoes { get; set; }

    }
}

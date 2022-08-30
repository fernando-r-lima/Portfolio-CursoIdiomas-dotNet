using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Curso_Idiomas.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O tamanho permitido desse campo é de 2 a 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O tamanho permitido desse campo é de 2 a 50 caracteres")]
        public string Sobrenome { get; set; }

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

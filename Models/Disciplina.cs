using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Curso_Idiomas.Models
{
    public class Disciplina
    {
        public int DisciplinaId { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O tamanho permitido desse campo é de 2 a 50 caracteres")]
        public string Nome { get; set; }
        //exemplo "Alemão A2"
        
        public List<Turma> Turmas { get; set; }
    }
}

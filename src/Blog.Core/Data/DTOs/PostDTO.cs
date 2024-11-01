using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Data.DTOs
{
    public class PostDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo {1} caracteres")]
        [MinLength(5, ErrorMessage = "O conteúdo deve ter pelo menos {1} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(20, ErrorMessage = "O conteúdo deve ter pelo menos {1} caracteres")]
        public string Conteudo { get; set; }


    }
}

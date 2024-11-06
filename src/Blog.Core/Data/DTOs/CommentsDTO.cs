using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Data.DTOs
{
    public class CommentsDTO
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public int PostId { get; set; }
    }
}

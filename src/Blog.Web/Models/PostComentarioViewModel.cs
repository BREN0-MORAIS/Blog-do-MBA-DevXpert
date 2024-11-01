using Blog.Core.Data.DTOs;
using Blog.Core.Entities;

namespace Blog.Web.Models
{
    public class PostComentarioViewModel
    {
        public Post Post { get; set; } = new Post();
        public string Comentario { get; set; }
    }
}

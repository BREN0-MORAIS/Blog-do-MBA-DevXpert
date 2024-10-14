
using Microsoft.AspNetCore.Identity;

namespace Blog.Core.Entities
{
    public class Autor : IdentityUser
    {
        public string NomeCompleto { get; set; }
        public string Biografia { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}

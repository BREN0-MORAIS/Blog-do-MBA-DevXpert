using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Entities
{
    public class Autor : IdentityUser
    {
        public string NomeCompleto { get; set; }

        public string Biografia { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}

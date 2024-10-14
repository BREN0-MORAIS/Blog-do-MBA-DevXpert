using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Autor>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}

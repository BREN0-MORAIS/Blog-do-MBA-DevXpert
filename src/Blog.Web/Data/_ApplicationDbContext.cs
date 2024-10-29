using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data
{
    public class _ApplicationDbContext : IdentityDbContext<Autor>
    {
        public _ApplicationDbContext(DbContextOptions<_ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}

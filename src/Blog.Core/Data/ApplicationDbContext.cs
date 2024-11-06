using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<Autor>
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Autor> Autor { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder); 

			modelBuilder.Entity<Post>()
				.HasKey(p => p.Id);

			modelBuilder.Entity<Post>()
				.HasMany(p => p.Comentarios)
				.WithOne(c => c.Post)
				.HasForeignKey(c => c.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			//modelBuilder.Entity<Comentario>()

			//  .HasOne(c => c.Post)
			//  .WithMany(p => p.Comentarios)
			//  .HasForeignKey(c => c.PostId)
			//  .OnDelete(DeleteBehavior.Restrict);





			modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "User", NormalizedName = "USER" },
                new IdentityRole { Id = "2", Name = "Admin", NormalizedName = "ADMIN" }
            );

   
            var hasher = new PasswordHasher<Autor>();

            modelBuilder.Entity<Autor>().HasData(
                new Autor
                {
                    Id = "1", // Id do usuário
                    UserName = "user1@example.com",
                    NormalizedUserName = "USER1@EXAMPLE.COM",
                    Email = "user1@example.com",
                    NormalizedEmail = "USER1@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "string@123"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new Autor
                {
                    Id = "2",
                    UserName = "user2@example.com",
                    NormalizedUserName = "USER2@EXAMPLE.COM",
                    Email = "user2@example.com",
                    NormalizedEmail = "USER2@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "string@123"),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new Autor
                {
                    Id = "3",
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "string@123"),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
            );

            // Associa usuários às roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1" }, // user1 como User
                new IdentityUserRole<string> { UserId = "2", RoleId = "1" }, // user2 como User
                new IdentityUserRole<string> { UserId = "3", RoleId = "2" }  // admin como Admin
            );
        }
    }
}

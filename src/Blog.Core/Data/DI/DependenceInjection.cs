using Blog.Core.AutoMapper;
using Blog.Core.Data.Repository;
using Blog.Core.Data.Services;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Core.Data.DI
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependenceInjection(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();

            services
            .AddIdentity<Autor, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(PostProfile));
            ConfigureRepository(services);
            return services;
        }

        public static IServiceCollection AddDependenceInjectionMVC(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();


            services.AddAutoMapper(typeof(PostProfile));
            ConfigureRepository(services);
            return services;
        }

        private static void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IAuthService, AuthService>();

        }

    }
}

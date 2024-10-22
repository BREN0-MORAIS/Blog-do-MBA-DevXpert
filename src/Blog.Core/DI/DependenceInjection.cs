using Blog.Core.Concrete;
using Blog.Core.Data;
using Blog.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Core.DI
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependenceInjection(this IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>();

            //services.AddAutoMapper(typeof(ProductProfile));


            ConfigureRepository(services);

            return services;

        }

        private static void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
       
        }
   
    }
}

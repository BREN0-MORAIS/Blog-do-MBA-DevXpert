using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;


namespace Blog.Core.Data.Services
{
    public class PostsService
    {
        private IPostsRepository _postsRepository;

        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async void CreatePost(Post entity)
        {
            await _postsRepository.Create(entity);
            await _postsRepository.SaveAsync();
        }

    }
}

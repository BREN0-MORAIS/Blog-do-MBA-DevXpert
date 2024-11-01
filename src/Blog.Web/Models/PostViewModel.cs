using Blog.Core.Entities;

namespace Blog.Web.Models
{
    public class PostViewModel
    {

        public List<Post> ListaPosts { get; set; } = new List<Post>();
    }
}

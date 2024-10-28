using Blog.Core.Data;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Data.Repository
{
    public class PostsRepository : Repository<Post>, IPostsRepository
    {
        public PostsRepository(ApplicationDbContext db) : base(db) { }
    }
}

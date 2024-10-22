using Blog.Core.Data;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Concrete
{
    public class CommentsRepository : Repository<Comentario>, ICommentsRepository
    {
        public CommentsRepository(ApplicationDbContext db) : base(db) { }
    }
}

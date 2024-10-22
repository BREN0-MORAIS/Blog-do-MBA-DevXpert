using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(string includes = null, Expression<Func<TEntity, bool>> expression = null);
        Task<TEntity> GetById(int id, string includes = null, Expression<Func<TEntity, bool>> expression = null);
        void Remove(TEntity entity);
        Task SaveAsync();
        void Update(TEntity entity);
        Task<TEntity> Create(TEntity entity);
    }
}

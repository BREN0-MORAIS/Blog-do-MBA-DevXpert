using Blog.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Blog.Core.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private DbContext _dbContext { get; set; }
        private DbSet<TEntity> _dbset { get; set; }

        public Repository(DbContext dbContext)
        {
            try
            {
                _dbContext = dbContext;
                _dbset = _dbContext.Set<TEntity>();
                _dbset.AsTracking();
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                throw;
            }
        }
		public async Task<IEnumerable<TEntity>> GetAll(string includes = null, Expression<Func<TEntity, bool>> expression = null)
		{
			IQueryable<TEntity> query = _dbset;

			if (expression != null)
				query = query.Where(expression);

			if (includes != null)
			{
				foreach (var includeProp in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return await query.ToListAsync();
		}



		public async Task<TEntity> GetById(int id, string includes = null, Expression<Func<TEntity, bool>> expression = null)
		{
			IQueryable<TEntity> query = _dbset;

			if (expression != null)
			{
				query = query.Where(expression);
			}

			if (!string.IsNullOrEmpty(includes))
			{
				foreach (var includeProp in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
		}


		public void Remove(TEntity entity)
        {
            _dbset.Remove(entity);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbset.Update(entity);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _dbset.AddAsync(entity);

            return entity;
        }


    }

}

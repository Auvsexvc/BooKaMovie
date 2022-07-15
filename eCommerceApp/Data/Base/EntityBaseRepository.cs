using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace eCommerceApp.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext _dbContext;

        public EntityBaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = await _dbContext.Set<T>().FirstOrDefaultAsync(a => a.Id == id);
            if (data != null)
            {
                _dbContext.Set<T>().Remove(data);
                await _dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbContext.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id) =>
            await _dbContext.Set<T>().FirstOrDefaultAsync(t => t.Id == id);

        public async Task<T> UpdateAsync(int id, T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
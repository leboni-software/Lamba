using Lamba.Domain.Concrete;
using Lamba.Infrastructure.Data.Contexts;
using Lamba.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Lamba.Repository.Concrete
{
    public class BaseReaderRepository<TEntity, TKey, TContext>(TContext dbContext)
        : BaseRepository<TEntity, TKey, TContext>(dbContext), IReaderRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
        where TContext : BaseReaderDbContext<TContext>
    {
        public virtual async Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(new { id }, cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).AnyAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual IQueryable<TEntity> GetQueryable()
        {
            return _dbSet;
        }
    }
}

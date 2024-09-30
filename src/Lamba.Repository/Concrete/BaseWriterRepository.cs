using Lamba.Domain.Concrete;
using Lamba.Infrastructure.Data.Contexts;
using Lamba.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Lamba.Repository.Concrete
{
    public class BaseWriterRepository<TEntity, TKey, TContext>(TContext dbContext)
        : BaseRepository<TEntity, TKey, TContext>(dbContext), IWriterRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
       where TContext : BaseWriterDbContext<TContext>
    {
        public virtual async Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken)
        {
            return await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public virtual async Task<int> ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken)
        {
            return await _dbSet.ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
        }

        public virtual async Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).ExecuteDeleteAsync(cancellationToken);
        }

        public virtual async ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            return await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual EntityEntry<TEntity> Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual void AddRange(params TEntity[] entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public virtual EntityEntry<TEntity> Update(TEntity entity)
        {
            return _dbSet.Update(entity);
        }

        public virtual void UpdateRange(params TEntity[] entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual EntityEntry<TEntity> Delete(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual void Delete(params TEntity[] entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

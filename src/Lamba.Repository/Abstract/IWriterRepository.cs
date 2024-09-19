using Lamba.Domain.Concrete;
using Lamba.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Lamba.Repository.Abstract
{
    public interface IWriterRepository<TEntity, TKey> : IRepository<TEntity, TKey, BaseWriterDbContext>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken);
        Task<int> ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken);
        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken);
        EntityEntry<TEntity> Add(TEntity entity);
        void AddRange(params TEntity[] entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
        EntityEntry<TEntity> Update(TEntity entity);
        void UpdateRange(params TEntity[] entities);
        EntityEntry<TEntity> Delete(TEntity entity);
        void Delete(params TEntity[] entities);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

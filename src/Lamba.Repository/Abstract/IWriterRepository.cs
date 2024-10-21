using Lamba.Domain.Concrete;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Lamba.Repository.Abstract
{
    public interface IWriterRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
        EntityEntry<TEntity> Attach(TEntity entity);
        ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Add(TEntity entity);
        void AddRange(params TEntity[] entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update(TEntity entity);
        void UpdateRange(params TEntity[] entities);
        EntityEntry<TEntity> Delete(TEntity entity);
        void Delete(params TEntity[] entities);
        Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default);
        Task<int> ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default);
        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

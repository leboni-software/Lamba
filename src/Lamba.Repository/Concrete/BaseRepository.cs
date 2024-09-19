using Lamba.Domain.Concrete;
using Lamba.Infrastructure.Data.Contexts;
using Lamba.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Repository.Concrete
{
    public class BaseRepository<TEntity, TKey, TDbContext> : IRepository<TEntity, TKey, TDbContext>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
        where TDbContext : BaseDbContext<TDbContext>
    {
        protected readonly TDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

    }
}

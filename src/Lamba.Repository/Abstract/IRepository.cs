using Lamba.Domain.Concrete;
using Lamba.Infrastructure.Data.Contexts;

namespace Lamba.Repository.Abstract
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
    }
}

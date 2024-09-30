using Lamba.Domain.Concrete;

namespace Lamba.Repository.Abstract
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : struct
    {
    }
}

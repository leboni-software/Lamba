using Lamba.Domain.Abstract;

namespace Lamba.Domain.Concrete
{
    public class BaseAuditEntity<TKey> : BaseEntity<TKey>, IHasUpdatedAt, IHasDeletedAt
        where TKey : struct
    {
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
    }
}

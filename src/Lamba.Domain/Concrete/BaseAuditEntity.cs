using Lamba.Domain.Abstract;

namespace Lamba.Domain.Concrete
{
    public class BaseAuditEntity<TKey> : BaseEntity<TKey>, IHasUpdatedAt, IHasDeletedAt, IHasUpdatedUserId<Guid?>, IHasDeletedUserId<Guid?>
        where TKey : struct
    {
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual Guid? UpdatedUserId { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual Guid? DeletedUserId { get; set; }
    }
}

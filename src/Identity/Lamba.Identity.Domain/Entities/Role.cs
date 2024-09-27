using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class Role : BaseAuditEntity<Guid>
    {
        public virtual required string Name { get; set; }
        public virtual required bool IsMasterRole { get; set; }
        public virtual required ICollection<UserRole> UserRoles { get; set; }
    }
}

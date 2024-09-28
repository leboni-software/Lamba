using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class UserRole : BaseAuditEntity<Guid>
    {
        public virtual Guid UserId { get; set; }
        public virtual Guid RoleId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}

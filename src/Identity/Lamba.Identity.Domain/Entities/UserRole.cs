using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class UserRole : BaseAuditEntity<Guid>
    {
        public virtual Guid UserId { get; set; }
        public virtual Guid RoleId { get; set; }
        public virtual required User User { get; set; }
        public virtual required Role Role { get; set; }
    }
}

using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class PermissionRole : BaseAuditEntity<Guid>
    {
        public required Guid PermissionId { get; set; }
        public required Guid RoleId { get; set; }
        public virtual Permission Permission { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
    }
}

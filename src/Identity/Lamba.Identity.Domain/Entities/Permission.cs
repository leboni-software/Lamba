using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class Permission : BaseAuditEntity<Guid>
    {
        public required string CommandName { get; set; }
        public virtual ICollection<PermissionRole> PermissionRoles { get; set; } = [];
    }
}

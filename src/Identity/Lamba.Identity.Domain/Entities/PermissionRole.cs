using Lamba.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

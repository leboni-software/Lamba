using Lamba.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Domain.Entities
{
    public class Permission : BaseAuditEntity<Guid>
    {
        public required string CommandName { get; set; }
        public virtual ICollection<PermissionRole> PermissionRoles { get; set; } = [];
    }
}

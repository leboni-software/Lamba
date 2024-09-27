using Lamba.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Domain.Entities
{
    public class Role : BaseAuditEntity<Guid>
    {
        public virtual required string Name { get; set; }
        public virtual required bool IsMasterRole { get; set; }
        public virtual required ICollection<UserRole> UserRoles { get; set; }
    }
}

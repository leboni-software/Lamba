using Lamba.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

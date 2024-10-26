using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class Role : BaseAuditEntity<Guid>
    {
        public virtual string Name { get; private set; } = null!;
        public virtual bool IsMasterRole { get; private set; }
        public virtual bool IsDefaultRole { get; private set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
        public virtual ICollection<PermissionRole> PermissionRoles { get; set; } = [];
        public Role()
        {

        }

        public Role(string name, bool isMasterRole, bool isDefaultRole)
        {
            SetName(name);
            SetMasterRole(isMasterRole);
            SetDefaultRole(isDefaultRole);
        }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetMasterRole(bool isMasterRole)
        {
            IsMasterRole = isMasterRole;
        }
        public void SetDefaultRole(bool isDefaultRole)
        {
            IsDefaultRole = isDefaultRole;
        }
    }
}

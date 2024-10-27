using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class UserRole : BaseAuditEntity<Guid>
    {
        public virtual Guid UserId { get; private set; }
        public virtual Guid RoleId { get; private set; }
        public virtual User User { get; private set; } = null!;
        public virtual Role Role { get; private set; } = null!;
        public UserRole()
        {

        }
        public UserRole(Guid userId, Guid roleId)
        {
            SetUserId(userId);
            SetRoleId(roleId);
        }
        public UserRole(User user, Role role)
        {
            SetUser(user);
            SetRole(role);
        }
        public UserRole(User user, Guid roleId)
        {
            SetUser(user);
            SetRoleId(roleId);
        }
        public UserRole(Guid userId, Role role)
        {
            SetUserId(userId);
            SetRole(role);
        }
        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
        public void SetUser(User user)
        {
            User = user;
        }
        public void SetRoleId(Guid roleId)
        {
            RoleId = roleId;
        }
        public void SetRole(Role role)
        {
            Role = role;
        }
    }
}

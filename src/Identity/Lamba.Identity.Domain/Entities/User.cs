using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class User : BaseAuditEntity<Guid>
    {
        public virtual required string FirstName { get; set; }
        public virtual required string LastName { get; set; }
        public virtual required string Username { get; set; }
        public virtual required string Email { get; set; }
        public virtual required string Password { get; set; }
        public virtual required string PasswordSalt { get; set; }
        public virtual required ICollection<UserRole> UserRoles { get; set; }
    }
}

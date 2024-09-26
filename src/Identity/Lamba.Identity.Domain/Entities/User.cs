using Lamba.Domain.Concrete;

namespace Lamba.Identity.Domain.Entities
{
    public class User : BaseAuditEntity<Guid>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordSalt { get; set; }
    }
}

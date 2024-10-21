using Lamba.Domain.Concrete;
using Lamba.Security.Common;

namespace Lamba.Identity.Domain.Entities
{
    public class User : BaseAuditEntity<Guid>
    {
        public virtual string FirstName { get; private set; } = null!;
        public virtual string LastName { get; private set; } = null!;
        public virtual string Username { get; private set; } = null!;
        public virtual string Email { get; private set; } = null!;
        public virtual string Password { get; private set; } = null!;
        public virtual string PasswordSalt { get; private set; } = null!;
        public virtual ICollection<UserRole> UserRoles { get; set; } = [];

        public User()
        {

        }

        public User(string firstName, string lastName, string username, string email, string password)
        {
            SetFirstName(firstName);
            SetLastName(lastName);
            SetUsername(username);
            SetEmail(email);
            SetPassword(password);
        }

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }
        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }
        public void SetUsername(string username)
        {
            Username = username;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetPassword(string password)
        {
            PasswordSalt = HashHelper.GenerateSalt();
            Password = HashHelper.ComputeHash(password, PasswordSalt);
        }
    }
}

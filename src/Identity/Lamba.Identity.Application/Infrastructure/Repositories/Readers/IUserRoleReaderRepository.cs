using Lamba.Identity.Domain.Entities;
using Lamba.Repository.Abstract;

namespace Lamba.Identity.Application.Infrastructure.Repositories.Readers
{
    public interface IUserRoleReaderRepository : IReaderRepository<UserRole, Guid>
    {
    }
}

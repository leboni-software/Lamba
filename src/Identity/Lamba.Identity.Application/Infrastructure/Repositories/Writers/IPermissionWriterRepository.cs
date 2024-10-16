using Lamba.Identity.Domain.Entities;
using Lamba.Repository.Abstract;

namespace Lamba.Identity.Application.Infrastructure.Repositories.Writers
{
    public interface IPermissionWriterRepository : IWriterRepository<Permission, Guid>
    {
    }
}

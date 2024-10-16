using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Domain.Entities;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Repository.Concrete;

namespace Lamba.Identity.Infrastructure.Data.Repositories.Readers
{
    public class PermissionReaderRepository(IdentityReaderDbContext dbContext) : BaseReaderRepository<Permission, Guid, IdentityReaderDbContext>(dbContext), IPermissionReaderRepository
    {
    }
}

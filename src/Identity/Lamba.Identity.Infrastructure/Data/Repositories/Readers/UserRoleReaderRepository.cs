using Lamba.Identity.Application.Infrastructure.Repositories.Readers;
using Lamba.Identity.Domain.Entities;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Infrastructure.Data.Repositories.Readers
{
    public class UserRoleReaderRepository(IdentityReaderDbContext dbContext) : BaseReaderRepository<UserRole, Guid, IdentityReaderDbContext>(dbContext), IUserRoleReaderRepository
    {
    }
}

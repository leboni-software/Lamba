using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Domain.Entities;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Infrastructure.Data.Repositories.Writers
{
    public class PermissionWriterRepository(IdentityWriterDbContext dbContext) : BaseWriterRepository<Permission, Guid, IdentityWriterDbContext>(dbContext), IPermissionWriterRepository
    {
    }
}

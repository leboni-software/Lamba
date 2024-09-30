using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamba.Identity.Infrastructure.Data.Repositories.UoW
{
    public class IdentityUnitOfWork(IdentityWriterDbContext writerDbContext) : UnitOfWork<IdentityWriterDbContext>(writerDbContext), IIdentityUnitOfWork
    {
    }
}

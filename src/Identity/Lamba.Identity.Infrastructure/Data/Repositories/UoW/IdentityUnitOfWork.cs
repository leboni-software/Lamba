using Lamba.Identity.Application.Infrastructure.Repositories.UoW;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Repository.Concrete;

namespace Lamba.Identity.Infrastructure.Data.Repositories.UoW
{
    public class IdentityUnitOfWork(IdentityWriterDbContext writerDbContext) : UnitOfWork<IdentityWriterDbContext>(writerDbContext), IIdentityUnitOfWork
    {
    }
}

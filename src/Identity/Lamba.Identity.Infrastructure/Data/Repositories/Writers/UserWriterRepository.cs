using Lamba.Identity.Application.Infrastructure.Repositories.Writers;
using Lamba.Identity.Domain.Entities;
using Lamba.Identity.Infrastructure.Data.Contexts;
using Lamba.Repository.Concrete;

namespace Lamba.Identity.Infrastructure.Data.Repositories.Writers
{
    public class UserWriterRepository(IdentityWriterDbContext dbContext) : BaseWriterRepository<User, Guid>(dbContext), IUserWriterRepository
    {
    }
}

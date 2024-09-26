using Lamba.Identity.Application.Infrastructure.Contexts;
using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Infrastructure.Data.Contexts
{
    public class IdentityReaderDbContext(DbContextOptions<BaseReaderDbContext> options) : BaseReaderDbContext(options), IIdentityReaderDbContext
    {
        public virtual DbSet<User> Users { get; set; }
    }
}

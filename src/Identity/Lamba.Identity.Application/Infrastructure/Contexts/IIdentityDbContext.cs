using Lamba.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Application.Infrastructure.Contexts
{
    public interface IIdentityDbContext
    {
        DbSet<User> Users { get; }
    }
}

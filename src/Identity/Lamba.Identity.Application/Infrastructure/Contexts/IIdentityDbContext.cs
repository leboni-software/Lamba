using Lamba.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Application.Infrastructure.Contexts
{
    public interface IIdentityDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Permission> Permissions { get; }
        DbSet<PermissionRole> PermissionRoles { get; }
    }
}

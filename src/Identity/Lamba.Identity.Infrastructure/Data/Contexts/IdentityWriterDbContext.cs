using Lamba.Identity.Application.Infrastructure.Contexts;
using Lamba.Identity.Domain.Entities;
using Lamba.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Identity.Infrastructure.Data.Contexts
{
    public class IdentityWriterDbContext(DbContextOptions<IdentityWriterDbContext> options) : BaseWriterDbContext<IdentityWriterDbContext>(options), IIdentityWriterDbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityWriterDbContext).Assembly);
        }
    }
}

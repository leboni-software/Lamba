using Lamba.Domain.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Infrastructure.Data.Contexts
{
    public class BaseDbContext<T>(DbContextOptions<T> options) : DbContext(options)
        where T : DbContext
    {
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                 .Entries()
                 .Where(x => x.Entity is IHasUpdatedAt or IHasDeletedAt)
                 .Where(x => x.State == EntityState.Modified || x.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified && entry.Entity is IHasUpdatedAt)
                {
                    ((IHasUpdatedAt)entry.Entity).UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Deleted && entry.Entity is IHasDeletedAt)
                {
                    entry.State = EntityState.Modified;
                    ((IHasDeletedAt)entry.Entity).DeletedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext<T>).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

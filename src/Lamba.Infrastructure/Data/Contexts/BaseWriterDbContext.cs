using Lamba.Domain.Abstract;
using Lamba.Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Infrastructure.Data.Contexts
{
    public class BaseWriterDbContext<TContext>(DbContextOptions<TContext> options) : BaseDbContext<TContext>(options)
        where TContext : BaseDbContext<TContext>
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
    }
}

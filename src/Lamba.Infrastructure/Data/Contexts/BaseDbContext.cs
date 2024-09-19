using Microsoft.EntityFrameworkCore;

namespace Lamba.Infrastructure.Data.Contexts
{
    public class BaseDbContext<T>(DbContextOptions<T> options) : DbContext(options)
        where T : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseDbContext<T>).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

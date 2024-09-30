using Microsoft.EntityFrameworkCore;

namespace Lamba.Infrastructure.Data.Contexts
{
    public class BaseReaderDbContext<TContext>(DbContextOptions<TContext> options) : BaseDbContext<TContext>(options)
        where TContext : BaseDbContext<TContext>
    {
    }
}

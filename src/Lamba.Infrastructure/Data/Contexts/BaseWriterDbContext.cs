using Lamba.Domain.Abstract;
using Lamba.Domain.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Lamba.Infrastructure.Data.Contexts
{
    public class BaseWriterDbContext<TContext>(DbContextOptions<TContext> options) : BaseDbContext<TContext>(options)
        where TContext : BaseDbContext<TContext>
    {
    }
}

using Microsoft.EntityFrameworkCore;

namespace Lamba.Infrastructure.Data.Contexts
{
    public class BaseWriterDbContext(DbContextOptions<BaseWriterDbContext> options) : BaseDbContext<BaseWriterDbContext>(options)
    {
    }
}

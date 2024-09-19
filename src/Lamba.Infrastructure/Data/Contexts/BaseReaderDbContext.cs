using Microsoft.EntityFrameworkCore;

namespace Lamba.Infrastructure.Data.Contexts
{
    public class BaseReaderDbContext(DbContextOptions<BaseReaderDbContext> options) : BaseDbContext<BaseReaderDbContext>(options)
    {
    }
}

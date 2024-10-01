using Microsoft.EntityFrameworkCore.Storage;

namespace Lamba.Repository.Abstract
{
    public interface IUnitOfWork
    {
        Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken);
        Task BeginTransactionAsync(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task CommitTransactionAsync(CancellationToken cancellationToken);
        Task RollbackTransactionAsync(CancellationToken cancellationToken);
    }
}

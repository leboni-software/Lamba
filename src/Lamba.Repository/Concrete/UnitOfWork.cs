using Lamba.Infrastructure.Data.Contexts;
using Lamba.Repository.Abstract;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lamba.Repository.Concrete
{
    public class UnitOfWork<TContext> : IUnitOfWork, IDisposable
        where TContext : BaseWriterDbContext<TContext>
    {
        private bool _disposed = false;
        private readonly TContext _writerDbContext;

        public UnitOfWork(TContext writerDbContext)
        {
            _writerDbContext = writerDbContext;
        }

        public async Task ExecuteTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
        {
            await using var transaction = await BeginTransactionAsync(cancellationToken);
            try
            {
                await action();
                await SaveChangesAsync(cancellationToken);
                await CommitTransactionAsync(cancellationToken);
            }
            catch (Exception)
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        public virtual Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return _writerDbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public virtual Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _writerDbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            return _writerDbContext.Database.CommitTransactionAsync(cancellationToken);
        }

        public virtual Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            return _writerDbContext.Database.RollbackTransactionAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _writerDbContext.DisposeAsync();
                }
            }
            _disposed = true;
        }
    }
}

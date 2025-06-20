using System.Data;

namespace Application.Abstractions;

public interface IUnitOfWork
{
    Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
using System.Data;
using Application.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public class UnitOfWork(ShopDbContext context): IUnitOfWork
{
    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
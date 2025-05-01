﻿using Basket.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Basket.Infrastructure.Data
{
    public class EfUnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        private IDbContextTransaction? _currentTransaction;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null) throw new InvalidOperationException("No transaction started.");
            await dbContext.SaveChangesAsync(cancellationToken);
            await _currentTransaction.CommitAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null) return;
            await _currentTransaction.RollbackAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

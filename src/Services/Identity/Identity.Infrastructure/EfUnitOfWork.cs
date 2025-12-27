using BuildingBlocks.Abstractions.Interfaces;
using Identity.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure
{
    public sealed class EfUnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _db;

        public EfUnitOfWork(TDbContext db) => _db = db;

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _db.SaveChangesAsync(ct);

        public async Task ExecuteInTransactionAsync(
            Func<CancellationToken, Task> action,
            CancellationToken ct = default)
        {
            var strategy = _db.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _db.Database.BeginTransactionAsync(ct);

                await action(ct);

                await _db.SaveChangesAsync(ct);
                await tx.CommitAsync(ct);
            });
        }
    }
}

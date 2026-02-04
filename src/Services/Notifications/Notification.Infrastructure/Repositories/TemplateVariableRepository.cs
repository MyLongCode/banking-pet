using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Interfaces.NotificationTemplates;
using Notifications.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Repositories
{
    public sealed class TemplateVariableRepository : ITemplateVariableRepository
    {
        private readonly NotificationsDbContext _db;

        public TemplateVariableRepository(NotificationsDbContext db)
        {
            _db = db;
        }

        public Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult(false);

            var normalized = NormalizeName(name);

            return _db.TemplateVariables
                .AsNoTracking()
                .AnyAsync(x => x.Name.ToLower() == normalized, ct);
        }

        public Task<TemplateVariable?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return _db.TemplateVariables
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public Task<TemplateVariable?> GetByNameAsync(string name, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Task.FromResult<TemplateVariable?>(null);

            var normalized = NormalizeName(name);

            return _db.TemplateVariables
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name.ToLower() == normalized, ct);
        }

        public async Task<IReadOnlyList<TemplateVariable>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.TemplateVariables
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ToListAsync(ct);
        }

        public async Task<Guid> AddAsync(TemplateVariable variable, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(variable);
            variable.Name = NormalizeNamePreserveCase(variable.Name);

            await _db.TemplateVariables.AddAsync(variable, ct);
            await _db.SaveChangesAsync(ct);
            return variable.Id;
        }

        public async Task UpdateAsync(TemplateVariable variable, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(variable);
            _db.TemplateVariables.Update(variable);
            await _db.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(TemplateVariable? variable, CancellationToken ct = default)
        {
            ArgumentNullException.ThrowIfNull(variable);

            _db.TemplateVariables.Remove(variable);
            await _db.SaveChangesAsync(ct);
        }

        private static string NormalizeName(string name)
            => name.Trim().ToLowerInvariant();

        private static string NormalizeNamePreserveCase(string name)
            => name.Trim();

        public Task<bool> IsUsedInActiveVersionsAsync(Guid variableId, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
 }



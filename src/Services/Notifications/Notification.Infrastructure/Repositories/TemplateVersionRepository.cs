using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities.NotificationTemplates;
using Notifications.Domain.Entities.NotificationTemplates.Enums;
using Notifications.Domain.Interfaces.NotificationTemplates;
using Notifications.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Repositories
{
    public class TemplateVersionRepository : ITemplateVersionRepository
    {
        private readonly NotificationsDbContext _context;

        public TemplateVersionRepository(NotificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(TemplateVersion templateVersion)
        {
            if (templateVersion == null)
                throw new ArgumentNullException(nameof(templateVersion));

            await _context.TemplateVersions.AddAsync(templateVersion);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(List<TemplateVersion> versions)
        {
            if (versions == null)
                throw new ArgumentNullException(nameof(versions));

            if (!versions.Any())
                return;

            await _context.TemplateVersions.AddRangeAsync(versions);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TemplateVersion>> GetActiveVersionsByTemplateAsync(Guid templateId)
        {
            return await _context.TemplateVersions
                .Where(tv => tv.TemplateId == templateId && tv.Category == NotificationTemplateVersionCategory.Active)
                .OrderBy(tv => tv.Language)
                .ThenByDescending(tv => tv.Created)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<TemplateVersion>> GetAllByTemplateIdAsync(Guid templateId)
        {
            return await _context.TemplateVersions
                .Where(tv => tv.TemplateId == templateId)
                .OrderBy(tv => tv.Language)
                .ThenByDescending(tv => tv.Created)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TemplateVersion?> GetByTemplateAndLanguageAsync(Guid templateId, string version, string language)
        {
            if (string.IsNullOrWhiteSpace(version))
                throw new ArgumentException("Version cannot be null or empty", nameof(version));

            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentException("Language cannot be null or empty", nameof(language));

            return await _context.TemplateVersions
                .AsNoTracking()
                .FirstOrDefaultAsync(tv =>
                    tv.TemplateId == templateId &&
                    tv.Version == version &&
                    tv.Language == language);
        }
    }
}
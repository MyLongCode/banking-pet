
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
    public class TemplateRepository : ITemplateRepository
    {
        private readonly NotificationsDbContext _context;

        public TemplateRepository(NotificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<NotificationTemplate>> GetAllAsync()
        {
            return await _context.NotificationTemplates.ToListAsync();
        }
        public async Task AddAsync(NotificationTemplate notificationTemplate)
        {
            await _context.NotificationTemplates.AddAsync(notificationTemplate);
            await _context.SaveChangesAsync();
        }

        public async Task<NotificationTemplate> FindByCodeAsync(string code)
        {
            return await _context.NotificationTemplates
                .FirstOrDefaultAsync(x => x.Code == code);
        }

        public Task UpdateAsync(NotificationTemplate notificationTemplate)
        {
            throw new NotImplementedException();
        }
    }
}

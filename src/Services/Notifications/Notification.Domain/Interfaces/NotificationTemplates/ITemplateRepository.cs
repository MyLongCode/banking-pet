using Notifications.Domain.Entities.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Interfaces.NotificationTemplates
{
    public interface ITemplateRepository
    {
        Task<NotificationTemplate> FindByCodeAsync(string code);
        Task AddAsync(NotificationTemplate notificationTemplate);
        Task UpdateAsync(NotificationTemplate notificationTemplate);
        Task<IEnumerable<NotificationTemplate>> GetAllAsync();
    }
}

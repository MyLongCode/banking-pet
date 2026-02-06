using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.ValueObjects.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> GetByRecipientAsync(string recipient, CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> GetByTypeAsync(NotificationType type, CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> GetPendingAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Notification>> GetByDateRangeAsync(DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<int> GetCountByStatusAsync(bool isSent, CancellationToken cancellationToken = default);
        Task AddAsync(Notification notification, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<Notification> notifications, CancellationToken cancellationToken = default);
        Task UpdateAsync(Notification notification, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);

        //Аналитика
        Task<Dictionary<DateOnly, NotificationStatusesCount>> GetDateAnalytics(CancellationToken cancellation = default);
    }
}

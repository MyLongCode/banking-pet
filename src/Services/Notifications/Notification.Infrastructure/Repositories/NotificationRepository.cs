using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Interfaces;
using Notifications.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Repositories
{
    internal class NotificationRepository : INotificationRepository
    {
        private readonly NotificationsDbContext _context;

        public NotificationRepository(NotificationsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Notification>> GetByRecipientAsync(
            string recipient, CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .Where(n => n.Recipient == recipient)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> GetByTypeAsync(
            NotificationType type, CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .Where(n => EF.Property<NotificationType>(n, "NotificationType") == type)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> GetPendingAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .Where(n => !n.IsSent)
                .OrderBy(n => n.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Notification>> GetByDateRangeAsync(
            DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .Where(n => n.CreatedAt >= startDate && n.CreatedAt <= endDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> GetCountByStatusAsync(
            bool isSent, CancellationToken cancellationToken = default)
        {
            return await _context.Notifications
                .CountAsync(n => n.IsSent == isSent, cancellationToken);
        }

        public async Task AddAsync(
            Notification notification, CancellationToken cancellationToken = default)
        {
            await _context.Notifications.AddAsync(notification, cancellationToken);
        }

        public async Task AddRangeAsync(
            IEnumerable<Notification> notifications, CancellationToken cancellationToken = default)
        {
            await _context.Notifications.AddRangeAsync(notifications, cancellationToken);
        }

        public async Task UpdateAsync(
            Notification notification, CancellationToken cancellationToken = default)
        {
            _context.Notifications.Update(notification);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var notification = await GetByIdAsync(id, cancellationToken);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
            }
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}

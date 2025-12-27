using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Notifications.Domain.Entities;

namespace Notifications.Application.Abstractions
{
    public interface IEmailMessageRepository
    {
        Task AddAsync(EmailMessage message, CancellationToken ct);
        Task<IReadOnlyList<EmailMessage>> GetPendingBatchAsync(int take, DateTimeOffset now, CancellationToken ct);
        Task<EmailMessage?> GetByIdAsync(Guid id, CancellationToken ct);
    }
}

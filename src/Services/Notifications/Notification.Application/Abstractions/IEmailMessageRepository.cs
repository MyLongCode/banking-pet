using Notifications.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Abstractions
{
    public interface IEmailMessageRepository
    {
        Task AddAsync(EmailNotification emailMessage, CancellationToken ct);
        Task<EmailNotification?> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<EmailNotification>> GetList(Expression<Func<EmailNotification, bool>> predicate);
    }
}

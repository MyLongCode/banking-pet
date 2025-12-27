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
        Task AddAsync(EmailMessage emailMessage, CancellationToken ct);
        Task<EmailMessage?> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<EmailMessage>> GetList(Expression<Func<EmailMessage, bool>> predicate);
    }
}

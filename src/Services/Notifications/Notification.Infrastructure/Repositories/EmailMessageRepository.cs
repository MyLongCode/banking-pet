using Notifications.Domain.Entities;
using Notifications.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Repositories
{
    public class EmailMessageRepository
    {
        private readonly NotificationsDbContext _db;

        public EmailMessageRepository(NotificationsDbContext db) => _db = db;

        public async Task AddAsync(EmailMessage emailMessage, CancellationToken ct)
        {
            await _db.EmailMessages.AddAsync(emailMessage, ct);
            await _db.SaveChangesAsync(ct);
        }

        public async Task<EmailMessage?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _db.EmailMessages.FindAsync(id, ct);
        }

        public async Task<IEnumerable<EmailMessage>> GetList(Expression<Func<EmailMessage, bool>> predicate)
        {
            return _db.EmailMessages.Where(predicate);
        }
    }
}

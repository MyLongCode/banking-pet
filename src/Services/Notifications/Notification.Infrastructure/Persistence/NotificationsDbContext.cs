using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Persistence
{
    public sealed class NotificationsDbContext : DbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) : base(options) { }

        public DbSet<EmailMessage> EmailMessages => Set<EmailMessage>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

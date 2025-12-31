using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;

namespace Notifications.Infrastructure.Persistence
{
    public class NotificationsDbContext : DbContext
    {
        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);

            modelBuilder.Entity<Notification>()
                .HasDiscriminator<NotificationType>("NotificationType")
                .HasValue<EmailNotification>(NotificationType.Email);

            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.Recipient)
                .HasDatabaseName("IX_Notifications_Recipient");

            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.CreatedAt)
                .HasDatabaseName("IX_Notifications_CreatedAt");

            modelBuilder.Entity<Notification>()
                .HasIndex(n => n.IsSent)
                .HasDatabaseName("IX_Notifications_IsSent");

            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.IsSent, n.CreatedAt })
                .HasDatabaseName("IX_Notifications_IsSent_CreatedAt");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Notification &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var notification = (Notification)entityEntry.Entity;

                if (entityEntry.State == EntityState.Added)
                {
                    notification.SetCreatedAt(DateTime.UtcNow);
                }

                notification.SetUpdatedAt(DateTime.UtcNow);
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
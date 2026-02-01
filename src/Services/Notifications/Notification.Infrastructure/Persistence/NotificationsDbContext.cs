using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;
using Notifications.Domain.Entities.NotificationTemplates;
using System.IO;

namespace Notifications.Infrastructure.Persistence
{
    public class NotificationsDbContext : DbContext
    {
        public NotificationsDbContext()
        {
        }

        public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<EmailNotification> EmailNotifications { get; set; }

        #region Шаблоны уведомлений

        public DbSet<NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<TemplateVersion> TemplateVersions{ get; set; }
        public DbSet<TemplateVariable> TemplateVariables{ get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile("appsettings.Development.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = configuration.GetConnectionString("NotificationsDb");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationsDbContext).Assembly);

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
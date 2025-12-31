using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Entities
{
    public abstract class Notification
    {
        public Guid Id { get; protected set; }
        public string Recipient { get; protected set; }
        public string Title { get; protected set; }
        public string Message { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public bool IsSent { get; protected set; }

        public abstract NotificationType Type { get; }

        protected Notification(string recipient, string title, string message)
        {
            Id = Guid.NewGuid();
            Recipient = recipient;
            Title = title;
            Message = message;
            CreatedAt = DateTime.UtcNow;
            IsSent = false;
        }

        public abstract void Validate();

        public void SetCreatedAt(DateTime createdAt) { CreatedAt = createdAt; }
        public void SetUpdatedAt(DateTime updatedAt) { UpdatedAt = updatedAt; }
    }
}

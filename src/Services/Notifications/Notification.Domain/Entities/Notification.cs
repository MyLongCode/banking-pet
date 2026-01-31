using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Entities
{
    [Table("Notifications", Schema = "notifications")]
    public class Notification  
    {
        public Guid Id { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsSent { get; set; }

        public NotificationType Type { get; set; }

        protected Notification() { }

        protected Notification(string recipient, string title, string message)
        {
            Id = Guid.NewGuid();
            Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            IsSent = false;
        }

        public virtual void Validate()
        {
            if (string.IsNullOrWhiteSpace(Recipient))
                throw new ArgumentException("Recipient is required");

            if (string.IsNullOrWhiteSpace(Title))
                throw new ArgumentException("Title is required");

            if (string.IsNullOrWhiteSpace(Message))
                throw new ArgumentException("Message is required");
        }

        public void SetCreatedAt(DateTime createdAt) { CreatedAt = createdAt; }
        public void SetUpdatedAt(DateTime updatedAt) { UpdatedAt = updatedAt; }
        public void MarkAsSent() { IsSent = true; UpdatedAt = DateTime.UtcNow; }
    }
}

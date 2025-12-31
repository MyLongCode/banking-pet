using BuildingBlocks.Abstractions.Models;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Entities
{
    [Table("EmailMessages", Schema = "notifications")]
    public class EmailNotification : Notification
    {
        public string? Subject { get; private set; }
        public string? HtmlContent { get; private set; }
        public List<string>? Attachments { get; private set; }

        public override NotificationType Type => NotificationType.Email;

        public EmailNotification(
             string recipient,
             string title,
             string message,
             Dictionary<string, object>? metadata = null)
             : base(recipient, title, message)
        {
            if (metadata != null)
            {
                Subject = metadata.TryGetValue("Subject", out var subject) ? subject.ToString() : null;
                HtmlContent = metadata.TryGetValue("HtmlContent", out var html) ? html.ToString() : null;
                Attachments = metadata.TryGetValue("Attachments", out var attachments)
                    ? (attachments as List<string>) : null;
            }
        }
        public override void Validate()
        {
            if (!Recipient.Contains('@')) throw new ArgumentException("This address not valid");
            if (string.IsNullOrEmpty(Title)) throw new ArgumentException("Title is required");
        }
    }
}

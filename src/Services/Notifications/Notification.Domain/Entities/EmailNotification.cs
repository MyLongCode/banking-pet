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
        public List<string>? AttachmentPaths { get; private set; }

        public EmailNotification(
            string recipient,
            string title,
            string message,
            string? subject = null,
            string? htmlContent = null,
            List<string>? attachmentPaths = null)
            : base(recipient, title, message)
        {
            Subject = subject ?? title;
            HtmlContent = htmlContent;
            AttachmentPaths = attachmentPaths;
        }
        public override void Validate()
        {
            base.Validate();

            if (!Recipient.Contains("@"))
                throw new ArgumentException("Invalid email address");
        }
    }
}

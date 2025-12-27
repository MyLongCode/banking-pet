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
    public class EmailMessage : BaseAuditableEntity
    {
        public string? UserId { get; set; }
        public string To { get; set; } = default!;
        public string Subject { get; set; } = default!;
        public string Body { get; set; } = default!;

        public EmailStatus Status { get; set; } = EmailStatus.Pending;
        public int Attempts { get; set; }
        public DateTimeOffset? SentAt { get; set; }

        public DateTimeOffset NextAttemptAt { get; set; }
        public string? LastError { get; set; }

        public string? CorrelationId { get; set; }
    }
}

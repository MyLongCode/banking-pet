using Notifications.Domain.Entities;
using Notifications.Domain.Entities.Enums;

namespace Notifications.Api.Models.Email.Responses
{
    public sealed record GetEmailResponse
    {
        public Guid Id { get; set; }
        public string? Recipient { get; set; } 
        public string? Title { get; set; } 
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsSent { get; set; }
        public string? Subject { get; private set; }
        public string? HtmlContent { get; private set; }
        public List<string>? AttachmentPaths { get; private set; }
        public Guid? NotificationId { get; set; }
    }
}

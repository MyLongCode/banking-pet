using Notifications.Domain.Entities.Enums;

namespace Notifications.Api.Models.Email.Requests
{
    public sealed record CreateBatchRequest
    {
        public List<BatchNotificationItemRequest> Items { get; init; } = new();
        public bool UseTransaction { get; init; } = true;
        public int? MaxDegreeOfParallelism { get; init; }
    }

    public sealed record BatchNotificationItemRequest
    {
        public NotificationType Type { get; init; }
        public string Recipient { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public Dictionary<string, string>? Metadata { get; init; }
    }
}

using MediatR;
using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands
{
    public sealed record CreateBatchNotificationsCommand : IRequest<BatchNotificationResult>
    {
        public List<CreateNotificationItem> Items { get; init; } = new();
        public bool UseTransaction { get; init; } = true;
        public int? MaxDegreeOfParallelism { get; init; }
    }

    public sealed record CreateNotificationItem
    {
        public NotificationType Type { get; init; }
        public string Recipient { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Message { get; init; } = string.Empty;
        public Dictionary<string, string>? Metadata { get; init; }
    }

    public sealed record BatchNotificationResult
    {
        public Guid BatchId { get; init; }
        public int TotalCount { get; init; }
        public int SuccessCount { get; init; }
        public int FailedCount { get; init; }
        public List<BatchNotificationItemResult> Items { get; init; } = new();
        public TimeSpan TotalDuration { get; init; }
    }

    public sealed record BatchNotificationItemResult
    {
        public Guid? NotificationId { get; init; }
        public NotificationType Type { get; init; }
        public string Recipient { get; init; } = string.Empty;
        public bool IsSuccess { get; init; }
        public string? ErrorMessage { get; init; }
        public TimeSpan Duration { get; init; }
    }
}

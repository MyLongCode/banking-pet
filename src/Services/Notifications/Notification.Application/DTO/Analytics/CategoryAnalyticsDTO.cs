namespace Notifications.Application.DTO.Analytics
{
    public class CategoryAnalyticsDTO
    {
        public string Category { get; set; }
        public int NotificationsSent { get; set; }
        public int NotificationsFailed { get; set; }
        public int NotificationsDelivered { get; set; }
    }
}

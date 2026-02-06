namespace Notifications.Application.DTO.Analytics
{
    public class DailyAnalyticsDTO
    {
        public DateOnly Date { get; set; }
        public int NotificationsSent { get; set; }
        public int NotificationsFailed { get; set; }
        public int NotificationsDelivered { get; set; }
    }
}

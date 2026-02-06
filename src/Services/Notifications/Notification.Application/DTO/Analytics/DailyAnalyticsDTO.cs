namespace Notifications.Application.DTO.Analytics
{
    public class DailyAnalyticsDTO
    {
        public DateTime Date { get; set; }
        public int NotificationsSent { get; set; }
        public int NotificationsFailed { get; set; }
        public int NotificationsDelivered { get; set; }
    }
}

namespace Notifications.Application.DTO.Analytics
{
    public class ChannelAnalyticsDTO
    {
        public string Channel { get; set; }
        public int NotificationsSent { get; set; }
        public int NotificationsFailed { get; set; }
        public int NotificationsDelivered { get; set; }
    }
}

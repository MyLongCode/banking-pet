namespace Notifications.Api.Models.Analytics.Responses
{
    public class TopUsersResponse
    {
        public string UserName { get; set; }
        public int NotificationsSent { get; set; }
        public int NotificationsFailed { get; set; }
        public int NotificationsDelivered { get; set; }
    }
}

namespace Notifications.Application.DTO.Analytics
{
    public class TopUsersDTO
    {
        public string UserName { get; set; }
        public int NotificationsSent { get; set; }
        public int NotificationsFailed { get; set; }
        public int NotificationsDelivered { get; set; }
    }
}

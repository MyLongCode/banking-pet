namespace Notifications.Api.Models.Email.Requests
{
    public sealed record SendEmailRequest(
        string To,
        string Subject,
        string Body,
        string? UserId
    );
}

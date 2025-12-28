namespace Notifications.Api.Models.Email.Responses
{
    public sealed record SendEmailResponse(
        int Id,
        string CorrelationId
    );
}

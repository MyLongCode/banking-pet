namespace Notifications.Api.Models.Email.Responses
{
    public sealed record SendEmailResponse(
        Guid Id,
        string CorrelationId
    );
}

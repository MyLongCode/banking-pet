namespace Notifications.Api.Viewmodels.Email.Responses
{
    public sealed record SendEmailResponse(
        Guid Id,
        string CorrelationId
    );
}

namespace Notifications.Api.Models.Email.Requests
{
    public sealed record SendEmailWithTemplateRequest(
        string To,
        string TemplateCode,
        string Version,
        string Language,
        Dictionary<string, object> Variables
        );
}

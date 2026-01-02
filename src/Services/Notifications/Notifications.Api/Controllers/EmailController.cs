using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Api.Models.Email.Requests;
using Notifications.Api.Models.Email.Responses;
using Notifications.Application.Handlers.Commands;
using Notifications.Domain.Entities.Enums;

namespace Notifications.Api.Controllers;

[ApiController]
[Route("api/v1/emails")]
public sealed class EmailsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("send")]
    [ProducesResponseType(typeof(SendEmailResponse), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Send([FromBody] SendEmailRequest request, CancellationToken ct)
    {
        var correlationId = GetOrCreateCorrelationId();

        var id = await _mediator.Send(new CreateNotificationCommand(
            Type: NotificationType.Email,
            Recipient: request.To,
            Title: request.Subject,
            Message: request.Body,
            null
        ), ct);

        return Accepted($"/api/v1/emails/{id}", new SendEmailResponse(id, correlationId));
    }

    private string GetOrCreateCorrelationId()
    {
        if (Request.Headers.TryGetValue("X-Correlation-Id", out var cid) &&
            !string.IsNullOrWhiteSpace(cid))
        {
            return cid.ToString();
        }

        return Guid.NewGuid().ToString("N");
    }
}
using AutoMapper;
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
    private readonly IMapper _mapper;

    public EmailsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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

    [HttpPost("send/batch")]
    public async Task<ActionResult<BatchNotificationResult>> CreateBatch(
        [FromBody] CreateBatchRequest request,
        CancellationToken ct)
    {
        var command = new CreateBatchNotificationsCommand
        {
            Items = request.Items.Select(i => new CreateNotificationItem
            {
                Type = i.Type,
                Recipient = i.Recipient,
                Title = i.Title,
                Message = i.Message,
                Metadata = i.Metadata
            }).ToList(),
            UseTransaction = request.UseTransaction,
            MaxDegreeOfParallelism = request.MaxDegreeOfParallelism
        };

        var result = await _mediator.Send(command, ct);

        if (result.FailedCount > 0)
            return Accepted(result); // 202 Accepted - частичный успех

        return Ok(result);
    }

    [HttpGet("")]
    [ProducesResponseType(typeof(GetEmailResponse), StatusCodes.Status202Accepted)]
    public async Task<IActionResult> Get(DateTime from, DateTime to, CancellationToken ct)
    {
        var correlationId = GetOrCreateCorrelationId();

        var notifications = await _mediator.Send(new GetNotificationCommand(
            Type: NotificationType.Email,
            From: from,
            To: to
        ), ct);

        return Ok(_mapper.Map<IEnumerable<GetEmailResponse>>(notifications));
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
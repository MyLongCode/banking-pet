using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Api.Models.Analytics.Responses;
using Notifications.Application.Handlers.Commands.Analytics;

namespace Notifications.Api.Controllers.Admin
{
    [ApiController]
    [Route("/api/admin/analytics")]
    public sealed class NotificationAnalyticsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NotificationAnalyticsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("daily")]
        [ProducesResponseType(typeof(DailyAnalyticsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDailyAnalytics(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetDailyAnalyticsQuery(), ct);
            return Ok(result);
        }

        [HttpGet("channels")]
        [ProducesResponseType(typeof(ChannelAnalyticsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetChannelAnalytics(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetChannelAnalyticsQuery(), ct);
            return Ok(result);
        }

        [HttpGet("categories")]
        [ProducesResponseType(typeof(CategoryAnalyticsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategoryAnalytics(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCategoryAnalyticsQuery(), ct);
            return Ok(result);
        }

        [HttpGet("users/top")]
        [ProducesResponseType(typeof(TopUsersResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopUsersAnalytics(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetTopUsersAnalyticsQuery(), ct);
            return Ok(result);
        }
    }
}

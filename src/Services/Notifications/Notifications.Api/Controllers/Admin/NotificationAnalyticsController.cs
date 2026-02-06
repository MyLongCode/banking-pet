using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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


    }
}

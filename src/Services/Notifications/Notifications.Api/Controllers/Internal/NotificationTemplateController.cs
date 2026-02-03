using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Notifications.Api.Controllers.Internal
{
    [ApiController]
    [Route("api/internal/templates")]
    public sealed class NotificationTemplateController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NotificationTemplateController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("")]
        public IActionResult GetTemplates()
        {

        }

        [HttpGet("{code}")]
        public IActionResult GetTemplate(string code)
        {

        }

    }
}

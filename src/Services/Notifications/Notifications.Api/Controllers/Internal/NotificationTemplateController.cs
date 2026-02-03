using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Api.Models.Email.Responses;
using Notifications.Application.Handlers.Commands;
using Notifications.Application.Handlers.Commands.Templates;
using Notifications.Domain.Entities.Enums;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var templates = await _mediator.Send(new GetAllTemplatesQuery(), ct);

            return Ok(templates);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetTemplateVersions(string code, CancellationToken ct)
        {
            var templateVerison = await _mediator.Send(new GetAllTemplateVersionsQuery(
                code
                ), ct);
            return Ok(templateVerison);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateTemplate([FromBody] CreateTemplateCommand command, CancellationToken ct)
        {
            var templateId = await _mediator.Send(command, ct);
            return CreatedAtAction(nameof(GetTemplateVersions), new { code = command.Code }, templateId);
        }

        [HttpPost("version")]
        public async Task<IActionResult> CreateTemplateVersion([FromBody] CreateTemplateVersionCommand command, CancellationToken ct)
        {
            var versionId = await _mediator.Send(command, ct);
            return CreatedAtAction("CreateTemplateVersion", versionId);
        }

    }
}

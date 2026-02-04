using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notifications.Application.Handlers.Commands.NotificationsTemplates.TemplateVariables;
using Notifications.Application.Handlers.Commands.Templates.TemplateVariables;

namespace Notifications.Api.Controllers.Internal
{
    [ApiController]
    [Route("api/internal/template-variables")]
    public sealed class NotificationTemplateVariableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public NotificationTemplateVariableController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить список всех переменных шаблонов.
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var variables = await _mediator.Send(new GetTemplateVariablesQuery(), ct);
            return Ok(variables);
        }

        /// <summary>
        /// Получить переменную по имени
        /// </summary>
        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName(string name, CancellationToken ct)
        {
            var variable = await _mediator.Send(new GetTemplateVariableByNameQuery(name), ct);
            return Ok(variable);
        }

        /// <summary>
        /// Создать новую переменную
        /// </summary>
        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateTemplateVariableCommand command, CancellationToken ct)
        {
            var id = await _mediator.Send(command, ct);
            return Ok(id);
        }


        /// <summary>
        /// Удалить переменную.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _mediator.Send(new DeleteTemplateVariableCommand(id), ct);
            return NoContent();
        }
    }
}

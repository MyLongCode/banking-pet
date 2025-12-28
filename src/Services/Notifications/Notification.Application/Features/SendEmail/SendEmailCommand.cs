using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Features.SendEmail
{
    public sealed record SendEmailCommand(
        string To,
        string Subject,
        string Body,
        string? UserId,
        string? CorrelationId
    ) : IRequest<int>;
}

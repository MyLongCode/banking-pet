using MediatR;
using Notifications.Application.DTO.Analytics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Handlers.Commands.Analytics
{
    public record GetTopUsersAnalyticsQuery() : IRequest<TopUsersDTO>;
}

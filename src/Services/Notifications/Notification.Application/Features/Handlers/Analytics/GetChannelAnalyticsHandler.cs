using MediatR;
using Notifications.Application.DTO.Analytics;
using Notifications.Application.Handlers.Commands.Analytics;
using Notifications.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Features.Handlers.Analytics
{
    public class GetChannelAnalyticsHandler : IRequestHandler<GetChannelAnalyticsQuery, IEnumerable<ChannelAnalyticsDTO>>
    {
        private readonly INotificationRepository _repo;
        public GetChannelAnalyticsHandler(INotificationRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<ChannelAnalyticsDTO>> Handle(GetChannelAnalyticsQuery request, CancellationToken cancellationToken)
        {
            return (await _repo.GetChannelAnalytics(cancellationToken))
                .Select(x => new ChannelAnalyticsDTO
                {
                    Channel = x.Key.ToString(),
                    NotificationsDelivered = x.Value.NotificationsDelivered,
                    NotificationsFailed = x.Value.NotificationsFailed,
                    NotificationsSent = x.Value.NotificationsSent
                });
        }
    }
}

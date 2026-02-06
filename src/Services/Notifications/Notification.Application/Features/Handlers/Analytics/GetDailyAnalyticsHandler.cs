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
    public class GetDailyAnalyticsHandler : IRequestHandler<GetDailyAnalyticsQuery, IEnumerable<DailyAnalyticsDTO>>
    {
        private readonly INotificationRepository _repo;
        public GetDailyAnalyticsHandler(INotificationRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<DailyAnalyticsDTO>> Handle(
            GetDailyAnalyticsQuery request, CancellationToken cancellationToken)
            => (await _repo.GetDateAnalytics(cancellationToken))
                .Select(x => new DailyAnalyticsDTO { 
                    Date = x.Key, 
                    NotificationsDelivered = x.Value.NotificationsDelivered,
                    NotificationsFailed = x.Value.NotificationsFailed,
                    NotificationsSent = x.Value.NotificationsSent
                });
    }
}

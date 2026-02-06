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
    public class GetCategoryAnalyticsHandler : IRequestHandler<GetCategoryAnalyticsQuery, IEnumerable<CategoryAnalyticsDTO>>
    {
        private readonly INotificationRepository _repo;
        public GetCategoryAnalyticsHandler(INotificationRepository repo)
        {
            _repo = repo;
        }
        public Task<IEnumerable<CategoryAnalyticsDTO>> Handle(GetCategoryAnalyticsQuery request, CancellationToken cancellationToken)
        {
            //Доделать категории
            throw new Exception();
        }
    }
}

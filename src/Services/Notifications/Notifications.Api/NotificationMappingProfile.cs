using AutoMapper;
using Notifications.Api.Models.Analytics.Responses;
using Notifications.Api.Models.Email.Responses;
using Notifications.Application.DTO.Analytics;
using Notifications.Application.Handlers.Commands;
using Notifications.Domain.Entities;

namespace Notifications.Api
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, GetEmailResponse>();

            #region Аналитика
            CreateMap<CategoryAnalyticsDTO, CategoryAnalyticsResponse>();
            CreateMap<ChannelAnalyticsDTO, ChannelAnalyticsResponse>();
            CreateMap<DailyAnalyticsDTO, DailyAnalyticsResponse>();
            CreateMap<TopUsersDTO, TopUsersResponse>();
            #endregion
        }
    }
}
using AutoMapper;
using Notifications.Api.Models.Email.Responses;
using Notifications.Application.Handlers.Commands;
using Notifications.Domain.Entities;

namespace Notifications.Api
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, GetEmailResponse>();
        }
    }
}
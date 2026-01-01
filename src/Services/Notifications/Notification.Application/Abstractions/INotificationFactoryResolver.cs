using Notifications.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Application.Abstractions
{
    public interface INotificationFactoryResolver
    {
        INotificationFactory Resolve(NotificationType type);
    }
}

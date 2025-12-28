using BuildingBlocks.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Services
{
    public sealed class SystemClock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.UtcNow;
    }
}

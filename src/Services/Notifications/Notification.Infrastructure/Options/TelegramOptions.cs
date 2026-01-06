using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Options
{
    public sealed class TelegramOptions
    {
        public string BotToken { get; init; } = default!;
        public string ApiBaseUrl { get; init; } = "https://api.telegram.org";
        public string? DefaultChatId { get; init; }
    }
}

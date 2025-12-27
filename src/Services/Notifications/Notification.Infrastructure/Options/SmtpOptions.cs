using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Infrastructure.Options
{
    public sealed class SmtpOptions
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; } = 587;
        public bool UseSsl { get; set; } = true;

        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string From { get; set; } = default!;
    }
}

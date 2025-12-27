using Identity.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class EmailSenderService : IEmailSender
    {
        public Task SendAsync(string to, string subject, string body, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}

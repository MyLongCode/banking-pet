using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.ValueObjects
{
    internal class RenderedTemplate
    {
        public string Content { get; private set; }
        public RenderedTemplate(string content) => Content = content;
    }
}

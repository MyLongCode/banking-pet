using BuildingBlocks.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notifications.Domain.Entities.NotificationTemplates
{
    public class TemplateVariables : BaseEntity
    {
        public string Name { get; set; } // Amount, UserName
        public string DisplayName { get; set; } //Сумма перевода, имя пользователя
        public JsonElement ValidationRules { get; set; } // например {"min": 0, "max": 1000000}
        public string Description { get; set; }
        

    }
}

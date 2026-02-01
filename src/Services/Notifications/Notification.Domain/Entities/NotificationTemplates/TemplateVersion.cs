using BuildingBlocks.Abstractions.Models;
using Notifications.Domain.Entities.NotificationTemplates.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notifications.Domain.Entities.NotificationTemplates
{
    [Table("TemplateVersion", Schema = "notifications")]
    public class TemplateVersion : BaseAuditableEntity
    {
        public Guid TemplateId { get; set; }
        public NotificationTemplate Template { get; set; }
        public string Version { get; set; }
        public string Languate { get; set; }
        public NotificationTemplateVersionCategory Category { get; set; } = NotificationTemplateVersionCategory.Draft;
        public JsonElement Content { get; set; }
        public void Publish() => this.Category = NotificationTemplateVersionCategory.Active;
        public void Deprecate() => this.Category = NotificationTemplateVersionCategory.Deprecated;
        public void Archive() => this.Category = NotificationTemplateVersionCategory.Archived;
        public bool ValidateVariables(List<object> inputVariables)
        {
            //ДОПИСАТЬ МЕТОД
            return true;
        }
    }
}

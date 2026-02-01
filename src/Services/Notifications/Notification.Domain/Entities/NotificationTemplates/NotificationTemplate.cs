using BuildingBlocks.Abstractions.Models;
using Notifications.Domain.Entities.NotificationTemplates.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Entities.NotificationTemplates
{
    [Table("NotificationTemplates", Schema = "notifications")]
    public class NotificationTemplate : BaseAuditableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public NotificationTemplateCategory Category { get; set; }
        public string DefaultLanguage { get; set; }
        public bool IsActive { get; set; }

        public void Activate() => this.IsActive = true;
        public void Deactivate() => this.IsActive = false;
    }
}

using Notifications.Domain.Entities.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Interfaces.NotificationTemplates
{
    public interface ITemplateVersionRepository
    {
        Task<TemplateVersion> GetByTemplateAndLanguageAsync(Guid templateId, string version, string language);
        Task<IEnumerable<TemplateVersion>> GetActiveVersionsByTemplateAsync(Guid templateId);
        Task AddRangeAsync(List<TemplateVersion> versions);
        Task AddAsync(TemplateVersion templateVersion);

    }
}

using Notifications.Domain.Entities.NotificationTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Domain.Interfaces.NotificationTemplates
{
    public interface ITemplateVariableRepository
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);

        Task<TemplateVariable?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<TemplateVariable?> GetByNameAsync(string name, CancellationToken ct = default);
        Task<IReadOnlyList<TemplateVariable>> GetAllAsync(CancellationToken ct = default);

        Task<Guid> AddAsync(TemplateVariable variable, CancellationToken ct = default);
        Task UpdateAsync(TemplateVariable variable, CancellationToken ct = default);
        Task DeleteAsync(TemplateVariable? variable, CancellationToken ct = default);
        Task<bool> IsUsedInActiveVersionsAsync(Guid variableId, CancellationToken ct = default);
    }
}

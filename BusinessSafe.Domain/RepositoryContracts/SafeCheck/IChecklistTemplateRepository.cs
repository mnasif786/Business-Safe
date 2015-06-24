using System;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IChecklistTemplateRepository : IRepository<ChecklistTemplate, Guid>
    {
        bool DoesChecklistTemplateExistWithTheSameName(string name, Guid templateId);
    }
}

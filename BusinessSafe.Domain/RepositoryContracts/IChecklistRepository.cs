using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IChecklistRepository : IRepository<Checklist, long>
    {
        IEnumerable<Checklist> GetAllUndeleted();
        IEnumerable<Checklist> GetByRiskAssessmentType(ChecklistRiskAssessmentType checklistRiskAssessmentType);
        Checklist GetFireRiskAssessmentChecklist();
    }
}
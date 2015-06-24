using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.Contracts.Checklists
{
    public interface IChecklistService
    {
        IEnumerable<ChecklistDto> GetAllUndeleted();
        IEnumerable<ChecklistDto> GetByRiskAssessmentType(ChecklistRiskAssessmentType checklistRiskAssessmentType);
    }
}

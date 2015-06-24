using System.Collections.Generic;
using BusinessSafe.Application.Contracts.Checklists;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;

namespace BusinessSafe.Application.Implementations.Checklists
{
    public class ChecklistService : IChecklistService
    {
        private readonly IChecklistRepository _checklistRepository;
        
        public ChecklistService(IChecklistRepository checklistRepository)
        {
            _checklistRepository = checklistRepository;
        }

        public IEnumerable<ChecklistDto> GetAllUndeleted()
        {
            var checklist = _checklistRepository.GetAllUndeleted();
            return new ChecklistDtoMapper().Map(checklist);
        }

        public IEnumerable<ChecklistDto> GetByRiskAssessmentType(ChecklistRiskAssessmentType checklistRiskAssessmentType)
        {
            var checklist = _checklistRepository.GetByRiskAssessmentType(checklistRiskAssessmentType);
            return new ChecklistDtoMapper().Map(checklist);
        }
    }
}

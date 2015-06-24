using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class ChecklistDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<SectionDto> Sections { get; set; }
        public ChecklistRiskAssessmentType ChecklistRiskAssessmentType { get; set; }
    }
}

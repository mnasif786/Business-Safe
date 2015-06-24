using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class RiskAssessmentDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Reference { get;  set; }
        public DateTime? AssessmentDate { get;  set; }
        public long CompanyId { get;  set; }
        public RiskAssessorDto RiskAssessor { get; set; }
        public RiskAssessmentStatus Status { get; set; }
        public SiteStructureElementDto RiskAssessmentSite { get; set; }
        public IEnumerable<RiskAssessmentEmployeeDto> Employees { get; set; }
        public IEnumerable<RiskAssessmentNonEmployeeDto> NonEmployees { get; set; }
        public List<RiskAssessmentReviewDto> Reviews { get; set; }
        public DateTime? NextReviewDate{ get;  set; }
        public AuditedUserDto CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public DateTime? CompletionDueDate { get; set; }
        
    }
}

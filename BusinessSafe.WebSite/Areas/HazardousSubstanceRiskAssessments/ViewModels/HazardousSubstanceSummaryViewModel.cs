using System;
using System.Security.Principal;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class HazardousSubstanceSummaryViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Reference { get; set; }
        public long CompanyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string RiskAssessmentStatus { get; set; }
        public bool Deleted { get; private set; }
        public string HazardousSubstance { get; set; }
        public bool IsArchived { get; set; }

        public HazardousSubstanceSummaryViewModel(
            long id, 
            long companyId, 
            string title, 
            string reference, 
            DateTime createdOn, 
            RiskAssessmentStatus assessmentStatuses, 
            string hazardousSubstance,
            bool deleted,
            bool isArchived
            )
        {
            Id = id;
            CompanyId = companyId;
            Title = title;
            Reference = reference;
            CreatedOn = createdOn;
            RiskAssessmentStatus = assessmentStatuses.ToString();
            HazardousSubstance = hazardousSubstance;
            Deleted = deleted;
            IsArchived = isArchived;
        }

        public bool IsEditEnabled(IPrincipal user)
        {
            return !Deleted && user.IsInRole(Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());
        }
    }
}
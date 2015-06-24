using System;
using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class DescriptionViewModel
    {
        public long CompanyId { get; set; }
        public long RiskAssessmentId { get; set; }
        public IEnumerable<Tuple<long, string>> NonEmployees { get; set; }
        public IEnumerable<Tuple<Guid, string>> Employees { get; set; }
        public bool IsInhalationRouteOfEntry { get; set; }
        public bool IsIngestionRouteOfEntry { get; set; }
        public bool IsAbsorptionRouteOfEntry { get; set; }
        public string WorkspaceExposureLimits { get; set; }
        public long HazardousSubstanceId { get; set; }
        
        public DescriptionViewModel()
        {
            NonEmployees = new List<Tuple<long, string>>();
            Employees = new List<Tuple<Guid, string>>();
        }

        public static DescriptionViewModel CreateFrom(HazardousSubstanceRiskAssessmentDto riskAssessment)
        {
            return new DescriptionViewModel()
                       {
                           RiskAssessmentId = riskAssessment.Id,
                           CompanyId = riskAssessment.CompanyId,
                           IsInhalationRouteOfEntry = riskAssessment.IsInhalationRouteOfEntry,
                           IsIngestionRouteOfEntry = riskAssessment.IsIngestionRouteOfEntry,
                           IsAbsorptionRouteOfEntry = riskAssessment.IsAbsorptionRouteOfEntry,
                           WorkspaceExposureLimits = riskAssessment.WorkspaceExposureLimits,
                           NonEmployees = riskAssessment.NonEmployees,
                           Employees = riskAssessment.Employees,
                           HazardousSubstanceId = riskAssessment.HazardousSubstance != null ? riskAssessment.HazardousSubstance.Id : 0,
                       };
        }


        public bool IsSaveButtonEnabled(IPrincipal user)
        {
            return user.IsInRole(RiskAssessmentId == 0 ? Permissions.AddGeneralandHazardousSubstancesRiskAssessments.ToString() : Permissions.EditGeneralandHazardousSubstancesRiskAssessments.ToString());
        }
    }
}
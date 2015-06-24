using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.ViewModels
{
    public class ControlMeasuresViewModel
    {
        public long RiskAssessmentId { get; set; }
        public long CompanyId { get; set; }
        public IEnumerable<HazardViewModel> Hazards { get; set; }
        
        public static ControlMeasuresViewModel CreateFrom(MultiHazardRiskAssessmentDto riskAssessment)
        {
            return new ControlMeasuresViewModel
            {
                RiskAssessmentId = riskAssessment.Id,
                CompanyId = riskAssessment.CompanyId,
                Hazards = HazardViewModel.CreateFrom(riskAssessment.Hazards)
            };
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels
{
    public class HazardousSubstanceRiskAssessmentControlMeasureViewModel
    {
        public long Id { get; set; }
        public string ControlMeasure { get; set; }
        public int ControlCount { get; set; }


        public static IEnumerable<HazardousSubstanceRiskAssessmentControlMeasureViewModel> CreateFrom(IEnumerable<HazardousSubstanceRiskAssessmentControlMeasureDto> controlMeasures)
        {
            return controlMeasures.Select(CreateFrom);
        }

        public static HazardousSubstanceRiskAssessmentControlMeasureViewModel CreateFrom(HazardousSubstanceRiskAssessmentControlMeasureDto controlMeasure, int count)
        {
            return new HazardousSubstanceRiskAssessmentControlMeasureViewModel
                       {
                           Id = controlMeasure.Id,
                           ControlCount = count + 1,
                           ControlMeasure = controlMeasure.ControlMeasure
                       };
        }
    }
}
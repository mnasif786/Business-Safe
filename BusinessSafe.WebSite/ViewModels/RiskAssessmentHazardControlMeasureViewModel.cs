using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.ViewModels
{
    public class RiskAssessmentHazardControlMeasureViewModel
    {
        public long Id { get; set; }
        public string ControlMeasure { get; set; }
        public int ControlCount { get; set; }

        public static IEnumerable<RiskAssessmentHazardControlMeasureViewModel> CreateFrom(IEnumerable<ControlMeasureDto> controlMeasures)
        {
            return controlMeasures.Select(CreateFrom);
        }

        public static RiskAssessmentHazardControlMeasureViewModel CreateFrom(ControlMeasureDto controlMeasure, int count)
        {
            return new RiskAssessmentHazardControlMeasureViewModel
                       {
                           Id = controlMeasure.Id,
                           ControlCount = count + 1,
                           ControlMeasure = controlMeasure.ControlMeasure
                       };
        }

    }
}
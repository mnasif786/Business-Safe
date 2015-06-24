using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class MultiHazardRiskAssessmentDto : RiskAssessmentDto
    {
        public string Location { get; set; }
        public string TaskProcessDescription { get; set; }
        public IList<HazardDto> Hazards { get; set; }

        public MultiHazardRiskAssessmentDto()
        {
            Hazards = new List<HazardDto>();
        }
    }
}

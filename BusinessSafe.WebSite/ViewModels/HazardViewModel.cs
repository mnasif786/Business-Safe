using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.ViewModels
{
    public class HazardViewModel
    {
        public string Title { get; set; }
        public IEnumerable<RiskAssessmentHazardControlMeasureViewModel> ControlMeasures { get; set; }
        public IEnumerable<FurtherControlMeasureTaskViewModel> ActionsRequired { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public bool AllowEditingOfTitle { get; set; }

        public static IEnumerable<HazardViewModel> CreateFrom(IEnumerable<HazardDto> hazards)
        {
            return hazards.Select(CreateFrom);
        }

        private static HazardViewModel CreateFrom(HazardDto hazard)
        {
            return new HazardViewModel
                       {
                           Id = hazard.Id,
                           RiskAssessmentHazardId = hazard.RiskAssessmentHazardId,
                           Description = hazard.Description,
                           Title = hazard.Name,
                           ActionsRequired = FurtherControlMeasureTaskViewModel.CreateFrom(hazard.FurtherActionTasks),
                           ControlMeasures = RiskAssessmentHazardControlMeasureViewModel.CreateFrom(hazard.ControlMeasures),
                           AllowEditingOfTitle = hazard.RiskAssessmentId.HasValue
                       };
        }

    }
}
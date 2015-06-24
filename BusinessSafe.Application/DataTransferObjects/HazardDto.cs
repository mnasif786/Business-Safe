using System.Collections.Generic;

namespace BusinessSafe.Application.DataTransferObjects
{
    public class HazardDto
    {
        public bool IsSystemDefault { get; set; }
        public long Id { get; set; }
        public long RiskAssessmentHazardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<ControlMeasureDto> ControlMeasures { get; set; }
        public IList<TaskDto> FurtherActionTasks { get; set; }
        public long? RiskAssessmentId { get; set; }

    }
}
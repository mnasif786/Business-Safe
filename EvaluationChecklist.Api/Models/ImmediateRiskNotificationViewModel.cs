using System;

namespace EvaluationChecklist.Models
{
    public class ImmediateRiskNotificationViewModel
    {
        public Guid Id { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public string SignificantHazard { get; set; }
        public string RecommendedImmediate { get; set; }
        //public virtual Guid ChecklistId { get; set; }
    }
}
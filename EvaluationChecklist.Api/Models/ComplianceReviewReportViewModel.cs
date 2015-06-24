using System;
using System.Collections.Generic;

namespace EvaluationChecklist.Models
{
    public class ComplianceReviewReportViewModel
    {
        public string PersonSeen { get; set; }
        public DateTime? VisitDate { get; set; }
        public string AreasVisited { get; set; }
        public string AreasNotVisited { get; set; }
        public SiteViewModel Site { get; set; }

        public ComplianceReviewReportViewModel()
        {
            ActionPlanItems = new List<ActionPlanItem>();
            ComplianceReviewItems = new List<ComplianceReviewItem>();
            ImmediateRiskNotifications = new List<ImmediateRiskNotificationPlanItem>();
        }

        public List<ActionPlanItem> ActionPlanItems { get; set; }
        public List<ComplianceReviewItem> ComplianceReviewItems { get; set; }
        public List<ImmediateRiskNotificationPlanItem> ImmediateRiskNotifications { get; set; }
    }

    public class ActionPlanItem
    {
        public string AreaOfNonCompliance { get; set; }
        public string ActionRequired { get; set; }
        public string GuidanceNumber { get; set; }
        public string TargetTimescale { get; set; }
        public string AllocatedTo { get; set; }
        public int QuestionNumber { get; set; }
        public int CategoryNumber { get; set; }

    }

    public class ImmediateRiskNotificationPlanItem
    {
        //public string Title { get; set; }
        //public string Reference { get; set; }
        public string SignificantHazardIdentified { get; set; }
        public string RecommendedImmediateAction { get; set; }
        public string AllocatedTo { get; set; }       
    }

}
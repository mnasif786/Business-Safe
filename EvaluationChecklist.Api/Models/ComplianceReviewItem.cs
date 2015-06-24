using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvaluationChecklist.Models
{
    public enum ComplianceReviewItemStatus
    {
        Satisfactory,
        FurtherActionRequired,
        ImmediateActionRequired
    }

    public class ComplianceReviewItem
    {
        public string QuestionText { get; set; }
        public ComplianceReviewItemStatus? Status { get; set; }
        public string SupportingEvidence { get; set; }
        public string ActionRequired { get; set; }
        public string CategoryName { get; set; }

        public int CategoryNumber { get; set; }
        public int QuestionNumber { get; set; }


    }
}

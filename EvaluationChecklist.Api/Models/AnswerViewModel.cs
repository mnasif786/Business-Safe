using System;

namespace EvaluationChecklist.Models
{
    public class AnswerViewModel
    {
        public virtual Guid? SelectedResponseId { get; set; }
        public virtual string SupportingEvidence { get; set; }
        public virtual string ActionRequired { get; set; }
        public string GuidanceNotes { get; set; }
        public TimescaleViewModel Timescale { get; set; }
        public AssignedToViewModel AssignedTo { get; set; }
        public virtual Guid QuestionId { get; set; }
        public virtual string QaComments { get; set; }
        public virtual string QaSignedOffBy { get; set; }
        public string ReportLetterStatement { get; set; }
    }
}
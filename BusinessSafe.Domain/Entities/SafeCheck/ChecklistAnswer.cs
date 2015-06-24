using System;
using System.Linq;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ChecklistAnswer : BaseEntity<Guid>
    {
        public virtual Checklist Checklist { get; set; }
        public virtual Question Question { get; set; }
        public virtual string SupportingEvidence { get; set; }
        public virtual string ActionRequired { get; set; }
        public virtual string GuidanceNotes { get; set; }
        public virtual Timescale Timescale { get; set; }
        public virtual Employee AssignedTo { get; set; }
        public virtual QuestionResponse Response { get; set; }
        public virtual string EmployeeNotListed { get; set; }
        public virtual string QaComments { get; set; }
        public virtual string QaSignedOffBy { get; set; }
        public virtual bool? QaCommentsResolved { get; set; }
        public virtual DateTime? QaSignedOffDate { get; set; }
        public virtual string AreaOfNonCompliance { get; set; }
        public virtual string SupportingDocumentationStatus { get; set; }
        public virtual DateTime? SupportingDocumentationDate { get; set; }

        public static ChecklistAnswer Create(Question question)
        {
            return new ChecklistAnswer()
                       {
                           Question = question
                       };
        }

        public virtual void UpdateQuestion(Question question)
        {
            Question = question;

            if (Response != null && !Response.Deleted)
            {
                var selectedResponse =
                        question.PossibleResponses.First(
                            r => r.Title == Response.Title && r.ResponseType == Response.ResponseType);

                Response = new QuestionResponse()
                {
                    Id = selectedResponse.Id,
                    Title = selectedResponse.Title,
                    ResponseType = selectedResponse.ResponseType,
                    SupportingEvidence = selectedResponse.SupportingEvidence,
                    ActionRequired = selectedResponse.ActionRequired,
                    ReportLetterStatement = selectedResponse.ReportLetterStatement,
                    ReportLetterStatementCategory = selectedResponse.ReportLetterStatementCategory,
                    GuidanceNotes = selectedResponse.GuidanceNotes,
                    CreatedBy = CreatedBy,
                    CreatedOn = DateTime.Now,
                    LastModifiedBy = LastModifiedBy,
                    LastModifiedOn = DateTime.Now,
                    Deleted = selectedResponse.Deleted,
                    Question = question
                };
            }
        }

        public virtual ChecklistAnswer Copy()
        {
            return new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = ActionRequired,
                GuidanceNotes = GuidanceNotes,
                AssignedTo = AssignedTo,
                EmployeeNotListed = EmployeeNotListed,
                AreaOfNonCompliance = AreaOfNonCompliance,
                CreatedBy = CreatedBy,
                CreatedOn = DateTime.Now,
                LastModifiedBy = LastModifiedBy,
                LastModifiedOn = DateTime.Now,
                Timescale = Timescale,
                Response = Response,
                SupportingEvidence = SupportingEvidence,
                SupportingDocumentationDate = SupportingDocumentationDate,
                SupportingDocumentationStatus = SupportingDocumentationStatus,
                Question = Question
            };
        }

        public virtual ChecklistAnswer CopyWithNewQuestion(Question question)
        {
            return new ChecklistAnswer()
            {
                Id = Guid.NewGuid(),
                ActionRequired = ActionRequired,
                GuidanceNotes = GuidanceNotes,
                AssignedTo = AssignedTo,
                EmployeeNotListed = EmployeeNotListed,
                AreaOfNonCompliance = AreaOfNonCompliance,
                CreatedBy = CreatedBy,
                CreatedOn = DateTime.Now,
                LastModifiedBy = LastModifiedBy,
                LastModifiedOn = DateTime.Now,
                Timescale = Timescale,
                Response = Response,
                SupportingEvidence = SupportingEvidence,
                SupportingDocumentationDate = SupportingDocumentationDate,
                SupportingDocumentationStatus = SupportingDocumentationStatus,
                Question = question
            };
        }
        
    }
}
using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class QuestionResponse : BaseEntity<Guid>
    {
        public virtual string Title { get; set; }
        public virtual string SupportingEvidence { get; set; }
        public virtual string ActionRequired { get; set; }
        public virtual string GuidanceNotes { get; set; }
        public virtual bool HasDateValue { get; set; }
        public virtual string DateText { get; set; } 
        public virtual DateTime? Date { get; set; }
        public virtual string ResponseType { get; set; }
        public virtual Question Question { get; set; }
        public virtual string ReportLetterStatement { get; set; }
        public virtual ReportLetterStatementCategory ReportLetterStatementCategory { get; set; }

        public class PossibleResponsesComparer : IEqualityComparer<QuestionResponse>
        {
            public virtual bool Equals(QuestionResponse a, QuestionResponse b)
            {
                if (a.Id == b.Id)
                    return true;
                else
                {
                    return false;
                }
            }

            public virtual int GetHashCode(QuestionResponse obj)
            {
                return obj.GetHashCode();
            }
        }

        public static QuestionResponse Create(Guid id, string title, UserForAuditing systemUser)
        {
            var questionResponse = new QuestionResponse() { Id = id, Title = title};
            questionResponse.CreatedBy = systemUser;
            questionResponse.CreatedOn = DateTime.Now;
            questionResponse.LastModifiedBy = systemUser;
            questionResponse.LastModifiedOn = DateTime.Now;
            return questionResponse;
        }
    }
}

using System;
using System.Collections.Generic;

namespace EvaluationChecklist.Models
{
    public class QuestionViewModel
    {
        public virtual Guid Id { get; set; }
        public virtual string Text { get; set; }
        public List<QuestionResponseViewModel> PossibleResponses { get; set; }
        public virtual Guid CategoryId { get; set; }
        public virtual CategoryViewModel Category { get; set; }
        public bool Mandatory { get; set; }
        public long? SpecificToClientId { get; set; }
        public int OrderNumber { get; set; }
        public bool Deleted { get; set; }
        public bool CustomQuestion { get; set; }
    }
    
    public class QuestionResponseViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string SupportingEvidence { get; set; }
        public string ActionRequired { get; set; }
        public string ResponseType { get; set; }
        public string GuidanceNotes { get; set; }
        public string ReportLetterStatement { get; set; }
        public string ReportLetterStatementCategory { get; set; }

        // needed to fix problem with returning duplicated possible responses if checklists saved incorrectly
        public class PossibleResponsesComparer : IEqualityComparer<QuestionResponseViewModel>
        {
            public bool Equals(QuestionResponseViewModel a, QuestionResponseViewModel b)
            {
                if (a.Title == b.Title)
                    return true;
                else
                {
                    return false;
                }
            }

            public int GetHashCode(QuestionResponseViewModel obj)
            {
                //Check whether the object is null 
                if (Object.ReferenceEquals(obj.Title, null)) return 0;

                return obj.Title.GetHashCode();
            }

        }
    }
}
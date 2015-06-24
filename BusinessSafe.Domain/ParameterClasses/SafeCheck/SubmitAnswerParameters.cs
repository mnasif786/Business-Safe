using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.ParameterClasses.SafeCheck
{
    public class SubmitAnswerParameters
    {
        public virtual string SupportingEvidence { get; set; }
        public virtual string ActionRequired { get; set; }
        public virtual QuestionResponse Response { get; set; }
    }
}

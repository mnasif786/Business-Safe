using System;
using System.Collections.Generic;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ChecklistTemplateQuestionComparer: IEqualityComparer<ChecklistTemplateQuestion>
    {

        public bool Equals(ChecklistTemplateQuestion x, ChecklistTemplateQuestion y)
        {
            return x.Question.Id == y.Question.Id;
        }

        public int GetHashCode(ChecklistTemplateQuestion templateQuestion)
        {
            //Check whether the object is null 
            if (Object.ReferenceEquals(templateQuestion, null)) return 0;

            //Get hash code for the Name field if it is not null. 
            return templateQuestion.Question.Id.GetHashCode();
        }
    }
}
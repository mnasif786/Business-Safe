using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public class ChecklistTemplateQuestion : BaseEntity<Guid>
    {
        public virtual ChecklistTemplate ChecklistTemplate { get; set; }
        public virtual Question Question { get; set; }

        public static ChecklistTemplateQuestion Create(ChecklistTemplate checklistTemplate, Question question, UserForAuditing user)
        {
            var industryQuestion = new ChecklistTemplateQuestion() { Id = Guid.NewGuid() };
            industryQuestion.ChecklistTemplate = checklistTemplate;
            industryQuestion.Question = question;
            industryQuestion.CreatedBy = user;
            industryQuestion.CreatedOn = DateTime.Now;
            industryQuestion.LastModifiedBy = user;
            industryQuestion.LastModifiedOn = DateTime.Now;

            return industryQuestion;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;

namespace BusinessSafe.Domain.Entities.SafeCheck
{
    public enum ChecklistTemplateType
    {
        Industry = 1,
        Bespoke = 2
    }

    public class ChecklistTemplate : BaseEntity<Guid>
    {
        
        public virtual string Name { get; set; }
        public virtual Boolean Draft { get; set; }
        public virtual ChecklistTemplateType TemplateType { get; set; }
        public virtual Boolean SpecialTemplate { get; set; }

        protected IList<ChecklistTemplateQuestion> _questions;
        public virtual IList<ChecklistTemplateQuestion> Questions
        {
            get { return _questions.Where(x => !x.Deleted).Distinct(new ChecklistTemplateQuestionComparer()).ToList(); }
        }

        public ChecklistTemplate()
        {
            _questions = new List<ChecklistTemplateQuestion>();
        }

		public static ChecklistTemplate Create(string name, ChecklistTemplateType templateType, UserForAuditing user)
        {
            var result = new ChecklistTemplate();
            result.Id = Guid.NewGuid();
            result.Name = name;
            result.TemplateType = templateType;
		    result.Draft = true;
            result.LastModifiedOn = DateTime.Now;
            result.LastModifiedBy = user;
            result.CreatedOn = DateTime.Now;
            result.CreatedBy = user;
            result.SpecialTemplate = false;
            return result;
        }

        public virtual void AddQuestion(ChecklistTemplateQuestion checklistTemplateQuestion, UserForAuditing user)
        {
            if (!_questions.Any(x => x.Question.Id == checklistTemplateQuestion.Question.Id))  //does not exist in checklist
            {
                _questions.Add(checklistTemplateQuestion);
            }
            else  //deleted question exists in checklist
            {
                var templateQuestion = _questions.First(x => x.Question.Id == checklistTemplateQuestion.Question.Id);
                templateQuestion.ReinstateFromDelete(user);
            }

        }

        public virtual void AddQuestion(Question question, UserForAuditing userForAuditing)
        {
            var templateQuestion = ChecklistTemplateQuestion.Create(this, question, userForAuditing);
            AddQuestion(templateQuestion,userForAuditing);
        }

        public virtual void RemoveQuestion(Question question, UserForAuditing userForAuditing)
        {
             if (_questions.Any(x => x.Question.Id == question.Id))
             {
                 _questions
                     .Where(x => x.Question.Id == question.Id)
                     .ToList()
                     .ForEach(x=> x.MarkForDelete(userForAuditing));
             }
        }
    }
}

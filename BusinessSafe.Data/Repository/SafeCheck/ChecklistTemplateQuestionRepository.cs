using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts.SafeCheck;
using NHibernate.Linq;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using BusinessSafe.Data.Common;

namespace BusinessSafe.Data.Repository.SafeCheck
{
     
    public class ChecklistTemplateQuestionRepository : Repository<ChecklistTemplateQuestion, Guid>, IChecklistTemplateQuestionRepository 
    {
        public ChecklistTemplateQuestionRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public List<ChecklistTemplateQuestion> GetByQuestion(Guid QuestionId)
        {
            var query = SessionManager.Session.Query<ChecklistTemplateQuestion>()
                .Where(x => x.Question.Id == QuestionId);

            return query.ToList();
        }
    }
}



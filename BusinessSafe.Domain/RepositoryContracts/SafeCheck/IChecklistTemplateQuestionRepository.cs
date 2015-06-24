using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;

namespace BusinessSafe.Domain.RepositoryContracts.SafeCheck
{
    public interface IChecklistTemplateQuestionRepository : IRepository<ChecklistTemplateQuestion,Guid>
    {
        List<ChecklistTemplateQuestion> GetByQuestion(Guid QuestionId);
    }
}

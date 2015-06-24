using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IFireAnswerRepository: IRepository<FireAnswer, long>
    {
        FireAnswer GetByChecklistIdAndQuestionId(long fireRiskAssessmentChecklistId, long questionId);
        IEnumerable<FireAnswer> GetByChecklistIdAndQuestionIds(long fireRiskAssessmentChecklistId, IEnumerable<long> questionIds);
    }
}
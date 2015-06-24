using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class FireAnswerRepository : Repository<FireAnswer, long>, IFireAnswerRepository
    {
        public FireAnswerRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public FireAnswer GetByChecklistIdAndQuestionId(long fireRiskAssessmentChecklistId, long questionId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<FireAnswer>()
                .Add(Restrictions.Eq("FireRiskAssessmentChecklist.Id", fireRiskAssessmentChecklistId))
                .Add(Restrictions.Eq("Question.Id", questionId))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1)
                .UniqueResult<FireAnswer>();

            return result;
        }

        public IEnumerable<FireAnswer> GetByChecklistIdAndQuestionIds(long fireRiskAssessmentChecklistId, IEnumerable<long> questionIds)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<FireAnswer>()
                .Add(Restrictions.Eq("FireRiskAssessmentChecklist.Id", fireRiskAssessmentChecklistId))
                .Add(Restrictions.In("Question.Id", questionIds.ToArray()))
                .Add(Restrictions.Eq("Deleted", false))
                .List<FireAnswer>();
            
            return result;
        }

    }
}
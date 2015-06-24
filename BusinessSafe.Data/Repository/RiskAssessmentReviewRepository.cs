using System.Collections.Generic;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class RiskAssessmentReviewRepository : Repository<RiskAssessmentReview, long>, IRiskAssessmentReviewRepository
    {
        public RiskAssessmentReviewRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IEnumerable<RiskAssessmentReview> Search(long riskAssessmentId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessmentReview>()
                .Add(Restrictions.Eq("RiskAssessment.Id", riskAssessmentId))
                .Add(Restrictions.Eq("Deleted", false))
                .AddOrder(Order.Desc("CompletionDueDate"));

            return result.List<RiskAssessmentReview>();
        }

        public RiskAssessmentReview GetByIdAndCompanyId(long id, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessmentReview>()
                .Add(Restrictions.Eq("Id", id))
                .CreateCriteria("RiskAssessment", "riskAssessment")
                .Add(Restrictions.Eq("riskAssessment.CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<RiskAssessmentReview>();

            if (result == null)
                throw new RiskAssessmentReviewNotFoundException(id);

            return result;
        }
    }
}
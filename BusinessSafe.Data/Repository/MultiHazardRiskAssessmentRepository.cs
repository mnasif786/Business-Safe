using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class MultiHazardRiskAssessmentRepository : Common.Repository<MultiHazardRiskAssessment, long>,IMultiHazardRiskAssessmentRepository
    {
        public MultiHazardRiskAssessmentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public MultiHazardRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<MultiHazardRiskAssessment>()
                .Add(Restrictions.Eq("Id", riskAssessmentId))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .SetMaxResults(1)
                .UniqueResult<MultiHazardRiskAssessment>();

            if (result == null)
                throw new RiskAssessmentNotFoundException(riskAssessmentId);

            return result;
        }

        public bool IsHazardAttachedToRiskAssessments(long hazardId, long companyId)
        {
            var riskAssessmentsCount = (int)SessionManager
                                               .Session
                                               .CreateCriteria<MultiHazardRiskAssessment>()
                                               .CreateAlias("Hazards","H")
                                               .Add(Restrictions.Eq("Deleted", false))
                                               .Add(Restrictions.Eq("CompanyId", companyId))
                                               .Add(Restrictions.Eq("H.Hazard.Id", hazardId))
                                               .SetMaxResults(1)
                                               .SetProjection(Projections.Count("Id")).UniqueResult();
            return riskAssessmentsCount > 0;
        }

     
    }
}
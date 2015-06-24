using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using NHibernate.Transform;

namespace BusinessSafe.Data.Repository
{
    public class RiskAssessmentRepository : Repository<RiskAssessment, long>, IRiskAssessmentRepository
    {
        public RiskAssessmentRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public RiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessment>()
                .Add(Restrictions.Eq("Id", riskAssessmentId))
                .Add(Restrictions.Eq("CompanyId", companyId))
                // Deleted Restrictions not applicable because need to view delete Risk Assessments
                .SetMaxResults(1)
                .UniqueResult<RiskAssessment>();

            if (result == null)
                throw new RiskAssessmentNotFoundException(riskAssessmentId);

            return result;
        }

        public RiskAssessment GetByIdAndCompanyIdIncludeDeleted(long riskAssessmentId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessment>()
                .Add(Restrictions.Eq("Id", riskAssessmentId))
                .Add(Restrictions.Eq("CompanyId", companyId))
                .SetMaxResults(1)
                .UniqueResult<RiskAssessment>();

            if (result == null)
                throw new RiskAssessmentNotFoundException(riskAssessmentId);

            return result;
        }

        public bool DoesAssessmentExistWithTheSameReference<T>(long companyId, string reference, long? riskAssessmentId) where T : class
        {
            var result = SessionManager
                .Session
                .CreateCriteria<T>()
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("Reference", reference));

            if (riskAssessmentId != null && riskAssessmentId > 0)
                result.Add(Restrictions.Not(Restrictions.Eq("Id", riskAssessmentId)));

            var riskAssessmentsCount = (int)result
                .SetMaxResults(1)
                .SetProjection(Projections.Count("Id")).UniqueResult();

            return riskAssessmentsCount > 0;
        }
        
        public bool DoesAssessmentExistWithTheSameTitle<T>(long clientId, string title, long? riskAssessmentId) where T : class
        {
            var result = SessionManager
                .Session
                .CreateCriteria<T>()
                .Add(Restrictions.Eq("CompanyId", clientId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("Title", title));

            if (riskAssessmentId != null && riskAssessmentId > 0)
                result.Add(Restrictions.Not(Restrictions.Eq("Id", riskAssessmentId)));

            var riskAssessmentsCount = (int)result
                .SetMaxResults(1)
                .SetProjection(Projections.Count("Id")).UniqueResult();

            return riskAssessmentsCount > 0;
        }

        public bool DoesAssessmentExistForRiskAssessor(long riskAssessorId, long companyId)
        {
            var result = SessionManager
                .Session
                .CreateCriteria<RiskAssessment>()
                .Add(Restrictions.Eq("CompanyId", companyId))
                .Add(Restrictions.Eq("Deleted", false))
                .Add(Restrictions.Eq("RiskAssessor.Id", riskAssessorId));
            
            var riskAssessmentsCount = (int)result
                .SetMaxResults(1)
                .SetProjection(Projections.Count("Id")).UniqueResult();

            return riskAssessmentsCount > 0;
        }
    }
}
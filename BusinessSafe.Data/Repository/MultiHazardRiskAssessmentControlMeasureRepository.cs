using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class MultiHazardRiskAssessmentControlMeasureRepository : Repository<MultiHazardRiskAssessmentControlMeasure, long>, IMultiHazardRiskAssessmentControlMeasureRepository
    {
        public MultiHazardRiskAssessmentControlMeasureRepository(IBusinessSafeSessionManager sessionManager) : base(sessionManager)
        {
        }
    }
}

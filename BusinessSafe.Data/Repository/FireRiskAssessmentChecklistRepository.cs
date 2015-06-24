using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class FireRiskAssessmentChecklistRepository : Repository<FireRiskAssessmentChecklist, long>, IFireRiskAssessmentChecklistRepository
    {
        public FireRiskAssessmentChecklistRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}
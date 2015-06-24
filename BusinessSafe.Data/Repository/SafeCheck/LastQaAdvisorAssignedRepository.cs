using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class LastQaAdvisorAssignedRepository : Repository<LastQaAdvisorAssigned, int>
    {
        public LastQaAdvisorAssignedRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

    }
}

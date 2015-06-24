using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities.SafeCheck;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository.SafeCheck
{
    public class ReportLetterStatementCategoryRepository : Repository<ReportLetterStatementCategory, Guid> , IRepository<ReportLetterStatementCategory,Guid>
    {
        public ReportLetterStatementCategoryRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }
    }
}

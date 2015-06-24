using System;
using BusinessSafe.Data.Common;
using BusinessSafe.Data.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NHibernate.Criterion;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Repository
{
    public class BusinessSafeCompanyDetailRepository : Repository<BusinessSafeCompanyDetail, long>, IBusinessSafeCompanyDetailRepository
    {
        public BusinessSafeCompanyDetailRepository(IBusinessSafeSessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public BusinessSafeCompanyDetail GetByCompanyId(long companyId)
        {
            var result = SessionManager
                    .Session
                    .CreateCriteria<BusinessSafeCompanyDetail>()
                    .Add(Restrictions.Eq("CompanyId", companyId))
                    .SetMaxResults(1)
                    .UniqueResult<BusinessSafeCompanyDetail>();
            return result;
        }
    }
}
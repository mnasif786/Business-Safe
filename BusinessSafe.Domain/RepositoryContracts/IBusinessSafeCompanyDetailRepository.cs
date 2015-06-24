using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IBusinessSafeCompanyDetailRepository : IRepository<BusinessSafeCompanyDetail, long>
    {
        BusinessSafeCompanyDetail GetByCompanyId(long companyId);
    }
}
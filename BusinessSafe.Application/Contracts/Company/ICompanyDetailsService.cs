using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;

namespace BusinessSafe.Application.Contracts.Company
{
    public interface ICompanyDetailsService 
    {
        CompanyDetailsDto GetCompanyDetails(long companyId);
        void Update(CompanyDetailsRequest request);
    }
}
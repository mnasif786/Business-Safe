using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Response;

namespace BusinessSafe.Application.Contracts.Company
{
    public interface IBusinessSafeCompanyDetailService
    {
        BusinessSafeCompanyDetailDto Get(long companyId);
        UpdateCompanyDetailsResponse UpdateBusinessSafeContact(CompanyDetailsRequest request);
    }
}
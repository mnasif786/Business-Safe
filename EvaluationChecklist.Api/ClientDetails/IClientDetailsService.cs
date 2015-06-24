using System.Collections.Generic;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Domain.Entities;
using CompanyDetails = EvaluationChecklist.ClientDetails.Models.CompanyDetails;

namespace EvaluationChecklist.ClientDetails
{
    public interface IClientDetailsService
    {
        List<SiteAddressResponse> GetSites(int clientId);
        CompanyDetails Get(int id);
        SiteAddressResponse GetSite(int clientId, int siteId);
        CompanyDetailsResponse GetByClientAccountNumber(string clientAccountNumber);
    }
}
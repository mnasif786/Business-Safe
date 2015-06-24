using System;
using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Extensions;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Application.RestAPI.Responses;
using RestSharp;

namespace BusinessSafe.Application.RestAPI
{
    public interface IClientService
    {
        CompanyDetailsDto GetCompanyDetails(long companyId);
        SiteAddressDto GetSite(long companyId, long siteId);
        IEnumerable<SiteAddressDto> GetSites(long companyId);
    }

    public class ClientService : IClientService
    {
        private readonly IRestClientAPI _restClient;

        public ClientService(IRestClientAPI restClient)
        {
            _restClient = restClient;
        }

        public CompanyDetailsDto GetCompanyDetails(long companyId)
        {
            var request = CreateGetCompanyRequest(companyId);
            
            var result = _restClient.Execute<CompanyDetailsResponse>(request);

            if (result == null)
            {
                throw new Exception("Company not found!");
            }
            
            return new CompanyDetailsDto(result.Id,result.CompanyName, result.CAN, result.MainSiteAddress.Address1, result.MainSiteAddress.Address2,
                                  result.MainSiteAddress.Address3, result.MainSiteAddress.Address4, result.MainSiteAddress.Id, result.MainSiteAddress.Postcode, "", result.Website,
                                  result.MainContact.ContactName);
        }

        public SiteAddressDto GetSite(long clientId, long siteId)
        {
            var request = CreateGetSiteRequest(clientId, siteId);

            var result = _restClient.Execute<SiteAddressResponse>(request);

            if (result == null)
            {
                throw new Exception("Site not found!");
            }

            return new SiteAddressDtoMapper().Map(result);
        }

        public IEnumerable<SiteAddressDto> GetSites(long clientId)
        {
            var request = CreateGetSitesRequest(clientId);

            var result = _restClient.Execute<List<SiteAddressResponse>>(request);

            if (result == null)
            {
                throw new Exception("Site not found!");
            }
            
            return new SiteAddressDtoMapper().Map(result.InculdeContractualHealthAndSafetySites());
        }

        private static RestRequest CreateGetCompanyRequest(long companyId)
        {            
            var request = new RestRequest("Client/{id}", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };
            request.AddUrlSegment("id", companyId.ToString());
            return request;
        }

        private static RestRequest CreateGetSitesRequest(long clientId)
        {
            var request = new RestRequest("Client/{id}/Sites", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };
            request.AddUrlSegment("id", clientId.ToString());            
            return request;
        }

        private static RestRequest CreateGetSiteRequest(long clientId, long siteId)
        {
            var request = new RestRequest("Client/{id}/Site/{siteid}", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };
            request.AddParameter("id", clientId.ToString(),ParameterType.UrlSegment);
            request.AddParameter("siteid", siteId.ToString(), ParameterType.UrlSegment);
            return request;
        }
    }
}

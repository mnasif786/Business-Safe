using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Domain.Entities;
using EvaluationChecklist.ClientDetails.Models;
using EvaluationChecklist.Helpers;
using RestSharp;
using StructureMap;
using CompanyDetails = EvaluationChecklist.ClientDetails.Models.CompanyDetails;

namespace EvaluationChecklist.ClientDetails
{
    public class ClientDetailsREST: IClientDetailsService
    {
        private readonly IRestClient _restClient;

        public ClientDetailsREST(IDependencyFactory dependencyFactory)
        {
            //TODO: move this to an app setting
            _restClient = dependencyFactory.GetNamedInstance<IRestClient> ("ClientDetailsServices");  // ObjectFactory.GetNamedInstance<IRestClient>("ClientDetailsServices");
        }

        public List<SiteAddressResponse> GetSites(int clientId)
        {

            var request = new RestRequest("Client/{id}/Sites", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };

            request.AddUrlSegment("id", clientId.ToString());

            var sites = _restClient.Execute<List<SiteAddressResponse>>(request).Data;

            return sites;
        }

        public CompanyDetails Get(int id)
        {
            //http://clientdetailsservicesrest/RestService/v1.0/Client/{ID}
            var request = new RestRequest("Client/{ID}", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };

            request.AddUrlSegment("ID", id.ToString());


            var result = _restClient.Execute<CompanyDetailsResponse>(request);

            var companyDetails = new CompanyDetails() { CAN = result.Data.CAN, CompanyName = result.Data.CompanyName, Id = result.Data.Id, Industry = result.Data.Industry};
            companyDetails.Sites = GetSites(companyDetails.Id).Select(x => new SiteDetails
                                                                               {
                                                                                   Id = x.Id,
                                                                                   Address1 = x.Address1,
                                                                                   Address2 = x.Address2,
                                                                                   Address3 = x.Address3,
                                                                                   Address4 = x.Address4,
                                                                                   Address5 = x.Address5,
                                                                                   County = x.County,
                                                                                   Postcode = x.Postcode,
                                                                                   Telephone = x.Telephone,
                                                                                   SiteContact = x.SiteContact,
                                                                                   SiteName = x.SiteName
                                                                               }).ToList();

            return companyDetails;
        }

        public  SiteAddressResponse GetSite(int clientId,int siteId)
        {
            var request = new RestRequest("Client/{id}/Site/{siteId}", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };

            request.AddUrlSegment("id", clientId.ToString());
            request.AddUrlSegment("siteId", siteId.ToString());

            return _restClient.Execute<SiteAddressResponse>(request).Data;
        }

        public CompanyDetailsResponse GetByClientAccountNumber(string clientAccountNumber)
        {

            var request = new RestRequest("Client?can={CAN}", Method.GET)
            {
                RequestFormat = DataFormat.Xml
            };

            request.AddUrlSegment("CAN", clientAccountNumber);


            return _restClient.Execute<CompanyDetailsResponse>(request).Data;

            //if (result.Data == null || result.Data.Id == -1)
            //{
            //    throw new HttpResponseException(HttpStatusCode.NotFound);
            //}

            //var companyDetails = new CompanyDetails() { CAN = result.Data.CAN, CompanyName = result.Data.CompanyName, Id = result.Data.Id, Industry = result.Data.Industry };
            //companyDetails.Sites = _clientDetailsService.GetSites(companyDetails.Id);

            //UpdateSiteNameToCompanyNameIfNullOrEmpty(companyDetails);
            //return companyDetails;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessSafe.Application.Extensions;
using BusinessSafe.Application.RestAPI.Responses;
using BusinessSafe.Domain.Entities.SafeCheck;
using BusinessSafe.Domain.RepositoryContracts;
using EvaluationChecklist.ClientDetails;
using EvaluationChecklist.ClientDetails.Models;
using EvaluationChecklist.Helpers;
using EvaluationChecklist.Models;
using CompanyDetails = EvaluationChecklist.ClientDetails.Models.CompanyDetails;

namespace EvaluationChecklist.Controllers.ClientDetails
{



    /// <summary>
    /// we shouldn't need this controller, but client details services is not configured to allow crosss origin requests. It does not permit the OPTIONS http method.
    /// </summary>
    public class ClientController : ApiController
    {
        private readonly IClientDetailsService _clientDetailsService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly BusinessSafe.Domain.RepositoryContracts.SafeCheck.ICheckListRepository _checklistRepository;

        public ClientController(IDependencyFactory dependencyFactory)
        {
            _clientDetailsService = dependencyFactory.GetInstance<IClientDetailsService>();
            _employeeRepository = dependencyFactory.GetInstance<IEmployeeRepository>();
            _checklistRepository = dependencyFactory.GetInstance<BusinessSafe.Domain.RepositoryContracts.SafeCheck.ICheckListRepository>();
            //_employeeRepository = ObjectFactory.GetInstance<IEmployeeRepository>();
        }


        /// <summary>
        /// Returns client details by company account number. Will return 404 error if not found.
        /// </summary>
        /// <param name="id">integer</param>
        /// <returns></returns>
        [HttpGet]
        public CompanyDetails Query(string clientAccountNumber)
        {
            var result = _clientDetailsService.GetByClientAccountNumber(clientAccountNumber);

            if (result == null || result.Id  == -1)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var companyDetails = new CompanyDetails() {CAN = result.CAN, CompanyName = result.CompanyName, Id = result.Id, Industry = result.Industry};
            var sites = _clientDetailsService.GetSites(companyDetails.Id);

            if (sites.Any())
            {
                companyDetails.Sites =
                    sites.InculdeMainAndAllHealthAndSafetySitesOnly().Select(x => new SiteDetails
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

                GetChecklistDetailsForSite(companyDetails, result);
            }

            UpdateSiteNameToCompanyNameIfNullOrEmpty(companyDetails);
            return companyDetails;
        }

        private void GetChecklistDetailsForSite(CompanyDetails companyDetails, CompanyDetailsResponse result)
        {
            var checklists = _checklistRepository
                .GetByClientId(clientId: result.Id, includeDeleted:false)
                .ToList();

            if (checklists.Any())
            {
                foreach (var site in companyDetails.Sites)
                {
                    var checklistForSite = checklists.FirstOrDefault(c => c.SiteId == site.Id && c.Status != Checklist.STATUS_SUBMITTED);
                    if (checklistForSite != null)
                    {
                        var checklistModel = new ChecklistDetails
                                                 {
                                                     Id = checklistForSite.Id,
                                                     VisitDate = checklistForSite.VisitDate,
                                                     VisitBy = checklistForSite.VisitBy,
                                                     CreatedOn = checklistForSite.CreatedOn,
                                                 };
                        site.Checklist = checklistModel;
                    }
                }
            }
        }

        /// <summary>
        /// Returns client details by company Id. Will return 404 error if not found.
        /// </summary>
        /// <param name="id">integer</param>
        /// <returns></returns>
        public CompanyDetails Get(int id)
        {
            var clientDetails = _clientDetailsService.Get(id);

            UpdateSiteNameToCompanyNameIfNullOrEmpty(clientDetails);

            return clientDetails;
        }

        private static void UpdateSiteNameToCompanyNameIfNullOrEmpty(CompanyDetails clientDetails)
        {
            if (clientDetails.Sites != null)
            {
                clientDetails.Sites.ForEach(x => { x.SiteName = string.IsNullOrEmpty(x.SiteName) ? clientDetails.CompanyName : x.SiteName; });
            }
        }


        /// <summary>
        /// Returns the employees for the client
        /// </summary>
        /// <param name="clientId">The client Id</param>
        /// <returns>List of employees associated with the client</returns>
        public IEnumerable<ClientEmployeeViewModel> GetEmployees(long clientId)
        {
            
           var employees = _employeeRepository.Search(clientId, null, null, null, new long[] { }, false, 0, true, false, "Surname", true);

           return employees.Select(e => new ClientEmployeeViewModel()
           {
               Id = e.Id,
               Forename = e.Forename,
               Surname = e.Surname,
               FullName = e.Forename + " " + e.Surname,
               EmailAddress = e.GetEmail()
           }).ToList();
            
        }

        /// <summary>
        /// we need this for CORS. if this is removed clients will receive a 405 method not allowed http error
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }



  
}

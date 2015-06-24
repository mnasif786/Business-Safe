using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.RestAPI;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Controllers
{
    public class LiveCheckController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly ISiteService _siteService;

        public LiveCheckController(
            IClientService clientService,
            ISiteService siteService)
        {
            _clientService = clientService;
            _siteService = siteService;
        }

        [PermissionFilter(Permissions.ViewRiskAssessmentTasks)]
        public ActionResult Index()
        {
            var errors = new List<string>();

            try
            {
                var client = _clientService.GetCompanyDetails(33749);
                
                if(client.CAN != "DEMO002")
                {
                    errors.Add("ClientDetailsService at http://clientdetailsservicesrest/restservice/v1.0/Client/33749 returned wrong information.");
                }
            }
            catch (Exception ex)
            {
                errors.Add("Error calling ClientDetailsService from http://clientdetailsservicesrest/restservice/v1.0/Client/33749:" + ex.Message);
            }

            try
            {
                var site = _siteService.GetByIdAndCompanyId(25, 33749);

                if(site.Name != "Main Site")
                {
                    errors.Add("SiteService returned wrong information. Possibly error accessing database PBSPROD2SQL/PROD2");
                }
            }
            catch (Exception ex)
            {
                errors.Add("Error reading data. Possibly error accessing database PBSPROD2SQL\\PROD2: " + ex.Message);
            }

            if(errors.Any())
            {
                return Json(new { success = false, errors }, JsonRequestBehavior.AllowGet);
            }


            //document library
            //client documentation
            //po
            //how to check message queues?

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Request.HazardousSubstanceInventory;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.ViewModels;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.SqlReports.Controllers
{
    public class HazardousSubstancesInventoryController :  BaseController
    {
        private readonly ISqlReportExecutionServiceFacade _sqlReportExecutionServiceFacade;
        private readonly IHazardousSubstancesService _hazardousSubstancesService;

        public HazardousSubstancesInventoryController(  ISqlReportExecutionServiceFacade sqlReportExecutionServiceFacade,
                                                        IHazardousSubstancesService hazardousSubstancesService)
        {
            _sqlReportExecutionServiceFacade = sqlReportExecutionServiceFacade;
            _hazardousSubstancesService = hazardousSubstancesService;
        }

        private string GetHazardousSubstanceIDList(string substanceName, long? supplierId)
        {
            string result = "";

            var searchResults = _hazardousSubstancesService.Search(new SearchHazardousSubstancesRequest()
            {
                CompanyId = CurrentUser.CompanyId,
                SubstanceNameLike = substanceName,
                SupplierId = supplierId
            });           

            long[] substanceIds = searchResults.Select(x => x.Id).ToArray();            

            return string.Join(",", substanceIds);
        }


        [PermissionFilter(Permissions.ViewHazardousSubstanceInventory)]
        public FileResult Index(string substanceName, long? supplierId)
        {
            string substanceIDList = GetHazardousSubstanceIDList(substanceName, supplierId);
            var document = _sqlReportExecutionServiceFacade.GetReport(SqlReportHelper.ReportType.HazardousSubstancesInventory,

                                                           new object[]
                                                               {
                                                                    substanceIDList                                                                    
                                                               },

                                                           SqlReportHelper.ReportFormatType.PDF);

            return File(document.FileStream, document.MimeType, "HazardousSubstancesInventory.pdf");
        }
    }
}

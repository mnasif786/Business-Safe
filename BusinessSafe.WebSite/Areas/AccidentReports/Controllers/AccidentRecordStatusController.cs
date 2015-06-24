using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Controllers
{
    public class AccidentRecordStatusController : BaseController 
    {
        private readonly IAccidentRecordService _accidentRecordService;

        public AccidentRecordStatusController(IAccidentRecordService accidentRecordService)
        {
            _accidentRecordService = accidentRecordService;
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteAccidentRecords)]
        public JsonResult Delete(long accidentRecordId)
        {
            _accidentRecordService.Delete(accidentRecordId, CurrentUser.CompanyId, CurrentUser.UserId);
            return Json(new {success = true});
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditAccidentRecords)]
        public JsonResult UpdateStatus(long accidentRecordId, bool isClosed)
        {
            var status = isClosed ? AccidentRecordStatusEnum.Closed : AccidentRecordStatusEnum.Open;

            _accidentRecordService.UpdateAccidentRecordStatus(accidentRecordId, CurrentUser.CompanyId, CurrentUser.UserId, status);

            return Json(new { success = true });
        }
    }
}

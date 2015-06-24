using System.Web.Mvc;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.AccidentReports.Factories;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using StructureMap;

namespace BusinessSafe.WebSite.Filters
{
    public class AccidentRecordContextFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest()) return;

            var accidentRecordId = new RiskAssessmentContextFilterHelper(filterContext)
                                       {
                                           ActionParamterIdKey = "accidentRecordId",
                                           ViewModelPropertyNameKey = "AccidentRecordId"
                                       }
                                       .GetRiskAssessmentId();
            if (accidentRecordId != 0)
            {
                var user = filterContext.HttpContext.User as CustomPrincipal;
                if (user == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }

                var accidentRecord = ObjectFactory.GetInstance<IAccidentRecordService>().GetByIdAndCompanyId(accidentRecordId, user.CompanyId);

                var summary = AccidentSummaryViewModel.Create(accidentRecord.Id,
                                                                        accidentRecord.CompanyId,
                                                                        accidentRecord.Title,
                                                                        accidentRecord.Reference,
                                                                        accidentRecord.CreatedOn.Value,
                                                                        accidentRecord.IsDeleted,
                                                                        accidentRecord.Status
                                                                        );
                dynamic viewBag = filterContext.Controller.ViewBag;
                viewBag.AccidentRecordSummary = summary;
            }
        }
    }
}
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using StructureMap;

namespace BusinessSafe.WebSite.Filters
{
    public class GeneralRiskAssessmentContextFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest()) return;

            var riskAssessmentId = new RiskAssessmentContextFilterHelper(filterContext)
                                       {
                                           ActionParamterIdKey = "riskAssessmentId",
                                           ViewModelPropertyNameKey = "RiskAssessmentId"
                                       }
                                       .GetRiskAssessmentId();
            if(riskAssessmentId > 0  )
            {
                var user = filterContext.HttpContext.User as CustomPrincipal;
                if (user == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }

                var riskAssessment = ObjectFactory.GetInstance<IGeneralRiskAssessmentService>().GetRiskAssessment(riskAssessmentId, user.CompanyId);
                var summary = new GeneralRiskAssessmentSummaryViewModel(riskAssessment.Id,
                                                                        riskAssessment.CompanyId,
                                                                        riskAssessment.Title,
                                                                        riskAssessment.Reference,
                                                                        riskAssessment.CreatedOn.Value,
                                                                        riskAssessment.Status,
                                                                        riskAssessment.Deleted,
                                                                        riskAssessment.Status == RiskAssessmentStatus.Archived
                                                                        );
                dynamic viewBag = filterContext.Controller.ViewBag;
                viewBag.RiskAssessmentSummary = summary;
                if (riskAssessment.Status == RiskAssessmentStatus.Archived)
                {
                    viewBag.IsReadOnly = true;
                }    
            }
        }
    }
}
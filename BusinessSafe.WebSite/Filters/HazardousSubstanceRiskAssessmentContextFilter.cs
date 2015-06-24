using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using StructureMap;

namespace BusinessSafe.WebSite.Filters
{
    public class HazardousSubstanceRiskAssessmentContextFilter : ActionFilterAttribute
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

            if (riskAssessmentId > 0)
            {
                var user = filterContext.HttpContext.User as CustomPrincipal;
                if (user == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }

                var riskAssessment = ObjectFactory.GetInstance<IHazardousSubstanceRiskAssessmentService>().GetRiskAssessment(riskAssessmentId, user.CompanyId);

                var summary = new HazardousSubstanceSummaryViewModel(riskAssessment.Id, riskAssessment.CompanyId,
                                                                     riskAssessment.Title, riskAssessment.Reference,
                                                                     riskAssessment.CreatedOn.GetValueOrDefault(),
                                                                     riskAssessment.Status,
                                                                     (riskAssessment.HazardousSubstance != null ? riskAssessment.HazardousSubstance.Name : ""),
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
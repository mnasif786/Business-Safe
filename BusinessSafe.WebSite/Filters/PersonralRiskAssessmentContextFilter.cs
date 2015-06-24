using System.Web.Mvc;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;

namespace BusinessSafe.WebSite.Filters
{
    public class ILookAfterOwnTransactionFilter :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sessionManager = ObjectFactory.GetInstance<IBusinessSafeSessionManager>();
            sessionManager.CloseSession();
        }
    }

    public class PersonalRiskAssessmentContextFilter : ActionFilterAttribute
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

                var riskAssessment = ObjectFactory.GetInstance<IPersonalRiskAssessmentService>().GetRiskAssessment(riskAssessmentId, user.CompanyId, user.UserId);
                var summary = new PersonalRiskAssessmentSummaryViewModel(riskAssessment.Id,
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
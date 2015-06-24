using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.Filters
{
    public class HazardousSubstanceRiskAssessmentCurrentTabActionFilter : ActionFilterAttribute
    {
        private readonly HazardousSubstancesTabs _hazardousSubstancesTabs;

        const string defaultKey = "riskAssessmentId";
        const string hazardousSubstanceRiskAssessmentIdKey = "riskAssessmentId";
        const string companyIdKey = "companyId";
        const string readonlyKey = "isReadonly";

        public HazardousSubstanceRiskAssessmentCurrentTabActionFilter(HazardousSubstancesTabs hazardousSubstancesTabs)
        {
            _hazardousSubstancesTabs = hazardousSubstancesTabs;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            long hazardousSubstanceRiskAssessmentId = default(long);

            if (filterContext.ActionParameters.ContainsKey(defaultKey))
                long.TryParse(filterContext.ActionParameters[defaultKey].ToString(), out hazardousSubstanceRiskAssessmentId);

            if (filterContext.ActionParameters.ContainsKey(hazardousSubstanceRiskAssessmentIdKey))
            {
                long.TryParse(filterContext.ActionParameters[hazardousSubstanceRiskAssessmentIdKey].ToString(), out hazardousSubstanceRiskAssessmentId);
            }
            else if (filterContext.ActionParameters.Count > 0)
            {
                var key = filterContext.ActionParameters.Keys.First();
                var property =
                    filterContext.ActionParameters[key].GetType().GetProperty("riskassessmentid",
                                                                                BindingFlags.IgnoreCase |
                                                                                BindingFlags.Public |
                                                                                BindingFlags.Instance);
                if (property != null)
                {
                    var result = property.GetValue(filterContext.ActionParameters[key], null);

                    if (result != null)
                    {
                        hazardousSubstanceRiskAssessmentId = (long)result;
                    }
                }

            }

            filterContext.HttpContext.Items[hazardousSubstanceRiskAssessmentIdKey] = hazardousSubstanceRiskAssessmentId;

            filterContext.HttpContext.Items[readonlyKey] = filterContext.RouteData.Values["action"].ToString() == "View";

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            long companyId = GetCompanyId(filterContext);
            long hazardousSubstanceRiskAssessmentId = default(long);
            bool isReadOnly = false;

            if (filterContext.HttpContext.Items[hazardousSubstanceRiskAssessmentIdKey] != null)
            {
                long.TryParse(filterContext.HttpContext.Items[hazardousSubstanceRiskAssessmentIdKey].ToString(), out hazardousSubstanceRiskAssessmentId);
            }
            if (filterContext.HttpContext.Items[readonlyKey] != null)
            {
                bool.TryParse(filterContext.HttpContext.Items[readonlyKey].ToString(), out isReadOnly);
            }
            

            filterContext.Controller.ViewBag.TabViewModel = new HazardousSubstanceRiskAssessmentTabViewModel
                                                                {
                                                                    CurrentTab = _hazardousSubstancesTabs,
                                                                    CompanyId = companyId,
                                                                    Id = hazardousSubstanceRiskAssessmentId,
                                                                    IsReadOnly = isReadOnly
                                                                };
            base.OnActionExecuted(filterContext);
        }

        private long GetCompanyId(ControllerContext filterContext)
        {
            var user = filterContext.HttpContext.User as CustomPrincipal;

            return user.CompanyId;
        }
    }
}
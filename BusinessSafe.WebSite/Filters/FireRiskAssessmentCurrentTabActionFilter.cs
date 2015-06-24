using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.Filters
{
    public class FireRiskAssessmentCurrentTabActionFilter : ActionFilterAttribute
    {
        private readonly FireRiskAssessmentTabs _fireRiskAssessmentTabs;
        const string riskAssessmentIdKey = "riskAssessmentId";
        const string companyIdKey = "companyId";
        private const string readOnlyKey = "readOnly";

        public FireRiskAssessmentCurrentTabActionFilter(FireRiskAssessmentTabs fireRiskAssessmentTabs)
        {
            _fireRiskAssessmentTabs = fireRiskAssessmentTabs;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            long companyIdVal;
            long riskAssessmentIdVal = default(long);

            companyIdVal = GetCompanyId(filterContext);

            filterContext.HttpContext.Items[companyIdKey] = companyIdVal;

            if (filterContext.ActionParameters.ContainsKey(riskAssessmentIdKey))
            {
                long.TryParse(filterContext.ActionParameters[riskAssessmentIdKey].ToString(), out riskAssessmentIdVal);
                filterContext.HttpContext.Items[riskAssessmentIdKey] = riskAssessmentIdVal;
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
                        riskAssessmentIdVal = (long)result;
                    }
                }

            }

            filterContext.HttpContext.Items[riskAssessmentIdKey] = riskAssessmentIdVal;

            bool isReadOnlyVal = filterContext.RouteData.Values["action"].ToString() == "View";
            filterContext.HttpContext.Items[readOnlyKey] = isReadOnlyVal;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            long riskAssessmentIdVal = default(long);
            long companyIdVal = default(long);
            bool isReadOnly = false;

            if (filterContext.HttpContext.Items[companyIdKey] != null)
            {
                long.TryParse(filterContext.HttpContext.Items[companyIdKey].ToString(), out companyIdVal);
            }

            if (filterContext.HttpContext.Items[riskAssessmentIdKey] != null)
            {
                long.TryParse(filterContext.HttpContext.Items[riskAssessmentIdKey].ToString(), out riskAssessmentIdVal);
            }

            if (filterContext.HttpContext.Items[readOnlyKey] != null)
            {
                bool.TryParse(filterContext.HttpContext.Items[readOnlyKey].ToString(), out isReadOnly);
            }

            filterContext.Controller.ViewBag.TabViewModel = new FireRiskAssessmentTabViewModel
                                                                {
                                                                    CurrentTab = _fireRiskAssessmentTabs,
                                                                    CompanyId = companyIdVal,
                                                                    Id = riskAssessmentIdVal,
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
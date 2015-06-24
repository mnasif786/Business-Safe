using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.AuthenticationService;

namespace BusinessSafe.WebSite.Areas.Filters
{
 
    public class GeneralRiskAssessmentCurrentTabActionFilter : ActionFilterAttribute
    {
        private readonly GeneralRiskAssessmentTabs _generalRiskAssessmentTabs;
       
        const string riskAssessmentIdKey = "riskAssessmentId";
        const string companyIdKey = "companyId";
        const string readonlyKey = "isReadonly";

        public GeneralRiskAssessmentCurrentTabActionFilter(GeneralRiskAssessmentTabs generalRiskAssessmentTabs)
        {
            _generalRiskAssessmentTabs = generalRiskAssessmentTabs;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            long riskAssessmentVal = default(long);
            long companyIdVal;
            bool isReadOnlyVal;

            companyIdVal = GetCompanyId(filterContext);
           
            filterContext.HttpContext.Items[companyIdKey] = companyIdVal;
           
            Log.Add("Checking for risk assessment key: " + riskAssessmentVal);
            if (filterContext.ActionParameters.ContainsKey(riskAssessmentIdKey))
            {
                Log.Add("Found key.");
                Log.Add("Key value: " + filterContext.ActionParameters[riskAssessmentIdKey]);
                long.TryParse(filterContext.ActionParameters[riskAssessmentIdKey].ToString(), out riskAssessmentVal);
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
                        riskAssessmentVal = (long)result;
                    }
                }
                
            }

            filterContext.HttpContext.Items[riskAssessmentIdKey] = riskAssessmentVal;

            isReadOnlyVal = filterContext.RouteData.Values["action"].ToString() == "View";
            filterContext.HttpContext.Items[readonlyKey] = isReadOnlyVal;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            long accidentRecordVal = default(long);
            long companyIdVal = default(long);
            bool isReadOnly = false;

            if (filterContext.HttpContext.Items[companyIdKey] != null)
            {
                long.TryParse(filterContext.HttpContext.Items[companyIdKey].ToString(), out companyIdVal);
            }

            if (filterContext.HttpContext.Items[riskAssessmentIdKey] != null)
            {
                long.TryParse(filterContext.HttpContext.Items[riskAssessmentIdKey].ToString(), out accidentRecordVal);
            }

            if (filterContext.HttpContext.Items[readonlyKey] != null)
            {
                bool.TryParse(filterContext.HttpContext.Items[readonlyKey].ToString(), out isReadOnly);
            }

            filterContext.Controller.ViewBag.TabViewModel = new GeneralRiskAssessmentTabViewModel
            {
                CurrentTab = _generalRiskAssessmentTabs,
                CompanyId = companyIdVal,
                Id = accidentRecordVal,
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
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using StructureMap;

namespace BusinessSafe.WebSite.Areas.Filters
{
    public class PersonalRiskAssessmentCurrentTabActionFilter : ActionFilterAttribute
    {
        private readonly IEmployeeChecklistGeneratorViewModelFactory _checklistGeneratorViewModelFactory;
        private readonly PersonalRiskAssessmentTabs _personalRiskAssessmentTabs;

        private const string riskAssessmentIdKey = "riskAssessmentId";
        private const string readonlyKey = "isReadonly";
        private const string checklistSentKey = "checklistSent";

        
        public PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs personalRiskAssessmentTabs)
        {
            _personalRiskAssessmentTabs = personalRiskAssessmentTabs;
            _checklistGeneratorViewModelFactory =
                ObjectFactory.GetInstance<IEmployeeChecklistGeneratorViewModelFactory>();
        }

        public PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs personalRiskAssessmentTabs,
                                                            IEmployeeChecklistGeneratorViewModelFactory
                                                                checklistGeneratorViewModelFactory)
        {
            _personalRiskAssessmentTabs = personalRiskAssessmentTabs;
            _checklistGeneratorViewModelFactory = checklistGeneratorViewModelFactory;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            long riskAssessmentId = default(long);

            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                return;

            long companyId = GetCompanyId(filterContext);
            var user = filterContext.HttpContext.User as CustomPrincipal;

            if (filterContext.ActionParameters.ContainsKey(riskAssessmentIdKey))
            {
                long.TryParse(filterContext.ActionParameters[riskAssessmentIdKey].ToString(), out riskAssessmentId);
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
                        riskAssessmentId = (long) result;
                    }
                }
                
            }

            filterContext.HttpContext.Items[riskAssessmentIdKey] = riskAssessmentId;

            filterContext.HttpContext.Items[readonlyKey] = filterContext.RouteData.Values["action"].ToString() == "View";

            if (riskAssessmentId != default(long))
            {
                var riskAssessment =
                    ObjectFactory.GetInstance<IPersonalRiskAssessmentService>().GetRiskAssessment(riskAssessmentId,
                                                                                                  companyId, user.UserId);

                filterContext.HttpContext.Items[checklistSentKey] =
                    riskAssessment.PersonalRiskAssessementEmployeeChecklistStatus !=
                    PersonalRiskAssessementEmployeeChecklistStatusEnum.NotSet;
            }
        }

        private long GetCompanyId(ControllerContext filterContext)
        {
            var user = filterContext.HttpContext.User as CustomPrincipal;

            return user.CompanyId;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            long companyId = GetCompanyId(filterContext);
            long riskAssessmentId = default(long);
            bool isReadOnly = false;
            bool hasChecklistBeenSent = false;


            if (filterContext.HttpContext.Items[riskAssessmentIdKey] != null)
            {
                long.TryParse(filterContext.HttpContext.Items[riskAssessmentIdKey].ToString(), out riskAssessmentId);
            }

            if (filterContext.HttpContext.Items[readonlyKey] != null)
            {
                bool.TryParse(filterContext.HttpContext.Items[readonlyKey].ToString(), out isReadOnly);
            }

            if (filterContext.HttpContext.Items[checklistSentKey] != null)
            {
                bool.TryParse(filterContext.HttpContext.Items[checklistSentKey].ToString(), out hasChecklistBeenSent);
            }

            filterContext.Controller.ViewBag.TabViewModel = new PersonalRiskAssessmentTabViewModel
                                                                {
                                                                    CurrentTab = _personalRiskAssessmentTabs,
                                                                    CompanyId = companyId,
                                                                    Id = riskAssessmentId,
                                                                    IsReadOnly = isReadOnly,
                                                                    ChecklistSent = hasChecklistBeenSent
                                                                };

            base.OnActionExecuted(filterContext);
        }
    }
}
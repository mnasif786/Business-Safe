using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.Areas.Responsibilities.ViewModels.Wizard;
using BusinessSafe.WebSite.Controllers;

using StructureMap;

namespace BusinessSafe.WebSite.Filters
{
    public class ResponsibilityWizardTabActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ResponsibilityWizardTabs _selectedTab;
        private const string responsibilityTemplateIdsKey = "responsibilityTemplateIds";
        private const string hasUncreatedTasksKey = "hasUncreatedTasks";

        public ResponsibilityWizardTabActionFilterAttribute(ResponsibilityWizardTabs selectedTab)
        {
            _selectedTab = selectedTab;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Items[responsibilityTemplateIdsKey] = GetResponsibilityTemplateIds(filterContext);
            filterContext.HttpContext.Items[hasUncreatedTasksKey] = GetHasUncreatedTasks(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string responsibilityTemplateIds = string.Empty;
            bool hasUncreatedTask = false;

            if (filterContext.HttpContext.Items[responsibilityTemplateIdsKey] != null)
            {
                responsibilityTemplateIds = filterContext.HttpContext.Items[responsibilityTemplateIdsKey].ToString();
            }

            if (filterContext.HttpContext.Items[hasUncreatedTasksKey] != null)
            {
                bool.TryParse(filterContext.HttpContext.Items[hasUncreatedTasksKey].ToString(), out hasUncreatedTask);
            }
            filterContext.Controller.ViewBag.TabViewModel = new ResponsibilitiesWizardTabViewModel
            {
                SelectedTab = _selectedTab,
                HasUncreatedTasks = hasUncreatedTask,
                SelectedResponsiblityTemplateIds = responsibilityTemplateIds
            };

            base.OnActionExecuted(filterContext);
        }

        private string GetResponsibilityTemplateIds(ActionExecutingContext filterContext)
        {
            const string key = "selectedResponsibilityTemplateIds";
            if (filterContext.ActionParameters.Count > 0 && 
                filterContext.ActionParameters.ContainsKey(key) && 
                filterContext.ActionParameters[key] != null)
            {
                return filterContext.ActionParameters[key].ToString();
            }

            return string.Empty;
        }

        private bool GetHasUncreatedTasks(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            var companyId = controller.CurrentUser.CompanyId;
            var responsibilitiesService = ObjectFactory.GetInstance<IResponsibilitiesService>();
            return responsibilitiesService.GetStatutoryResponsibiltiesWithUncreatedStatutoryTasks(companyId).Any();
        }
    }

    public class ResponsibilitiesWizardTabViewModel
    {
        public string SelectedResponsiblityTemplateIds;
        public ResponsibilityWizardTabs SelectedTab;
        public bool HasUncreatedTasks;
    }
}
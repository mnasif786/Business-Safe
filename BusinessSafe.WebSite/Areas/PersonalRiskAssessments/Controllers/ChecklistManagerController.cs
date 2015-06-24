using System;
using System.Web.Mvc;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Filters;
using System.Linq;
using Telerik.Web.Mvc;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    [PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs.ChecklistManager)]
    [PersonalRiskAssessmentContextFilter]
    public class ChecklistManagerController : BaseController
    {
        private readonly IChecklistManagerViewModelFactory _checklistGeneratorViewModelFactory;
        private readonly IEmployeeChecklistSummaryViewModelFactory _employeeChecklistSummaryViewModelFactory;

        public ChecklistManagerController(
            IChecklistManagerViewModelFactory checklistGeneratorViewModelFactory,
            IEmployeeChecklistSummaryViewModelFactory employeeChecklistSummaryViewModelFactory)
        {
            _checklistGeneratorViewModelFactory = checklistGeneratorViewModelFactory;
            _employeeChecklistSummaryViewModelFactory = employeeChecklistSummaryViewModelFactory;
        }

        [PermissionFilter(Permissions.AddPersonalRiskAssessments)]
        public ActionResult Index(long riskAssessmentId, long companyId)
        {
            var model = _checklistGeneratorViewModelFactory
                            .WithCompanyId(companyId)
                            .WithRiskAssessmentId(riskAssessmentId)
                            .WithCurrentUserId(CurrentUser.UserId)
                            .GetViewModel();

            if (model.PersonalRiskAssessementEmployeeChecklistStatus == PersonalRiskAssessementEmployeeChecklistStatusEnum.NotSet)
            {
                return RedirectToAction("Index", "ChecklistGenerator", new { area = "PersonalRiskAssessments", companyId = companyId, riskAssessmentId = riskAssessmentId });
            }

            return View("Index", model);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult FilterGrid(long riskAssessmentId, string emailContains, string nameContains)
        {
            var model = _checklistGeneratorViewModelFactory
                            .WithCompanyId(CurrentUser.CompanyId)
                            .WithRiskAssessmentId(riskAssessmentId)
                            .WithCurrentUserId(CurrentUser.UserId)
                            .WithEmailContains(emailContains)
                            .WithNameContains(nameContains)
                            .GetViewModel();

            if (model.PersonalRiskAssessementEmployeeChecklistStatus == PersonalRiskAssessementEmployeeChecklistStatusEnum.NotSet)
            {
                return RedirectToAction("Index", "ChecklistGenerator", new { area = "PersonalRiskAssessments", companyId = CurrentUser.CompanyId, riskAssessmentId = riskAssessmentId });
            }

            return View("Index", model);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;
            return Index(riskAssessmentId, companyId);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult ViewSummary(Guid employeeChecklistId)
        {
            var model = _employeeChecklistSummaryViewModelFactory
                .WithEmployeeChecklistId(employeeChecklistId)
                .GetViewModel();

            return PartialView("_EmployeeChecklistSummary", model);
        }

      
    }
}

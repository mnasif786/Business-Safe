using System;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Company;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.Company.Factories;
using BusinessSafe.WebSite.Areas.Company.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Controllers
{
    public class RiskAssessorController : BaseController
    {
        private readonly IAddEditRiskAssessorViewModelFactory _addEditRiskAssessorViewModelFactory;
        private readonly IRiskAssessorService _riskAssessorService;
        private readonly IEmployeeService _employeeService;
        private readonly IRiskAssessorsDefaultAddEditViewModelFactory _riskAssessorsDefaultAddEditViewModelFactory;

        public RiskAssessorController(
            IAddEditRiskAssessorViewModelFactory addEditRiskAssessorViewModelFactory,
            IRiskAssessorService riskAssessorService,
            IEmployeeService employeeService,
            IRiskAssessorsDefaultAddEditViewModelFactory riskAssessorsDefaultAddEditViewModelFactory)
        {
            _addEditRiskAssessorViewModelFactory = addEditRiskAssessorViewModelFactory;
            _riskAssessorService = riskAssessorService;
            _employeeService = employeeService;
            _riskAssessorsDefaultAddEditViewModelFactory = riskAssessorsDefaultAddEditViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewCompanyDefaults)]
        public PartialViewResult Index(long companyId, bool showDeleted)
        {
            var riskAssessors = _riskAssessorService.Search(new SearchRiskAssessorRequest
            {
                CompanyId = companyId,
                IncludeDeleted = showDeleted,
                ExcludeActive = showDeleted
            });

            var viewModel = _riskAssessorsDefaultAddEditViewModelFactory
                .WithShowingDeleted(showDeleted)
                .WithRiskAssessors(riskAssessors)
                .GetViewModel();

            return PartialView("_Grid", viewModel);
        }

        [HttpGet]
        [PermissionFilter(Permissions.ViewEmployeeRecords)]
        public JsonResult Search(string filter, long siteId, long companyId, int pageLimit)
        {
            var riskAssessors = _riskAssessorService.Search(new SearchRiskAssessorRequest
            {
                CompanyId = companyId,
                MaximumResults = pageLimit,
                SearchTerm = filter,
                SiteId = siteId
            });
            var result = riskAssessors.Select(AutoCompleteViewModel.ForRiskAssessor).AddDefaultOption().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get(long siteId, long companyId)
        {
            var riskAssessors = _riskAssessorService.Search(new SearchRiskAssessorRequest
            {
                CompanyId = companyId,
                SiteId = siteId
            });
            var result = riskAssessors.Select(AutoCompleteViewModel.ForRiskAssessor).AddDefaultOption().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [PermissionFilter(Permissions.AddCompanyDefaults)]
        public PartialViewResult Add()
        {
            var viewModel = _addEditRiskAssessorViewModelFactory
                .GetViewModel();

            return PartialView("_AddForm", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddCompanyDefaults)]
        public JsonResult Add(AddEditRiskAssessorViewModel viewModel)
        {
            Validate(viewModel);

            if (!ModelState.IsValid)
            {
                return ModelStateErrorsWithKeysAsJson();
            }

            var riskAssessorId = CreateNewRiskAssessor(viewModel);
            var riskAssessor = _riskAssessorService.GetByIdAndCompanyId(riskAssessorId, CurrentUser.CompanyId);
            return Json(new
            {
                Success = true,
                RiskAssessorId = riskAssessorId,
                Forename = riskAssessor.Employee.Forename,
                Surname = riskAssessor.Employee.Surname,
                SiteName = riskAssessor.HasAccessToAllSites ? "All Sites" : riskAssessor.Site.Name,
                DoNotSendReviewDueNotification = riskAssessor.DoNotSendReviewDueNotification,
                DoNotSendTaskCompletedNotifications = riskAssessor.DoNotSendTaskCompletedNotifications,
                DoNotSendTaskOverdueNotifications = riskAssessor.DoNotSendTaskOverdueNotifications,
                FormattedName = riskAssessor.Employee.FullName,
            });
        }

        [PermissionFilter(Permissions.AddCompanyDefaults)]
        public PartialViewResult Edit(long companyId, long riskAssessorId)
        {
            var viewModel =
                _addEditRiskAssessorViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessorId(riskAssessorId)
                .GetViewModel();

            return PartialView("_EditForm", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.AddCompanyDefaults)]
        public JsonResult Edit(AddEditRiskAssessorViewModel viewModel)
        {
            Validate(viewModel);

            if (viewModel.RiskAssessorId.HasValue == false)
                throw new ArgumentNullException("RiskAssessorId not defined");

            if (!ModelState.IsValid)
            {
                return ModelStateErrorsWithKeysAsJson();
            }

            var request = new CreateEditRiskAssessorRequest()
                              {
                                  CompanyId = CurrentUser.CompanyId,
                                  CreatingUserId = CurrentUser.UserId,
                                  DoNotSendReviewDueNotification = viewModel.DoNotSendReviewDueNotification,
                                  DoNotSendTaskCompletedNotifications = viewModel.DoNotSendTaskCompletedNotifications,
                                  DoNotSendTaskOverdueNotifications = viewModel.DoNotSendTaskOverdueNotifications,
                                  EmployeeId = viewModel.EmployeeId.Value,
                                  HasAccessToAllSites = viewModel.HasAccessToAllSites,
                                  SiteId = viewModel.SiteId,
                                  RiskAssessorId = viewModel.RiskAssessorId.Value
                              };
            _riskAssessorService.Update(request);
            var riskAssessor = _riskAssessorService.GetByIdAndCompanyId(viewModel.RiskAssessorId.Value, CurrentUser.CompanyId);
            return Json(new
            {
                Success = true,
                RiskAssessorId = viewModel.RiskAssessorId.Value,
                Forename = riskAssessor.Employee.Forename,
                Surname = riskAssessor.Employee.Surname,
                SiteName = riskAssessor.SiteName,
                DoNotSendReviewDueNotification = riskAssessor.DoNotSendReviewDueNotification,
                DoNotSendTaskCompletedNotifications = riskAssessor.DoNotSendTaskCompletedNotifications,
                DoNotSendTaskOverdueNotifications = riskAssessor.DoNotSendTaskOverdueNotifications,
                FormattedName = riskAssessor.Employee.FullName,
            });
        }

        private void Validate(AddEditRiskAssessorViewModel viewModel)
        {
            if (viewModel.HasAccessToAllSites && viewModel.SiteId != null)
            {
                ModelState.AddModelError("Site", "Please select either All Sites or an individual site");
            }
            if (viewModel.HasAccessToAllSites == false && viewModel.SiteId == null)
            {
                ModelState.AddModelError("Site", "Please select a Site");
            }
            if (viewModel.EmployeeId == null)
            {
                ModelState.AddModelError("Employee", "Please select an Employee");
            }
        }

        private long CreateNewRiskAssessor(AddEditRiskAssessorViewModel viewModel)
        {
            var request = new CreateEditRiskAssessorRequest
                          {
                              CompanyId = CurrentUser.CompanyId,
                              CreatingUserId = CurrentUser.UserId,
                              EmployeeId = viewModel.EmployeeId.Value,
                              SiteId = viewModel.SiteId,
                              DoNotSendTaskOverdueNotifications = viewModel.DoNotSendTaskOverdueNotifications,
                              DoNotSendTaskCompletedNotifications = viewModel.DoNotSendTaskCompletedNotifications,
                              DoNotSendReviewDueNotification = viewModel.DoNotSendReviewDueNotification,
                              HasAccessToAllSites = viewModel.HasAccessToAllSites
                          };

            return _riskAssessorService.Create(request);
        }

        [PermissionFilter(Permissions.AddCompanyDefaults)]
        public JsonResult GetEmployeeJobAndSite(long companyId, Guid? employeeId)
        {
            if (employeeId.HasValue)
            {
                var employee = _employeeService.GetEmployee(employeeId.Value, companyId);
                return Json(new
                                {
                                    JobTitle = employee.JobTitle,
                                    Site = employee.SiteName,
                                    EmployeeNotAUser = (employee.User == null)
                                },
                                JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                                {
                                    JobTitle = "",
                                    Site = "",
                                    EmployeeNotAUser = false
                                },
                                JsonRequestBehavior.AllowGet);
            }
        }

        [PermissionFilter(Permissions.DeleteEmployeeRecords)]
        public JsonResult CanDeleteRiskAssessor(CanDeleteRiskAssessorViewModel viewModel)
        {
            if (viewModel.CompanyId == 0 || viewModel.RiskAssessorId == 0)
            {
                throw new ArgumentException("Invalid riskAssessorId and companyId");
            }

            var result = _riskAssessorService.HasRiskAssessorGotOutstandingRiskAssessments(viewModel.RiskAssessorId, viewModel.CompanyId);

            if (result)
            {
                return Json(new { CanDeleteRiskAssessor = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { CanDeleteRiskAssessor = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(Permissions.DeleteEmployeeRecords)]
        public JsonResult MarkDeleted(long companyId, long riskAssessorId)
        {
            if (companyId <= 0 || riskAssessorId <= 0)
            {
                throw new ArgumentException("Invalid riskAssessorId or companyId when trying to mark risk assessor as deleted.");
            }

            _riskAssessorService.MarkDeleted(new MarkRiskAssessorAsDeletedAndUndeletedRequest()
            {
                RiskAssessorId = riskAssessorId,
                CompanyId = companyId,
                UserId = CurrentUser.UserId
            });

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditEmployeeRecords)]
        public JsonResult MarkUndeleted(long companyId, long riskAssessorId)
        {
            if (companyId <= 0 || riskAssessorId <= 0)
            {
                throw new ArgumentException("Invalid riskAssessorId or companyId when trying to mark risk assessor as reinstated.");
            }
            _riskAssessorService.MarkUndeleted(new MarkRiskAssessorAsDeletedAndUndeletedRequest()
            {
                RiskAssessorId = riskAssessorId,
                CompanyId = companyId,
                UserId = CurrentUser.UserId
            });

            return Json(new { Success = true });
        }
    }
}

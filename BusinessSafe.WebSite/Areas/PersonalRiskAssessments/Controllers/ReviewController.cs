using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using BusinessSafe.Application.Request;
using System;
using NServiceBus;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Messages.Events;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers
{
    [PersonalRiskAssessmentCurrentTabActionFilter(PersonalRiskAssessmentTabs.Review)]
    [PersonalRiskAssessmentContextFilter]
    public class ReviewController : BaseController
    {
        private readonly IRiskAssessmentService _riskAssessmentService;
        private readonly ICompleteReviewViewModelFactory _completeReviewViewModelFactory;
        private readonly IPersonalRiskAsessmentReviewsViewModelFactory _personalRiskAssessmentReviewsViewModelFactory;
        private readonly IRiskAssessmentReviewService _riskAssessmentReviewService;
        private readonly IPersonalRiskAssessmentService _personalRiskAssessmentService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;
        private readonly IBus _bus;

        public ReviewController(
            IRiskAssessmentService riskAssessmentService,
            ICompleteReviewViewModelFactory completeReviewViewModelFactory,
            IPersonalRiskAsessmentReviewsViewModelFactory personalRiskAssessmentReviewsViewModelFactory,
            IRiskAssessmentReviewService riskAssessmentReviewService,
            IPersonalRiskAssessmentService personalRiskAssessmentService,
            IBusinessSafeSessionManager businessSafeSessionManager,
            IBus bus)
        {
            _riskAssessmentService = riskAssessmentService;
            _completeReviewViewModelFactory = completeReviewViewModelFactory;
            _personalRiskAssessmentReviewsViewModelFactory = personalRiskAssessmentReviewsViewModelFactory;
            _riskAssessmentReviewService = riskAssessmentReviewService;
            _personalRiskAssessmentService = personalRiskAssessmentService;
            _businessSafeSessionManager = businessSafeSessionManager;
            _bus = bus;
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult View(long riskAssessmentId, long companyId)
        {
            IsReadOnly = true;

            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewPersonalRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var model = _personalRiskAssessmentReviewsViewModelFactory
                .WithCompanyId(companyId)
                .WithRiskAssessmentId(riskAssessmentId)
                .WithUser(CurrentUser)
                .GetViewModel();

            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditPersonalRiskAssessments)]
        public PartialViewResult Complete(long companyId, long riskAssessmentId, long riskAssessmentReviewId)
        {
            bool hasUncompletedTasks = _riskAssessmentService.HasUncompletedTasks(companyId, riskAssessmentId);

            var viewModel = _completeReviewViewModelFactory
                .WithCompanyId(companyId)
                .WithReviewId(riskAssessmentReviewId)
                .WithHasUncompletedTasks(hasUncompletedTasks)
                .WithRiskAssessmentType(RiskAssessmentType.PRA)
                .GetViewModel();

            return PartialView("_CompleteRiskAssessmentReview", viewModel);
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        [ILookAfterOwnTransactionFilter]
        public JsonResult SaveRiskAssessmentReview(AddEditRiskAssessmentReviewViewModel viewModel)
        {
            if (!viewModel.RiskAssessmentReviewId.HasValue)
            {

                return AddRiskAssessmentReview(viewModel);
            }
            else
            {
                return EditRiskAssessmentReview(viewModel);
            }
        }

        private JsonResult EditRiskAssessmentReview(AddEditRiskAssessmentReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            bool userCantAccessPersonalRiskAssessment = false;

            using (var session = _businessSafeSessionManager.Session)
            {
                _riskAssessmentReviewService.Edit(new EditRiskAssessmentReviewRequest
                {
                    RiskAssessmentReviewId = viewModel.RiskAssessmentReviewId.Value,
                    AssigningUserId = CurrentUser.UserId,
                    CompanyId = viewModel.CompanyId,
                    ReviewDate = DateTime.Parse(viewModel.ReviewDate),
                    ReviewingEmployeeId = viewModel.ReviewingEmployeeId
                });

                userCantAccessPersonalRiskAssessment = !_personalRiskAssessmentService.CanUserAccess(viewModel.RiskAssessmentId,
                    viewModel.CompanyId, CurrentUser.UserId);

                _businessSafeSessionManager.CloseSession();

                var riskAssessmentReview = _riskAssessmentReviewService.GetByIdAndCompanyId(
                                                                viewModel.RiskAssessmentReviewId.Value,
                                                                CurrentUser.CompanyId
                                                                );

                _bus.Publish(new ReviewAssigned { TaskGuid = riskAssessmentReview.RiskAssessmentReviewTask.TaskGuid });
            }


            return Json(new { Success = true, userCantAccessPersonalRiskAssessment });
        }

        private JsonResult AddRiskAssessmentReview(AddEditRiskAssessmentReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            var taskGuid = Guid.NewGuid();

            var userCantAccessPersonalRiskAssessment = false;

            using (var session = _businessSafeSessionManager.Session)
            {
                _riskAssessmentReviewService.Add(new AddRiskAssessmentReviewRequest
                {
                    TaskGuid = taskGuid,
                    CompanyId = viewModel.CompanyId,
                    ReviewDate = DateTime.Parse(viewModel.ReviewDate),
                    ReviewingEmployeeId =
                        viewModel.ReviewingEmployeeId,
                    RiskAssessmentId = viewModel.RiskAssessmentId,
                    AssigningUserId = CurrentUser.UserId,
                    SendTaskNotification = viewModel.DoNotSendTaskNotification
                });

                userCantAccessPersonalRiskAssessment = !_personalRiskAssessmentService.CanUserAccess(viewModel.RiskAssessmentId,
                    viewModel.CompanyId, CurrentUser.UserId);

                _businessSafeSessionManager.CloseSession();
            }

            _bus.Publish(new ReviewAssigned { TaskGuid = taskGuid });

            TempData["Notice"] = "Risk Assessment Review Request Successfully Saved";

            return Json(new { Success = true, userCantAccessPersonalRiskAssessment });
        }
    }
}

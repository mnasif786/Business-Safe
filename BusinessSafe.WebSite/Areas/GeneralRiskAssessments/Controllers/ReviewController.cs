using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Areas.Filters;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
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
using BusinessSafe.Messages.Events;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers
{
    [GeneralRiskAssessmentCurrentTabActionFilter(GeneralRiskAssessmentTabs.Review)]
    [GeneralRiskAssessmentContextFilter]
    public class ReviewController : BaseController
    {
        private readonly IRiskAssessmentService _riskAssessmentService;
        private readonly ICompleteReviewViewModelFactory _completeReviewViewModelFactory;
        private readonly IGeneralRiskAssessmentReviewsViewModelFactory _generalRiskAssessmentReviewsViewModelFactory;

        public ReviewController(
            IGeneralRiskAssessmentReviewsViewModelFactory generalRiskAssessmentReviewsViewModelFactory, 
            IRiskAssessmentService riskAssessmentService,
            ICompleteReviewViewModelFactory completeReviewViewModelFactory)
        {
            _generalRiskAssessmentReviewsViewModelFactory = generalRiskAssessmentReviewsViewModelFactory;
            _riskAssessmentService = riskAssessmentService;
            _completeReviewViewModelFactory = completeReviewViewModelFactory;
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult View(long companyId, long riskAssessmentId)
        {
            IsReadOnly = true;
            return Index(companyId, riskAssessmentId);
        }

        [PermissionFilter(Permissions.ViewGeneralandHazardousSubstancesRiskAssessments)]
        public ActionResult Index(long companyId, long riskAssessmentId)
        {
            var model = _generalRiskAssessmentReviewsViewModelFactory
                                        .WithCompanyId(companyId)
                                        .WithRiskAssessmentId(riskAssessmentId)
                                        .WithUser(CurrentUser)
                                        .GetViewModel();

            return View("Index", model);
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public PartialViewResult Complete(long companyId, long riskAssessmentId, long riskAssessmentReviewId)
        {
            bool hasUncompletedTasks = _riskAssessmentService.HasUncompletedTasks(companyId, riskAssessmentId);

            var viewModel = _completeReviewViewModelFactory
                .WithCompanyId(companyId)
                .WithReviewId(riskAssessmentReviewId)
                .WithHasUncompletedTasks(hasUncompletedTasks)
                .WithRiskAssessmentType(RiskAssessmentType.GRA)
                .GetViewModel();

            return PartialView("_CompleteRiskAssessmentReview", viewModel);
        }

        //[HttpPost]
        //[PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        //[ILookAfterOwnTransactionFilter]
        //public JsonResult SaveRiskAssessmentReview(AddEditRiskAssessmentReviewViewModel viewModel)
        //{
        //    if (!viewModel.RiskAssessmentReviewId.HasValue)
        //    {

        //        return AddRiskAssessmentReview(viewModel);
        //    }
        //    else
        //    {
        //        return EditRiskAssessmentReview(viewModel);
        //    }
        //}

        //private JsonResult EditRiskAssessmentReview(AddEditRiskAssessmentReviewViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

        //    using (var session = _businessSafeSessionManager.Session)
        //    {
        //        _riskAssessmentReviewService.Edit(new EditRiskAssessmentReviewRequest
        //        {
        //            RiskAssessmentReviewId = viewModel.RiskAssessmentReviewId.Value,
        //            AssigningUserId = CurrentUser.UserId,
        //            CompanyId = viewModel.CompanyId,
        //            ReviewDate = DateTime.Parse(viewModel.ReviewDate),
        //            ReviewingEmployeeId = viewModel.ReviewingEmployeeId
        //        });

        //        _businessSafeSessionManager.CloseSession();

        //        var riskAssessmentReview = _riskAssessmentReviewService.GetByIdAndCompanyId(
        //                                                        viewModel.RiskAssessmentReviewId.Value,
        //                                                        CurrentUser.CompanyId
        //                                                        );

        //        _bus.Publish(new ReviewAssigned { TaskGuid = riskAssessmentReview.RiskAssessmentReviewTask.TaskGuid });
        //    }


        //    return Json(new { Success = true });
        //}

        //private JsonResult AddRiskAssessmentReview(AddEditRiskAssessmentReviewViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //        return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

        //    var taskGuid = Guid.NewGuid();

        //    using (var session = _businessSafeSessionManager.Session)
        //    {
        //        _riskAssessmentReviewService.Add(new AddRiskAssessmentReviewRequest
        //        {
        //            TaskGuid = taskGuid,
        //            CompanyId = viewModel.CompanyId,
        //            ReviewDate = DateTime.Parse(viewModel.ReviewDate),
        //            ReviewingEmployeeId =
        //                viewModel.ReviewingEmployeeId,
        //            RiskAssessmentId = viewModel.RiskAssessmentId,
        //            AssigningUserId = CurrentUser.UserId,
        //            SendTaskNotification = viewModel.DoNotSendTaskNotification
        //        });

        //        _businessSafeSessionManager.CloseSession();
        //    }

        //    _bus.Publish(new ReviewAssigned { TaskGuid = taskGuid });

        //    TempData["Notice"] = "Risk Assessment Review Request Successfully Saved";

        //    return Json(new { Success = true });
        //}
    }
}
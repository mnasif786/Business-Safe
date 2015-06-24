using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.Messages.Events;
using BusinessSafe.WebSite.Areas.SqlReports.Factories;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.HtmlHelpers;
using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;
using NServiceBus;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Controllers
{
    public class RiskAssessmentReviewController : BaseController
    {
        private readonly IBus _bus;
        private readonly IRiskAssessmentService _riskAssessmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IRiskAssessmentReviewService _riskAssessmentReviewService;
        private readonly IReviewAuditDocumentHelper _reviewAuditDocumentHelper;
        private readonly IFireRiskAssessmentService _fireRiskAssessmentService;
        private readonly IBusinessSafeSessionManager _businessSafeSessionManager;

        public RiskAssessmentReviewController(
            IRiskAssessmentService riskAssessmentService,
            IEmployeeService employeeService,
            IRiskAssessmentReviewService riskAssessmentReviewService,
            IReviewAuditDocumentHelper reviewAuditDocumentHelper,
            IFireRiskAssessmentService fireRiskAssessmentService,
            IBusinessSafeSessionManager businessSafeSessionManager, 
            IBus bus)
        {
            _riskAssessmentService = riskAssessmentService;
            _employeeService = employeeService;
            _riskAssessmentReviewService = riskAssessmentReviewService;
            _reviewAuditDocumentHelper = reviewAuditDocumentHelper;
            _fireRiskAssessmentService = fireRiskAssessmentService;
            _businessSafeSessionManager = businessSafeSessionManager;
            _bus = bus;
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public PartialViewResult Add(long companyId, long riskAssessmentId)
        {
            var riskAssessment = _riskAssessmentService.GetByIdAndCompanyId(riskAssessmentId, companyId);
            var employees = _employeeService.GetAll(companyId);
            var viewModel = AddEditRiskAssessmentReviewViewModel.CreateFrom(riskAssessment, employees, _riskAssessmentService);

            return PartialView("_AddEditRiskAssessmentReview", viewModel);
        }

        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public PartialViewResult Edit(long companyId, long riskAssessmentReviewId)
        {
            var riskAssessmentReview = _riskAssessmentReviewService.GetByIdAndCompanyId(riskAssessmentReviewId, companyId);
            var employees = _employeeService.GetAll(companyId);
            var viewModel = AddEditRiskAssessmentReviewViewModel.CreateFrom(riskAssessmentReview, employees);

            return PartialView("_AddEditRiskAssessmentReview", viewModel);
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

                _businessSafeSessionManager.CloseSession();

                var riskAssessmentReview = _riskAssessmentReviewService.GetByIdAndCompanyId(
                                                                viewModel.RiskAssessmentReviewId.Value,
                                                                CurrentUser.CompanyId
                                                                );

                _bus.Publish(new ReviewAssigned { TaskGuid = riskAssessmentReview.RiskAssessmentReviewTask.TaskGuid });
            }

            return Json(new { Success = true });
        }

        private JsonResult AddRiskAssessmentReview(AddEditRiskAssessmentReviewViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            var taskGuid = Guid.NewGuid();

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

                _businessSafeSessionManager.CloseSession();
            }

            _bus.Publish(new ReviewAssigned { TaskGuid = taskGuid });
            
            TempData["Notice"] = "Risk Assessment Review Request Successfully Saved";

            return Json(new { Success = true });
        }

        [HttpPost]
        [PermissionFilter(Permissions.EditGeneralandHazardousSubstancesRiskAssessments)]
        public JsonResult Complete(CompleteReviewViewModel viewModel)
        {

            if (!viewModel.Archive && !viewModel.NextReviewDate.HasValue)
                ModelState.AddModelError("", "Either a valid Next Review Date must be entered or Archive must be checked.");

            if (!ModelState.IsValid)
                return Json(new { Success = false, Errors = ModelState.GetErrorMessages() });

            var riskAssessmentReview = _riskAssessmentReviewService.GetByIdAndCompanyId(
                                                                viewModel.RiskAssessmentReviewId,
                                                                CurrentUser.CompanyId
                                                                );

            var riskAssessment = riskAssessmentReview.RiskAssessment;


            var documentResult = CreateReviewAuditDocument(viewModel.RiskAssessmentType, riskAssessment);

            var completeRiskAssessmentReviewRequest = CreateCompleteRiskAssessmentReviewRequest(
                                                                                viewModel,
                                                                                riskAssessment,
                                                                                documentResult.NewFileName,
                                                                                documentResult.DocumentLibraryId,
                                                                                documentResult.DocumentType);


            if (viewModel.RiskAssessmentType == RiskAssessmentType.FRA)
            {
                _fireRiskAssessmentService.CompleteFireRiskAssessementReview(completeRiskAssessmentReviewRequest);
            }
            else
            {
                _riskAssessmentReviewService.CompleteRiskAssessementReview(completeRiskAssessmentReviewRequest);
            }

            if (riskAssessmentReview.RiskAssessment.RiskAssessor != null
                && riskAssessmentReview.RiskAssessment.RiskAssessor.Employee.MainContactDetails != null 
                && !string.IsNullOrEmpty(riskAssessmentReview.RiskAssessment.RiskAssessor.Employee.MainContactDetails.Email))
            {
                var task = riskAssessmentReview.RiskAssessmentReviewTask;
                _bus.Send(new SendReviewCompletedEmail
                              {
                                  TaskReference = task.RiskAssessment.Reference,
                                  Title = task.RiskAssessment.Title,
                                  Description = task.Description,
                                  RiskAssessorName = riskAssessmentReview.RiskAssessment.RiskAssessor.Employee.FullName,
                                  RiskAssessorEmail =
                                      riskAssessmentReview.RiskAssessment.RiskAssessor.Employee.MainContactDetails !=
                                      null
                                          ? riskAssessmentReview.RiskAssessment.RiskAssessor.Employee.MainContactDetails
                                                .Email
                                          : null,
                                  TaskAssignedTo = task.TaskAssignedTo != null ? task.TaskAssignedTo.FullName : string.Empty
                              });
            }

            return Json(new { Success = true });
        }

        private CompleteRiskAssessmentReviewRequest CreateCompleteRiskAssessmentReviewRequest(CompleteReviewViewModel viewModel,
            RiskAssessmentDto riskAssessment, string newFileName, long documentLibraryId, DocumentTypeEnum documentTypeEnum)
        {
            var completeRiskAssessmentReviewRequest = new CompleteRiskAssessmentReviewRequestBuilder()
                .WithClientId(CurrentUser.CompanyId)
                .WithUserId(CurrentUser.UserId)
                .WithCompleteReview_completeReviewViewModel(viewModel);

            if (documentLibraryId != default(long))
            {
                var requestBuilder = new CreateDocumentRequestBuilder()
                    .WithCompanyId(CurrentUser.CompanyId)
                    .WithDocumentOriginTypeId(DocumentOriginType.TaskCompleted)
                    .WithDocumentType(documentTypeEnum)
                    .WithFilename(newFileName)
                    .WithDocumentLibraryId(documentLibraryId);

                if (riskAssessment.RiskAssessmentSite != null)
                    requestBuilder.WithSiteId(riskAssessment.RiskAssessmentSite.Id);

                var createDocumentRequest = requestBuilder.Build();

                completeRiskAssessmentReviewRequest.WithCreateDocumentRequest(createDocumentRequest);
            }

            return completeRiskAssessmentReviewRequest.Build();
        }

        public virtual ReviewAuditDocumentResult CreateReviewAuditDocument(RiskAssessmentType riskAssessmentType, RiskAssessmentDto riskAssessment)
        {
            FeatureSwitches featureSwitch = SqlReportHelper.GetSqlReportFeatureSwitch(riskAssessmentType);
            var isReportAvailable = FeatureSwitchEnabled(featureSwitch, User.GetCustomPrinciple());
            var doesReportDefinitionExist = SqlReportDefinitionFactory.GetSqlReportDefinitions().Exists(r => r.Report == SqlReportHelper.GetSqlReportType(riskAssessmentType));

            if (!isReportAvailable || !doesReportDefinitionExist)
            {
                return new ReviewAuditDocumentResult();
            }

            return _reviewAuditDocumentHelper.CreateReviewAuditDocument(riskAssessmentType, riskAssessment);

        }

        public virtual bool FeatureSwitchEnabled(FeatureSwitches featureSwitch, ICustomPrincipal customPrincipal)
        {
            return FeatureSwitchChecker.Enabled(featureSwitch, customPrincipal);
        }
    }

    public class ReviewAuditDocumentResult
    {
        public long DocumentLibraryId { get; set; }
        public string NewFileName { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }
    }
}

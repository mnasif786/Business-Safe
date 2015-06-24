using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Employee;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Emails.Commands;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.HtmlHelpers;
using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NServiceBus;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.RiskAssessmentReview
{
    public class MyRiskAssessmentReviewController : RiskAssessmentReviewController
    {
        public MyRiskAssessmentReviewController(IRiskAssessmentService riskAssessmentService, IEmployeeService employeeService,
             IRiskAssessmentReviewService riskAssessmentReviewService, IReviewAuditDocumentHelper reviewAuditDocumentHelper,IFireRiskAssessmentService fireRiskAssessmentService
            , IBusinessSafeSessionManager businessSafeSessionManager, IBus bus)
            : base(riskAssessmentService, employeeService, riskAssessmentReviewService, reviewAuditDocumentHelper, fireRiskAssessmentService, businessSafeSessionManager, bus)
        {
            IsSqlReportAvailable = true;
        }

        public bool IsSqlReportAvailable { get; set; }

        public override ReviewAuditDocumentResult CreateReviewAuditDocument(RiskAssessmentType riskAssessmentType, RiskAssessmentDto riskAssessment)
        {
            if (IsSqlReportAvailable)
                return base.CreateReviewAuditDocument(riskAssessmentType, riskAssessment);

            return new ReviewAuditDocumentResult();
        }
    }

    [TestFixture]
    public class CompleteTests : BaseRiskAssessmentReviewTest
    {
        private RiskAssessmentReviewDto _raReviewDto;

        public MyRiskAssessmentReviewController GetTarget()
        {
            var result = new MyRiskAssessmentReviewController(
                _riskAssessmentService.Object,
                _employeeService.Object,
                _riskAssessmentReviewService.Object,
                _reviewAuditService.Object,
                _fireRiskAssessmentService.Object,
                _businessSafeSessionManager.Object,
                _bus.Object
                );

            return TestControllerHelpers.AddUserToController(result);
        }

        [SetUp]
        public void Setup()
        {
            base.SetUp();

            _raReviewDto = new RiskAssessmentReviewDto()
                               {
                                   RiskAssessment = new RiskAssessmentDto()
                                                        {
                                                            Title = "title",
                                                            Reference = "ref",
                                                            RiskAssessmentSite = new SiteStructureElementDto() {Id = 1234}
                                                        }
                               };

            _riskAssessmentReviewService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(_raReviewDto);

        }

        [Test]
        public void Given_that_complete_is_called_When_required_fields_are_supplied_Then_json_success_returned()
        {
            //Given
            var target = GetTarget();
            var validModel = new CompleteReviewViewModel()
            {
                CompanyId = 123,
                RiskAssessmentReviewId = 789,
                IsComplete = true,
                CompletedComments = "complete",
                NextReviewDate = DateTime.Now.AddYears(1),
                RiskAssessmentType = RiskAssessmentType.GRA
            };
            
            //When
            var result = target.Complete(validModel);
            dynamic data = result.Data;

            //Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
            Assert.That(data.Success, Is.EqualTo(true));
        }

        [Test]
        public void Given_that_complete_is_called_When_required_fields_are_not_supplied_Then_json_object_is_returned()
        {
            //Given
            var target = GetTarget();
            target.ModelState.AddModelError("", "There is an error in your submission");

            //When
            var result = target.Complete(new CompleteReviewViewModel()
            {
                IsComplete = false
            });

            dynamic data = result.Data;

            //Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
            Assert.That(data.Success, Is.EqualTo(false));
        }

        [Test]
        public void Given_that_complete_is_called_When_required_fields_are_not_supplied_Then_call_to_service_to_complete_review_is_not_made()
        {
            //Given
            var target = GetTarget();
            target.ModelState.AddModelError("", "There is an error in your submission");

            _riskAssessmentReviewService
                .Setup(x => x.CompleteRiskAssessementReview(It.IsAny<CompleteRiskAssessmentReviewRequest>()));

            //When
            target.Complete(new CompleteReviewViewModel()
                                {
                                    IsComplete = false
                                });

            //Then
            _riskAssessmentReviewService.Verify(x => x.CompleteRiskAssessementReview(It.IsAny<CompleteRiskAssessmentReviewRequest>()), Times.Never());
        }

        [Test]
        public void Given_that_complete_is_called_When_required_fields_are_supplied_Then_current_user_is_added_to_request_model()
        {
            //Given
            var target = GetTarget();
            var validModel = new CompleteReviewViewModel()
            {
                CompanyId = 123,
                RiskAssessmentId = 456,
                RiskAssessmentReviewId = 789,
                IsComplete = true,
                CompletedComments = "complete",
                NextReviewDate = DateTime.Today,
                RiskAssessmentType = RiskAssessmentType.GRA
            };
            var modelPassedToService = new CompleteRiskAssessmentReviewRequest();
            _riskAssessmentReviewService
                .Setup(x => x.CompleteRiskAssessementReview(It.IsAny<CompleteRiskAssessmentReviewRequest>()))
                .Callback<CompleteRiskAssessmentReviewRequest>(y => modelPassedToService = y);



            //When
            target.Complete(validModel);

            //Then
            _riskAssessmentReviewService.VerifyAll();
            Assert.That(modelPassedToService.ReviewingUserId.ToString(), Is.Not.EqualTo("00000000-0000-0000-0000-000000000000"));
        }
        
        [Test]
        [TestCase(1, 999)]
        public void Given_map_file_from_sql_report_service_Then_CreateCompleteRiskAssessmentReviewRequest_Then_values_are_valid(long riskAssessmentId, long reviewId)
        {
            // Given
            var completeReviewViewModel = new CompleteReviewViewModel()
                                              {
                                                  CompanyId = TestControllerHelpers.CompanyIdAssigned, 
                                                  RiskAssessmentId = riskAssessmentId, 
                                                  RiskAssessmentReviewId = reviewId, 
                                                  IsComplete = true, 
                                                  CompletedComments = "complete", 
                                                  NextReviewDate = DateTime.Today, 
                                                  RiskAssessmentType = RiskAssessmentType.GRA,
                                              };

            var target = GetTarget();

            //When
            target.Complete(completeReviewViewModel);

            
            //Then
            _riskAssessmentReviewService
                .Verify(x => x.CompleteRiskAssessementReview(It.Is<CompleteRiskAssessmentReviewRequest>(y => y.RiskAssessmentId == completeReviewViewModel.RiskAssessmentId && 
                                                                                                             y.ClientId == completeReviewViewModel.CompanyId && 
                                                                                                             y.CompletedComments == completeReviewViewModel.CompletedComments && 
                                                                                                             y.IsComplete == completeReviewViewModel.IsComplete &&
                                                                                                             y.RiskAssessmentReviewId == completeReviewViewModel.RiskAssessmentReviewId &&
                                                                                                             y.NextReviewDate == completeReviewViewModel.NextReviewDate)));
        }
        
        [Test]
        [TestCase(RiskAssessmentType.GRA, "SqlReportsForGRA", true, SqlReportHelper.ReportType.GRA)]
        [TestCase(RiskAssessmentType.HSRA, "SqlReportsForHSRA", false, SqlReportHelper.ReportType.HSRA)]
        [TestCase(RiskAssessmentType.PRA, "SqlReportsForPRA", false, SqlReportHelper.ReportType.PRA)]
        [TestCase(RiskAssessmentType.FRA, "SqlReportsForFRA", false, SqlReportHelper.ReportType.FRA)]
        public void When_complete_is_called_Then_check_sql_feature_is_available_then_call_GetSqlReport_documentLibraryUploader_Upload(RiskAssessmentType raType, string configKeyName, bool featureAvailable, SqlReportHelper.ReportType reportType)
        {
            // Given
            var model = new CompleteReviewViewModel()
            {
                CompanyId = TestControllerHelpers.CompanyIdAssigned,
                RiskAssessmentId = 123,
                RiskAssessmentReviewId = 456,
                IsComplete = true,
                CompletedComments = "complete",
                NextReviewDate = DateTime.Today,
                RiskAssessmentType = raType
            };

            var target = GetTarget();
            target.IsSqlReportAvailable = raType == RiskAssessmentType.GRA;

            // When
            target.Complete(model);

            //Then
            var timesToCall = featureAvailable ? Times.Once() : Times.Never();
            
            _reviewAuditService.Verify(x => x.CreateReviewAuditDocument(raType, It.IsAny<RiskAssessmentDto>()), timesToCall);
        }

        [Test]
        [Ignore]
        public void Given_the_FRA_report_defintion_doesnt_exist_and_SqlReportFeatureSwitched_on_when_CreateReviewAuditDocument_then_ReviewAuditDocument_is_not_created()
        {
            //Given
            var target = new Mock<RiskAssessmentReviewController>(new object[] { _riskAssessmentService.Object, _employeeService.Object, _riskAssessmentReviewService.Object, _fireRiskAssessmentService.Object, _reviewAuditService.Object,_fireRiskAssessmentService.Object, _businessSafeSessionManager.Object }) { CallBase = true };
            target.Setup(x => x.FeatureSwitchEnabled(It.IsAny<FeatureSwitches>(), It.IsAny<ICustomPrincipal>()))
                .Returns(true);

            //When
            target.Object.CreateReviewAuditDocument(RiskAssessmentType.FRA, null);

            //Then
            _reviewAuditService.Verify(x => x.CreateReviewAuditDocument(It.IsAny<RiskAssessmentType>(), It.IsAny<RiskAssessmentDto>()), Times.Exactly(0));
        }

        [Test]
        public void Given_the_FRA_report_definition_doesnt_exist_and_SqlReportFeatureSwitched_off_when_CreateReviewAuditDocument_then_ReviewAuditDocument_is_not_created()
        {
            //Given
            var target = new Mock<RiskAssessmentReviewController>(
                new object[] 
                { 
                    _riskAssessmentService.Object, 
                    _employeeService.Object, 
                    _riskAssessmentReviewService.Object, 
                    _reviewAuditService.Object,
                    _fireRiskAssessmentService.Object,  
                    _businessSafeSessionManager.Object,
                    _bus.Object }) { CallBase = true };
            target.Setup(x => x.FeatureSwitchEnabled(It.IsAny<FeatureSwitches>(), It.IsAny<ICustomPrincipal>()))
                .Returns(false);

            //When
            target.Object.CreateReviewAuditDocument(RiskAssessmentType.FRA, null);

            //Then
            _reviewAuditService.Verify(x => x.CreateReviewAuditDocument(It.IsAny<RiskAssessmentType>(), It.IsAny<RiskAssessmentDto>()), Times.Exactly(0));
        }

        [Test]
        public void Given_a_valid_CompleteReviewViewModel_When_completing_a_review_Then_the_correct_command_is_created_and_sent()
        {
            //Given
            _raReviewDto.RiskAssessment.RiskAssessor = new RiskAssessorDto() { Id = 243L, Employee = new EmployeeDto { FullName = "ReviewAssignedTo full name", MainContactDetails = new EmployeeContactDetailDto { Email = "ReviewAssignedTo@test.com" } } };
            _raReviewDto.ReviewAssignedTo = new EmployeeDto() { MainContactDetails = new EmployeeContactDetailDto { Email = "ReviewAssignedTo@test.com" }, FullName = "ReviewAssignedTo full name" };
            _raReviewDto.RiskAssessmentReviewTask = new RiskAssessmentReviewTaskDto()
                                                        {
                                                            TaskAssignedTo = new EmployeeDto {MainContactDetails = new EmployeeContactDetailDto{ Email = "TaskAssignedToDto@test.com"}, FullName = "TaskAssignedToDto name"}
                                                            ,
                                                            RiskAssessment = _raReviewDto.RiskAssessment
                                                        };


            var validModel = new CompleteReviewViewModel()
                                 {
                                     CompanyId = 123,
                                     RiskAssessmentReviewId = 789,
                                     IsComplete = true,
                                     CompletedComments = "complete",
                                     NextReviewDate = DateTime.Now.AddYears(1),
                                     RiskAssessmentType = RiskAssessmentType.GRA,
                                 };



            var param = new object[0];
            SendReviewCompletedEmail emailCommand = null;
            _bus.Setup(x => x.Send(It.IsAny<SendReviewCompletedEmail>()))
                .Callback<object[]>(x => emailCommand = (SendReviewCompletedEmail) x[0]);

            var target = GetTarget();

            //When
            var result = target.Complete(validModel);

            //Then
            _bus.Verify(x => x.Send(It.IsAny<SendReviewCompletedEmail>()), Times.Once());

            Assert.AreEqual(_raReviewDto.RiskAssessment.Title, emailCommand.Title);
            Assert.AreEqual(_raReviewDto.RiskAssessment.Reference, emailCommand.TaskReference);
            Assert.AreEqual(_raReviewDto.RiskAssessmentReviewTask.Description, emailCommand.Description);
            Assert.AreEqual(_raReviewDto.RiskAssessment.RiskAssessor.Employee.FullName, emailCommand.RiskAssessorName);
            Assert.AreEqual(_raReviewDto.RiskAssessmentReviewTask.TaskAssignedTo.FullName, emailCommand.TaskAssignedTo);

        }
    }
}
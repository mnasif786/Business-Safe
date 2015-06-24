using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

using Moq;

using NServiceBus;

using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistGeneratorTests
{
    [TestFixture]
    public class SaveAndNextTests
    {
        private IEmployeeChecklistGeneratorViewModelFactory _checklistGeneratorViewModelFactory;
        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        
        [SetUp]
        public void Setup()
        {
            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            _bus = new Mock<IBus>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void Correct_result_is_returned()
        {
            // Given
            var target = GetTarget();

            var viewModel = new EmployeeChecklistGeneratorViewModel()
            {
                Message = "Hello Message",
                ChecklistsToGenerate = new ChecklistsToGenerateViewModel()
                {
                    ChecklistIds = new List<long>() { 1, 2, 3 },
                    RequestEmployees = new List<EmployeeWithNewEmailRequest>() { new EmployeeWithNewEmailRequest() }
                },
                RiskAssessmentId = 500,

            };

            // When
            var result = target.SaveAndNext(viewModel) as JsonResult;


            // Then
            Assert.That(result, Is.TypeOf<JsonResult>());
        }

        [Test]
        public void Correct_methods_are_called()
        {
            // Given
            var target = GetTarget();

            var viewModel = new EmployeeChecklistGeneratorViewModel()
                                {
                                    Message = "Hello Message",
                                    ChecklistsToGenerate = new ChecklistsToGenerateViewModel()
                                                               {
                                                                   ChecklistIds = new List<long>(){1,2,3},
                                                                   RequestEmployees = new List<EmployeeWithNewEmailRequest>(){new EmployeeWithNewEmailRequest()}
                                                               },
                                    RiskAssessmentId = 500,
                                    
                                };

            var userId = target.CurrentUser.UserId;

            // When
            target.SaveAndNext(viewModel);

            // Then
            _personalRiskAssessmentService
                .Verify(x =>x.SaveChecklistGenerator(It.Is<SaveChecklistGeneratorRequest>(y => y.Message == viewModel.Message &&
                                                                                               y.ChecklistIds == viewModel.ChecklistsToGenerate.ChecklistIds &&
                                                                                               y.CurrentUserId == userId &&
                                                                                               y.PersonalRiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                               y.RequestEmployees ==viewModel.ChecklistsToGenerate.RequestEmployees)));

        }

        private ChecklistGeneratorController GetTarget()
        {
            var controller = new ChecklistGeneratorController(
                _checklistGeneratorViewModelFactory,
               null,
                _personalRiskAssessmentService.Object,
                null,
                _bus.Object,
                _businessSafeSessionManager.Object);

            return TestControllerHelpers.AddUserToController(controller);
        }

    }
}

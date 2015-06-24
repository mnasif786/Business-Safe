using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Messages.Commands;
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
    public class GenerateTestsTests
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
                CompanyId = 1500
            };

            // When
            var result = target.Generate(viewModel) as RedirectToRouteResult;


            // Then
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["RiskAssessmentId"], Is.EqualTo(viewModel.RiskAssessmentId));
            Assert.That(result.RouteValues["CompanyId"], Is.EqualTo(viewModel.CompanyId));
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
                    ChecklistIds = new List<long>() { 1, 2, 3 },
                    RequestEmployees = new List<EmployeeWithNewEmailRequest>() { new EmployeeWithNewEmailRequest() }
                },
                RiskAssessmentId = 500,
                CompanyId = 1500
            };

            var userId = target.CurrentUser.UserId;

            // When
            target.Generate(viewModel);


            // Then
            _personalRiskAssessmentService.Verify(x => x.SetAsGenerating(viewModel.RiskAssessmentId));

            _personalRiskAssessmentService.Verify(x => x.ResetChecklistAfterGenerate(It.Is<ResetAfterChecklistGenerateRequest>(y => y.CurrentUserId == userId && 
                                                                                                                                    y.PersonalRiskAssessmentId == viewModel.RiskAssessmentId &&
                                                                                                                                    y.CompanyId == viewModel.CompanyId)));

            _businessSafeSessionManager.Verify(x => x.CloseSession(), Times.Exactly(1));
            _bus.Verify(x => x.Send(It.IsAny<GenerateEmployeeChecklistEmails>()));
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

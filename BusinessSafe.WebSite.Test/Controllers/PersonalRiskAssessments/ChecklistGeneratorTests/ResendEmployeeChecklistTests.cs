using System;
using System.Web.Mvc;

using BusinessSafe.Messages.Commands;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

using Moq;

using NServiceBus;

using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.ChecklistGeneratorTests
{
    [TestFixture]
    public class ResendEmployeeChecklistTests
    {
        private Mock<IBus> _bus;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;
        
        [SetUp]
        public void Setup()
        {
            _bus = new Mock<IBus>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void When_ResendEmployeeChecklist_Then_should_call_correct_methods()
        {
            // Given
            var target = GetTarget();

            var viewModel = new ResendEmployeeChecklistViewModel()
                                {
                                    RiskAssessmentId = 200,
                                    EmployeeChecklistId = Guid.NewGuid()
                                };

            var userId = target.CurrentUser.UserId;

            // When
            target.ResendEmployeeChecklist(viewModel);
            

            // Then
            _bus.Verify(
                x =>
                x.Send(
                    It.Is<ResendEmployeeChecklistEmail>(
                        y =>
                        y.EmployeeChecklistId == viewModel.EmployeeChecklistId &&
                        y.RiskAssessmentId == viewModel.RiskAssessmentId && y.ResendUserId == userId)));
        }

        [Test]
        public void When_ResendEmployeeChecklist_Then_should_return_correct_result()
        {
            // Given
            var target = GetTarget();

            var viewModel = new ResendEmployeeChecklistViewModel()
            {
                RiskAssessmentId = 200,
                EmployeeChecklistId = Guid.NewGuid()
            };

            var userId = target.CurrentUser.UserId;

            // When
            var result = target.ResendEmployeeChecklist(viewModel) as JsonResult;


            // Then
            Assert.That(result.Data.ToString(), Is.EqualTo("{ Success = True }"));
        }

        

        private ChecklistGeneratorController GetTarget()
        {
            var controller = new ChecklistGeneratorController(
                null,
                null,
                null,
                null,
                _bus.Object,
                _businessSafeSessionManager.Object);

            return TestControllerHelpers.AddUserToController(controller);
        }

    }
}
using BusinessSafe.Application.Contracts.ActionPlan;
using BusinessSafe.WebSite.Areas.ActionPlans.Controllers;
using BusinessSafe.WebSite.Areas.ActionPlans.ViewModels;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.WebSite.Tests.Controllers.ActionPlans
{
    [TestFixture]
    public class CompleteTaskTests
    {
        private Mock<IActionTaskService> _actionTaskService;
        private Mock<IBusinessSafeSessionManager> _businessSafeSessionManager;

        [SetUp]
        public void Setup()
        {
            _actionTaskService = new Mock<IActionTaskService>();
            _businessSafeSessionManager = new Mock<IBusinessSafeSessionManager>();
        }

        [Test]
        public void When_Complete_Then_tell_service_to_send_notification_email()
        {
            // Given
            var target = GetTarget();
            var taskId = 123L;
            var companyId = 1234L;
            var model = new CompleteActionTaskViewModel
                            {
                                ActionTaskId = taskId,
                                CompanyId = companyId
                            };

            // When
            target.Complete(model, new DocumentsToSaveViewModel());

            // Then
            _actionTaskService.Verify(x => x.SendTaskCompletedNotificationEmail(It.Is<long>(a => a == taskId),It.Is<long>(b=>b==companyId)));
        }


        private ImmediateRiskNotificationsActionsController GetTarget()
        {
            var controller = new ImmediateRiskNotificationsActionsController(null, null, null, null, _actionTaskService.Object, null, _businessSafeSessionManager.Object);
            TestControllerHelpers.AddUserToController(controller);
            return controller;
        }
    }
}

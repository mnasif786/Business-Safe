
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities
{
    [TestFixture]
    public class CheckCanBeDeletedTests
    {
        private ResponsibilityController _target;
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private const long _responsibilityId = 1234L;

        [SetUp]
        public void Setup()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilitiesService
                .Setup(x => x.HasUndeletedTasks(_responsibilityId, TestControllerHelpers.CompanyIdAssigned))
                .Returns(false);

        }

        [Test]
        public void When_CheckCanBeDeleted_Then_ask_service_if_can()
        {
            // Given
            _target = GetTarget();

            // When
            _target.CheckCanBeDeleted(_responsibilityId);

            // Then
            _responsibilitiesService
                .Verify(x => x.HasUndeletedTasks(_responsibilityId, TestControllerHelpers.CompanyIdAssigned));
        }

        [Test]
        public void Given_responsibility_has_no_undeleted_tasks_When_CheckCanBeDeleted_Then_return_hasUndeletedTasks_false()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.CheckCanBeDeleted(_responsibilityId);

            // Then
            Assert.IsFalse(result.Data.hasUndeletedTasks);
        }

        [Test]
        public void Given_responsibility_does_have_undeleted_tasks_When_CheckCanBeDeleted_Then_return_hasUndeletedTasks_true()
        {
            // Given
            _responsibilitiesService
                .Setup(x => x.HasUndeletedTasks(_responsibilityId, TestControllerHelpers.CompanyIdAssigned))
                .Returns(true);
            _target = GetTarget();

            // When
            dynamic result = _target.CheckCanBeDeleted(_responsibilityId);

            // Then
            Assert.IsTrue(result.Data.hasUndeletedTasks);
        }

        [Test]
        public void When_CheckCanBeDeleted_Then_allow_json_get()
        {
            // Given
            _target = GetTarget();

            // When
            JsonResult result = _target.CheckCanBeDeleted(_responsibilityId);

            // Then
            Assert.That(result.JsonRequestBehavior, Is.EqualTo(JsonRequestBehavior.AllowGet));
        }

        private ResponsibilityController GetTarget()
        {
            var controller = new ResponsibilityController(_responsibilitiesService.Object, null, null, null, null, null, null, null, null, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}

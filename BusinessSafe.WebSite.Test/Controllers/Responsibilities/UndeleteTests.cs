

using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.WebSite.Areas.Responsibilities.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Responsibilities
{
    [TestFixture]
    public class UndeleteTests
    {
        private ResponsibilityController _target;
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private const long _responsibilityId = 1234L;

        [SetUp]
        public void Setup()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _responsibilitiesService
                .Setup(x => x.Undelete(_responsibilityId, TestControllerHelpers.CompanyIdAssigned, TestControllerHelpers.UserIdAssigned));
        }

        [Test]
        public void When_Undelete_Then_ask_service_to_undelete()
        {
            // Given
            _target = GetTarget();

            // When
            _target.Undelete(_responsibilityId);

            // Then
            _responsibilitiesService
                .Verify(x => x.Undelete(_responsibilityId, TestControllerHelpers.CompanyIdAssigned, TestControllerHelpers.UserIdAssigned));
        }

        [Test]
        public void Given_success_When_Undelete_Then_return_success()
        {
            // Given
            _target = GetTarget();

            // When
            dynamic result = _target.Undelete(_responsibilityId);

            // Then
            Assert.IsTrue(result.Data.success);
        }

        private ResponsibilityController GetTarget()
        {
            var controller = new ResponsibilityController(_responsibilitiesService.Object, null, null, null, null, null, null, null, null, null, null);
            return TestControllerHelpers.AddUserToController(controller);
        }
    }
}

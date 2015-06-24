using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.DescriptionController
{
    [TestFixture]
    [Category("Unit")]
    public class ViewTests
    {
        private Mock<IHazardousSubstanceDescriptionViewModelFactory> _viewModelFactory;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;

        [SetUp]
        public void SetUp()
        {
            _viewModelFactory = new Mock<IHazardousSubstanceDescriptionViewModelFactory>();

            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_viewModelFactory.Object);

        }

        [Test]
        public void When_view_hazardous_description_view_page_Then_should_set_correct_is_read_only_viewbag_property()
        {
            // Given
            var controller = GetTarget();

            // When
            controller.View(_riskAssessmentId, _companyId);

            // Then
            Assert.That(controller.ViewBag.IsReadOnly, Is.True);
        }

        private WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.DescriptionController GetTarget()
        {
            var target = new WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers.DescriptionController(null, _viewModelFactory.Object);
            return TestControllerHelpers.AddUserToController(target);
        }
    }
}
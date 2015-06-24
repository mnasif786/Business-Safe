
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FurtherActionTask
{
    [TestFixture]
    public class ViewTests
    {
        private const long companyId = 200;
        private const long furtherControlMeasureTaskId = 500;
        private Mock<IEditFurtherControlMeasureTaskViewModelFactory> _addEditFurtherControlMeasureTaskViewModelFactory;

        private Mock<IAddFurtherControlMeasureTaskViewModelFactory>
            _addGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory;

        private Mock<ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>
            _completeFurtherControlMeasureTaskViewModelFactory;

        private Mock<IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory>
            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory;

        [SetUp]
        public void SetUp()
        {
  
            _addEditFurtherControlMeasureTaskViewModelFactory =
                new Mock<IEditFurtherControlMeasureTaskViewModelFactory>();

            _addGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory = new Mock<IAddFurtherControlMeasureTaskViewModelFactory>();

            _completeFurtherControlMeasureTaskViewModelFactory = new Mock<ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>();

            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory = new Mock<IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory>();

            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(It.IsAny<long>()))
                .Returns(_viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ViewRiskAssessmentFurtherControlMeasureTaskViewModel());
        }

        [Test]
        public void When_get_new_Then_should_return_the_correct_view()
        {
            // Given
            var target = CreateController();

            // When
            var result = target.View(companyId, furtherControlMeasureTaskId);

            // Then
            Assert.That(result.ViewName, Is.EqualTo("_ViewRiskAssessmentFurtherControlMeasureTask"));
        }

        private FurtherControlMeasureTaskController CreateController()
        {
            var result = new FurtherControlMeasureTaskController(
                _addEditFurtherControlMeasureTaskViewModelFactory.Object,
                _addGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object,
                _completeFurtherControlMeasureTaskViewModelFactory.Object,
                null,
                _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);
            return result;
        }
    }
}
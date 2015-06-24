using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FurtherActionTask
{
    [TestFixture]
    public class EditTests
    {
        private const long companyId = 200;
        private const long furtherControlMeasureTaskId = 500;
        private Mock<IEditFurtherControlMeasureTaskViewModelFactory> _addEditFurtherControlMeasureTaskViewModelFactory;

        private Mock<IAddFurtherControlMeasureTaskViewModelFactory>
             _addGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory;

        private Mock<ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>
            _completeFurtherControlMeasureTaskViewModelFactory;

        [SetUp]
        public void SetUp()
        {
            
            _addEditFurtherControlMeasureTaskViewModelFactory =
                new Mock<IEditFurtherControlMeasureTaskViewModelFactory>();

            _addEditFurtherControlMeasureTaskViewModelFactory.Setup(x => x.WithCompanyId(It.IsAny<long>())).Returns(
                _addEditFurtherControlMeasureTaskViewModelFactory.Object);

            _addEditFurtherControlMeasureTaskViewModelFactory.Setup(x => x.WithFurtherControlMeasureTaskId(It.IsAny<long>())).Returns(
                _addEditFurtherControlMeasureTaskViewModelFactory.Object);

            _addEditFurtherControlMeasureTaskViewModelFactory.Setup(x => x.WithCanDeleteDocuments(It.IsAny<bool>())).Returns(
                _addEditFurtherControlMeasureTaskViewModelFactory.Object);

            _addGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory = new Mock<IAddFurtherControlMeasureTaskViewModelFactory>();

            _completeFurtherControlMeasureTaskViewModelFactory = new Mock<ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>();
        }

        [Test]
        public void When_get_edit_Then_should_return_the_correct_view()
        {
            // Given
            var target = CreateController();

            // When
            var result = target.Edit(companyId, furtherControlMeasureTaskId);

            // Then
            Assert.That(result.ViewName, Is.EqualTo("_EditRiskAssessmentFurtherControlMeasureTask"));
        }

        [Test]
        public void When_get_new_Then_should_call_the_correct_view_methods()
        {
            // Given
            var target = CreateController();


            _addEditFurtherControlMeasureTaskViewModelFactory.Setup(x => x.GetViewModel()).Returns(
                new EditRiskAssessmentFurtherControlMeasureTaskViewModel());

            // When
            target.Edit(companyId, furtherControlMeasureTaskId);

            // Then
            _addEditFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel());

        }

        private FurtherControlMeasureTaskController CreateController()
        {
            var result = new FurtherControlMeasureTaskController(
               _addEditFurtherControlMeasureTaskViewModelFactory.Object,
                _addGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object,
                _completeFurtherControlMeasureTaskViewModelFactory.Object,
                null,
                null);
            return result;
        }
    }
}
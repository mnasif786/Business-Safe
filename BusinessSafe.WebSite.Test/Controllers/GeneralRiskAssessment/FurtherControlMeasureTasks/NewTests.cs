using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.FurtherControlMeasureTasks
{
    [TestFixture]
    public class NewTests
    {
        private const long companyId = 200;
        private const long riskAssessmentHazardId = 400;
        private Mock<IAddFurtherControlMeasureTaskViewModelFactory>_viewModelFactory;

        
        [SetUp]
        public void SetUp()
        {
        
            _viewModelFactory = new Mock<IAddFurtherControlMeasureTaskViewModelFactory>();

            _viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentHazardId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
            .Setup(x => x.WithFurtherControlMeasureTaskCategory(FurtherControlMeasureTaskCategoryEnum.GeneralRiskAssessments))
            .Returns(_viewModelFactory.Object);
        
        }

        [Test]
        public void When_get_new_Then_should_return_the_correct_view()
        {
            // Given
            var target = CreateController();

            // When
            var result = target.New(companyId, riskAssessmentHazardId);

            // Then
            Assert.That(result.ViewName, Is.EqualTo("_AddRiskAssessmentFurtherControlMeasureTask"));
        }

        [Test]
        public void When_get_new_Then_should_call_the_correct_view_methods()
        {
            // Given
            var target = CreateController();


            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new AddRiskAssessmentFurtherControlMeasureTaskViewModel());

            // When
            target.New(companyId, riskAssessmentHazardId);

            // Then
            _viewModelFactory.Verify(x => x.GetViewModel());

        }

        private FurtherControlMeasureTaskController CreateController()
        {
            var result = new FurtherControlMeasureTaskController(
                 
                null,
                _viewModelFactory.Object,
                null,
                null,
                null);
            return result;
        }
    }
}
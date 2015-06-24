using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.FurtherControlMeasureTasks
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteTest
    {
        private const long CompanyId = 12312L;
        private const long FurtherControlMeasureTaskId = 72L;
        private const bool CanDeleteDocuments = true;
        private Mock<ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>
            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory =
                new Mock<ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>();

            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new CompleteRiskAssessmentFurtherControlMeasureTaskViewModel());
        }

        [Test]
        public void When_View_called_Then_correct_methods_are_called()
        {
            var controller = GetTarget();
            controller.Complete(CompanyId, FurtherControlMeasureTaskId);
            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        [Test]
        public void When_View__called_Then_corrct_view_model_is_of_correct_type()
        {
            var controller = GetTarget();
            var result = controller.Complete(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.Model, Is.InstanceOf<CompleteRiskAssessmentFurtherControlMeasureTaskViewModel>());
        }
        
        [Test]
        public void When_Print_called_Then_correct_view_is_returned()
        {
            var controller = GetTarget();
            var result = controller.Complete(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.ViewName, Is.EqualTo("_CompleteRiskAssessmentFurtherControlMeasureTask"));
        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            return new FurtherControlMeasureTaskController(
                null,
                null,
                _completeGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object,
                null,
                null);
        }
    }
}

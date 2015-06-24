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
    public class ReassignTest
    {
        private const long CompanyId = 12312L;
        private const long FurtherControlMeasureTaskId = 72L;
        private const bool CanDeleteDocuments = true;
        private Mock<IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>
            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory =
                new Mock<IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory>();

            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ReassignRiskAssessmentFurtherControlMeasureTaskViewModel());
        }

        [Test]
        public void When_View_called_Then_correct_methods_are_called()
        {
            var controller = GetTarget();
            controller.Reassign(CompanyId, FurtherControlMeasureTaskId);
            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        [Test]
        public void When_View__called_Then_corrct_view_model_is_of_correct_type()
        {
            var controller = GetTarget();
            var result = controller.Reassign(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.Model, Is.InstanceOf<ReassignRiskAssessmentFurtherControlMeasureTaskViewModel>());
        }
        
        [Test]
        public void When_Print_called_Then_correct_view_is_returned()
        {
            var controller = GetTarget();
            var result = controller.Reassign(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.ViewName, Is.EqualTo("_ReassignRiskAssessmentFurtherControlMeasureTask"));
        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            return new FurtherControlMeasureTaskController(
                null,
                null,
                null,
                _reassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object,
                null);
        }
    }
}

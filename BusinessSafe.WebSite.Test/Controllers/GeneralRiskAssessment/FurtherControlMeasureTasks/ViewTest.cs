using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Controllers;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.GeneralRiskAssessment.FurtherControlMeasureTasks
{
    [TestFixture]
    [Category("Unit")]
    public class ViewTest
    {
        private const long CompanyId = 37325L;
        private const long FurtherControlMeasureTaskId = 28L;
        private Mock<IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory>
            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory =
                new Mock<IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory>();

            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ViewRiskAssessmentFurtherControlMeasureTaskViewModel());
        }

        [Test]
        public void When_View_called_Then_correct_methods_are_called()
        {
            var controller = GetTarget();
            controller.View(CompanyId, FurtherControlMeasureTaskId);
            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        [Test]
        public void When_View__called_Then_corrct_view_model_is_of_correct_type()
        {
            var controller = GetTarget();
            var result = controller.View(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.Model, Is.InstanceOf<ViewRiskAssessmentFurtherControlMeasureTaskViewModel>());
        }

        [Test]
        public void When_View__called_Then_IsReadOnly_is_set_true()
        {
            var controller = GetTarget();
            var result = controller.View(CompanyId, FurtherControlMeasureTaskId);
            dynamic viewBag = result.ViewBag;
            Assert.That(viewBag.IsReadOnly, Is.True);
        }
        
        [Test]
        public void When_Print_called_Then_correct_view_is_returned()
        {
            var controller = GetTarget();
            var result = controller.View(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.ViewName, Is.EqualTo("_ViewRiskAssessmentFurtherControlMeasureTask"));
        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            return new FurtherControlMeasureTaskController(
                null,
                null,
                null,
                null,
                _viewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);
        }
    }
}

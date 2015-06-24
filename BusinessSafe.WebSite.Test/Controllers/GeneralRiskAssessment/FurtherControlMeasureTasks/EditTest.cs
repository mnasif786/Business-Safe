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
    public class EditTest
    {
        private const long CompanyId = 12312L;
        private const long FurtherControlMeasureTaskId = 72L;
        private const bool CanDeleteDocuments = true;
        private Mock<IEditFurtherControlMeasureTaskViewModelFactory>
            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory;

        [SetUp]
        public void Setup()
        {
            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory =
                new Mock<IEditFurtherControlMeasureTaskViewModelFactory>();

            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCanDeleteDocuments(CanDeleteDocuments))
                .Returns(_editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object);

            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new EditRiskAssessmentFurtherControlMeasureTaskViewModel());
        }

        [Test]
        public void When_View_called_Then_correct_methods_are_called()
        {
            var controller = GetTarget();
            controller.Edit(CompanyId, FurtherControlMeasureTaskId);
            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCanDeleteDocuments(CanDeleteDocuments), Times.Once());
            _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        [Test]
        public void When_View__called_Then_corrct_view_model_is_of_correct_type()
        {
            var controller = GetTarget();
            var result = controller.Edit(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.Model, Is.InstanceOf<EditRiskAssessmentFurtherControlMeasureTaskViewModel>());
        }
        
        [Test]
        public void When_Print_called_Then_correct_view_is_returned()
        {
            var controller = GetTarget();
            var result = controller.Edit(CompanyId, FurtherControlMeasureTaskId);
            Assert.That(result.ViewName, Is.EqualTo("_EditRiskAssessmentFurtherControlMeasureTask"));
        }

        private FurtherControlMeasureTaskController GetTarget()
        {
            return new FurtherControlMeasureTaskController(
                _editGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactory.Object,
                null,
                null,
                null,
                null);
        }
    }
}

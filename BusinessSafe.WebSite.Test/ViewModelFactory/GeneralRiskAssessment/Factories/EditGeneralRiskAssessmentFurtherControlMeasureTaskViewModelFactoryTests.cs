using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessment.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class EditGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactoryTests
    {
        private const long CompanyId = 51760L;
        private const long FurtherControlMeasureTaskId = 486L;
        private const long RiskAssessmentHazardId = 48L;
        private const bool CanDeleteDocuments = true;
        private Mock<IRiskAssessmentHazardSummaryViewModelFactory> _generalRiskAssessmentHazardSummaryViewModelFactory;
        private Mock<IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory> _addEditFurtherControlMeasureTaskViewModelFactory;
        private Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;

        [SetUp]
        public void Setup()
        {
            _generalRiskAssessmentHazardSummaryViewModelFactory =
                new Mock<IRiskAssessmentHazardSummaryViewModelFactory>();

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_generalRiskAssessmentHazardSummaryViewModelFactory.Object);

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.WithRiskAssessmentHazardId(It.IsAny<long>()))
                .Returns(_generalRiskAssessmentHazardSummaryViewModelFactory.Object);

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new RiskAssessmentHazardSummaryViewModel());

            _addEditFurtherControlMeasureTaskViewModelFactory =
                new Mock<IEditHazardousSubstanceFurtherControlMeasureTaskViewModelFactory>();

            _addEditFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_addEditFurtherControlMeasureTaskViewModelFactory.Object);

            _addEditFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_addEditFurtherControlMeasureTaskViewModelFactory.Object);

            _addEditFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCanDeleteDocuments(CanDeleteDocuments))
                .Returns(_addEditFurtherControlMeasureTaskViewModelFactory.Object);

            _addEditFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new AddEditFurtherControlMeasureTaskViewModel());

            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();

            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId))
                .Returns(new MultiHazardRiskAssessmentFurtherControlMeasureTaskDto
                             {
                                 Id = FurtherControlMeasureTaskId,
                                 RiskAssessmentHazard = new RiskAssessmentHazardDto
                                                            {
                                                                Id = RiskAssessmentHazardId
                                                            }
                             });
        }

        [Test]
        public void When_GetViewModel_called_Then_correct_methods_are_called()
        {
            var factory = GetTarget();

            factory
                .WithCompanyId(CompanyId)
                .WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId)
                .WithCanDeleteDocuments(CanDeleteDocuments)
                .GetViewModel();

            _furtherControlMeasureTaskService.Verify(x => x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithRiskAssessmentHazardId(RiskAssessmentHazardId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
            _addEditFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _addEditFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _addEditFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCanDeleteDocuments(CanDeleteDocuments), Times.Once());
            _addEditFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        private IEditFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new EditFurtherControlMeasureTaskViewModelFactory(
                _generalRiskAssessmentHazardSummaryViewModelFactory.Object,
                _addEditFurtherControlMeasureTaskViewModelFactory.Object,
                _furtherControlMeasureTaskService.Object);
        }
    }
}

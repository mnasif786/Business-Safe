using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessment.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class ViewGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactoryTests
    {
        private const long CompanyId = 1963L;
        private const long FurtherControlMeasureTaskId = 16L;
        private const long RiskAssessmentHazardId = 6L;
        private Mock<IRiskAssessmentHazardSummaryViewModelFactory> _generalRiskAssessmentHazardSummaryViewModelFactory;
        private Mock<IViewFurtherControlMeasureTaskViewModelFactory> _viewFurtherControlMeasureTaskViewModelFactory;
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

            _viewFurtherControlMeasureTaskViewModelFactory =
                new Mock<IViewFurtherControlMeasureTaskViewModelFactory>();

            _viewFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_viewFurtherControlMeasureTaskViewModelFactory.Object);

            _viewFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_viewFurtherControlMeasureTaskViewModelFactory.Object);

            _viewFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ViewFurtherControlMeasureTaskViewModel());

            _furtherControlMeasureTaskService = new Mock<IFurtherControlMeasureTaskService>();

            _furtherControlMeasureTaskService
                .Setup(x => x.GetByIdIncludeDeleted(FurtherControlMeasureTaskId))
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
                .GetViewModel();

            _furtherControlMeasureTaskService.Verify(x => x.GetByIdIncludeDeleted(FurtherControlMeasureTaskId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithRiskAssessmentHazardId(RiskAssessmentHazardId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
            _viewFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _viewFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _viewFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        private IViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new ViewRiskAssessmentFurtherControlMeasureTaskViewModelFactory(
                _generalRiskAssessmentHazardSummaryViewModelFactory.Object,
                _viewFurtherControlMeasureTaskViewModelFactory.Object,
                _furtherControlMeasureTaskService.Object);
        }
    }
}

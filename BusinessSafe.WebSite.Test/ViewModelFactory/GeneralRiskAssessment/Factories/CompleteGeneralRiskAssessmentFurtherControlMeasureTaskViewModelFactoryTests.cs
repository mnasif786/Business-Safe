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
    public class CompleteGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactoryTests
    {
        private const long CompanyId = 24598L;
        private const long FurtherControlMeasureTaskId = 24L;
        private const long RiskAssessmentHazardId = 16L;
        private Mock<IRiskAssessmentHazardSummaryViewModelFactory> _generalRiskAssessmentHazardSummaryViewModelFactory;
        private Mock<ICompleteFurtherControlMeasureTaskViewModelFactory> _completeFurtherControlMeasureTaskViewModelFactory;
        private Mock<IFurtherControlMeasureTaskService> _furtherControlMeasureTaskService;

        [SetUp]
        public void Setup()
        {
            _generalRiskAssessmentHazardSummaryViewModelFactory = new Mock<IRiskAssessmentHazardSummaryViewModelFactory>();

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_generalRiskAssessmentHazardSummaryViewModelFactory.Object);

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.WithRiskAssessmentHazardId(It.IsAny<long>()))
                .Returns(_generalRiskAssessmentHazardSummaryViewModelFactory.Object);

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new RiskAssessmentHazardSummaryViewModel());

            _completeFurtherControlMeasureTaskViewModelFactory = new Mock<ICompleteFurtherControlMeasureTaskViewModelFactory>();

            _completeFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_completeFurtherControlMeasureTaskViewModelFactory.Object);

            _completeFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_completeFurtherControlMeasureTaskViewModelFactory.Object);

            _completeFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new CompleteFurtherControlMeasureTaskViewModel());

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
                .GetViewModel();

            _furtherControlMeasureTaskService.Verify(x=> x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithRiskAssessmentHazardId(RiskAssessmentHazardId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
            _completeFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _completeFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _completeFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        private ICompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory GetTarget()
        {
            return new CompleteFurtherControlMeasureTaskWithHazardSummaryViewModelFactory(
                _generalRiskAssessmentHazardSummaryViewModelFactory.Object,
                _completeFurtherControlMeasureTaskViewModelFactory.Object,
                _furtherControlMeasureTaskService.Object);
        }
    }
}

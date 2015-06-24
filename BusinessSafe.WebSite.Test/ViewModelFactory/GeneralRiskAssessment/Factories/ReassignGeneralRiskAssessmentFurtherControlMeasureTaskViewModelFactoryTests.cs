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
    public class ReassignGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactoryTests
    {
        private const long CompanyId = 82543L;
        private const long FurtherControlMeasureTaskId = 926L;
        private const long RiskAssessmentHazardId = 82;
        private Mock<IRiskAssessmentHazardSummaryViewModelFactory> _generalRiskAssessmentHazardSummaryViewModelFactory;
        private Mock<IReassignFurtherControlMeasureTaskViewModelFactory> _reassignFurtherControlMeasureTaskViewModelFactory;
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

            _reassignFurtherControlMeasureTaskViewModelFactory =
                new Mock<IReassignFurtherControlMeasureTaskViewModelFactory>();

            _reassignFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithCompanyId(CompanyId))
                .Returns(_reassignFurtherControlMeasureTaskViewModelFactory.Object);

            _reassignFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId))
                .Returns(_reassignFurtherControlMeasureTaskViewModelFactory.Object);

            _reassignFurtherControlMeasureTaskViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new ReassignFurtherControlMeasureTaskViewModel());

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

            _furtherControlMeasureTaskService.Verify(x => x.GetByIdAndCompanyId(FurtherControlMeasureTaskId, CompanyId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithRiskAssessmentHazardId(RiskAssessmentHazardId), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
            _reassignFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithCompanyId(CompanyId), Times.Once());
            _reassignFurtherControlMeasureTaskViewModelFactory.Verify(x => x.WithFurtherControlMeasureTaskId(FurtherControlMeasureTaskId), Times.Once());
            _reassignFurtherControlMeasureTaskViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        private IReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory GetTarget()
        {
            return new ReassignFurtherControlMeasureTaskWithHazardSummaryViewModelFactory(
                _generalRiskAssessmentHazardSummaryViewModelFactory.Object,
                _reassignFurtherControlMeasureTaskViewModelFactory.Object,
                _furtherControlMeasureTaskService.Object);
        }
    }
}

using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;
using BusinessSafe.Application.Contracts.RiskAssessments;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessment.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class AddGeneralRiskAssessmentFurtherControlMeasureTaskViewModelFactoryTests
    {
        private Mock<IRiskAssessmentHazardSummaryViewModelFactory> _generalRiskAssessmentHazardSummaryViewModelFactory;
        private Mock<IRiskAssessmentHazardService> _riskAssessmentHazardService;

        [SetUp]
        public void Setup()
        {
            _generalRiskAssessmentHazardSummaryViewModelFactory = new Mock<IRiskAssessmentHazardSummaryViewModelFactory>();
            _riskAssessmentHazardService = new Mock<IRiskAssessmentHazardService>();

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_generalRiskAssessmentHazardSummaryViewModelFactory.Object);

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.WithRiskAssessmentHazardId(It.IsAny<long>()))
                .Returns(_generalRiskAssessmentHazardSummaryViewModelFactory.Object);

            _generalRiskAssessmentHazardSummaryViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new RiskAssessmentHazardSummaryViewModel());

            _riskAssessmentHazardService
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new RiskAssessmentHazardDto
                             {
                                 RiskAssessment = new GeneralRiskAssessmentDto
                                                      {
                                                          RiskAssessor = new RiskAssessorDto()
                                                      }
                             });
        }

        [Test]
        public void When_GetViewModel_called_Then_correct_methods_called()
        {
            var factory = GetTarget();

            factory
                .WithCompanyId(1717L)
                .WithRiskAssessmentHazardId(26L)
                .GetViewModel();

            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithCompanyId(1717L), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.WithRiskAssessmentHazardId(26L), Times.Once());
            _generalRiskAssessmentHazardSummaryViewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        [Test]
        public void Given_correct_parameters_When_GetViewModel_called_Then_correct_view_model_is_returned()
        {
            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(1717L)
                .WithRiskAssessmentHazardId(26L)
                .GetViewModel();

            Assert.That(viewModel.CompanyId, Is.EqualTo(1717L));
            Assert.That(viewModel.RiskAssessmentHazardId, Is.EqualTo(26L));
            Assert.That(viewModel.ExistingDocuments, Is.Not.Null);
        }

        [Test]
        public void Given_no_risk_assessor_When_GetViewModel_then_do_not_send_Notifications_set_to_false()
        {
            //Given
            _riskAssessmentHazardService
             .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
             .Returns(new RiskAssessmentHazardDto
             {
                 RiskAssessment = new GeneralRiskAssessmentDto
                 {
                     RiskAssessor = null
                 }
             });

            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(1717L)
                .WithRiskAssessmentHazardId(26L)
                .GetViewModel();
            

            Assert.IsFalse(viewModel.DoNotSendTaskCompletedNotification);
            Assert.IsFalse(viewModel.DoNotSendTaskOverdueNotification);
        }

        private IAddFurtherControlMeasureTaskViewModelFactory GetTarget()
        {
            return new AddFurtherControlMeasureTaskViewModelFactory(_generalRiskAssessmentHazardSummaryViewModelFactory.Object, _riskAssessmentHazardService.Object);
        }
    }
}

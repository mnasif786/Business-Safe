using BusinessSafe.Application.Contracts.GeneralRiskAssessments;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories;
using BusinessSafe.WebSite.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.GeneralRiskAssessment.Factories
{
    [TestFixture]
    [Category("Unit")]
    public class GeneralRiskAssessmentHazardSummaryViewModelFactoryTests
    {
        private const long CompanyId = 78136L;
        private const long RiskAssessmentHazardId = 27L;
        private const long HazardId = 12L;
        private const long RiskAssessmentId = 23L;
        private const string RiskAssessmentHazardName = "Test Hazard Name 01";
        private const string RiskAssessmentHazardDescription = "Test RiskAssessmentHazard Description 01";
        private const string RiskAssessmentTitle = "Test RiskAssessment Title 01";
        private Mock<IRiskAssessmentHazardService> _riskAssessmentHazardService;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentHazardService = new Mock<IRiskAssessmentHazardService>();

            _riskAssessmentHazardService
                .Setup(x => x.GetByIdAndCompanyId(RiskAssessmentHazardId, CompanyId))
                .Returns(new RiskAssessmentHazardDto
                             {
                                 Id = RiskAssessmentHazardId,
                                 Description = RiskAssessmentHazardDescription,
                                 Hazard = new HazardDto
                                              {
                                                  Id = HazardId,
                                                  Name = RiskAssessmentHazardName
                                              },
                                 RiskAssessment = new GeneralRiskAssessmentDto
                                                      {
                                                          Id = RiskAssessmentId,
                                                          CompanyId = CompanyId,
                                                          Title = RiskAssessmentTitle
                                                      }
                             });
        }

        [Test]
        public void When_GetViewModel_called_Then_correct_methods_called()
        {
            var factory = GetTarget();

            factory
                .WithCompanyId(CompanyId)
                .WithRiskAssessmentHazardId(RiskAssessmentHazardId)
                .GetViewModel();

            _riskAssessmentHazardService.Verify(x=> x.GetByIdAndCompanyId(RiskAssessmentHazardId, CompanyId), Times.Once());
        }

        [Test]
        public void Given_valid_parameters_When_GetViewModel_called_Then_correct_view_model_is_returned()
        {
            var factory = GetTarget();

            var viewModel = factory
                .WithCompanyId(CompanyId)
                .WithRiskAssessmentHazardId(RiskAssessmentHazardId)
                .GetViewModel();

            Assert.That(viewModel.Id, Is.EqualTo(RiskAssessmentHazardId));
            Assert.That(viewModel.RiskAssessmentHazardName, Is.EqualTo(RiskAssessmentHazardName));
            Assert.That(viewModel.RiskAssessmentHazardDescription, Is.EqualTo(RiskAssessmentHazardDescription));
            Assert.That(viewModel.RiskAssessmentTitle, Is.EqualTo(RiskAssessmentTitle));
        }

        private IRiskAssessmentHazardSummaryViewModelFactory GetTarget()
        {
            return new RiskAssessmentHazardSummaryViewModelFactory(_riskAssessmentHazardService.Object);
        }
    }
}

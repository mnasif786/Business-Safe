using BusinessSafe.Application.Contracts.HazardousSubstanceInventory;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.HazardousSubstanceInventory.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstances
{
    [TestFixture]
    public class HazardousSubstanceDescriptionViewModelFactoryTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;
        private Mock<IHazardousSubstancesService> _hazardousSubstanceService;
        private long _companyId;
        private long _riskAssessmentId;


        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _hazardousSubstanceService = new Mock<IHazardousSubstancesService>();
            _companyId = 200;
            _riskAssessmentId = 300;
        }

        [Test]
        public void When_GetViewModel_Then_should_call_the_correct_methods()
        {
            // Given
            var target = CreateTarget();

            var riskAssessment = new HazardousSubstanceRiskAssessmentDto()
                                {
                                    CompanyId = _companyId,
                                    Id = _riskAssessmentId,
                                    HazardousSubstance = new HazardousSubstanceDto { Name = "Test Hazardous Substance 1" }
                                };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(_riskAssessmentId, _companyId))
                .Returns(riskAssessment);

          
            var hazardousSubstances = new HazardousSubstanceDto[]{ new HazardousSubstanceDto(), };
            _hazardousSubstanceService
                .Setup(x => x.GetHazardousSubstancesForSearchTerm("", _companyId, 100))
                .Returns(hazardousSubstances);

            // When
            target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            // Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void When_GetViewModel_Then_should_return_the_correct_viewmodel()
        {
            // Given
            var target = CreateTarget();

            var riskAssessment = new HazardousSubstanceRiskAssessmentDto()
                                     {
                                         CompanyId = _companyId,
                                         Id = _riskAssessmentId,
                                         HazardousSubstance = new HazardousSubstanceDto { Name = "Test Hazardous Substance 1" }
                                     };

            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(_riskAssessmentId, _companyId))
                .Returns(riskAssessment);
            
           
            var hazardousSubstances = new HazardousSubstanceDto[] { new HazardousSubstanceDto(), };
            _hazardousSubstanceService
                .Setup(x => x.GetHazardousSubstancesForSearchTerm("", _companyId, 100))
                .Returns(hazardousSubstances);

            // When
            var result = target
                            .WithCompanyId(_companyId)
                            .WithRiskAssessmentId(_riskAssessmentId)
                            .GetViewModel();

            // Then
            Assert.That(result, Is.TypeOf<DescriptionViewModel>());
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(_riskAssessmentId));
        }

        private HazardousSubstanceDescriptionViewModelFactory CreateTarget()
        {
            return new HazardousSubstanceDescriptionViewModelFactory(_riskAssessmentService.Object);
        }
    }
}

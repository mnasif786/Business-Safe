using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.HazardousSubstanceRiskAssessementTests
{
    [TestFixture]
    public partial class CrontolMeasuresViewModelFactoryTests
    {
        private ControlMeasuresViewModelFactory _target;
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;
        private long _riskAssessmentId;
        private long _companyId;
        private HazardousSubstanceRiskAssessmentDto _hazardousSubstanceRiskAssessmentDto;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _target = new ControlMeasuresViewModelFactory(_riskAssessmentService.Object);
            _riskAssessmentId = 300;
            _companyId = 500;
            _hazardousSubstanceRiskAssessmentDto = new HazardousSubstanceRiskAssessmentDto()
                                                       {
                                                           HazardousSubstance = new HazardousSubstanceDto()
                                                                                    {
                                                                                        Name = "JS"
                                                                                    },
                                                           ControlMeasures = new List<HazardousSubstanceRiskAssessmentControlMeasureDto>()
                                                                                      {
                                                                                          new HazardousSubstanceRiskAssessmentControlMeasureDto()
                                                                                              {
                                                                                                  Id = 1,
                                                                                                  ControlMeasure = "hey hey",
                                                                                                  HazardousSubstanceRiskAssessmentId = _riskAssessmentId
                                                                                              }
                                                                                      }
                                                       };
        }

        [Test]
        public void GetViewModel_should_call_correct_methods()
        {
            // Given
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(_riskAssessmentId, _companyId))
                .Returns(_hazardousSubstanceRiskAssessmentDto);


            // When
            _target
                .WithCompanyId(_companyId)
                .WithHazardousSubstanceRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            // Then
            _riskAssessmentService.VerifyAll();
        }

        [Test]
        public void GetViewModel_should_call_correct_properties()
        {
            // Given
            _riskAssessmentService
                .Setup(x => x.GetRiskAssessment(_riskAssessmentId, _companyId))
                .Returns(_hazardousSubstanceRiskAssessmentDto);

            // When
            var result = _target
                .WithCompanyId(_companyId)
                .WithHazardousSubstanceRiskAssessmentId(_riskAssessmentId)
                .GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(_companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(_riskAssessmentId));
            Assert.That(result.ControlMeasures.Count(), Is.EqualTo(_hazardousSubstanceRiskAssessmentDto.ControlMeasures.Count()));
            
            Assert.That(result.ControlMeasures.First().Id, Is.EqualTo(_hazardousSubstanceRiskAssessmentDto.ControlMeasures.First().Id));
            Assert.That(result.ControlMeasures.First().ControlMeasure, Is.EqualTo(_hazardousSubstanceRiskAssessmentDto.ControlMeasures.First().ControlMeasure));
        }

    }
}
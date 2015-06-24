using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDefaultServiceTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetAllFireSafetyControlMeasuresForCompanyTests
    {
        private Mock<IFireSafetyControlMeasureRepository> _fireSafetyControlMeasuresRepository;
        private Mock<IPeninsulaLog> _log;
        long companyId = 1;
        long riskAssessmentId = 2;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<long>()));
            _fireSafetyControlMeasuresRepository = new Mock<IFireSafetyControlMeasureRepository>();
        }

        [Test]
        public void When_calling_GetAllFireSafetyControlMeasuresForCompany_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateCompanyDefaultService();

            var result = new List<FireSafetyControlMeasure>();
            _fireSafetyControlMeasuresRepository
                .Setup(tp => tp.GetAllFireSafetyControlMeasureForRiskAssessments(companyId, riskAssessmentId))
                .Returns(result);

            //When
            target.GetAllFireSafetyControlMeasuresForRiskAssessments(companyId, riskAssessmentId);

            //Then
            _fireSafetyControlMeasuresRepository.VerifyAll();
        }

        [Test]
        public void When_GetAllFireSafetyControlMeasuresForCompany_Then_Maps_To_Dtos()
        {
            // Given
            const long companyId = 100;
            const long riskAssessmentId = 200;

            var fireSafetyControlMeasures = new List<FireSafetyControlMeasure>()
                                        {
                                            new FireSafetyControlMeasure() { Id = 1, Name = "Test 1"},
                                            new FireSafetyControlMeasure() { Id = 2 , Name = "Test 2"},
                                            new FireSafetyControlMeasure() { Id = 3, Name = "Test 3" },
                                            new FireSafetyControlMeasure() { Id = 4, Name = "Test 4" }
                                        };

            _fireSafetyControlMeasuresRepository
                .Setup(tp => tp.GetAllFireSafetyControlMeasureForRiskAssessments(companyId, riskAssessmentId))
                .Returns(fireSafetyControlMeasures);

            var target = CreateCompanyDefaultService();

            // When
            var result = target.GetAllFireSafetyControlMeasuresForRiskAssessments(companyId, riskAssessmentId);

            // Then
            Assert.That(result.Count(), Is.EqualTo(4));
            Assert.That(result.Count(x => x.Id == 1 && x.Name == "Test 1"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 2 && x.Name == "Test 2"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 3 && x.Name == "Test 3"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 4 && x.Name == "Test 4"), Is.EqualTo(1));
        }

        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, null, null, null, null, _fireSafetyControlMeasuresRepository.Object, null, null, null, null, _log.Object);
            return target;
        }
    }
}
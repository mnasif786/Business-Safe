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
    public class GetAllSourceOfFuelForCompanyTests
    {
        private Mock<ISourceOfFuelRepository> _sourceOfFuelRepository;
        private Mock<IPeninsulaLog> _log;
        long companyId = 1;
        long riskAssessmentId = 2;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<long>()));
            _sourceOfFuelRepository = new Mock<ISourceOfFuelRepository>();
        }

        [Test]
        public void When_calling_GetAllSourceOfFuelForCompany_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateCompanyDefaultService();

            var result = new List<SourceOfFuel>();
            _sourceOfFuelRepository
                .Setup(tp => tp.GetAllSourceOfFuelForRiskAssessments(companyId,riskAssessmentId))
                .Returns(result);

            //When
            target.GetAllSourceOfFuelForRiskAssessment(companyId, riskAssessmentId);

            //Then
            _sourceOfFuelRepository.VerifyAll();
        }

        [Test]
        public void When_GetAllSourceOfFuelForCompany_Then_Maps_To_Dtos()
        {
            // Given
            const long companyId = 100;
            const long riskAssessmentId = 200;

            var sourceOfFuels = new List<SourceOfFuel>()
                                        {
                                            new SourceOfFuel() { Id = 1, Name = "Test 1"},
                                            new SourceOfFuel() { Id = 2 , Name = "Test 2"},
                                            new SourceOfFuel() { Id = 3, Name = "Test 3" },
                                            new SourceOfFuel() { Id = 4, Name = "Test 4" }
                                        };

            _sourceOfFuelRepository
                .Setup(x => x.GetAllSourceOfFuelForRiskAssessments(companyId, riskAssessmentId))
                .Returns(sourceOfFuels);

            var target = CreateCompanyDefaultService();

            // When
            var result = target.GetAllSourceOfFuelForRiskAssessment(companyId, riskAssessmentId);

            // Then
            Assert.That(result.Count(), Is.EqualTo(4));
            Assert.That(result.Count(x => x.Id == 1 && x.Name == "Test 1"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 2 && x.Name == "Test 2"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 3 && x.Name == "Test 3"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 4 && x.Name == "Test 4"), Is.EqualTo(1));
        }

        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, null, null, null, null, null, null, _sourceOfFuelRepository.Object, null, null, _log.Object);
            return target;
        }
    }
}
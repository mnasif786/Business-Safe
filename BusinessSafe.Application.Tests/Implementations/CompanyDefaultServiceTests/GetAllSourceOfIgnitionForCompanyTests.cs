using System;
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
    public class GetAllSourceOfIgnitionForCompanyTests
    {
        private Mock<ISourceOfIgnitionRepository> _sourceOfIgnitionRepository;
        private Mock<IPeninsulaLog> _log;
        long companyId = 1;
        long riskAssessmentId = 2;

        [SetUp]
        public void SetUp()
        {
            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<long>()));
            _sourceOfIgnitionRepository = new Mock<ISourceOfIgnitionRepository>();
        }

        [Test]
        public void When_calling_GetAllSourceOfIgnitionForCompany_Then_should_call_correct_methods()
        {
            //Given
            var target = CreateCompanyDefaultService();

            var result = new List<SourceOfIgnition>();
            _sourceOfIgnitionRepository
                .Setup(tp => tp.GetAllSourceOfIgnitionForRiskAssessments(companyId,riskAssessmentId))
                .Returns(result);

            //When
            target.GetAllSourceOfIgnitionForRiskAssessment(companyId,riskAssessmentId);

            //Then
            _sourceOfIgnitionRepository.VerifyAll();
        }

        [Test]
        public void When_GetAllSourceOfIgnitionForCompany_Then_Maps_To_Dtos()
        {
            // Given
            const long companyId = 100;
            const long riskAssessmentId = 200;

            var sourceOfIgnitions = new List<SourceOfIgnition>()
                                      {
                                          new SourceOfIgnition() { Id = 1, Name = "Test 1"},
                                          new SourceOfIgnition() { Id = 2 , Name = "Test 2"},
                                          new SourceOfIgnition() { Id = 3, Name = "Test 3" },
                                          new SourceOfIgnition() { Id = 4, Name = "Test 4" }
                                      };

            _sourceOfIgnitionRepository
                .Setup(x => x.GetAllSourceOfIgnitionForRiskAssessments(companyId, riskAssessmentId))
                .Returns(sourceOfIgnitions);

            var target = CreateCompanyDefaultService();

            // When
            var result = target.GetAllSourceOfIgnitionForRiskAssessment(companyId,riskAssessmentId);

            // Then
            Assert.That(result.Count(), Is.EqualTo(4));
            Assert.That(result.Count(x => x.Id == 1 && x.Name == "Test 1"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 2 && x.Name == "Test 2"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 3 && x.Name == "Test 3"), Is.EqualTo(1));
            Assert.That(result.Count(x => x.Id == 4 && x.Name == "Test 4"), Is.EqualTo(1));
        }
        
        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, null, null, null, null, null, _sourceOfIgnitionRepository.Object, null, null, null, _log.Object);
            return target;
        }
    }
}
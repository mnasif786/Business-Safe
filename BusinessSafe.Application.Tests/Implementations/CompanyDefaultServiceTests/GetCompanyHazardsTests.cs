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
    public class GetCompanyHazardsTests
    {
        private Mock<IHazardRepository> _hazardRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();

            _log = new Mock<IPeninsulaLog>();
            _log.Setup(x => x.Add(It.IsAny<long>()));

            _hazardRepository = new Mock<IHazardRepository>();
            _hazardRepository
                .Setup(tp => tp.GetByCompanyIdAndHazardTypeId(It.IsAny<long>(), It.IsAny<HazardTypeEnum>(), It.IsAny<long>()))
                .Returns(new List<Hazard>());

        }

        [Test]
        public void Given_compamydefaultservice_is_initilized_When_calling_Then_repository_GetByCompanyId_is_called()
        {
            //Given
            var target = CreateCompanyDefaultService();

            var result = new List<Hazard>();
            _hazardRepository.Setup(tp => tp.GetByCompanyId(It.IsAny<long>())).Returns(result);

            //When
            target.GetAllHazardsForCompany(It.IsAny<long>());

            //Then
            _hazardRepository.Verify(tp => tp.GetByCompanyId(It.IsAny<long>()));
        }

     
        [Test]
        public void When_GetAllMultiHazardRiskAssessmentHazardsForCompany_Then_Calls_Repository()
        {
            // Given
            long companyId = 100;
            long riskAssessmentId = 200;

            _hazardRepository
                .Setup(x => x.GetByCompanyIdAndHazardTypeId(companyId, It.IsAny<HazardTypeEnum>(),riskAssessmentId))
                .Returns(new List<Hazard>());

            var target = CreateCompanyDefaultService();

            // When
            target.GetAllMultiHazardRiskAssessmentHazardsForCompany(companyId, HazardTypeEnum.General, riskAssessmentId);

            // Then
            _hazardRepository.Verify(x => x.GetByCompanyIdAndHazardTypeId(companyId, HazardTypeEnum.General, riskAssessmentId));
        }

        [Test]
        public void When_GetAllMultiHazardRiskAssessmentHazardsForCompany_Then_Maps_Hazards_To_Dtos()
        {
            // Given
            const long companyId = 100;
            long riskAssessmentId = 200;

            var returnedHazards = new List<Hazard>()
                                  {
                                      new Hazard() { Id = 1 },
                                      new Hazard() { Id = 2 },
                                      new Hazard() { Id = 3 },
                                      new Hazard() { Id = 4 }
                                  };

            _hazardRepository
                .Setup(x => x.GetByCompanyIdAndHazardTypeId(companyId, It.IsAny<HazardTypeEnum>(), riskAssessmentId))
                .Returns(returnedHazards);

            var target = CreateCompanyDefaultService();

            // When
            var result = target.GetAllMultiHazardRiskAssessmentHazardsForCompany(companyId, It.IsAny<HazardTypeEnum>(),riskAssessmentId);

            // Then
            Assert.That(result.Count(), Is.EqualTo(4));
            Assert.That(result.ElementAt(0).Id, Is.EqualTo(returnedHazards[0].Id));
        }

        [Test]
        public void Given_GetAllMultiHazardRiskAssessmentHazardsForCompany_Called_When_HazSubRepo_Throws_Exception_Then_Exception_Is_Logged_And_Thrown()
        {
            // Given
            const long companyId = 100;
            long riskAssessmentId = 200;

            _log.Setup(x => x.Add(It.IsAny<Exception>()));

            var exception = new Exception();
            _hazardRepository
                .Setup(x => x.GetByCompanyIdAndHazardTypeId(companyId, It.IsAny<HazardTypeEnum>(), riskAssessmentId))
                .Throws(exception);

            var target = CreateCompanyDefaultService();
            
            // When
            // Then
            Assert.Throws<Exception>(() => target.GetAllMultiHazardRiskAssessmentHazardsForCompany(companyId, It.IsAny<HazardTypeEnum>(),riskAssessmentId));
            _log.Verify(x => x.Add(exception));
        }

        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(_hazardRepository.Object, null, _userRepository.Object, null, null, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}

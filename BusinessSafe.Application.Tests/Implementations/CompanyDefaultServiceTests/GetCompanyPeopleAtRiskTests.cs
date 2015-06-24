using System.Collections.Generic;
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
    public class GetCompanyPeopleAtRiskTests
    {
        private Mock<IPeopleAtRiskRepository> _peopleAtRiskRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IPeninsulaLog> _log;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();
            _peopleAtRiskRepository = new Mock<IPeopleAtRiskRepository>();
            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_clientdefaultservice_is_initilized_When_calling_Then_repository_GetByCompanyId_is_called()
        {
            //Given
            var target = CreateCompanyDefaultService();

            var result = new List<PeopleAtRisk>();
            _peopleAtRiskRepository.Setup(tp => tp.GetByCompanyId(It.IsAny<long>())).Returns(result);

            //When
            target.GetAllPeopleAtRiskForCompany(It.IsAny<long>());

            //Then
            _peopleAtRiskRepository.VerifyAll();
        }



        private CompanyDefaultService CreateCompanyDefaultService()
        {
            var target = new CompanyDefaultService(null, _peopleAtRiskRepository.Object,_userRepository.Object, null, null, null, null, null, null, null, _log.Object);
            return target;
        }
    }
}


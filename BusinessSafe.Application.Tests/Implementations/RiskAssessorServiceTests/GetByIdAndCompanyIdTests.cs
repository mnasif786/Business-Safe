using System.Collections.Generic;
using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessorServiceTests
{
    [TestFixture]
    public class GetByIdAndCompanyIdTests
    {
        public Mock<IRiskAssessorRepository> _riskAssessorRepository;
        public Mock<IPeninsulaLog> _log;
        public IEnumerable<RiskAssessor> _riskAssessors;

        [SetUp]
        public void Setup()
        {
            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();

            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new RiskAssessor() { Employee = new Employee(), Site = new Site() });

            _log = new Mock<IPeninsulaLog>();
            _log
                .Setup(x => x.Add(It.IsAny<object>()));
        }

        [Test]
        public void Given_request_When_GetByIdAndCompanyId_Then_requests_from_repo()
        {
            // Given
            var target = GetTarget();
            const long riskAssessorId = 1234L;
            const long companyId = 5678L;

            // When
            target.GetByIdAndCompanyId(riskAssessorId, companyId);

            // Then
            _riskAssessorRepository.Verify(x => x.GetByIdAndCompanyId(
                It.Is<long>(y => y == riskAssessorId),
                It.Is<long>(y => y == companyId)
            ));
        }
        
        private RiskAssessorService GetTarget()
        {
            return new RiskAssessorService(
                _riskAssessorRepository.Object,
                null,
                null,
                null,
                null, null, _log.Object);
        }
    }
}

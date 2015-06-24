using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Implementations.Company;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessorServiceTests
{
    [TestFixture]
    public class MarkRiskAssessorUndeletedTests
    {
        private Mock<IRiskAssessorRepository> _riskAssessorRepository;
        private Mock<IPeninsulaLog> _log;
        private Mock<IUserForAuditingRepository> _userForAuditingRepository;

        private long _companyId;
        private long _riskAssessorId;
        private Guid _creatingUserId;

        private Mock<RiskAssessor> _riskAssessor;
        private MarkRiskAssessorAsDeletedAndUndeletedRequest _request;

        [SetUp]
        public void Setup()
        {

            _riskAssessor = new Mock<RiskAssessor>() { CallBase = true };

            _companyId = 123L;
            _riskAssessorId = 456L;
            _creatingUserId = Guid.NewGuid();

            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();
            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyIdIncludingDeleted(_riskAssessorId, _companyId))
                .Returns(_riskAssessor.Object);

            _riskAssessorRepository
                .Setup(x => x.SaveOrUpdate(_riskAssessor.Object));

            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(_creatingUserId, _companyId))
                .Returns(new UserForAuditing() { Id = _creatingUserId });

            _request = new MarkRiskAssessorAsDeletedAndUndeletedRequest()
                       {
                           CompanyId = _companyId,
                           RiskAssessorId = _riskAssessorId,
                           UserId = _creatingUserId
                       };

            _log = new Mock<IPeninsulaLog>();
        }

        [Test]
        public void Given_Deleted_Risk_Assessor_When_MarkUndeleted_Then_requested_risk_assessor_is_no_longer_marked_as_deleted()
        {
            // Given
            var target = GetTarget();

            // When
            target.MarkUndeleted(_request);

            // Then
            Assert.That(_riskAssessor.Object.Deleted, Is.False);
        }

        [Test]
        public void Given_Deleted_Risk_Assessor_When_MarkUndeleted_Then_calls_SaveOrUpdate_on_repo()
        {
            // Given
            var target = GetTarget();

            // When
            target.MarkUndeleted(_request);

            // Then
            _riskAssessorRepository.Verify(x=>x.SaveOrUpdate(_riskAssessor.Object));
        }

        [Test]
        public void Given_Deleted_Risk_Assessor_When_MarkUndeleted_Then_calls_logs_request()
        {
            // Given
            var target = GetTarget();

            // When
            target.MarkUndeleted(_request);

            // Then
            _log.Verify(x => x.Add(_request));
        }

        private RiskAssessorService GetTarget()
        {
            return new RiskAssessorService(
                _riskAssessorRepository.Object,
                null,
                null,
                _userForAuditingRepository.Object,
                null,
                null,
                _log.Object);
        }
    }
}

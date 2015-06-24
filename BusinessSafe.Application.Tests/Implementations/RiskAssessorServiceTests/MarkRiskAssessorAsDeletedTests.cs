using System;
using BusinessSafe.Application.Implementations;
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
    public class MarkRiskAssessorAsDeletedTests
    {
        public Mock<IRiskAssessorRepository> _riskAssessorRepository;
        public Mock<IRiskAssessmentRepository> _riskAssessmentRepository;
        public Mock<IPeninsulaLog> _log;
        public Mock<IUserForAuditingRepository> _userForAuditingRepository;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentRepository = new Mock<IRiskAssessmentRepository>();
            _riskAssessorRepository = new Mock<IRiskAssessorRepository>();
            _log = new Mock<IPeninsulaLog>();
            _userForAuditingRepository = new Mock<IUserForAuditingRepository>();
        }

        [Test]
        public void Given_valid_request_When_MarkRiskAssessorAsDeleted_Then_should_call_correct_methods()
        {
            // Given
            var target = GetTarget();
            var request = new MarkRiskAssessorAsDeletedAndUndeletedRequest()
                              {
                                  CompanyId = 100,
                                  UserId = Guid.NewGuid(),
                                  RiskAssessorId = 400
                              };


            var user = new UserForAuditing();
            _userForAuditingRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistForRiskAssessor(request.RiskAssessorId, request.CompanyId))
                .Returns(false);

            var riskAssessor = new Mock<RiskAssessor>();
            _riskAssessorRepository
                .Setup(x => x.GetByIdAndCompanyId(request.RiskAssessorId, request.CompanyId))
                .Returns(riskAssessor.Object);

            // When
            target.MarkDeleted(request);

            // Then
            _riskAssessmentRepository.VerifyAll();
            riskAssessor.Verify(x => x.MarkForDelete(user));
            _userForAuditingRepository.VerifyAll();
            _riskAssessorRepository.Verify(x => x.SaveOrUpdate(riskAssessor.Object));
        }

        [Test]
        public void Given_invalid_request_risk_assessor_got_outstanding_tasks_When_MarkRiskAssessorAsDeleted_Then_should_throw_correct_exception()
        {
            // Given
            var target = GetTarget();
            var request = new MarkRiskAssessorAsDeletedAndUndeletedRequest()
            {
                CompanyId = 100,
                UserId = Guid.NewGuid(),
                RiskAssessorId = 400
            };

            _riskAssessmentRepository
                .Setup(x => x.DoesAssessmentExistForRiskAssessor(request.RiskAssessorId, request.CompanyId))
                .Returns(true);
            
            // When
            // Then
            Assert.Throws<TryingToDeleteRiskAssessorWithOutstandingRiskAssessmentsException>(()=>  target.MarkDeleted(request));
            
        }

        private RiskAssessorService GetTarget()
        {
            return new RiskAssessorService(
                _riskAssessorRepository.Object,
                null,
                null,
                _userForAuditingRepository.Object,
                _riskAssessmentRepository.Object,
                null,
                _log.Object);
        }
    }
}
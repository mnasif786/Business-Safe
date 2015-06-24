using System;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.SignificantFindingServiceTests
{
    [TestFixture]
    public class MarkSignificantFindingAsDeletedTests
    {
        private Mock<IUserForAuditingRepository> _userRepository;
        private Mock<IFireAnswerRepository> _fireAnswerRepository;

        [SetUp]
        public void Setup()
        {
            _userRepository = new Mock<IUserForAuditingRepository>();
            _fireAnswerRepository = new Mock<IFireAnswerRepository>();
        }

        [Test]
        public void When_MarkSignificantFindingAsDeleted_calls_correct_methods()
        {
            // Given
            var target = GetTarget();
            var request = new MarkSignificantFindingAsDeletedRequest
                              {
                                  UserId = Guid.NewGuid(),
                                  CompanyId = 12L
                              };

            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var significantFinding = new Mock<SignificantFinding>();
            var fireAnswer = new FireAnswer
                                 {
                                     SignificantFinding = significantFinding.Object
                                 };

            _fireAnswerRepository
                .Setup(x => x.GetByChecklistIdAndQuestionId(request.FireChecklistId, request.FireQuestionId))
                .Returns(fireAnswer);

            // When
            target.MarkSignificantFindingAsDeleted(request);

            // Then
            _userRepository.Verify(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId));
            _fireAnswerRepository.VerifyAll();
            significantFinding.Verify(x => x.MarkForDelete(user));
            _fireAnswerRepository.Verify(x=>x.SaveOrUpdate(fireAnswer));
        }

        private SignificantFindingService GetTarget()
        {
            return new SignificantFindingService(
                _userRepository.Object,
                _fireAnswerRepository.Object
                );
        }
    }
}
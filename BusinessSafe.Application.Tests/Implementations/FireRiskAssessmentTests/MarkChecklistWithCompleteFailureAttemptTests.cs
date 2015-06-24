using System;
using System.Collections.Generic;
using BusinessSafe.Application.Implementations.FireRiskAssessments;
using BusinessSafe.Application.Request.FireRiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.FireRiskAssessmentTests
{
    [TestFixture]
    public class MarkChecklistWithCompleteFailureAttemptTests
    {
        private Mock<IFireRiskAssessmentChecklistRepository> _fireRiskAssessmentChecklistRepository;
        private Mock<IUserForAuditingRepository> _userRepository;
        
        [SetUp]
        public void Setup()
        {
            _fireRiskAssessmentChecklistRepository = new Mock<IFireRiskAssessmentChecklistRepository>();
            _userRepository = new Mock<IUserForAuditingRepository>();
        }

        [Test]
        public void When_MarkChecklistWithCompleteFailureAttempt_Then_should_call_correct_methods()
        {
            // Given
            var target = GetTarget();
            var request = new MarkChecklistWithCompleteFailureAttemptRequest()
                              {
                                  ChecklistId = 100,
                                  CompanyId = 200,
                                  UserId = Guid.NewGuid()
                              };

            var user = new UserForAuditing();
            _userRepository
                .Setup(x => x.GetByIdAndCompanyId(request.UserId, request.CompanyId))
                .Returns(user);

            var mockFireRiskAssessmentChecklist = new Mock<FireRiskAssessmentChecklist>();
            _fireRiskAssessmentChecklistRepository
                .Setup(x => x.GetById(request.ChecklistId))
                .Returns(mockFireRiskAssessmentChecklist.Object);

            // When
            target.MarkChecklistWithCompleteFailureAttempt(request);

            // Then
            _userRepository.VerifyAll();
            _fireRiskAssessmentChecklistRepository.Verify(x => x.SaveOrUpdate(mockFireRiskAssessmentChecklist.Object));
            mockFireRiskAssessmentChecklist.Verify(x => x.MarkChecklistWithCompleteFailureAttempt(user));
        }
        
        private FireRiskAssessmentChecklistService GetTarget()
        {
            return new FireRiskAssessmentChecklistService(
                _fireRiskAssessmentChecklistRepository.Object,
                _userRepository.Object,
                null,
                null,
                null);
        }
    }
}
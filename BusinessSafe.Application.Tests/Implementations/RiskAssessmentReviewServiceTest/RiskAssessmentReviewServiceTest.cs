using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;
using System;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentReviewServiceTest
{
    [TestFixture]
    public class RiskAssessmentReviewServiceTest : BaseRiskAssessmentReviewTests
    {

        [Test]
        public void Given_valid_request_When_calling_search_method_on_riskassessmentreviews_Then_should_return_correct_results()
        {
            // Given
            const int riskAssessmentId = 1;
            var riskAssessmentReview = new RiskAssessmentReview()
                                           {
                                               RiskAssessment = new GeneralRiskAssessment() { Id = 1 },
                                               ReviewAssignedTo = new Employee{ User = new User{ Id = Guid.NewGuid()}},
                                               CompletedBy = new Employee()
                                           };

            var riskAssessmentReviews = new List<RiskAssessmentReview>() { riskAssessmentReview };

            _riskAssessmentReviewRepo.Setup(x => x.Search(riskAssessmentId)).Returns(riskAssessmentReviews);

            var target = CreateRiskAssessmentReviewService();

            // When
            var result = target.Search(riskAssessmentId);

            // Then
            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Given_valid_request_When_calling_search_method_on_riskassessmentreviews_Then_should_call_correct_methods()
        {
            // Given
            _riskAssessmentReviewRepo.Setup(x => x.Search(It.IsAny<long>())).Returns(
                new List<RiskAssessmentReview>());

            var target = CreateRiskAssessmentReviewService();
            var riskAssessmentId = 1;

            // When
            target.Search(riskAssessmentId);

            // Then
            _riskAssessmentReviewRepo.Verify(x => x.Search(riskAssessmentId), Times.Once());
        }
    }
}
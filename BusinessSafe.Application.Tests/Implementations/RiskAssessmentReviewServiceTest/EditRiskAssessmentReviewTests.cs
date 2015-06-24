using System;

using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Implementations.RiskAssessments;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentReviewServiceTest
{
    [TestFixture]
    [Category("Unit")]
    public class EditRiskAssessmentReviewTests : BaseRiskAssessmentReviewTests
    {

        [Test]
        public void Given_a_valid_request_When_Edit_is_called_Then_review_repo_asked_to_add_a_RiskAssessmentReview()
        {
            //Given
            var target = CreateRiskAssessmentReviewService();
            var request = new EditRiskAssessmentReviewRequest()
            {
                RiskAssessmentReviewId = 1234,
                CompanyId = 5678,
                ReviewDate = DateTime.Now,
                ReviewingEmployeeId = Guid.NewGuid(),
                AssigningUserId = Guid.NewGuid(),
            };

            _employeeRepo
                .Setup(x => x.GetByIdAndCompanyId(request.ReviewingEmployeeId, request.CompanyId))
                .Returns(new Employee() { Id = request.ReviewingEmployeeId, CompanyId = request.CompanyId });

            _userForAuditingRepo
                .Setup(x => x.GetByIdAndCompanyId(request.AssigningUserId, request.CompanyId))
                .Returns(new UserForAuditing() { Id = request.AssigningUserId, CompanyId = request.CompanyId });

            var riskAssessmentReview = new RiskAssessmentReview
            {
                Id = request.RiskAssessmentReviewId,
                RiskAssessmentReviewTask = new RiskAssessmentReviewTask(),
                RiskAssessment = new GeneralRiskAssessment()
            };

            _riskAssessmentReviewRepo
                .Setup(x => x.GetByIdAndCompanyId(request.RiskAssessmentReviewId, request.CompanyId))
                .Returns(riskAssessmentReview);

            //When
            target.Edit(request);

            //Then
            _employeeRepo.VerifyAll();
            _userForAuditingRepo.VerifyAll();
            _riskAssessmentReviewRepo.VerifyAll();
            _riskAssessmentReviewRepo.Verify(x => x.SaveOrUpdate(riskAssessmentReview));
        }

    }
}

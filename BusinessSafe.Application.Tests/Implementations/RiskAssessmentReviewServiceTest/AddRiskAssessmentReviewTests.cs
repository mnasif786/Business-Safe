using System;

using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.RiskAssessmentReviewServiceTest
{
    [TestFixture]
    [Category("Unit")]
    public class AddRiskAssessmentReviewTests : BaseRiskAssessmentReviewTests
    {

        [Test]
        public void Given_a_valid_request_When_AddRiskAssessmentReview_called_Then_review_repo_asked_to_add_a_RiskAssessmentReview()
        {
            //Given
            var target = CreateRiskAssessmentReviewService();
            var request = new AddRiskAssessmentReviewRequest()
                              {
                                  CompanyId = 1234,
                                  ReviewDate = DateTime.Now,
                                  ReviewingEmployeeId = Guid.NewGuid(),
                                  RiskAssessmentId = 1234,
                                  AssigningUserId = Guid.NewGuid()
                              };
            var riskAssessmentReviewToSaveToRepo = new RiskAssessmentReview();

            _employeeRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new Employee() { Id = request.ReviewingEmployeeId, CompanyId = request.CompanyId});

            _userForAuditingRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(new UserForAuditing() { Id = request.AssigningUserId, CompanyId = request.CompanyId });

            _riskAssessmentRepo
                .Setup(x => x.GetByIdAndCompanyId(It.IsAny<long>(), It.IsAny<long>()))
                .Returns(new GeneralRiskAssessment() { Id = request.RiskAssessmentId });

            _riskAssessmentReviewRepo
                .Setup(x => x.Save(It.IsAny<RiskAssessmentReview>()))
                .Callback<RiskAssessmentReview>(x => riskAssessmentReviewToSaveToRepo = x);

            _responsibilityTaskCategoryRepository
                .Setup(x => x.GetGeneralRiskAssessmentTaskCategory())
                .Returns(new TaskCategory {Id = 3});

            //When
            target.Add(request);

            //Then
            _riskAssessmentReviewRepo.Verify(x => x.Save(It.IsAny<RiskAssessmentReview>()), Times.Once());
            Assert.That(riskAssessmentReviewToSaveToRepo.CompletionDueDate, Is.EqualTo(request.ReviewDate));
            Assert.That(riskAssessmentReviewToSaveToRepo.ReviewAssignedTo.Id, Is.EqualTo(request.ReviewingEmployeeId));
            Assert.That(riskAssessmentReviewToSaveToRepo.RiskAssessment.Id, Is.EqualTo(request.RiskAssessmentId));
            Assert.That(riskAssessmentReviewToSaveToRepo.CreatedBy.Id, Is.EqualTo(request.AssigningUserId));
        }
    }
}
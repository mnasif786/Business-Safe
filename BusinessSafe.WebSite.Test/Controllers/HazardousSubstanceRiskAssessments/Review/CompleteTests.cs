using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.Review
{
    [TestFixture]
    public class CompleteTests
    {
        private Mock<IRiskAssessmentService> _riskAssessmentService;
        private Mock<ICompleteReviewViewModelFactory> _completeReviewViewModelFactory;
        private Mock<IHazardousSubstanceRiskAssessmentReviewsViewModelFactory> _reviewsViewModelFactory;
        private ReviewController _target;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IRiskAssessmentService>();
            _completeReviewViewModelFactory = new Mock<ICompleteReviewViewModelFactory>();
            _reviewsViewModelFactory = new Mock<IHazardousSubstanceRiskAssessmentReviewsViewModelFactory>();

            _target = GetTarget();
        }

        [Test]
        [TestCase(123,456,789,true)]
        [TestCase(1,2,3,false)]
        public void populates_view_model(long companyId, long riskAssessmentId, long riskAssessmentReviewId, bool hasUncompletedTasks)
        {
            // Given
            _riskAssessmentService
                .Setup(x => x.HasUncompletedTasks(companyId, riskAssessmentId))
                .Returns(hasUncompletedTasks);

            _completeReviewViewModelFactory
                .Setup(x => x.WithCompanyId(companyId)).Returns(_completeReviewViewModelFactory.Object);
            _completeReviewViewModelFactory
                .Setup(x => x.WithHasUncompletedTasks(hasUncompletedTasks)).Returns(_completeReviewViewModelFactory.Object);
            _completeReviewViewModelFactory
                .Setup(x => x.WithReviewId(riskAssessmentReviewId)).Returns(_completeReviewViewModelFactory.Object);
            _completeReviewViewModelFactory
                .Setup(x => x.WithRiskAssessmentType(RiskAssessmentType.HSRA)).Returns(_completeReviewViewModelFactory.Object);
            _completeReviewViewModelFactory.Setup(x => x.GetViewModel()).Returns(new CompleteReviewViewModel());

            // When
            var result = _target.Complete(companyId, riskAssessmentId, riskAssessmentReviewId);

            // Then
            _completeReviewViewModelFactory.VerifyAll();
        }

        private ReviewController GetTarget()
        {
            return new ReviewController(
                _reviewsViewModelFactory.Object,
                _riskAssessmentService.Object,
                _completeReviewViewModelFactory.Object);
        }
    }
}

using System.Web.Mvc;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.Review
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteTests
    {
        private Mock<ICompleteReviewViewModelFactory> _viewModelFactory;
        private Mock<IRiskAssessmentService> _riskAssessmentService;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        private long _riskAssessmentReviewId = 300;

        [SetUp]
        public void SetUp()
        {
            _viewModelFactory = new Mock<ICompleteReviewViewModelFactory>();
            _riskAssessmentService = new Mock<IRiskAssessmentService>();

            _riskAssessmentService
            .Setup(x => x.HasUncompletedTasks(_companyId, _riskAssessmentId))
            .Returns(true);

            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithReviewId(_riskAssessmentReviewId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithHasUncompletedTasks(true))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                    .Setup(x => x.WithRiskAssessmentType(RiskAssessmentType.PRA))
                    .Returns(_viewModelFactory.Object);
        }

        [Test]
        public void When_get_complete_Then_should_call_appropiate_methods()
        {
            // Given
            var controller = GetTarget();

            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new CompleteReviewViewModel());

            // When
            controller.Complete(_companyId, _riskAssessmentId, _riskAssessmentReviewId);

            // Then
            _viewModelFactory.VerifyAll();
        }

        [Test]
        public void When_get_complete_Then_should_return_correct_view()
        {
            // Given
            var controller = GetTarget();

            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new CompleteReviewViewModel());

            // When
            var result = controller.Complete(_companyId, _riskAssessmentId, _riskAssessmentReviewId);

            // Then
            Assert.That(result.ViewName, Is.EqualTo("_CompleteRiskAssessmentReview"));
        }

        [Test]
        public void When_get_complete_Then_should_return_correct_viewmodel_type()
        {
            // Given
            var controller = GetTarget();

            _viewModelFactory
                    .Setup(x => x.GetViewModel())
                    .Returns(new CompleteReviewViewModel());

            // When
            var result = controller.Complete(_companyId, _riskAssessmentId, _riskAssessmentReviewId) as PartialViewResult;

            // Then
            Assert.That(result.Model, Is.TypeOf<CompleteReviewViewModel>());
        }


        private WebSite.Areas.PersonalRiskAssessments.Controllers.ReviewController GetTarget()
        {
            var result = new WebSite.Areas.PersonalRiskAssessments.Controllers.ReviewController(_riskAssessmentService.Object, _viewModelFactory.Object, null, null, null, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
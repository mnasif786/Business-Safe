using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.Review
{
    [TestFixture]
    public class IndexTests
    {
        private ReviewController target;
        private long _riskAssessmentId;
        private Mock<IFireRiskAssessmentReviewsViewModelFactory> _viewModelFactory;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentId = 100;
            _companyId = 500;

            _viewModelFactory = new Mock<IFireRiskAssessmentReviewsViewModelFactory>();

            target = GetTarget();

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_viewModelFactory.Object);
            
            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);

            var customPrincipal = target.CurrentUser;

            _viewModelFactory
                .Setup(x => x.WithUser(customPrincipal))
                .Returns(_viewModelFactory.Object);
            
            
        }

        [Test]
        public void When_get_Index_Then_should_return_correct_view()
        {
            // Given
            // When
            var result = target.Index(_companyId,_riskAssessmentId ) as ViewResult;

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.That(result.ViewName,Is.EqualTo("Index"));
        }

        [Test] public void When_get_Index_Then_should_returns_correct_viewmodel()
        {
            // Given
            var viewModel = new FireRiskAssessmentReviewsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = target.Index(_companyId, _riskAssessmentId) as ViewResult;
            var model = result.Model;

            // Then
            Assert.IsInstanceOf<FireRiskAssessmentReviewsViewModel>(model);
        }

        [Test]
        public void When_get_Index_Then_should_call_the_correct_methods()
        {
            // Given
            var viewModel = new FireRiskAssessmentReviewsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            target.Index(_companyId, _riskAssessmentId);
            
            // Then
            _viewModelFactory.VerifyAll();
        }

        private ReviewController GetTarget()
        {
            var result = new ReviewController(_viewModelFactory.Object, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }

    }
}

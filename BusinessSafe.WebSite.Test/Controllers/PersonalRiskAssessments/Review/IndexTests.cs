using System.Security.Principal;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using NUnit.Framework;
using Moq;
using System.Web.Mvc;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.Review
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IPersonalRiskAsessmentReviewsViewModelFactory> _viewModelFactory;
        private long _companyId = 100;
        private long _riskAssessmentId = 200;
        
        [SetUp]
        public void SetUp()
        {

            _viewModelFactory = new Mock<IPersonalRiskAsessmentReviewsViewModelFactory>();
            
            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_viewModelFactory.Object);

            _viewModelFactory
                .Setup(x => x.WithUser(It.IsAny<IPrincipal>()))
                .Returns(_viewModelFactory.Object);
        }

        [Test]
        public void When_get_index_Then_should_call_appropiate_methods()
        {
            // Given
            var controller = GetTarget();
            
            var viewModel = new PersonalRiskAssessmentReviewsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            controller.Index(_companyId, _riskAssessmentId);

            // Then
            _viewModelFactory.VerifyAll();
        }

        [Test]
        public void When_get_index_Then_should_return_correct_view()
        {
            // Given
            var controller = GetTarget();
            
            var viewModel = new PersonalRiskAssessmentReviewsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = controller.Index(_companyId, _riskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void When_get_index_Then_should_return_correct_viewmodel_type()
        {
            // Given
            var controller = GetTarget();

            var viewModel = new PersonalRiskAssessmentReviewsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = controller.Index(_companyId, _riskAssessmentId) as ViewResult;

            // Then
            Assert.That(result.Model, Is.TypeOf<PersonalRiskAssessmentReviewsViewModel>());
        }


        private WebSite.Areas.PersonalRiskAssessments.Controllers.ReviewController GetTarget()
        {
            var result = new WebSite.Areas.PersonalRiskAssessments.Controllers.ReviewController(null, null, _viewModelFactory.Object, null, null, null, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}

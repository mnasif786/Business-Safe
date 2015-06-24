using System.Security.Principal;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using BusinessSafe.WebSite.Factories;
using BusinessSafe.WebSite.Models;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories
{
    [TestFixture]
    public class FireRiskAssessmentReviewsViewModelFactoryTests
    {
        private FireRiskAssessmentReviewsViewModelFactory _target;
        private Mock<IRiskAssessmentReviewsViewModelFactory> _reviewModelFactory;
        private long _companyId = 500;
        private long _riskAssessmentId = 888;
        private Mock<IPrincipal> _user;

        [SetUp]
        public void Setup()
        {
            _user = new Mock<IPrincipal>();
            _reviewModelFactory = new Mock<IRiskAssessmentReviewsViewModelFactory>();

            _reviewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_reviewModelFactory.Object);

            _reviewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_reviewModelFactory.Object);

            _reviewModelFactory
                .Setup(x => x.WithUser(_user.Object))
                .Returns(_reviewModelFactory.Object);

            _target = new FireRiskAssessmentReviewsViewModelFactory(_reviewModelFactory.Object);
        }

        [Test]
        public void When_GetViewModel_should_call_correct_methods()
        {
            // Given
            var viewModel = new RiskAssessmentReviewsViewModel();
            _reviewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            _target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithUser(_user.Object)
                .GetViewModel();

            // Then
            _reviewModelFactory.VerifyAll();

        }

        [Test]
        public void When_GetViewModel_should_return_correct_result()
        {
            // Given
            var viewModel = new RiskAssessmentReviewsViewModel();
            _reviewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = _target
                .WithCompanyId(_companyId)
                .WithRiskAssessmentId(_riskAssessmentId)
                .WithUser(_user.Object)
                .GetViewModel();

            // Then
            Assert.That(result, Is.TypeOf<FireRiskAssessmentReviewsViewModel>());
            Assert.That(result.ReviewViewModel, Is.Not.Null);
            Assert.That(result.ReviewViewModel.RiskAssessmentType, Is.EqualTo(RiskAssessmentType.FRA));

        }
    }
}
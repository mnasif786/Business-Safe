using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.SignificantFindingsTests
{
    [TestFixture]
    public class IndexTests
    {
        private SignificantFindingsController target;
        private long _riskAssessmentId;
        private Mock<ISignificantFindingViewModelFactory> _viewModelFactory;
        private long _companyId;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentId = 100;
            _companyId = 500;

            _viewModelFactory = new Mock<ISignificantFindingViewModelFactory>();

            target = GetTarget();

            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(_riskAssessmentId))
                .Returns(_viewModelFactory.Object);
            
            _viewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_viewModelFactory.Object);
            
            
        }

        [Test]
        public void When_get_Index_Then_should_return_correct_view()
        {
            // Given
            // When
            var result = target.Index(_riskAssessmentId ,_companyId) as ViewResult;

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.That(result.ViewName,Is.EqualTo("Index"));
        }

        [Test]
        public void When_get_Index_Then_should_returns_correct_viewmodel()
        {
            // Given
            var viewModel = new RiskAssessmentSignificantFindingsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            var result = target.Index(_riskAssessmentId, _companyId) as ViewResult;
            var model = result.Model;

            // Then
            Assert.IsInstanceOf<RiskAssessmentSignificantFindingsViewModel>(model);
        }

        [Test]
        public void When_get_Index_Then_should_call_the_correct_methods()
        {
            // Given
            var viewModel = new RiskAssessmentSignificantFindingsViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            // When
            target.Index(_riskAssessmentId, _companyId);
            
            // Then
            _viewModelFactory.VerifyAll();
        }

        private SignificantFindingsController GetTarget()
        {
            var result = new SignificantFindingsController(_viewModelFactory.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }

    }
}

using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments.Summary
{
    [TestFixture]
    public class IndexTests
    {
        private SummaryController target;
        private long _riskAssessmentId;
        private Mock<IEditFireRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        
        [SetUp]
        public void Setup()
        {
            _riskAssessmentId = 100;
            _viewModelFactory = new Mock<IEditFireRiskAssessmentSummaryViewModelFactory>();
            _viewModelFactory
                .Setup(x => x.WithRiskAssessmentId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.WithAllowableSiteIds(It.IsAny<IList<long>>()))
                .Returns(_viewModelFactory.Object);
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new EditSummaryViewModel());
            
            target = GetTarget();
        }

        [Test]
        public void When_get_Index_Then_should_return_correct_view()
        {
            // Given
            // When
            var result = target.Index(_riskAssessmentId,It.IsAny<long>()) as ViewResult;

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.That(result.ViewName,Is.EqualTo("Index"));
        }

        [Test] public void When_get_Index_Then_should_returns_correct_viewmodel()
        {
            // Given

            // When
            var result = target.Index(_riskAssessmentId,It.IsAny<long>()) as ViewResult;
            var model = result.Model;

            // Then
            Assert.IsInstanceOf<EditSummaryViewModel>(model);
        }

        [Test]
        public void When_get_Index_Then_should_call_the_correct_methods()
        {
            // Given
            var allowableSites = target.CurrentUser.GetSitesFilter();

            // When
            target.Index(_riskAssessmentId,It.IsAny<long>());
            
            // Then
            _viewModelFactory.Verify(x => x.WithRiskAssessmentId(_riskAssessmentId), Times.Once());
            _viewModelFactory.Verify(x => x.WithCompanyId(It.IsAny<long>()), Times.Once());
            _viewModelFactory.Verify(x => x.WithAllowableSiteIds(allowableSites));
            _viewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        private SummaryController GetTarget()
        {
            var result = new SummaryController(_viewModelFactory.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }

    }
}

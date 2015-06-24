using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments.Summary
{
    [TestFixture]
    public class IndexTests
    {
        private SummaryController target;
        private long _riskAssessmentId;
        private Mock<IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentId = 100;
            _viewModelFactory = new Mock<IEditHazardousSubstanceRiskAssessmentSummaryViewModelFactory>();
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

            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();

            target = GetTarget();
        }

        [Test]
        public void When_Index_Then_Returns_View()
        {
            // Given

            // When
            var result = target.Index(_riskAssessmentId, It.IsAny<long>());

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Given_Index_Then_Returns_View_Has_SummaryViewModel()
        {
            // Given

            // When
            var result = target.Index(_riskAssessmentId, It.IsAny<long>()) as ViewResult;
            var model = result.Model;

            // Then
            Assert.IsInstanceOf<EditSummaryViewModel>(model);
        }

        [Test]
        public void Given_Index_Then_Gets_SummaryViewModel_From_Factory()
        {
            // Given

            // When
            var result = target.Index(_riskAssessmentId, TestControllerHelpers.CompanyIdAssigned) as ViewResult;
            var model = result.Model as EditSummaryViewModel;

            // Then
            _viewModelFactory.Verify(x => x.WithRiskAssessmentId(_riskAssessmentId), Times.Once());
            _viewModelFactory.Verify(x => x.WithCompanyId(TestControllerHelpers.CompanyIdAssigned), Times.Once());
            _viewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        private SummaryController GetTarget()
        {
            var result = new SummaryController(_riskAssessmentService.Object, _viewModelFactory.Object);

            return TestControllerHelpers.AddUserToController(result);
        }

    }
}

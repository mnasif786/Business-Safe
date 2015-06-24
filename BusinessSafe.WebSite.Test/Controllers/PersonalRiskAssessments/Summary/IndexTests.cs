using System;
using System.Collections.Generic;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments.Summary
{
    [TestFixture]
    public class IndexTests
    {
        private SummaryController _target;
        private long _riskAssessmentId;
        private Mock<IEditPersonalRiskAssessmentSummaryViewModelFactory> _viewModelFactory;
        private Mock<IPersonalRiskAssessmentService> _riskAssessmentService;
        private Guid _currentUserId;
        
        [SetUp]
        public void Setup()
        {
            _riskAssessmentId = 100;
            _viewModelFactory = new Mock<IEditPersonalRiskAssessmentSummaryViewModelFactory>();
           
            var viewModel = new EditSummaryViewModel();
            _viewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(viewModel);

            _riskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
        
            _target = GetTarget();
            _currentUserId = _target.CurrentUser.UserId;

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
                .Setup(x => x.WithCurrentUserId(_currentUserId))
                .Returns(_viewModelFactory.Object);
        }

        [Test]
        public void When_Index_Then_Returns_View()
        {
            // Given

            // When
            var result = _target.Index(_riskAssessmentId, It.IsAny<long>());

            // Then
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Given_Index_Then_Returns_View_Has_SummaryViewModel()
        {
            // Given

            // When
            var result = _target.Index(_riskAssessmentId, It.IsAny<long>()) as ViewResult;
            var model = result.Model;

            // Then
            Assert.IsInstanceOf<EditSummaryViewModel>(model);
        }

        [Test]
        public void Given_Index_Then_Gets_SummaryViewModel_From_Factory()
        {
            // Given

            // When
            _target.Index(_riskAssessmentId, It.IsAny<long>());

            // Then
            _viewModelFactory.Verify(x => x.WithRiskAssessmentId(_riskAssessmentId), Times.Once());
            _viewModelFactory.Verify(x => x.WithCompanyId(It.IsAny<long>()), Times.Once());
            _viewModelFactory.Verify(x => x.GetViewModel(), Times.Once());
        }

        private SummaryController GetTarget()
        {
            var result = new SummaryController(_riskAssessmentService.Object,_viewModelFactory.Object);

            return TestControllerHelpers.AddUserToController(result);
        }

    }
}
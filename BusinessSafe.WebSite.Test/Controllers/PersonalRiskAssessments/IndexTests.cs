using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.PersonalRiskAssessments;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.PersonalRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;

using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.PersonalRiskAssessments
{
    [TestFixture]
    [Category("Unit")]
    public class IndexWithViewModelTests
    {
        private Mock<IPersonalRiskAssessmentService> _personalRiskAssessmentService;
        private Mock<ISearchPersonalRiskAssessmentsViewModelFactory> _searchRiskAssessmentViewModelFactory;
        private readonly long _companyId = TestControllerHelpers.CompanyIdAssigned;
        private readonly string _createdFrom = DateTime.Now.ToShortDateString();
        private readonly string _createdTo = DateTime.Now.ToShortDateString();
        private const bool _showDeleted = false;
        private List<long> _allowedSites;

        private RiskAssessmentSearchViewModel _searchViewModel;

        [SetUp]
        public void SetUp()
        {
            _allowedSites = new List<long>() { 123L, 456L, 789L };

            _searchViewModel = new RiskAssessmentSearchViewModel()
                              {
                                  title="title",
                                  companyId = _companyId,
                                  createdFrom = _createdFrom,
                                  createdTo = _createdTo,
                                  siteGroupId = null,
                                  siteId = null,
                                  showDeleted = _showDeleted,
                                  showArchived = false,
                                  isGeneralRiskAssessmentTemplating = false
                              };

            _personalRiskAssessmentService = new Mock<IPersonalRiskAssessmentService>();
            _searchRiskAssessmentViewModelFactory = new Mock<ISearchPersonalRiskAssessmentsViewModelFactory>();

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithTitle(It.IsAny<string>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithCompanyId(_companyId))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithCreatedFrom(It.IsAny<string>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithCreatedTo(It.IsAny<string>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithSiteGroupId(It.IsAny<long?>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long?>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithShowDeleted(It.IsAny<bool>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithShowArchived(It.IsAny<bool>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithRiskAssessmentTemplatingMode(It.IsAny<bool>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new SearchRiskAssessmentsViewModel());

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithCurrentUserId(It.IsAny<Guid>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithPageNumber(It.IsAny<int>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithPageSize(It.IsAny<int>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithOrderBy(It.IsAny<string>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

        }

        [Test]
        public void Given_get_When_personal_risk_assessment_index_page_Then_should_return_index_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_personal_risk_assessment_index_page_Then_should_return_PersonalRiskAssessmentsViewModel()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<SearchRiskAssessmentsViewModel>());
        }

        [Test]
        public void Given_get_When_personal_risk_assessment_index_page_When_request_show_archived_Then_should_return_index_view()
        {
            // Given
            _searchViewModel.showArchived = true;
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_personal_risk_assessment_index_page_When_request_show_archived_Then_should_return_correct_a_SearchPersonalRiskAssessmentsViewModel()
        {
            // Given
            _searchViewModel.showArchived = true;
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.ViewData.Model, Is.InstanceOf<SearchRiskAssessmentsViewModel>());
        }

        [Test]
        public void Given_get_When_personal_risk_assessment_index_page_When_request_show_archived_Then_should_ask_viewmodelfactory_to_set_with_all_settings()
        {
            // Given
            _searchViewModel.showArchived = true;
            var controller = GetTarget();

            // When
            controller.Index(_searchViewModel);

            // Then
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithTitle(_searchViewModel.title), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithShowArchived(true), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithShowDeleted(_showDeleted), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithCompanyId(_companyId), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithCreatedFrom(_createdFrom), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithCreatedTo(_createdTo), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithCurrentUserId(TestControllerHelpers.UserIdAssigned), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithRiskAssessmentTemplatingMode(_searchViewModel.isGeneralRiskAssessmentTemplating), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithAllowedSiteIds(_allowedSites), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithSiteGroupId(_searchViewModel.siteGroupId), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithSiteId(_searchViewModel.siteId), Times.Once());
        }

        private RiskAssessmentController GetTarget()
        {
            var result = new RiskAssessmentController(_searchRiskAssessmentViewModelFactory.Object, _personalRiskAssessmentService.Object);

            return TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(result, _allowedSites);
        }        
    }
}
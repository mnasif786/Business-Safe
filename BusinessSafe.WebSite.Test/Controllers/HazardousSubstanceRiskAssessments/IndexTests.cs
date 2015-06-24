using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.ViewModels;
using BusinessSafe.WebSite.ViewModels;

using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.HazardousSubstanceRiskAssessments
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private Mock<IHazardousSubstanceRiskAssessmentService> _riskAssessmentService;
        private readonly long _companyId = TestControllerHelpers.CompanyIdAssigned;
        private readonly string _createdFrom = DateTime.Now.ToShortDateString();
        private readonly string _createdTo = DateTime.Now.ToShortDateString();
        private List<long> _allowedSites;

        private HazardousSubstanceRiskAssessmentSearchViewModel _searchViewModel;

        private Mock<IHazardousSubstanceRiskAssessmentService> _hazardousSubstanceAssessmentService;
        private Mock<ISearchRiskAssessmentsViewModelFactory> _searchViewModelFactory;
        private Mock<ICreateRiskAssessmentViewModelFactory> _createViewModelFactory;

        [SetUp]
        public void SetUp()
        {
            _allowedSites = new List<long>() {123L, 456L, 789L};

            _searchViewModel = new HazardousSubstanceRiskAssessmentSearchViewModel()
            {
                title = "title",
                companyId = _companyId,
                createdFrom = _createdFrom,
                createdTo = _createdTo,
                siteGroupId = null,
                siteId = null,
                showDeleted = false,
                showArchived = false,
                isGeneralRiskAssessmentTemplating = false,
                hazardousSubstanceId = 123L,
                Page = 1,
                Size = 10,
                OrderBy = "Title-desc"
            };

            _hazardousSubstanceAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
            _searchViewModelFactory = new Mock<ISearchRiskAssessmentsViewModelFactory>() {CallBase = true};

            _searchViewModelFactory
                .Setup(x => x.WithTitle(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCreatedFrom(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCreatedTo(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithHazardousSubstanceId(It.IsAny<long>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithAllowedSiteIds(It.IsAny<IList<long>>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithCurrentUserId(It.IsAny<Guid>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithSiteId(It.IsAny<long?>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithSiteGroupId(It.IsAny<long?>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithShowArchived(It.IsAny<bool>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithShowDeleted(It.IsAny<bool>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithPageNumber(It.IsAny<int>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.WithPageSize(It.IsAny<int>()))
                .Returns(_searchViewModelFactory.Object);

            _searchViewModelFactory
                .Setup(x => x.GetViewModel(_hazardousSubstanceAssessmentService.Object))
                .Returns(new SearchRiskAssessmentsViewModel());

            _searchViewModelFactory
                .Setup(x => x.WithOrderBy(It.IsAny<string>()))
                .Returns(_searchViewModelFactory.Object);

            _createViewModelFactory = new Mock<ICreateRiskAssessmentViewModelFactory>();

            _createViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);

            _createViewModelFactory
                .Setup(x => x.WithNewHazardousSubstanceId(It.IsAny<long>()))
                .Returns(_createViewModelFactory.Object);
            
            _createViewModelFactory
                .Setup(x => x.GetViewModel())
                .Returns(new CreateRiskAssessmentViewModel());

            _riskAssessmentService = new Mock<IHazardousSubstanceRiskAssessmentService>();
        }

        [Test]
        public void Given_get_When_hazardous_substances_assessment_index_page_Then_should_return_index_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void Given_get_When_hazardous_substances_assessment_index_page_Then_should_return_HazardousSubstanceAssessmentsViewModel()
        {
            // Given
            var controller = GetTarget();

            _searchViewModelFactory
               .Setup(x => x.GetViewModel(_riskAssessmentService.Object))
               .Returns(new SearchRiskAssessmentsViewModel());

            // When
            var result = controller.Index(_searchViewModel);

            // Then
            Assert.That(result.Model, Is.InstanceOf<SearchRiskAssessmentsViewModel>());
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
            _searchViewModelFactory.Verify(x => x.WithTitle(_searchViewModel.title), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithShowArchived(true), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithShowDeleted(_searchViewModel.showDeleted), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithCompanyId(_companyId), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithCreatedFrom(_createdFrom), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithCreatedTo(_createdTo), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithCurrentUserId(TestControllerHelpers.UserIdAssigned), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithAllowedSiteIds(_allowedSites), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithSiteGroupId(_searchViewModel.siteGroupId), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithSiteId(_searchViewModel.siteId), Times.Once());
            _searchViewModelFactory.Verify(x => x.WithHazardousSubstanceId(_searchViewModel.hazardousSubstanceId), Times.Once());
        }

        private RiskAssessmentController GetTarget()
        {
            var target =  new RiskAssessmentController(_searchViewModelFactory.Object, _createViewModelFactory.Object, _riskAssessmentService.Object, null);
            return TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(target, _allowedSites);
        }
    }
}
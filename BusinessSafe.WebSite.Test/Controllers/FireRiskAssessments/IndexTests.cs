﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Controllers;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.ViewModels;

using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.FireRiskAssessments
{
    [TestFixture]
    [Category("Unit")]
    public class IndexTests
    {
        private List<long> _allowedSites;

        private RiskAssessmentSearchViewModel _searchViewModel;

        private Mock<ISearchRiskAssessmentViewModelFactory> _searchRiskAssessmentViewModelFactory;
        private Mock<IFireRiskAssessmentService> _fireRiskAssessmentService;

        [SetUp]
        public void SetUp()
        {
            _allowedSites = new List<long>() { 123L, 456L, 789L };

            _searchViewModel = new RiskAssessmentSearchViewModel()
            {
                title = "title",
                companyId = TestControllerHelpers.CompanyIdAssigned,
                createdFrom = DateTime.Now.ToShortDateString(),
                createdTo = DateTime.Now.ToShortDateString(),
                siteGroupId = null,
                siteId = null,
                showDeleted = false,
                showArchived = false,
                isGeneralRiskAssessmentTemplating = false
            };
            _fireRiskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _searchRiskAssessmentViewModelFactory = new Mock<ISearchRiskAssessmentViewModelFactory>();

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithCompanyId(It.IsAny<long>()))
                .Returns(_searchRiskAssessmentViewModelFactory.Object);

            _searchRiskAssessmentViewModelFactory
                .Setup(x => x.WithTitle(It.IsAny<string>()))
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
        public void When_get_fire_risk_assessment_index_page_Then_should_return_index_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_fire_risk_assessment_index_page_Then_should_return_SearchRiskAssessmentsViewModel()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.Model, Is.InstanceOf<SearchRiskAssessmentsViewModel>());
        }

        [Test]
        public void Given_get_When_fire_risk_assessment_index_page_When_request_show_archived_Then_should_return_index_view()
        {
            // Given
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }

        [Test]
        public void Given_get_When_fire_risk_assessment_index_page_When_request_show_archived_Then_should_return_correct_a_SearchRiskAssessmentsViewModel()
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
        public void Given_get_When_fire_risk_assessment_index_page_When_request_show_archived_Then_should_ask_viewmodelfactory_to_set_with_all_settings()
        {
            // Given
            _searchViewModel.showArchived = true;
            var controller = GetTarget();

            // When
            var result = controller.Index(_searchViewModel) as ViewResult;

            // Then
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithTitle(_searchViewModel.title), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithShowArchived(_searchViewModel.showArchived), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithShowDeleted(_searchViewModel.showDeleted), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithCompanyId(_searchViewModel.companyId.Value), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithCreatedFrom(_searchViewModel.createdFrom), Times.Once());
            _searchRiskAssessmentViewModelFactory.Verify(x => x.WithCreatedTo(_searchViewModel.createdTo), Times.Once());
        }

        private RiskAssessmentController GetTarget()
        {
            var target = new RiskAssessmentController(_searchRiskAssessmentViewModelFactory.Object, _fireRiskAssessmentService.Object);
            return TestControllerHelpers.AddUserWithDefinableAllowedSitesToController(target, _allowedSites);
        }
    }
}
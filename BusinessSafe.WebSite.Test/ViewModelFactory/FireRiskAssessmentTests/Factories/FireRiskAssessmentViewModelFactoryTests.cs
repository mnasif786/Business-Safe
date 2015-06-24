using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.FireRiskAssessments;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.Factories;
using BusinessSafe.WebSite.Areas.FireRiskAssessments.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.FireRiskAssessmentTests.Factories
{
    [TestFixture]
    public class FireRiskAssessmentViewModelFactoryTests
    {
        private Mock<IFireRiskAssessmentService> _riskAssessmentService;
        private SearchRiskAssessmentViewModelFactory _target;
        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<ISiteService> _siteService;

        [SetUp]
        public void Setup()
        {
            _riskAssessmentService = new Mock<IFireRiskAssessmentService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _siteService = new Mock<ISiteService>();

            _riskAssessmentService
                .Setup(x => x.Search(It.IsAny<SearchRiskAssessmentsRequest>()))
                .Returns(new List<FireRiskAssessmentDto>());


            _target = new SearchRiskAssessmentViewModelFactory(_siteGroupService.Object, _riskAssessmentService.Object, _siteService.Object);
        }

        [Test]
        public void GetViewModel_returns_FireRiskAssessmentsViewModel()
        {
            // Given

            // When
            var result = _target.GetViewModel();

            // Then
            Assert.That(result, Is.InstanceOf<SearchRiskAssessmentsViewModel>());
        }

        [Test]
        public void When_values_are_passed_Then_values_are_set()
        {
            // Given
            var companyId = 123;
            var from = DateTime.Now.AddDays(-1).ToShortDateString();
            var to = DateTime.Now.ToShortDateString();
            _target = (SearchRiskAssessmentViewModelFactory)_target
                .WithCompanyId(companyId)
                .WithCreatedFrom(from)
                .WithCreatedTo(to);

            // When
            var result = _target.GetViewModel();

            // Then
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.CreatedFrom, Is.EqualTo(from));
            Assert.That(result.CreatedTo, Is.EqualTo(to));
        }

        [Test]
        public void When_values_are_passed_Then_riskAssessmentService_Search_receives_passed_values()
        {
            // Given
            var companyId = 123;
            var from = DateTime.Now.AddDays(-1);
            var to = DateTime.Now;
            var showDeleted = true;
            var showArchived = true;

            _target = (SearchRiskAssessmentViewModelFactory)_target
                .WithCompanyId(companyId)
                .WithCreatedFrom(from.ToShortDateString())
                .WithCreatedTo(to.ToShortDateString())
                .WithShowDeleted(showDeleted)
                .WithShowArchived(showArchived);

            var passedSearchRiskAssessmentsRequest = new SearchRiskAssessmentsRequest();

            _riskAssessmentService
                .Setup(x => x.Search(It.IsAny<SearchRiskAssessmentsRequest>()))
                .Returns(new List<FireRiskAssessmentDto>())
                .Callback<SearchRiskAssessmentsRequest>(y => passedSearchRiskAssessmentsRequest = y);

            // When
            _target.GetViewModel();

            // Then
            Assert.That(passedSearchRiskAssessmentsRequest.CompanyId, Is.EqualTo(companyId));
            Assert.That(passedSearchRiskAssessmentsRequest.CreatedFrom.Value, Is.EqualTo(from.Date));
            Assert.That(passedSearchRiskAssessmentsRequest.CreatedTo.Value, Is.EqualTo(to.Date));
            Assert.That(passedSearchRiskAssessmentsRequest.ShowDeleted, Is.EqualTo(showDeleted));
            Assert.That(passedSearchRiskAssessmentsRequest.ShowArchived, Is.EqualTo(showArchived));
        }

        [Test]
        public void When_allowed_sites_are_passed_Then_search_is_called_on_the_siteservice()
        {
            // Given
            var companyId = 123;
            var from = DateTime.Now.AddDays(-1).ToShortDateString();
            var to = DateTime.Now.ToShortDateString();
            var allowedSites = new List<long>() { 1, 2 };

            _siteService.Setup(x => x.Search(It.IsAny<SearchSitesRequest>()));
            _target = (SearchRiskAssessmentViewModelFactory)_target
                                                                 .WithCompanyId(companyId)
                                                                 .WithCreatedFrom(from)
                                                                 .WithCreatedTo(to)
                                                                 .WithAllowedSiteIds(allowedSites);

            // When
            _target.GetViewModel();

            // Then
            _siteService.Verify(s => s.Search(It.IsAny<SearchSitesRequest>()), Times.Once());
        }
    }
}

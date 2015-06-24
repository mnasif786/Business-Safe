using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.Contracts.Responsibilities;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Responsibilities;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Responsibilities.Factories;
using BusinessSafe.WebSite.AuthenticationService;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory.Responsibilities
{
    [TestFixture]
    [Category("Unit")]
    public class ResponsibilitiesIndexViewModelTests
    {
        private Mock<IResponsibilitiesService> _responsibilitiesService;
        private Mock<ISiteGroupService> _siteGroupService;
        private Mock<ISiteService> _siteService;

        private long _companyId;

        [SetUp]
        public void SetUp()
        {
            _responsibilitiesService = new Mock<IResponsibilitiesService>();
            _siteGroupService = new Mock<ISiteGroupService>();
            _siteService = new Mock<ISiteService>();

            _responsibilitiesService
                .Setup(x => x.GetResponsibilityCategories())
                .Returns(new List<ResponsibilityCategoryDto>());

            _responsibilitiesService
                .Setup(x => x.Search(It.IsAny<SearchResponsibilitiesRequest>()))
                .Returns(new List<ResponsibilityDto>());

            _siteGroupService
                .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(new List<SiteGroupDto>());

            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(new List<SiteDto>());
            
            _companyId = 250;
        }

        [Test]
        public void Given_search_by_companyId_When_GetViewModel_is_called_Then_category_drop_down_data_is_retrieved()
        {
            //Given
            var target = CreateTarget();

            //When
            target
                .WithCompanyId(_companyId)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.GetResponsibilityCategories());
        }

        [Test]
        public void Given_search_by_companyId_When_GetViewModel_is_called_Then_site_drop_down_data_is_retrieved()
        {
            //Given
            var target = CreateTarget();

            //When
            target
                .WithCompanyId(_companyId)
                .GetViewModel();

            //Then
            _siteService.Verify(x => x.Search(It.IsAny<SearchSitesRequest>()));
        }

        [Test]
        public void Given_search_by_companyId_When_GetViewModel_is_called_Then_sitegroup_drop_down_data_is_retrieved()
        {
            //Given
            var target = CreateTarget();

            //When
            target
                .WithCompanyId(_companyId)
                .GetViewModel();

            //Then
            _siteGroupService.Verify(x => x.GetByCompanyId(_companyId));
        }

        [Test]
        public void Given_search_by_companyId_When_GetViewModel_is_called_Then_undeleted_responsibilities_for_that_company_are_retrieved()
        {
            //Given
            var target = CreateTarget();

            //When
            target
                .WithCompanyId(_companyId)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.CompanyId == _companyId &&
                y.ShowDeleted == false
            )));
        }

        [Test]
        public void Given_search_by_category_When_GetViewModel_is_called_Then_responsibilities_for_that_category_are_retrieved()
        {
            //Given
            const long categoryId = 1234L;
            var target = CreateTarget();

            //When
            target
                .WithCategoryId(categoryId)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.ResponsibilityCategoryId == categoryId
            )));
        }

        [Test]
        public void Given_search_by_site_When_GetViewModel_is_called_Then_responsibilities_for_that_site_are_retrieved()
        {
            //Given
            const long siteId = 1234L;
            var target = CreateTarget();

            //When
            target
                .WithSiteId(siteId)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.SiteId == siteId
            )));
        }

        [Test]
        public void Given_search_by_sitegroup_When_GetViewModel_is_called_Then_responsibilities_for_that_sitegroup_are_retrieved()
        {
            //Given
            const long siteGroupId = 1234L;
            var target = CreateTarget();

            //When
            target
                .WithSiteGroupId(siteGroupId)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.SiteGroupId == siteGroupId
            )));
        }

        [Test]
        public void Given_search_by_title_When_GetViewModel_is_called_Then_responsibilities_for_that_title_are_retrieved()
        {
            //Given
            const string title = "title";
            var target = CreateTarget();

            //When
            target
                .WithTitle(title)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.Title == title
            )));
        }

        [Test]
        public void Given_search_by_createdFrom_When_GetViewModel_is_called_Then_responsibilities_created_on_or_after_are_retrieved()
        {
            //Given
            var from = DateTime.Now;
            var target = CreateTarget();

            //When
            target
                .WithCreatedFrom(from)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.CreatedFrom.Value.Date == from.Date
            )));
        }

        [Test]
        public void Given_search_by_createdTo_When_GetViewModel_is_called_Then_responsibilities_created_on_or_before_are_retrieved()
        {
            //Given
            var to = DateTime.Now;
            var target = CreateTarget();

            //When
            target
                .WithCreatedTo(to)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.CreatedTo.Value.Date == to.Date
            )));
        }

        [Test]
        public void Given_search_deleted_When_GetViewModel_is_called_Then_deleted_responsibilities_are_retrieved()
        {
            //Given
            const bool deleted = true;
            var target = CreateTarget();

            //When
            target
                .WithShowDeleted(deleted)
                .GetViewModel();

            //Then
            _responsibilitiesService.Verify(x => x.Search(It.Is<SearchResponsibilitiesRequest>(y =>
                y.ShowDeleted == deleted
            )));
        }

        [Test]
        public void Given_search_with_parameters_When_GetViewModel_parameters_set_in_returned_viewModel()
        {
            //Given
            const long categoryId = 123L;
            const long siteId = 324234L;
            const long siteGroupId = 157473L;
            const bool deleted = true;
            var from = DateTime.Now.AddDays(-124);
            var to = DateTime.Now.AddDays(-3231);
            const string title = "title";

            var target = CreateTarget();

            //When
            var result = target
                .WithCategoryId(categoryId)
                .WithCreatedFrom(from)
                .WithCreatedTo(to)
                .WithTitle(title)
                .WithSiteId(siteId)
                .WithSiteGroupId(siteGroupId)
                .WithShowDeleted(deleted)
                .GetViewModel();

            //Then
            Assert.That(result.CategoryId, Is.EqualTo(categoryId));
            Assert.That(result.SiteId, Is.EqualTo(siteId));
            Assert.That(result.SiteGroupId, Is.EqualTo(siteGroupId));
            Assert.That(result.IsShowDeleted, Is.EqualTo(deleted));
            Assert.That(DateTime.Parse(result.CreatedFrom), Is.EqualTo(from.Date));
            Assert.That(DateTime.Parse(result.CreatedTo), Is.EqualTo(to.Date));
            Assert.That(result.Title, Is.EqualTo(title));
        }

        [Test]
        public void Given_search_with_parameters_When_GetViewModel_return_only_allowed_sites_in_viewModel()
        {
            //Given
            const long categoryId = 123L;
            const long siteId = 1L;
            const long siteGroupId = 157473L;
            const bool deleted = true;
            var from = DateTime.Now.AddDays(-124);
            var to = DateTime.Now.AddDays(-3231);
            const string title = "title";
            IList<long> allowedSites = new List<long>() { siteId };
            var target = CreateTarget();

            var site1 = new SiteDto() { Id = siteId };
            
            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Returns(new List<SiteDto>() { site1 });

            var searchSitesRequest = new SearchSitesRequest();
            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                    .Returns(new List<SiteDto>() { site1 })
                        .Callback<SearchSitesRequest>(y => searchSitesRequest = y);

            //When
            var result = target
                .WithCategoryId(categoryId)
                .WithCreatedFrom(from)
                .WithCreatedTo(to)
                .WithTitle(title)
                .WithSiteId(siteId)
                .WithSiteGroupId(siteGroupId)
                .WithShowDeleted(deleted)
                .WithAllowedSiteIds(allowedSites)
                .GetViewModel();

            //Then
            Assert.That(result.CategoryId, Is.EqualTo(categoryId));
            Assert.That(result.SiteId, Is.EqualTo(siteId));
            Assert.That(result.SiteGroupId, Is.EqualTo(siteGroupId));
            Assert.That(result.IsShowDeleted, Is.EqualTo(deleted));
            Assert.That(DateTime.Parse(result.CreatedFrom), Is.EqualTo(from.Date));
            Assert.That(DateTime.Parse(result.CreatedTo), Is.EqualTo(to.Date));
            Assert.That(result.Title, Is.EqualTo(title));
            Assert.That(result.Sites.Count(), Is.EqualTo(2)); //One for autocomplete
            Assert.That(result.Sites.Last().value, Is.EqualTo(siteId.ToString()));
            Assert.That(searchSitesRequest.AllowedSiteIds.Count, Is.EqualTo(allowedSites.Count));
        }

        [Test]
        public void Given_search_with_parameters_When_GetViewModel_return_only_allowed_site_groups_in_viewModel()
        {
            //Given
            const long categoryId = 123L;
            const long siteId = 324234L;
            const long siteGroupId = 2L;
            const bool deleted = true;
            var from = DateTime.Now.AddDays(-124);
            var to = DateTime.Now.AddDays(-3231);
            const string title = "title";
            IList<long> allowedSites = new List<long>() { siteGroupId };
            var target = CreateTarget();

            //When
            var siteGroup1 = new SiteGroupDto() { Id = 1L };
            var siteGroup2 = new SiteGroupDto() { Id = 2L };

            _siteGroupService
                .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
                .Returns(new List<SiteGroupDto>() { siteGroup1, siteGroup2 });

            var result = target
                .WithCategoryId(categoryId)
                .WithCreatedFrom(from)
                .WithCreatedTo(to)
                .WithTitle(title)
                .WithSiteId(siteId)
                .WithSiteGroupId(siteGroupId)
                .WithShowDeleted(deleted)
                .WithAllowedSiteIds(allowedSites)
                .GetViewModel();

            //Then
            Assert.That(result.CategoryId, Is.EqualTo(categoryId));
            Assert.That(result.SiteId, Is.EqualTo(siteId));
            Assert.That(result.SiteGroupId, Is.EqualTo(siteGroupId));
            Assert.That(result.IsShowDeleted, Is.EqualTo(deleted));
            Assert.That(DateTime.Parse(result.CreatedFrom), Is.EqualTo(from.Date));
            Assert.That(DateTime.Parse(result.CreatedTo), Is.EqualTo(to.Date));
            Assert.That(result.Title, Is.EqualTo(title));
            Assert.That(result.SiteGroups.Count(), Is.EqualTo(2));
            Assert.That(result.SiteGroups.Last().value, Is.EqualTo(siteGroupId.ToString()));
        }

        //[Test]
        //public void Given_search_with_parameters_When_GetViewModel_return_only_responsibilities_tied_to_user_sites_in_viewModel()
        //{
        //    //Given
        //    const long categoryId = 123L;
        //    const long siteId = 324234L;
        //    const long siteGroupId = 2L;
        //    const bool deleted = true;
        //    var from = DateTime.Now.AddDays(-124);
        //    var to = DateTime.Now.AddDays(-3231);
        //    const string title = "title";
        //    IList<long> allowedSites = new List<long>() { siteGroupId };
        //    var target = CreateTarget();

        //    //When
        //    var siteGroup1 = new SiteGroupDto() { Id = 1L };
        //    var siteGroup2 = new SiteGroupDto() { Id = 2L };

        //    var resp1 = new ResponsibilityDto() {Site = new SiteDto() { Id = siteId }, Id = 1};
        //    var resp2 = new ResponsibilityDto() {Site = new SiteDto() { Id = siteId }, Id = 2};
        //    var resp3 = new ResponsibilityDto() {Site = new SiteDto() { Id = siteId - 1 }, Id = 3 };
            
        //     _responsibilitiesService
        //        .Setup(x => x.Search(It.IsAny<SearchResponsibilitiesRequest>()))
        //        .Returns(new List<ResponsibilityDto>() {resp1, resp2 });
            
        //    _siteGroupService
        //        .Setup(x => x.GetByCompanyId(It.IsAny<long>()))
        //        .Returns(new List<SiteGroupDto>() { siteGroup1, siteGroup2 });

        //    var result = target
        //        .WithCategoryId(categoryId)
        //        .WithCreatedFrom(from)
        //        .WithCreatedTo(to)
        //        .WithTitle(title)
        //        .WithSiteId(siteId)
        //        .WithSiteGroupId(siteGroupId)
        //        .WithShowDeleted(deleted)
        //        .WithAllowedSiteIds(allowedSites)
        //        .GetViewModel();

        //    //Then
        //    Assert.That(result.CategoryId, Is.EqualTo(categoryId));
        //    Assert.That(result.SiteId, Is.EqualTo(siteId));
        //    Assert.That(result.SiteGroupId, Is.EqualTo(siteGroupId));
        //    Assert.That(result.IsShowDeleted, Is.EqualTo(deleted));
        //    Assert.That(DateTime.Parse(result.CreatedFrom), Is.EqualTo(from.Date));
        //    Assert.That(DateTime.Parse(result.CreatedTo), Is.EqualTo(to.Date));
        //    Assert.That(result.Title, Is.EqualTo(title));
        //    Assert.That(result.SiteGroups.Count(), Is.EqualTo(2));
        //    Assert.That(result.SiteGroups.Last().value, Is.EqualTo(siteGroupId.ToString()));
        //}
        

        private ISearchResponsibilityViewModelFactory CreateTarget()
        {
            return new SearchResponsibilityViewModelFactory(_responsibilitiesService.Object, _siteGroupService.Object, _siteService.Object);
        }
    }
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Infrastructure.Security;
using BusinessSafe.WebSite.Areas.Documents.Controllers;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.DocumentLibraryService;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using BusinessSafe.WebSite.StreamingDocumentLibraryService;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.DocumentControllerTests
{
    [TestFixture]
    public class DocumentUploadedTests : BaseUploadDocument
    {
        [Test]
        public void Given_DocumentUploaded_When_the_User_has_a_subset_of_all_sites_as_their_AllowedSites_Then_ViewModel_only_has_those_allowed_sites()
        {
            // Given
            var passedSearchSitesRequest = new SearchSitesRequest();
            _siteService
                .Setup(x => x.Search(It.IsAny<SearchSitesRequest>()))
                .Callback<SearchSitesRequest>(y => passedSearchSitesRequest = y)
                .Returns(new List<SiteDto>());

            // When
            var result = _target.DocumentUploaded(
                It.IsAny<long>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                AttachDocumentReturnView.AddedDocuments);
            var viewModel = result.Model as NewlyAddedDocumentGridRowViewModel;

            // Then
            Assert.That(passedSearchSitesRequest.CompanyId, Is.EqualTo(TestControllerHelpers.CompanyIdAssigned));
            Assert.That(passedSearchSitesRequest.PageLimit, Is.EqualTo(100));
            Assert.That(passedSearchSitesRequest.AllowedSiteIds, Is.EqualTo(_allowedSites));
        }
    }
}

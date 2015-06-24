using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Factories
{
    public interface IAddedDocumentsLibraryViewModelFactory
    {
        AddedDocumentsLibraryViewModel GetViewModel();
        IAddedDocumentsLibraryViewModelFactory WithCompanyId(long companyId);
        IAddedDocumentsLibraryViewModelFactory WithDocumentTypeId (long documentTypeId);
        IAddedDocumentsLibraryViewModelFactory WithDocumentTitle(string title);
        IAddedDocumentsLibraryViewModelFactory WithSiteId(long siteId);
        IAddedDocumentsLibraryViewModelFactory WithSiteGroupId(long siteGroupId);
        IAddedDocumentsLibraryViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
    }
}
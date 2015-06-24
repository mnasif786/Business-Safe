using System.Collections.Generic;

using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Factories
{
    public interface IDocumentLibraryViewModelFactory
    {
        BusinessSafeSystemDocumentsLibraryViewModel GetViewModel();
        IDocumentLibraryViewModelFactory WithCompanyId(long companyId);
        IDocumentLibraryViewModelFactory WithAllowedSites(IList<long> allowedSites);
        IDocumentLibraryViewModelFactory WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup docHandlerDocumentTypeGroup);
        IDocumentLibraryViewModelFactory WithDocumentTypeId(long documentTypeId);
        IDocumentLibraryViewModelFactory WithDocumentSubTypeId(long documentSubTypeId);
        IDocumentLibraryViewModelFactory WithDocumentTitle(string title);
        IDocumentLibraryViewModelFactory WithKeywords(string keywords);
        IDocumentLibraryViewModelFactory WithSiteId(long siteId);

        IDocumentLibraryViewModelFactory WithDocumentTypeIds(IList<long> documentTypeIds);
    }
}
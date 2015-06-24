using System.Collections.Generic;
using System.Security.Principal;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.ViewModels
{
    public class AddedDocumentsLibraryViewModel
    {
        public long CompanyId { get; set; }
        public long DocumentTypeId { get; set; }
        public long SiteId { get; set; }
        public long SiteGroupId { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public IEnumerable<AutoCompleteViewModel> DocumentTypes { get; set; }
        public IEnumerable<AutoCompleteViewModel> Sites { get; set; }
        public IEnumerable<AutoCompleteViewModel> SiteGroups { get; set; }
        public IEnumerable<DocumentViewModel> Documents { get; set; }

        public bool IsDeleteDocumentEnabled(IPrincipal user, DocumentViewModel item)
        {
            return user.IsInRole(Permissions.DeleteAddedDocuments.ToString()) && item.CanDelete;
        }
    }
}
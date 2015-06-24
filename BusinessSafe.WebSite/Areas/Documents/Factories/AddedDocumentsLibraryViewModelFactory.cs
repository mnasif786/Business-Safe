using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Factories
{
    public class AddedDocumentsLibraryViewModelFactory: IAddedDocumentsLibraryViewModelFactory
    {
        private readonly IDocumentTypeService _documentTypeService;
        private readonly IDocumentService _documentService;
        private readonly ISiteService _siteService;
        private readonly ISiteGroupService _siteGroupService;
        private long _companyId ;
        private string _title;
        private long _documentTypeId;
        private string _keywords;
        
        private long _siteId;
        private long _siteGroupId;
        private IList<long> _allowedSiteIds; 

        public AddedDocumentsLibraryViewModelFactory(
            IDocumentTypeService documentTypeService, 
            IDocumentService documentService,
            ITaskService taskService,
            ISiteService siteService,
            ISiteGroupService siteGroupService)
        {
            _documentTypeService = documentTypeService;
            _documentService = documentService;
            _siteService = siteService;
            _siteGroupService = siteGroupService;
        }

        public AddedDocumentsLibraryViewModel GetViewModel()
        {
            var documents = GetDocuments();

            return new AddedDocumentsLibraryViewModel()
                       {
                           Documents = documents,
                           CompanyId = _companyId,
                           DocumentTypes = GetDocumentTypes(),
                           Sites = GetSites(),
                           SiteGroups = GetSiteGroups(),
                           Title = _title,
                           Keywords = _keywords,
                           DocumentTypeId = _documentTypeId,
                           SiteId = _siteId,
                           SiteGroupId = _siteGroupId
                       };
        }

        private IEnumerable<DocumentViewModel> GetDocuments()
        {
            var documents = _documentService.Search(new SearchDocumentRequest()
                                                        {
                                                            CompanyId = _companyId,
                                                            TitleLike = _title,
                                                            DocumentTypeId = _documentTypeId,
                                                            SiteId = _siteId,
                                                            SiteGroupId = _siteGroupId,
                                                            AllowedSiteIds = _allowedSiteIds
                                                        });

            return documents
                .Where(document => !document.Deleted)
                .Select(DocumentViewModel.CreateFrom)
                .OrderByDescending(x=> x.DateUploaded).ToList();
        }

        private IEnumerable<AutoCompleteViewModel> GetDocumentTypes()
        {
            List<DocumentTypeDto> documentTypes = _documentTypeService.GetAll().ToList();
            if(!documentTypes.Any())
            {
                return new List<AutoCompleteViewModel>().AddDefaultOption();
            }
            return documentTypes.OrderBy(x =>x.Name).Select(AutoCompleteViewModel.ForDocumentType).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            var sites = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
            });

            var result = sites != null
                             ? sites.OrderBy(x => x.Name).Select(AutoCompleteViewModel.ForSite).AddDefaultOption()
                             : new List<AutoCompleteViewModel>();

            return result;
        }

        private IEnumerable<AutoCompleteViewModel> GetSiteGroups()
        {
            var sites = _siteGroupService.GetByCompanyId(_companyId);

            return sites != null
                             ? sites.OrderBy(x => x.Name).Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption()
                             : new List<AutoCompleteViewModel>();
        }

        public IAddedDocumentsLibraryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IAddedDocumentsLibraryViewModelFactory WithDocumentTypeId(long documentTypeId)
        {
            _documentTypeId = documentTypeId;
            return this;
        }

        public IAddedDocumentsLibraryViewModelFactory WithDocumentTitle(string title)
        {
            _title = title;
            return this;
        }

        public IAddedDocumentsLibraryViewModelFactory WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public IAddedDocumentsLibraryViewModelFactory WithSiteGroupId(long siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public IAddedDocumentsLibraryViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds)
        {
            _allowedSiteIds = allowedSiteIds;
            return this;
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.Entities;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.ClientDocumentService;
using BusinessSafe.WebSite.DocumentSubTypeService;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Factories
{
    public class DocumentLibraryViewModelFactory: IDocumentLibraryViewModelFactory
    {
        private readonly IClientDocumentService _clientDocumentService;
        private readonly IDocHandlerDocumentTypeService _docHandlerdocumentTypeService;
        private readonly DocumentTypeService.IDocumentTypeService _documentTypeService;
        private readonly IDocumentSubTypeService _documentSubTypeService;
        private readonly ISiteService _siteService;
        private readonly ICacheHelper _cacheHelper;
        private DocHandlerDocumentTypeGroup _docHandlerDocumentTypeGroup;
        private long _companyId;
        private long _documentTypeId;
        private string _title;
        private long _documentSubTypeId;
        private string _keywords;
        private long _siteId;
        private IList<long> _allowedSites;
        private IList<long> _documentTypeIds; 
        private const long DepartmentId = 2;

        public DocumentLibraryViewModelFactory(IClientDocumentService clientDocumentService, IDocHandlerDocumentTypeService docHandlerdocumentTypeService, DocumentTypeService.IDocumentTypeService documentTypeService, IDocumentSubTypeService documentSubTypeService, ISiteService siteService, ICacheHelper cacheHelper)
        {
            _clientDocumentService = clientDocumentService;
            _docHandlerdocumentTypeService = docHandlerdocumentTypeService;
            _documentTypeService = documentTypeService;
            _documentSubTypeService = documentSubTypeService;
            _siteService = siteService;
            _cacheHelper = cacheHelper;
        }

        public IDocumentLibraryViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithDocHandlerDocumentTypeGroup(DocHandlerDocumentTypeGroup docHandlerDocumentTypeGroup)
        {
            _docHandlerDocumentTypeGroup = docHandlerDocumentTypeGroup;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithDocumentTypeId(long documentTypeId)
        {
            _documentTypeId = documentTypeId;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithDocumentSubTypeId(long documentSubTypeId)
        {
            _documentSubTypeId = documentSubTypeId;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithDocumentTitle(string title)
        {
            _title = title;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithKeywords(string keywords)
        {
            _keywords = keywords;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithSiteId(long siteId)
        {
            _siteId = siteId;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithAllowedSites(IList<long> allowedSites)
        {
            _allowedSites = allowedSites;
            return this;
        }

        public IDocumentLibraryViewModelFactory WithDocumentTypeIds(IList<long> documentTypeIds)
        {
            _documentTypeIds = documentTypeIds;
            return this;
        }

        public BusinessSafeSystemDocumentsLibraryViewModel GetViewModel()
        {
            var documentTypeIdsForThisGroup = GetDocumentTypeIdsForGroup();
            var sites = GetAllowedSites();
            var documents = GetDocuments(sites, documentTypeIdsForThisGroup, _keywords);

            return new BusinessSafeSystemDocumentsLibraryViewModel()
            {
                CompanyId = _companyId,
                DocumentTypeId = _documentTypeId,
                SiteId = _siteId,
                DocumentTypes = GetDocumentTypes(documentTypeIdsForThisGroup),
                DocumentSubTypeId = _documentSubTypeId,
                DocumentSubTypes = GetDocumentSubTypes(),
                Sites = sites.Select(AutoCompleteViewModel.ForSite).AddDefaultOption(),
                Title = _title,
                Keywords = _keywords,
                Documents = documents,
            };
        }

        private IEnumerable<SiteDto> GetAllowedSites()
        {
            return _siteService.Search(new SearchSitesRequest()
                                       {
                                           AllowedSiteIds = _allowedSites,
                                           CompanyId = _companyId,
                                           PageLimit = 250
                                       }).ToList();
        }

        private long[] GetDocumentTypeIdsForGroup()
        {
            return _docHandlerdocumentTypeService.GetForDocumentGroup(_docHandlerDocumentTypeGroup).Select(x => x.Id).ToArray();
        }

        private IEnumerable<AutoCompleteViewModel> GetDocumentTypes(long[] documentTypeIdsForThisGroup)
        {
            const string key = "DocumentTypes";

            if (!documentTypeIdsForThisGroup.Any())
                return new List<AutoCompleteViewModel>().AddDefaultOption();


            DocumentTypeService.DocumentTypeDto[] documentTypes;

            if (!_cacheHelper.Load(key, out documentTypes))
            {
                documentTypes = _documentTypeService.GetByDepartmentId(DepartmentId);
                _cacheHelper.Add(documentTypes, key, 60);
            }

            var docHandlerNativeDocumentTypes = documentTypes.Where(d => d.Id != null && documentTypeIdsForThisGroup.Contains(d.Id.Value)).OrderBy(d => d.Title);

            return docHandlerNativeDocumentTypes.Select(AutoCompleteViewModel.ForDocumentType).AddDefaultOption();
        }

        private IList<long> GetDocumentTypeIds(long[] documentTypeIdsForThisGroup, IList<long> documentTypeIdsToSerachFor )
        {
            var documentTypeIds = new List<long>();
            const string key = "DocumentTypes";

            if (!documentTypeIdsForThisGroup.Any())
                return documentTypeIds;


            DocumentTypeService.DocumentTypeDto[] documentTypes;

            if (!_cacheHelper.Load(key, out documentTypes))
            {
                documentTypes = _documentTypeService.GetByDepartmentId(DepartmentId);
                _cacheHelper.Add(documentTypes, key, 60);
            }
            
            var docHandlerNativeDocumentTypes = documentTypes.Where(d => d.Id != null && documentTypeIdsForThisGroup.Contains(d.Id.Value));
            docHandlerNativeDocumentTypes = docHandlerNativeDocumentTypes.Where(d => d.Id != null && documentTypeIdsToSerachFor.Contains(d.Id.Value));

            foreach (var docType in docHandlerNativeDocumentTypes)
            {
                if (docType.Id != null) documentTypeIds.Add(docType.Id.Value);
            }

            return documentTypeIds;
        }

        private IEnumerable<AutoCompleteViewModel> GetDocumentSubTypes()
        {
            const string key = "DocumentSubTypes";
            DocumentSubTypeService.DocumentSubTypeDto[] documentSubTypes;

            if (!_cacheHelper.Load(key, out documentSubTypes))
            {
                documentSubTypes = _documentSubTypeService.GetByDepartmentId(DepartmentId);
                _cacheHelper.Add(documentSubTypes, key, 60);
            }

            var filteredDocumentSubTypes = documentSubTypes.Where(d => d.DocumentType.Id == _documentTypeId).OrderBy(d => d.Title);

            return filteredDocumentSubTypes.Select(AutoCompleteViewModel.ForDocumentSubType).AddDefaultOption();
        }

        private IEnumerable<DocumentViewModel> GetDocuments(IEnumerable<SiteDto> sites, long[] documentTypeIdsForThisGroup, string keywords)
        {
            var request = new SearchClientDocumentsRequest()
                                    {
                                        ClientId = _companyId,
                                        IncludeCrossClientDocuments = true,
                                        TagString = keywords
                                    };

            if (_documentTypeId > 0)
                request.DocumentTypeId = _documentTypeId;
            else if (_documentTypeIds!= null && _documentTypeIds.Any())
                {
                    var documentTypeIds = GetDocumentTypeIds(documentTypeIdsForThisGroup, _documentTypeIds);
                    request.DocumentTypeIds = (documentTypeIds.Any()) ? documentTypeIds.ToArray() : documentTypeIdsForThisGroup;
                }
            else
                request.DocumentTypeIds = documentTypeIdsForThisGroup;
        
                

            if (_documentSubTypeId > 0)
                request.DocumentSubTypeId = _documentSubTypeId;

            if (!string.IsNullOrEmpty(_title))
                request.TitleLike = _title;

            if (!string.IsNullOrEmpty(_keywords))
                request.TagString = _keywords;

            //Where did SiteId go on the request object? Seems like it was never pushed into mercurial
            //if (_siteId > 0)
            //{
            //    request.SiteId = GetPeninsulaSiteId(_siteId, _companyId);
            //}

            //if duplicate documents, select the latest
            var documents = _clientDocumentService.Search(request);
            
            //Todo: this is a temporary measure until The above client Documentation issue is resolved.
            if (_siteId > 0)
            {
                var siteId = GetPeninsulaSiteId(_siteId, _companyId);
                documents = documents.Where(document => document.SiteId == siteId).ToArray();
            }
            else
            {
                var allowedPeninsulaSiteIds = sites.Select(site => site.SiteId);
                documents = documents.Where(document => !document.SiteId.HasValue || allowedPeninsulaSiteIds.Contains(document.SiteId.Value)).ToArray();
            }

            var latestDocumentIdForEachGroup = documents
                .GroupBy(d => new {d.Title, d.SiteId, docTypeId = d.DocumentType.Id})
               .Select(d => d.Max(dd => dd.Id.Value));

            var deduplicatedDocuments = documents.Where(d => latestDocumentIdForEachGroup.Contains(d.Id.Value));
                
               
            var siteReferencesLookupDictionary = CreateSiteReferencesLookupDictionary(sites);

            return deduplicatedDocuments.Select(x => DocumentViewModel.CreateFrom(x, siteReferencesLookupDictionary)).ToList();
        }

        private long? GetPeninsulaSiteId(long siteId, long companyId)
        {
            var site = _siteService.GetByIdAndCompanyId(siteId, companyId);
            return site.SiteId;
        }

        private IDictionary<long, string> CreateSiteReferencesLookupDictionary(IEnumerable<SiteDto> sites)
        {
            var siteDictionary = new Dictionary<long, string>();

            sites.ToList().ForEach(x=>
                                       {
                                           if (!siteDictionary.ContainsKey(x.SiteId.HasValue ? x.SiteId.Value : 0))
                                           {
                                               siteDictionary.Add(x.SiteId.HasValue ? x.SiteId.Value : 0, x.Name);
                                           }

                                       });
            return siteDictionary;
        }

        public class ClientDocumentDtoComparer : IEqualityComparer<ClientDocumentDto>
        {

            #region IEqualityComparer<ClientDocumentDto> Members

            public bool Equals(ClientDocumentDto x, ClientDocumentDto y)
            {
                return x.Title == y.Title
                       && x.SiteId == y.SiteId
                       && x.DocumentType == y.DocumentType
                       && x.DocumentSubType == y.DocumentSubType;
            }

            public int GetHashCode(ClientDocumentDto obj)
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }
    }
}
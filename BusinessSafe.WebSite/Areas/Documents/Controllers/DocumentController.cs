using System;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.Request;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.DocumentLibraryService;
using BusinessSafe.WebSite.Filters;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.StreamingClientDocumentService;
using BusinessSafe.WebSite.StreamingDocumentLibraryService;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.Documents.Controllers
{
    public class DocumentController : BaseController
    {
        private readonly ILookupService _lookupService;
        private readonly IDocumentLibraryService _documentLibraryService;
        private readonly IStreamingClientDocumentService _streamingClientDocumentService;
        private readonly IPeninsulaLog _peninsulaLog;
        private readonly IStreamingDocumentLibraryService _streamingDocumentLibraryService;
        private readonly IDocumentLibraryUploader _documentLibraryUploader;
        private readonly ISiteService _siteService;
        private readonly IDocumentService _documentService;

        public DocumentController(
            ILookupService lookupService,
            IDocumentLibraryService documentLibraryService,
            IStreamingDocumentLibraryService streamingDocumentLibraryService,
            IDocumentLibraryUploader documentLibraryUploader,
            ISiteService siteService, IDocumentService documentService,
            IStreamingClientDocumentService streamingClientDocumentService,
            IPeninsulaLog peninsulaLog)
        {
            _lookupService = lookupService;
            _documentLibraryService = documentLibraryService;
            _streamingDocumentLibraryService = streamingDocumentLibraryService;
            _documentLibraryUploader = documentLibraryUploader;
            _siteService = siteService;
            _documentService = documentService;
            _streamingClientDocumentService = streamingClientDocumentService;
            _peninsulaLog = peninsulaLog;
        }

        [PermissionFilter(Permissions.AddAddedDocuments)]
        public ActionResult ShowUploadButton(bool? error, string errorMessage, int documentOriginTypeId = 0, bool canEditDocumentType = false, AttachDocumentReturnView returnView = AttachDocumentReturnView.GeneralRiskAssessmentDocuments, int documentTypeId = 0)
        {
            var viewModel = new DocumentUploadButtonViewModel() { DocumentOriginTypeId = documentOriginTypeId, CanEditDocumentType = canEditDocumentType, ReturnView = returnView, DocumentTypeId = documentTypeId };

            if (error.HasValue && error.Value)
            {
                viewModel.Error = true;
                viewModel.ErrorMessage = "An error occured.";
            }

            return View("~/Areas/Documents/Views/Document/DocumentUploadButton.cshtml", viewModel);
        }

        [PermissionFilter(Permissions.AddAddedDocuments)]
        [HttpPost]
        public ActionResult UploadDocument(DocumentUploadButtonViewModel viewModel)
        {
            try
            {
                viewModel.LastUploadedDocumentLibraryId = _documentLibraryUploader.Upload(viewModel.File.FileName, viewModel.File.InputStream);
                viewModel.LastUploadedDocumentFilename = viewModel.File.FileName;
                viewModel.DocumentUploaded = true;
            }
            catch (Exception ex)
            {
                viewModel.Error = true;
                viewModel.ErrorMessage = ex.Message;
                _peninsulaLog.Add(ex);
                LogExceptionInElmah(ex);
                _peninsulaLog.Add(viewModel);// help us to find out what part of the view model is null
            }

            return View("~/Areas/Documents/Views/Document/DocumentUploadButton.cshtml", viewModel);
        }

        public virtual void LogExceptionInElmah(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        [PermissionFilter(Permissions.ViewAddedDocuments)]
        public PartialViewResult DocumentUploaded(long documentLibraryId, string fileName, int documentOriginTypeId = 0, int documentTypeId = 0, bool canEditDocumentType = false, AttachDocumentReturnView returnView = AttachDocumentReturnView.GeneralRiskAssessmentDocuments)
        {
            var viewModel = new NewlyAddedDocumentGridRowViewModel()
                                                     {
                                                         DocumentLibraryId = documentLibraryId,
                                                         Filename = fileName,
                                                         DocumentOriginTypeId = documentOriginTypeId,
                                                         CanEditDocumentType = canEditDocumentType,
                                                         DocumentTypeId = documentTypeId
                                                     };

            var documentTypes = _lookupService.GetDocumentTypes();

            viewModel.DocumentTypes = new SelectList(documentTypes, "Id", "Name", documentTypeId);


            if (returnView == AttachDocumentReturnView.AddedDocuments)
            {
                viewModel.Sites = GetSites();
                return PartialView("~/Areas/Documents/Views/Document/_AddedDocumentGridRow.cshtml", viewModel);
            }

            return PartialView("~/Areas/Documents/Views/Document/_DocumentGridRow.cshtml", viewModel);
            
        }

        [PermissionFilter(Permissions.DeleteAddedDocuments)]
        [HttpPost]
        public JsonResult DeleteDocument(string enc)
        {
            var parameters = EncryptionHelper.DecryptQueryString(enc);
            var documentLibraryId = Convert.ToInt64(parameters["documentLibraryId"]);

            try
            {
                _documentLibraryService.DeleteDocument(new DeleteDocumentRequest
                                                           {
                                                               DocumentId = documentLibraryId
                                                           });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            }

            return Json(new { success = true, deletedDocumentLibraryId = documentLibraryId });
        }

        [PermissionFilter(Permissions.ViewAddedDocuments)]
        public FileStreamResult DownloadEmbeddedDocument(string filename)
        {
            var stream = GetType()
                            .Assembly
                            .GetManifestResourceStream("BusinessSafe.WebSite.Documents." + filename);

            var extension = filename.Substring(filename.LastIndexOf(".") + 1);
            var file = File(stream, ContentTypeHelper.GetContentTypeFromExtension(extension), filename);
            return file;
        }

        [PermissionFilter(Permissions.ViewAddedDocuments)]
        public FileStreamResult DownloadDocument(string enc)
        {
            var parameters = EncryptionHelper.DecryptQueryString(enc);
            
            long documentLibraryId = Convert.ToInt64(parameters["documentLibraryId"]);

            _documentService.ValidateDocumentForCompany(documentLibraryId, CurrentUser.CompanyId);

            var document = _streamingDocumentLibraryService.GetStreamedDocumentById(new GetStreamedDocumentByIdRequest { DocumentId = documentLibraryId });
            string contentType = ContentTypeHelper.GetContentTypeFromExtension(document.MetaData.Extension);

            return File(document.Content, contentType, document.MetaData.Filename);
        }

        [PermissionFilter(Permissions.ViewAddedDocuments)]
        public FileStreamResult DownloadPublicDocument(string enc)
        {
            var parameters = EncryptionHelper.DecryptQueryString(enc);
            
            long documentLibraryId = Convert.ToInt64(parameters["documentLibraryId"]);
            var document = _streamingDocumentLibraryService.GetStreamedDocumentById(new GetStreamedDocumentByIdRequest { DocumentId = documentLibraryId });
            string contentType = ContentTypeHelper.GetContentTypeFromExtension(document.MetaData.Extension);
            return File(document.Content, contentType, document.MetaData.Filename);
        }

        [PermissionFilter(Permissions.ViewAddedDocuments)]
        public FileStreamResult DownloadClientDocument(string enc)
        {
            var parameters = EncryptionHelper.DecryptQueryString(enc);
            var clientDocumentId = Convert.ToInt64(parameters["clientDocumentId"]);

            var document = _streamingClientDocumentService.GetById(new GetStreamedClientDocumentByIdRequest() { ClientDocumentId = clientDocumentId });

            if (document.MetaData.ClientId.HasValue && document.MetaData.ClientId != CurrentUser.CompanyId)
                throw new InvalidDocumentForCompanyException(clientDocumentId, CurrentUser.CompanyId);

            var contentType = ContentTypeHelper.GetContentTypeFromExtension(document.MetaData.Extension);

            var fileName = parameters["filenameWithoutExtension"] != null ? Server.UrlDecode(parameters["filenameWithoutExtension"]) + document.MetaData.Extension : document.MetaData.OriginalFilename;

            return File(document.Content, contentType, fileName);
        }

        private SelectList GetSites()
        {
            var sites = _siteService.Search(new SearchSitesRequest()
                                            {
                                                CompanyId = CurrentUser.CompanyId,
                                                AllowedSiteIds = CurrentUser.GetSitesFilter(),
                                                PageLimit = 100
                                            });

            return new SelectList(sites, "Id", "Name");
        }
    }
}
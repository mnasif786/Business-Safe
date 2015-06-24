using System;
using System.Configuration;
using System.IO;
using System.Web;

using BusinessSafe.Infrastructure.Security;
using BusinessSafe.WebSite.DocumentLibraryService;

namespace BusinessSafe.WebSite.Helpers
{
    public class DocumentLibraryUploader : IDocumentLibraryUploader
    {
        private readonly IImpersonator _impersonator;
        private readonly IDocumentLibraryService _docService;
        private readonly ISaveFileStreamHelper _saveFileStreamHelper;

        private const string _username = "Network.Monitor";
        private const string _domain = "HQ";
        private const string _encryptedPassword = "WBS9CNrr1YzIFNRx8Wtx5tZ7UsIs5jgV4yBuP3nAFuM=";
        private const string _uploadPathKey = "DocumentUploadHoldingPath";

        public DocumentLibraryUploader(
            IImpersonator impersonator, 
            IDocumentLibraryService documentLibraryService, 
            ISaveFileStreamHelper saveFileStreamHelper)
        {
            _impersonator = impersonator;
            _docService = documentLibraryService;
            _saveFileStreamHelper = saveFileStreamHelper;
        }

        public long Upload(string fileName, Stream stream)
        {
            try
            {
                _impersonator.ImpersonateValidUser(_username, _domain, _encryptedPassword);

                var filePath = ConfigurationManager.AppSettings[_uploadPathKey] + Guid.NewGuid();
                _saveFileStreamHelper.Write(filePath, stream);

                var response = _docService.CreateDocumentFromPath(new CreateDocumentFromPathRequest
                                                                  {
                                                                      ApplicationId = 6, // magic number means bso
                                                                      Filename = Path.GetFileName(fileName),
                                                                      FilePath = filePath
                                                                  });

                return response.DocumentId;
            }
            finally
            {
                _impersonator.Dispose();
            }
        }
    }
}
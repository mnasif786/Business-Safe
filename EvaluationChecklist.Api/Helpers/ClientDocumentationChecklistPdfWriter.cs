using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using EvaluationChecklist.ClientDocumentService;
using EvaluationChecklist.SecurityService;

namespace EvaluationChecklist.Helpers
{
    public class ClientDocumentationChecklistPdfWriter : IClientDocumentationChecklistPdfWriter
    {
        public void WriteToClientDocumentation(string fileName, byte[] pdfBytes, int clientId)
        {
            var holdingPath = @"\\pbsw23datastore\DragonDropHoldingPath\";
            var fullFileName = holdingPath + Guid.NewGuid();
            File.WriteAllBytes(fullFileName, pdfBytes);

            using(var securityService = new SecurityServiceClient())
            using (new OperationContextScope(securityService.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(MessageHeader.CreateHeader("Username", "Peninsula.Common", "SafeCheckUser"));
                securityService.EnsureUserExists(null);
            }

            using(var clientDocumentService = new ClientDocumentServiceClient())
            using (new OperationContextScope(clientDocumentService.InnerChannel))
            {
                OperationContext.Current.OutgoingMessageHeaders.Add(MessageHeader.CreateHeader("Username", "Peninsula.Common", "SafeCheckUser"));

                var createClientDocumentRequest = new CreateClientDocumentRequest()
                                                      {
                                                          ClientId = clientId,
                                                          DocumentTypeId = 131, //MAGIC NUMBER: refactor out
                                                          OriginalFilename = fileName,
                                                          PhysicalFilePath = fullFileName,
                                                          Title = fileName
                                                      };

                clientDocumentService.CreateDocument(createClientDocumentRequest);
            }

            File.Delete(fullFileName);
        }
    }
}
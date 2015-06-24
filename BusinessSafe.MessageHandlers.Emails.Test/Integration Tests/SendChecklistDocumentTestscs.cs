using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using BusinessSafe.MessageHandlers.Emails.ClientDocumentService;
using BusinessSafe.MessageHandlers.Emails.DocumentLibraryService;
using NUnit.Framework;

namespace BusinessSafe.MessageHandlers.Emails.Test.Integration_Tests
{
    [TestFixture]
    public class SendChecklistDocumentTestscs
    {
        private IDocumentLibraryService _documentLibraryService;
        private IClientDocumentService _clientDocumentService;

        [SetUp]
        public void Setup()
        {
            try
            {
                _documentLibraryService = new DocumentLibraryServiceClient();
                _clientDocumentService = new ClientDocumentServiceClient();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Test]
        public void Given_client_document_Id_given_clientID()
        {
            //given
            var documentId = 571;

            //when
            var documents = _clientDocumentService.GetById(documentId);

            Assert.That(documents.DocumentLibraryId, Is.EqualTo(6516));
        }
        
        
        [Test]
        public void Given_document_library_id_when_getdocumentsById_then_physicalFilePath_Returned()
        {
            //given
            var req1 = new GetDocumentsByIdsRequest();
            req1.DocumentIds = new long[]{6516};

            //when
            var documents = _documentLibraryService.GetDocumentsByIds(req1);
            
            //then
            Debug.WriteLine(documents[0].PhysicalFilePath);
            Assert.That(documents[0].PhysicalFilePath, Is.EqualTo(@"\\UATMAINTWS1\DocumentLibraryTestFiles_WARNING_DoNotPutImportantStuffInHereItGetsWiped\2014\2\13\11\"));
        }
    }
}

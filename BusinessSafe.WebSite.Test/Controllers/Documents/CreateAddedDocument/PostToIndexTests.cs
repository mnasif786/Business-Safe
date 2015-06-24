using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.Documents.Controllers;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.CreateAddedDocument
{
    [TestFixture]
    public class PostToIndexTests
    {
        private CreateAddedDocumentController _target;
        private Mock<IAddedDocumentsService> _addedDocumentsService;

        [SetUp]
        public void SetUp()
        {
            _addedDocumentsService = new Mock<IAddedDocumentsService>();
            _target = GetCreateAddedDocumentController();
        }

        [Test]
        public void Posting_Valid_FormCollection_to_Index_Returns_Success_Json()
        {
            // Given
            _addedDocumentsService.Setup(x => x.Add(It.IsAny<List<CreateDocumentRequest>>(), It.IsAny<Guid>(), It.IsAny<long>()));

            var documentsToSaveViewModel = new DocumentsToSaveViewModel()
                                               {
                                                   CreateDocumentRequests = new List<CreateDocumentRequest>()
                                                                                {
                                                                                    new CreateDocumentRequest()
                                                                                }
                                               };

            // When
            var result = _target.Index(documentsToSaveViewModel);

            // Then
            Assert.That(result, Is.InstanceOf<JsonResult>());
            _addedDocumentsService.VerifyAll();
        }

        private CreateAddedDocumentController GetCreateAddedDocumentController()
        {
            var result = new CreateAddedDocumentController(_addedDocumentsService.Object);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
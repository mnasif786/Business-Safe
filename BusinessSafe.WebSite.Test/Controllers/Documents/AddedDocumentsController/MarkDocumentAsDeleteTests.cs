using System;
using System.Web.Mvc;

using BusinessSafe.Application.Contracts.Documents;
using BusinessSafe.Application.Request;
using BusinessSafe.Application.Request.Documents;
using BusinessSafe.WebSite.Areas.Documents.Controllers;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents.AddedDocumentsController
{
    [TestFixture]
    [Category("Unit")]
    public class MarkDocumentAsDeleteTests
    {
        private long _companyId;
        private Mock<IDocumentService> _documentService;
        private long _documentId;


        [SetUp]
        public void SetUp()
        {
            _companyId = 1;
            _documentId = 200;
            _documentService = new Mock<IDocumentService>();
        }

        [Test]
        public void Given_invalid_request_companyId_not_set_When_Delete_Then_should_throw_correct_exception()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();

            _companyId = 0;

            //Get
            //Then
            Assert.Throws<ArgumentException>(() => controller.MarkDocumentAsDeleted(_companyId, _documentId));
            
        }

        [Test]
        public void Given_invalid_request_documentId_not_set_When_Delete_Then_should_throw_correct_exception()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();

            _documentId = 0;

            //Get
            //Then
            Assert.Throws<ArgumentException>(() => controller.MarkDocumentAsDeleted(_companyId, _documentId));

        }

        [Test]
        public void Given_valid_request_When_Delete_Then_should_return_correct_result()
        {
            //Given
            var controller = CreateAddedDocumentsLibraryController();
            
            //Get
            var result = controller.MarkDocumentAsDeleted(_companyId, _documentId) as JsonResult;

            //Then
            Assert.That(result.Data.ToString(), Contains.Substring("Success = True"));
        }


        [Test]
        public void Given_valid_request_When_Delete_Then_should_call_correct_methods()
        {
            // Given
            var controller = CreateAddedDocumentsLibraryController();
            
            // When
            controller.MarkDocumentAsDeleted(_companyId, _documentId);

            // Then
            _documentService.Verify(x => x.MarkDocumentAsDeleted(It.Is<MarkDocumentAsDeletedRequest>(r => r.CompanyId == _companyId && 
                                                                                                          r.DocumentId == _documentId && 
                                                                                                          r.UserId == controller.CurrentUser.UserId)));
        }


        private AddedDocumentsLibraryController CreateAddedDocumentsLibraryController()
        {
            var result = new AddedDocumentsLibraryController(_documentService.Object, null);
            return TestControllerHelpers.AddUserToController(result);
        }
    }
}
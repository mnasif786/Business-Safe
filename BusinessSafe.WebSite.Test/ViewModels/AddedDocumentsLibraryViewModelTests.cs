using System.Security.Principal;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Documents.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.ViewModels;
using Moq;
using NUnit.Framework;
using System;

namespace BusinessSafe.WebSite.Tests.ViewModels
{
    [TestFixture]
    [Category("Unit")]
    public class AddedDocumentsLibraryViewModelTests
    {

        [Test]
        public void Given_a_user_without_delete_added_documents_permissions_When_IsDeleteEnabled_Then_returns_false
            ()
        {
            //Given
            var target = new AddedDocumentsLibraryViewModel();
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(Permissions.DeleteAddedDocuments.ToString())).Returns(false);

            var addedDocumentDto = new AddedDocumentDto()
            {
                DocumentType = new DocumentTypeDto(),
                CreatedOn = DateTime.Now
            };
            var viewModel = DocumentViewModel.CreateFrom(addedDocumentDto);

            //When
            var result = target.IsDeleteDocumentEnabled(user.Object, viewModel);

            //Then
            Assert.IsFalse(result);
        }


        [Test]
        public void Given_a_user_with_delete_added_documents_permissions_and_task_is_an_added_document_When_IsDeleteEnabled_Then_returns_true
            ()
        {
            //Given
            var target = new AddedDocumentsLibraryViewModel();
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var addedDocumentDto = new AddedDocumentDto()
                                       {
                                           DocumentType = new DocumentTypeDto(),
                                           CreatedOn = DateTime.Now
                                       };
            var viewModel = DocumentViewModel.CreateFrom(addedDocumentDto);

            //When
            var result = target.IsDeleteDocumentEnabled(user.Object, viewModel);

            //Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_a_user_with_delete_added_documents_permissions_and_task_is_not_an_added_document_When_IsDeleteEnabled_Then_returns_false
            ()
        {
            //Given
            var target = new AddedDocumentsLibraryViewModel();
            var user = new Mock<IPrincipal>();
            user.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var addedDocumentDto = new TaskDocumentDto()
            {
                DocumentType = new DocumentTypeDto(),
                CreatedOn = DateTime.Now
            };
            var viewModel = DocumentViewModel.CreateFrom(addedDocumentDto);

            //When
            var result = target.IsDeleteDocumentEnabled(user.Object, viewModel);

            //Then
            Assert.IsFalse(result);
        }
    }
}
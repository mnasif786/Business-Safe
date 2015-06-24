using System.Collections.Generic;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.WebSite.Areas.Documents.Factories;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModelFactory
{
    [TestFixture]
    [Category("Unit")]
    public class ExistingDocumentsViewModelFactoryTests
    {
        private IEnumerable<TaskDocumentDto> _documents;

        [SetUp]
        public void Setup()
        {
            _documents = new List<TaskDocumentDto>
                             {
                                 new TaskDocumentDto
                                     {
                                         Id = 1300L,
                                         Filename = "Test Document 01",
                                         Description = "Test Description 1"
                                     },
                                 new TaskDocumentDto
                                     {
                                         Id = 1301L,
                                         Filename = "Test Document 02",
                                         Description = "Test Description 2"
                                     },
                                 new TaskDocumentDto
                                     {
                                         Id = 1302L,
                                         Filename = "Test Document 03",
                                         Description = "Test Description 3"
                                     }
                             };
        }

        [Test]
        public void Given_can_delete_documents_is_true_When_GetViewModel_is_called_then_correct_view_model_is_called()
        {
            var factory = new ExistingDocumentsViewModelFactory();
            var viewModel = factory
                .WithCanDeleteDocuments(true)
                .GetViewModel(_documents);

            Assert.That(viewModel.PreviouslyAddedDocuments.Count, Is.EqualTo(3));
            Assert.That(viewModel.CanDeleteDocuments, Is.EqualTo(true));
            Assert.That(viewModel.PreviouslyAddedDocuments[0].Id, Is.EqualTo(1300L));
            Assert.That(viewModel.PreviouslyAddedDocuments[0].Filename, Is.EqualTo("Test Document 01"));
            Assert.That(viewModel.PreviouslyAddedDocuments[0].Description, Is.EqualTo("Test Description 1"));
            Assert.That(viewModel.PreviouslyAddedDocuments[1].Id, Is.EqualTo(1301L));
            Assert.That(viewModel.PreviouslyAddedDocuments[1].Filename, Is.EqualTo("Test Document 02"));
            Assert.That(viewModel.PreviouslyAddedDocuments[1].Description, Is.EqualTo("Test Description 2"));
            Assert.That(viewModel.PreviouslyAddedDocuments[2].Id, Is.EqualTo(1302L));
            Assert.That(viewModel.PreviouslyAddedDocuments[2].Filename, Is.EqualTo("Test Document 03"));
            Assert.That(viewModel.PreviouslyAddedDocuments[2].Description, Is.EqualTo("Test Description 3"));
        }

        [Test]
        public void Given_can_delete_documents_is_false_When_GetViewModel_is_called_then_correct_view_model_is_called()
        {
            var factory = new ExistingDocumentsViewModelFactory();
            var viewModel = factory
                .WithCanDeleteDocuments(false)
                .GetViewModel(_documents);

            Assert.That(viewModel.PreviouslyAddedDocuments.Count, Is.EqualTo(3));
            Assert.That(viewModel.CanDeleteDocuments, Is.EqualTo(false));
            Assert.That(viewModel.PreviouslyAddedDocuments[0].Id, Is.EqualTo(1300L));
            Assert.That(viewModel.PreviouslyAddedDocuments[0].Filename, Is.EqualTo("Test Document 01"));
            Assert.That(viewModel.PreviouslyAddedDocuments[0].Description, Is.EqualTo("Test Description 1"));
            Assert.That(viewModel.PreviouslyAddedDocuments[1].Id, Is.EqualTo(1301L));
            Assert.That(viewModel.PreviouslyAddedDocuments[1].Filename, Is.EqualTo("Test Document 02"));
            Assert.That(viewModel.PreviouslyAddedDocuments[1].Description, Is.EqualTo("Test Description 2"));
            Assert.That(viewModel.PreviouslyAddedDocuments[2].Id, Is.EqualTo(1302L));
            Assert.That(viewModel.PreviouslyAddedDocuments[2].Filename, Is.EqualTo("Test Document 03"));
            Assert.That(viewModel.PreviouslyAddedDocuments[2].Description, Is.EqualTo("Test Description 3"));
        }
    }
}

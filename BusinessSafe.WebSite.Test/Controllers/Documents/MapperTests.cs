using System.Web.Mvc;

using BusinessSafe.WebSite.Controllers.AutoMappers;

using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.Controllers.Documents
{
    [TestFixture]
    [Category("Unit")]
    public class MapperTests
    {
        [Test]
        public void Given_a_valid_form_collection_When_MapCreateRequests_is_called_Then_returns_collection_of_NewlyAddedDocumentGridRowViewModel()
        {
            //Given
            var formCollection = new FormCollection();
            formCollection.Add("NotRelevantKey", "Some Value");
            formCollection.Add("DocumentGridRow_20000_DocumentLibraryId", "2000");
            formCollection.Add("DocumentGridRow_20000_FileName", "Test File 1.txt");
            formCollection.Add("DocumentGridRow_20000_Description", "First test file.");
            formCollection.Add("DocumentGridRow_20000_DocumentType", "6");
            formCollection.Add("DocumentGridRow_20001_DocumentLibraryId", "2001");
            formCollection.Add("DocumentGridRow_20001_FileName", "Test File 2.txt");
            formCollection.Add("DocumentGridRow_20001_Description", "Second test file.");
            formCollection.Add("DocumentGridRow_20001_DocumentType", "7");

            //When
            var documentRequestMapper = new DocumentRequestMapper();
            var newlyAddedDocumentGridRowViewModels = documentRequestMapper.MapCreateRequests(formCollection);

            //Then
            Assert.AreEqual(2, newlyAddedDocumentGridRowViewModels.Count);
            Assert.AreEqual(20000L, newlyAddedDocumentGridRowViewModels[0].DocumentLibraryId);
            Assert.AreEqual("Test File 1.txt", newlyAddedDocumentGridRowViewModels[0].Filename);
            Assert.AreEqual("First test file.", newlyAddedDocumentGridRowViewModels[0].Description);
            Assert.AreEqual(6L, (long)newlyAddedDocumentGridRowViewModels[0].DocumentType);
            Assert.AreEqual(20001L, newlyAddedDocumentGridRowViewModels[1].DocumentLibraryId);
            Assert.AreEqual("Test File 2.txt", newlyAddedDocumentGridRowViewModels[1].Filename);
            Assert.AreEqual("Second test file.", newlyAddedDocumentGridRowViewModels[1].Description);
            Assert.AreEqual(7L, (long)newlyAddedDocumentGridRowViewModels[1].DocumentType);
        }

        [Test]
        public void Given_a_valid_form_collection_When_MapDeleteRequests_is_called_Then_returns_a_list_of_ids()
        {
            //Given
            var formCollection = new FormCollection();
            formCollection.Add("NotRelevantKey", "Some Value");
            formCollection.Add("PreviouslyAddedDocumentsRow_20004_Delete", "true,false");
            formCollection.Add("PreviouslyAddedDocumentsRow_20005_Delete", "false");
            formCollection.Add("PreviouslyAddedDocumentsRow_20006_Delete", "true,false");

            //When
            var documentRequestMapper = new DocumentRequestMapper();
            var documentLibraryIdsToDelete =
                documentRequestMapper.MapDeleteRequests(formCollection);

            //Then
            Assert.AreEqual(2, documentLibraryIdsToDelete.Count);
            Assert.IsTrue(documentLibraryIdsToDelete.Contains(20004L));
            Assert.IsFalse(documentLibraryIdsToDelete.Contains(20005L));
            Assert.IsTrue(documentLibraryIdsToDelete.Contains(20006L));
        }
    }
}

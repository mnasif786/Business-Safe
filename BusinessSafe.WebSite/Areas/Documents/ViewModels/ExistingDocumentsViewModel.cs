using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;

namespace BusinessSafe.WebSite.Areas.Documents.ViewModels
{
    public class ExistingDocumentsViewModel
    {
        public List<PreviouslyAddedDocumentGridRowViewModel> PreviouslyAddedDocuments { get; set; }
        public bool CanDeleteDocuments { get; set; }
        public int DocumentOriginTypeId { get; set; }
        public bool CanEditDocumentType { get; set; }
        public int DocumentTypeId { get; set; }

        public ExistingDocumentsViewModel()
        {
            PreviouslyAddedDocuments = new List<PreviouslyAddedDocumentGridRowViewModel>();
        }

        public static ExistingDocumentsViewModel CreateFrom(IEnumerable<TaskDocumentDto> documents)
        {
            var viewModel = new ExistingDocumentsViewModel();

            foreach (var previouslyAddedDocument in documents.Where(document => !document.Deleted))
            {
                var previouslyAddedDocumentGridRowViewModel = new PreviouslyAddedDocumentGridRowViewModel
                {
                    Id = previouslyAddedDocument.Id,
                    DocumentLibraryId =previouslyAddedDocument.DocumentLibraryId,
                    Description =previouslyAddedDocument.Description,
                    Filename = previouslyAddedDocument.Filename,
                    DocumentOriginType =previouslyAddedDocument.DocumentOriginType,
                };

                if (previouslyAddedDocument.DocumentType != null)
                    previouslyAddedDocumentGridRowViewModel.DocumentTypeName = previouslyAddedDocument.DocumentType.Name;

                viewModel.PreviouslyAddedDocuments.Add(previouslyAddedDocumentGridRowViewModel);
            }

            return viewModel;
        }
    }
}
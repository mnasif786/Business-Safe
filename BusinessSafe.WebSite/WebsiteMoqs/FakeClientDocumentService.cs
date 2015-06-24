using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.WebSite.ClientDocumentService;

namespace BusinessSafe.WebSite.WebsiteMoqs
{
    public class FakeClientDocumentService : IClientDocumentService
    {
        private List<ClientDocumentDto> _documents;

        public FakeClientDocumentService()
        {
            _documents = new List<ClientDocumentDto>();
        }

        public long CreateDocument(CreateClientDocumentRequest request)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(UpdateClientDocumentRequest request)
        {
            throw new NotImplementedException();
        }

        public ClientDocumentDto GetById(long id)
        {
            throw new NotImplementedException();
        }

        public ClientDocumentDto GetByIdWithContent(long id)
        {
            throw new NotImplementedException();
        }

        public ClientDocumentDto[] Search(SearchClientDocumentsRequest request)
        {
            LoadDocuments();

            var result = _documents.AsQueryable();

            if (!string.IsNullOrEmpty(request.TitleLike))
                result = result.Where(d => d.Title.StartsWith(request.TitleLike) || d.Title.EndsWith(request.TitleLike));

            if (request.DocumentTypeIds != null)
                result = result.Where(d => request.DocumentTypeIds.Contains(d.DocumentType.Id.Value));

            return result.ToArray();
        }

        private void LoadDocuments()
        {
            var documentTypeA = new DocumentTypeDto()
            {
                Id = 124,
                Title = "Type A"
            };

            var documentTypeB = new DocumentTypeDto()
            {
                Id = 126,
                Title = "Type B"
            };
            var documentTypeC = new DocumentTypeDto()
            {
                Id = 136,
                Title = "Type C"
            };

            var documentTypeD = new DocumentTypeDto()
            {
                Id = 137,
                Title = "Type D"
            };

            var documentSubTypeA = new DocumentSubTypeDto()
            {
                Id = 1,
                Title = "Sub Type A"
            };

            var documentSubTypeB = new DocumentSubTypeDto()
            {
                Id = 2,
                Title = "Sub Type B"
            };

            var documentSubTypeC = new DocumentSubTypeDto()
            {
                Id = 3,
                Title = "Sub Type C"
            };

            var documentSubTypeD = new DocumentSubTypeDto()
            {
                Id = 4,
                Title = "Sub Type D"
            };

            _documents.Add(new ClientDocumentDto()
            {
                Id = 1,
                DocumentType = documentTypeA,
                DocumentSubType = documentSubTypeA,
                Title = "Test Title 1",
                Description = "Test Description 1",
                CreatedOn = DateTime.Now
            });

            _documents.Add(new ClientDocumentDto()
            {
                Id = 1,
                DocumentType = documentTypeB,
                DocumentSubType = documentSubTypeB,
                Title = "Test Title 2",
                Description = "Test Description 2",
                CreatedOn = DateTime.Now
            });

            _documents.Add(new ClientDocumentDto()
            {
                Id = 1,
                DocumentType = documentTypeC,
                DocumentSubType = documentSubTypeC,
                Title = "Test Title 3",
                Description = "Test Description 3",
                CreatedOn = DateTime.Now
            });

            _documents.Add(new ClientDocumentDto()
            {
                Id = 1,
                DocumentType = documentTypeD,
                DocumentSubType = documentSubTypeD,
                Title = "Test Title 4",
                Description = "Test Description 4",
                CreatedOn = DateTime.Now
            });
        }

        public void DeleteByIds(long[] ids)
        {
            throw new NotImplementedException();
        }

        public void RestoreByIds(long[] ids)
        {
            throw new NotImplementedException();
        }

        public void CreateZipFile(string zipPath, string zipFileName, long[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
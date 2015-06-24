using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IDocumentRepository : IRepository<Document, long>
    {
        IEnumerable<Document> Search(
            long? companyId,
            string titleLike,
            long? documentTypeId);

        IEnumerable<Document> Search(
            long companyId,
            string titleLike,
            long? documentTypeId,
            long? siteId);

        Document GetByIdAndCompanyId(long documentId, long companyId);
        Document GetByDocumentLibraryIdAndCompanyId(long documentLibraryId, long companyId);
        
    }
}

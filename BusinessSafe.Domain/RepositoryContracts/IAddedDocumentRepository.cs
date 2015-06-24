using System.Collections.Generic;

using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IAddedDocumentRepository : IRepository<AddedDocument, long>
    {
        IEnumerable<AddedDocument> Search(long? companyId, string titleLike, long? documentTypeId, long? siteId, long? siteGroupId, IList<long> allowedSiteIds);
    }
}

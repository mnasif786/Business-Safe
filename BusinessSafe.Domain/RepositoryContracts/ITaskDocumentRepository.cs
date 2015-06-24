using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ITaskDocumentRepository
    {
        IEnumerable<TaskDocument> Search(long companyId, string titleLike, long? documentTypeId, long? siteId, long? siteGroupId, IList<long> allowedSiteIds);
    }
}

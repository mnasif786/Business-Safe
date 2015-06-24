using System.Collections.Generic;

using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IRiskAssessmentDocumentRepository : IRepository<RiskAssessmentDocument, long>
    {
        IEnumerable<RiskAssessmentDocument> Search(long? companyId, string titleLike, long? documentTypeId, long? siteId, long? siteGroupId, IList<long> allowedSiteIds);
    }
}
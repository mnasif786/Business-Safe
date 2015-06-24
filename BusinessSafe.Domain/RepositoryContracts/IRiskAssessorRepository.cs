using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IRiskAssessorRepository : IRepository<RiskAssessor, long>
    {
        RiskAssessor GetByIdAndCompanyId(long riskAssessorId, long companyId);
        RiskAssessor GetByIdAndCompanyIdIncludingDeleted(long riskAssessorId, long companyId);
        IEnumerable<RiskAssessor> GetByCompanyId(long companyId);
        IEnumerable<RiskAssessor> Search(string searchTerm, long siteId, long companyId, int pageLimit, bool includeDeleted, bool excludeActive, bool showDistinct);
        IEnumerable<RiskAssessor> Search(string searchTerm, long[] siteIds, long companyId, int pageLimit, bool includeDeleted, bool excludeActive, bool showDistinct);
    }
}
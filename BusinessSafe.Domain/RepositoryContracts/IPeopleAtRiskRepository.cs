using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IPeopleAtRiskRepository : IRepository<PeopleAtRisk, long>
    {
        IList<PeopleAtRisk> GetByCompanyId(long isAny);
        IEnumerable<PeopleAtRisk> GetAllByNameSearch(string nameToSearch, long personAtRiskId, long companyId);
        IList<PeopleAtRisk> GetAllPeopleAtRiskForRiskAssessments(long companyId, long riskAssessmentId);
        PeopleAtRisk GetByIdAndCompanyId(long id, long companyId);
    }
}

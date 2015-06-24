using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IHazardRepository : IRepository<Hazard, long>
    {
        IList<Hazard> GetByCompanyId(long companyId);
        IList<Hazard> GetAllByNameSearch(string name, long hazardId, long comapnyId);
        Hazard GetByIdAndCompanyId(long id, long companyId);
        IList<Hazard> GetByCompanyIdAndHazardTypeId(long companyId, HazardTypeEnum hazardTypeEnum, long riskAssessmentId);
    }
}

using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ISourceOfFuelRepository : IRepository<SourceOfFuel, long>
    {
        IEnumerable<SourceOfFuel> GetByCompanyId(long companyId);
        IList<SourceOfFuel> GetAllSourceOfFuelForRiskAssessments(long companyId, long riskAssessmentId);
    }
}
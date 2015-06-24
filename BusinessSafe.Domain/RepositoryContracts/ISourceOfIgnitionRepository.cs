using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface ISourceOfIgnitionRepository : IRepository<SourceOfIgnition, long>
    {
        IEnumerable<SourceOfIgnition> GetByCompanyId(long companyId);
        IList<SourceOfIgnition> GetAllSourceOfIgnitionForRiskAssessments(long companyId, long riskAssessmentId);
    }
}
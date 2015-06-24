using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IFireSafetyControlMeasureRepository : IRepository<FireSafetyControlMeasure, long>
    {
        IEnumerable<FireSafetyControlMeasure> GetByCompanyId(long companyId);

        IList<FireSafetyControlMeasure> GetAllFireSafetyControlMeasureForRiskAssessments(long companyId,
                                                                                         long riskAssessmentId);
    }
}
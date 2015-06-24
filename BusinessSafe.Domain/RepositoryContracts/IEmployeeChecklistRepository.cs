using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IEmployeeChecklistRepository : IRepository<EmployeeChecklist, Guid>
    {
        IEnumerable<EmployeeChecklist> GetByPersonalRiskAssessmentId(long personalRiskAssessmentId);
        EmployeeChecklist GetByIdAndRiskAssessmentId(Guid employeeChecklistId, long riskAssessmentId);
        IEnumerable<ExistingReferenceParameters> GetExistingReferencesForPrefixes(IEnumerable<string> prefixes);
    }
}

using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IFireRiskAssessmentRepository : IRepository<FireRiskAssessment, long>
    {
        IEnumerable<FireRiskAssessment> Search(string title, long clientId, IEnumerable<long> siteIds,
                                               DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                                               long? siteId, Guid currentUserId, bool showDeleted,
                                               bool showArchived, int page, int pageSize, RiskAssessmentOrderByColumn orderBy, OrderByDirection orderByDirection);

        int Count(string title, long clientId, IEnumerable<long> siteIds,
                  DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                  long? siteId, Guid currentUserId, bool showDeleted, bool showArchived);

        FireRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId);
    }
}

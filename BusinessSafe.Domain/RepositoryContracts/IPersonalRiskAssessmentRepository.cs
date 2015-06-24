using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public enum PersonalRiskAssessmentOrderByColumn
    {
        Reference,
        Title,
        Site,
        AssignedTo,
        Status,
        AssessmentDate,
        NextReview
    }

    public interface IPersonalRiskAssessmentRepository : IRepository<PersonalRiskAssessment, long>
    {
        PersonalRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId, Guid currentUserId);

        IEnumerable<PersonalRiskAssessment> Search(string title, long clientId, IEnumerable<long> siteIds,
                                                   DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                                                   long? siteId, Guid currentUserId, bool showDeleted, bool showArchived,
                                                   int page, int pageSize, RiskAssessmentOrderByColumn orderBy, OrderByDirection orderByDirection);
        int Count(string title, long clientId, IEnumerable<long> siteIds,
                                                   DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                                                   long? siteId, Guid currentUserId, bool showDeleted, bool showArchived);
    }
}
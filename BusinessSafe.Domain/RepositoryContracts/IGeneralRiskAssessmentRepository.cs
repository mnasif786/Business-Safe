using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public enum RiskAssessmentOrderByColumn
    {
        Reference,
        Title,
        Site,
        AssignedTo,
        Status,
        AssessmentDate,
        NextReview,
        CreatedOn
    }

    public enum OrderByDirection
    {
        Ascending,
        Descending
    }

    public interface IGeneralRiskAssessmentRepository : IRepository<GeneralRiskAssessment, long>
    {
        GeneralRiskAssessment GetByIdAndCompanyId(long riskAssessmentId, long companyId);

        IEnumerable<GeneralRiskAssessment> Search(string title, long clientId, IEnumerable<long> siteIds,
                                                  DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                                                  long? siteId, Guid currentUserId, bool showDeleted, bool showArchived,
                                                  int page, int pageSize, RiskAssessmentOrderByColumn orderBy, OrderByDirection orderByDirection);

        int Count(string title, long clientId, IEnumerable<long> siteIds,
                  DateTime? createdFrom, DateTime? createdTo, long? siteGroupId,
                  long? siteId, Guid currentUserId, bool showDeleted, bool showArchived);
    }
}
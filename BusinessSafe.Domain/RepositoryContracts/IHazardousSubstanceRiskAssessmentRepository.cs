using BusinessSafe.Domain.Common;
using BusinessSafe.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BusinessSafe.Domain.RepositoryContracts
{
    public interface IHazardousSubstanceRiskAssessmentRepository: IRepository<HazardousSubstanceRiskAssessment, long>
    {
        HazardousSubstanceRiskAssessment GetByIdAndCompanyId(long hazardousSubstanceAssessmentId, long companyId);

        IEnumerable<HazardousSubstanceRiskAssessment> Search(string title, long clientId, DateTime? createdFrom, DateTime? createdTo, long hazardousSubstanceId, IEnumerable<long> allowedSiteIds, bool showDeleted, bool showArchived, Guid currentUserId, long? siteId, long? siteGroupId, int page, int pageSize, RiskAssessmentOrderByColumn OrderBy, OrderByDirection SortOrder);

        int Count(string title, long companyId, DateTime? createdFrom, DateTime? createdTo,long hazardousSubstanceId, IEnumerable<long> allowedSiteIds, bool showDeleted, bool showArchived, Guid currentUserId, long? siteId, long? siteGroupId);

        bool DoesRiskAssessmentsExistForHazardousSubstance(long hazardousSubstanceId, long companyId);
    }
}
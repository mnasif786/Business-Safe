using System;
using System.Collections.Generic;
using BusinessSafe.Application.Contracts.HazardousSubstanceRiskAssessments;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.HazardousSubstanceRiskAssessments.Factories
{
    public interface ISearchRiskAssessmentsViewModelFactory
    {
        ISearchRiskAssessmentsViewModelFactory WithTitle(string title);
        ISearchRiskAssessmentsViewModelFactory WithCompanyId(long companyId);
        ISearchRiskAssessmentsViewModelFactory WithCreatedFrom   (string createFrom);
        ISearchRiskAssessmentsViewModelFactory WithCreatedTo(string createTo);
        ISearchRiskAssessmentsViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        ISearchRiskAssessmentsViewModelFactory WithHazardousSubstanceId(long hazardousSubstanceId);
        ISearchRiskAssessmentsViewModelFactory WithCurrentUserId(Guid currentUserId);
        ISearchRiskAssessmentsViewModelFactory WithSiteId(long? siteId);
        ISearchRiskAssessmentsViewModelFactory WithSiteGroupId(long? siteGroupId);
        ISearchRiskAssessmentsViewModelFactory WithShowDeleted(bool showDeleted);
        ISearchRiskAssessmentsViewModelFactory WithShowArchived(bool showArchived);
        SearchRiskAssessmentsViewModel GetViewModel(IHazardousSubstanceRiskAssessmentService riskAssessmentService);
        ISearchRiskAssessmentsViewModelFactory WithPageNumber(int i);
        ISearchRiskAssessmentsViewModelFactory WithPageSize(int i);
        ISearchRiskAssessmentsViewModelFactory WithOrderBy(string order);
    }
}
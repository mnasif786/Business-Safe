using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.PersonalRiskAssessments.Factories
{
    public interface ISearchPersonalRiskAssessmentsViewModelFactory
    {
        ISearchPersonalRiskAssessmentsViewModelFactory WithTitle(string title);
        ISearchPersonalRiskAssessmentsViewModelFactory WithCompanyId(long companyId);
        ISearchPersonalRiskAssessmentsViewModelFactory WithCreatedFrom(string createdFrom);
        ISearchPersonalRiskAssessmentsViewModelFactory WithCreatedTo(string createdTo);
        ISearchPersonalRiskAssessmentsViewModelFactory WithSiteGroupId(long? siteGroupId);
        ISearchPersonalRiskAssessmentsViewModelFactory WithShowDeleted(bool showDeleted);
        ISearchPersonalRiskAssessmentsViewModelFactory WithShowArchived(bool showArchived);
        ISearchPersonalRiskAssessmentsViewModelFactory WithRiskAssessmentTemplatingMode(bool isRiskAssessmentTemplating);
        ISearchPersonalRiskAssessmentsViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        ISearchPersonalRiskAssessmentsViewModelFactory WithCurrentUserId(Guid currentUserId);
        ISearchPersonalRiskAssessmentsViewModelFactory WithSiteId(long? siteId);
        ISearchPersonalRiskAssessmentsViewModelFactory WithPageNumber(int page);
        ISearchPersonalRiskAssessmentsViewModelFactory WithPageSize(int pageSize);
        ISearchPersonalRiskAssessmentsViewModelFactory WithOrderBy(string orderBy);
        SearchRiskAssessmentsViewModel GetViewModel();
    }
}
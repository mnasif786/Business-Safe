using System;
using System.Collections.Generic;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.GeneralRiskAssessments.Factories
{
    public interface ISearchRiskAssessmentViewModelFactory
    {
        ISearchRiskAssessmentViewModelFactory WithTitle(string title);
        ISearchRiskAssessmentViewModelFactory WithCompanyId(long companyId);
        ISearchRiskAssessmentViewModelFactory WithCreatedFrom(string createdFrom);
        ISearchRiskAssessmentViewModelFactory WithCreatedTo(string createdTo);
        ISearchRiskAssessmentViewModelFactory WithSiteGroupId(long? siteGroupId);
        ISearchRiskAssessmentViewModelFactory WithShowDeleted(bool showDeleted);
        ISearchRiskAssessmentViewModelFactory WithShowArchived(bool showArchived);
        ISearchRiskAssessmentViewModelFactory WithAllowedSiteIds(IList<long> allowedSiteIds);
        ISearchRiskAssessmentViewModelFactory WithCurrentUserId(Guid currentUserId);
        ISearchRiskAssessmentViewModelFactory WithSiteId(long? siteId);
        ISearchRiskAssessmentViewModelFactory WithPageNumber(int page);
        ISearchRiskAssessmentViewModelFactory WithPageSize(int pageSize);
        ISearchRiskAssessmentViewModelFactory WithOrderBy(string orderBy);
        SearchRiskAssessmentsViewModel GetViewModel();

        
    }
}
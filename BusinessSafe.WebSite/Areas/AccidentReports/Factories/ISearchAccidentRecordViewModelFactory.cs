
using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using System;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public interface ISearchAccidentRecordViewModelFactory
    {        
        ISearchAccidentRecordViewModelFactory WithCurrentUserId(Guid currentUserId);
        ISearchAccidentRecordViewModelFactory WithCompanyId(long companyId);
        ISearchAccidentRecordViewModelFactory WithAllowedSites(IList<long> siteIds);
        
        ISearchAccidentRecordViewModelFactory WithSiteId(long? siteId);
        ISearchAccidentRecordViewModelFactory WithCreatedFrom(DateTime createdFrom);
        ISearchAccidentRecordViewModelFactory WithCreatedTo(DateTime createdTo);
        ISearchAccidentRecordViewModelFactory WithTitle(string title);

        ISearchAccidentRecordViewModelFactory WithShowDeleted(bool showDeleted);
        ISearchAccidentRecordViewModelFactory WithInjuredPersonForename(string injuredPersonForename);
        ISearchAccidentRecordViewModelFactory WithInjuredPersonSurname(string injuredPersonSurname);
        
        ISearchAccidentRecordViewModelFactory WithPageNumber(int pageNumber);
        ISearchAccidentRecordViewModelFactory WithPageSize(int pageSize);
        ISearchAccidentRecordViewModelFactory WithOrderBy(string orderBy);
        AccidentRecordsIndexViewModel GetViewModel();

    }
}

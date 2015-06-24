using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public interface IAccidentRecordOverviewViewModelFactory
    {
        IAccidentRecordOverviewViewModelFactory WithAccidentRecordId(long accidentRecordId);
        IAccidentRecordOverviewViewModelFactory WithCompanyId(long companyId);

        OverviewViewModel GetViewModel();
    }
}
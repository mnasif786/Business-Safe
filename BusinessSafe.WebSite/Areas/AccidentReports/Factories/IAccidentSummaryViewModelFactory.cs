using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public interface IAccidentSummaryViewModelFactory
    {
        IAccidentSummaryViewModelFactory WithCompanyId(long companyId);
        IAccidentSummaryViewModelFactory WithAccidentRecordId(long accidentRecordId);
        AccidentSummaryViewModel GetViewModel();
    }
}
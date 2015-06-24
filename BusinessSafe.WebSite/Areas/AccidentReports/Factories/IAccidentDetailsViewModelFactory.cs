using System.Collections.Generic;
using BusinessSafe.WebSite.Areas.AccidentReports.ViewModels;

namespace BusinessSafe.WebSite.Areas.AccidentReports.Factories
{
    public interface IAccidentDetailsViewModelFactory
    {
        IAccidentDetailsViewModelFactory WithCompanyId(long companyId);
        IAccidentDetailsViewModelFactory WithAccidentRecordId(long accidentRecordId);
        IAccidentDetailsViewModelFactory WithSites(IList<long> sites);
        AccidentDetailsViewModel GetViewModel();

        
    }
}
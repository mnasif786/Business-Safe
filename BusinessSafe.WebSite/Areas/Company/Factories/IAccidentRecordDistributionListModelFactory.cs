using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.WebSite.Areas.Company.ViewModels;

namespace BusinessSafe.WebSite.Areas.Company.Factories
{
    public interface IAccidentRecordDistributionListModelFactory
    {
        IAccidentRecordDistributionListModelFactory WithCompanyId(long companyId);
        IAccidentRecordDistributionListModelFactory WithAllowedSites(IList<long> sites);        
        IAccidentRecordDistributionListModelFactory WithSiteId(long siteId);
        
        AccidentRecordDistributionListViewModel GetViewModel();
    }
}

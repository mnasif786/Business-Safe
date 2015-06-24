using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessSafe.WebSite.Contracts.SqlReports
{
    public interface IAccidentRecordsFactory
    {
        IAccidentRecordsFactory WithCompanyId(long CompanyId);
        IAccidentRecordsFactory WithShowDeleted(bool showDeleted);
        IAccidentRecordsFactory WithSiteId(long? siteId);
        IAccidentRecordsFactory WithStartDate(DateTime? startDate);
        IAccidentRecordsFactory WithEndDate(DateTime? endDate);
        IAccidentRecordsFactory WithAllowedSiteIds(List<long> allowedSiteIds );


        long[] GetAccidentRecords();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Contracts.SqlReports;

namespace BusinessSafe.WebSite.Areas.SqlReports.Factories
{
    public class AccidentRecordsFactory : IAccidentRecordsFactory
    {
        private IAccidentRecordService _accidentRecordsService;

        private SearchAccidentRecordsRequest _request = new SearchAccidentRecordsRequest();

        public AccidentRecordsFactory (IAccidentRecordService accidentRecordsService)
        {
            _accidentRecordsService = accidentRecordsService;
        }

        public IAccidentRecordsFactory WithCompanyId(long CompanyId)
        {
            _request.CompanyId = CompanyId;
            return this;
        }

        public IAccidentRecordsFactory WithShowDeleted(bool showDeleted)
        {
            _request.ShowDeleted = showDeleted;
            return this;
        }

        public IAccidentRecordsFactory WithSiteId(long? siteId)
        {
            _request.SiteId = siteId; 
            return this;
        }

        public IAccidentRecordsFactory WithStartDate(DateTime? startDate)
        {
            _request.CreatedFrom = startDate;
            return this;
        }

        public IAccidentRecordsFactory WithEndDate(DateTime? endDate)
        {
            _request.CreatedTo = endDate;
            return this;
        }

        public IAccidentRecordsFactory WithAllowedSiteIds(List<long> allowedSiteIds)
        {
            _request.AllowedSiteIds = allowedSiteIds;
            return this;
        }

        public long[] GetAccidentRecords()
        {                     
            var searchResults = _accidentRecordsService.Search( _request);

            if (searchResults.Any())
            {
                return searchResults.Select(x => x.Id).ToArray();
            }
            else
            {
                return new[] {0L};
            }
        }
    }
}
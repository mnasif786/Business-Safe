using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessSafe.Application.Contracts.Sites;
using BusinessSafe.Application.Helpers;
using BusinessSafe.Application.Request;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.ViewModels;

namespace BusinessSafe.WebSite.Areas.SqlReports.Factories
{
    public class SqlReportsViewModelFactory : ISqlReportsViewModelFactory
    {
        private readonly ISiteGroupService _siteGroupService;
        private readonly ISiteService _siteService;

        private IDictionary<SqlReportHelper.ReportType, string> _reportTypes;
        private long _companyId;
        private long? _siteId;
        private long? _siteGroupId;
        private SqlReportHelper.ReportType _reportId;

        private IList<long> _allowedSiteIds;
        
        public SqlReportsViewModelFactory(ISiteGroupService siteGroupService, ISiteService siteService)
        {
            _siteGroupService = siteGroupService;
            _siteService = siteService;
        }

        public ISqlReportsViewModelFactory WithReportId(SqlReportHelper.ReportType reportId)
        {
            _reportId = reportId;
            return this;
        }

        public ISqlReportsViewModelFactory WithSiteId(long? siteId)
        {
            _siteId = siteId;
            return this;
        }

        public ISqlReportsViewModelFactory WithSiteGroupId(long? siteGroupId)
        {
            _siteGroupId = siteGroupId;
            return this;
        }

        public ISqlReportsViewModelFactory WithCompanyId(long companyId)
        {
            _companyId = companyId;
            return this;
        }

        public ISqlReportsViewModelFactory WithReportTypes()
        {
            var reportTypes = new Dictionary<SqlReportHelper.ReportType, string>
            {                
                {SqlReportHelper.ReportType.AccidentRecords,                EnumHelper.GetEnumDescription(SqlReportHelper.ReportType.AccidentRecords)},
                {SqlReportHelper.ReportType.AccidentRecords_AnalysisReport, EnumHelper.GetEnumDescription(SqlReportHelper.ReportType.AccidentRecords_AnalysisReport)},
                {SqlReportHelper.ReportType.HazardousSubstancesInventory,   EnumHelper.GetEnumDescription(SqlReportHelper.ReportType.HazardousSubstancesInventory)},
                {SqlReportHelper.ReportType.TaskStatus,                     EnumHelper.GetEnumDescription(SqlReportHelper.ReportType.TaskStatus)},
                {SqlReportHelper.ReportType.TaskStatus_Completed,           EnumHelper.GetEnumDescription(SqlReportHelper.ReportType.TaskStatus_Completed)},
                {SqlReportHelper.ReportType.TaskStatus_Outstanding,         EnumHelper.GetEnumDescription(SqlReportHelper.ReportType.TaskStatus_Outstanding)}
            };

            _reportTypes = reportTypes;
            return this;
        }

        private IEnumerable<AutoCompleteViewModel> GetSiteGroups()
        {
            var linkedGroupsDtos = _siteGroupService.GetByCompanyId(_companyId);
            return linkedGroupsDtos.Select(AutoCompleteViewModel.ForSiteGroup).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetSites()
        {
            var siteDtos = _siteService.Search(new SearchSitesRequest
            {
                CompanyId = _companyId,
                AllowedSiteIds = _allowedSiteIds
            });
            return siteDtos.Select(AutoCompleteViewModel.ForSite).AddDefaultOption();
        }

        private IEnumerable<AutoCompleteViewModel> GetSupportedReportType()
        {                      
            return _reportTypes.Select(AutoCompleteViewModel.ForSqlReportType).AddDefaultOption();
        }


        public SqlReportViewModel GetViewModel()
        {         
            var sites = GetSites();
            var siteGroups = GetSiteGroups();
            var reports = GetSupportedReportType();

            var viewModel = new SqlReportViewModel
            {
                CompanyId = _companyId,
               
                Sites = sites,
                SiteGroups = siteGroups,

                SiteId = _siteId,
                SiteGroupId = _siteGroupId,  

                ReportTypes = reports,
                ReportId = (long)_reportId
            };

            return viewModel;

        }
    }
}
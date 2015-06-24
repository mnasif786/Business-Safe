using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessSafe.Application.Common;
using BusinessSafe.Application.Contracts.AccidentRecord;
using BusinessSafe.Application.Custom_Exceptions;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.Factories;
using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.AuthenticationService;
using BusinessSafe.WebSite.Contracts;
using BusinessSafe.WebSite.Controllers;
using BusinessSafe.WebSite.Custom_Exceptions;
using BusinessSafe.WebSite.Extensions;
using BusinessSafe.WebSite.Filters;

namespace BusinessSafe.WebSite.Areas.SqlReports.Controllers
{
    public class SqlReportsController : BaseController
    {
        private readonly ISqlReportExecutionServiceFacade _sqlReportExecutionServiceFacade;
        private readonly ISqlReportsViewModelFactory _sqlReportsViewModelFactory;

        private readonly AccidentRecordsFactory _accidentRecordsFactory;

        public SqlReportsController(ISqlReportsViewModelFactory sqlReportsViewModelFactory,
                                    ISqlReportExecutionServiceFacade sqlReportExecutionServiceFacade,
            
                                    IAccidentRecordService accidentRecordsService)
        {
            _sqlReportExecutionServiceFacade = sqlReportExecutionServiceFacade;
            _sqlReportsViewModelFactory = sqlReportsViewModelFactory;
            _accidentRecordsFactory = new AccidentRecordsFactory(accidentRecordsService);
        }
        
        [PermissionFilter(Permissions.ViewViewReports)]
        public ActionResult Index()
        {                      
            ViewBag.HideClass = "hide";

            return View("Index", GetReportsViewModel());
        }

        [HttpPost]
        [PermissionFilter(Permissions.ViewViewReports)]
        public ActionResult View(SqlReportViewModel model)
        {
            if (!IsModelValid(model) )
            {
                ViewBag.HideClass = string.Empty; 
          
                var initModel = GetReportsViewModel();
                initModel.SiteId = model.SiteId;
                initModel.DateStart = model.DateStart;
                initModel.DateEnd = model.DateEnd;
                initModel.ReportId = model.ReportId;
                
                if (model.DateEnd.HasValue && model.DateStart.HasValue && model.DateStart.Value > model.DateEnd.Value)
                {             
                    ViewBag.ErrorString = "Start Date must be before End Date.";
                }

                if (!model.DateEnd.HasValue)
                {
                   ViewBag.ErrorString = "Please Select an End Date.";
                }

                if (!model.DateStart.HasValue)
                {
                    ViewBag.ErrorString = "Please Select a Start Date.";
                }

                if (!model.ReportId.HasValue)
                {
                    ViewBag.ErrorString = "Please Select a Report.";
                }

                return View("Index", initModel);
            }

            model.CompanyId = CurrentUser.CompanyId;

            DocumentViewModel document = null;

            var reportType = (SqlReportHelper.ReportType) model.ReportId;

            object[] parameters;

            if (model.DateEnd == null)
                model.DateEnd = DateTime.Now;

            if (model.DateStart == null)
                model.DateStart = DateTime.MinValue;

            Log4NetHelper.Logger.Debug(String.Format("Calling Report type {0}", reportType));
            switch (reportType)
            {
                case (SqlReportHelper.ReportType.TaskStatus): 
                case (SqlReportHelper.ReportType.TaskStatus_Completed): 
                case (SqlReportHelper.ReportType.TaskStatus_Outstanding):                     
                    parameters = new object[] { GetSiteOrSiteGroupId(model), model.DateStart, model.DateEnd }; 
                    break;
                
                case (SqlReportHelper.ReportType.AccidentRecords):
                    long[] accidentRecords = _accidentRecordsFactory
                                        .WithCompanyId(CurrentUser.CompanyId)
                                        .WithShowDeleted(false)                                        
                                        .WithStartDate(model.DateStart)
                                        .WithEndDate(model.DateEnd)
                                        .WithAllowedSiteIds(CurrentUser.GetSitesFilter().ToList())                                        
                                        .GetAccidentRecords();

                    string csvAccidentRecords = String.Join(",", accidentRecords);
                
                    parameters = new object[] { csvAccidentRecords, model.DateStart, model.DateEnd, GetSiteOrSiteGroupId(model) };                 
                    break;

                case (SqlReportHelper.ReportType.AccidentRecords_AnalysisReport): 
                    parameters = new object[] { model.DateStart, model.DateEnd, GetSiteOrSiteGroupId(model) }; 
                    break;

                case (SqlReportHelper.ReportType.HazardousSubstancesInventory):
                    parameters = new object[] { GetSiteOrSiteGroupId(model) }; 
                    break;

                default: throw new Exception(String.Format("Unsupported or unknown Report Type {0}", model.ReportId)); break;
            }

            

            document = _sqlReportExecutionServiceFacade.GetReport(reportType,
                                                                  parameters,
                                                                  SqlReportHelper.ReportFormatType.Excel);


            return File(document.FileStream, document.MimeType, document.FileName);
        }

        private bool IsModelValid(SqlReportViewModel model)
        {
            return model.ReportId.HasValue && model.DateStart.HasValue && model.DateEnd.HasValue &&  model.DateStart.Value < model.DateEnd.Value;
        }

        private SqlReportViewModel GetReportsViewModel()
        {            
            SqlReportViewModel model = _sqlReportsViewModelFactory
                .WithCompanyId(CurrentUser.CompanyId)
                .WithReportTypes()
                .GetViewModel();
           

            return model;
        }

        private string GetSiteOrSiteGroupId(SqlReportViewModel model)
        {
            string siteID = null;

            if (model.SiteGroupId == null && model.SiteId == null)
            {
                // return all sites user is allowed to see as CSV string                           
                siteID = String.Join(",", CurrentUser.GetSitesFilter());
            }
            else
            {
                siteID = model.SiteGroupId == null ? model.SiteId.ToString() : model.SiteGroupId.ToString();
            }

            return siteID;
        }
    }
}

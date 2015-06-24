using System.Collections.Generic;

using BusinessSafe.WebSite.Areas.SqlReports.Helpers;
using BusinessSafe.WebSite.Areas.SqlReports.ViewModels;
using BusinessSafe.WebSite.SqlReportExecutionService;

namespace BusinessSafe.WebSite.Areas.SqlReports.Factories
{
    public class SqlReportDefinitionFactory
    {
        public static List<SqlReportDefinition> GetSqlReportDefinitions()
        {
            var reportDefinitions = new List<SqlReportDefinition>();

            reportDefinitions.Add(new SqlReportDefinition()
            {
                Report = SqlReportHelper.ReportType.GRA,
                ReportPath = @"/BusinessSafe_OnlineContent/GRA",
                Parameters = new List<ParameterValue> { new ParameterValue { Name = "RAID" } },
                Filename = "Risk Assessment"
            });

            reportDefinitions.Add(new SqlReportDefinition()
            {
                Report = SqlReportHelper.ReportType.HSRA,
                ReportPath = @"/BusinessSafe_OnlineContent/BSO006_HazardsRA",
                Parameters = new List<ParameterValue> { new ParameterValue { Name = "RAID" } },
                Filename = "Risk Assessment"
            });

            reportDefinitions.Add(new SqlReportDefinition()
            {
                Report = SqlReportHelper.ReportType.FRA,
                ReportPath = @"/BusinessSafe_OnlineContent/BSO12_FRA",
                Parameters = new List<ParameterValue> { new ParameterValue { Name = "RAID" } },
                Filename = "Risk Assessment"
            });

            reportDefinitions.Add(new SqlReportDefinition()
            {
                Report = SqlReportHelper.ReportType.PRA,
                ReportPath = @"/BusinessSafe_OnlineContent/BSO007_PRA",
                Parameters = new List<ParameterValue> { new ParameterValue { Name = "RAID" } },
                Filename = "Risk Assessment"
            });

            reportDefinitions.Add(new SqlReportDefinition()
            {
                Report = SqlReportHelper.ReportType.Responsibilities,
                ReportPath = @"/BusinessSafe_OnlineContent/BSO024_ResponsibilitiesRecordPrintOut",
                Parameters = new List<ParameterValue>
                                 {
                                     new ParameterValue { Name = "ResponsibilityID" }                                   
                                 },
                Filename = "Risk Assessment"
            });

            reportDefinitions.Add(new SqlReportDefinition()
                                      {
                                          Report = SqlReportHelper.ReportType.Responsibilities_Index,
                                          ReportPath = @"/BusinessSafe_OnlineContent/BSO022_Responsibilities",
                                          Parameters = new List<ParameterValue>
                                                           {
                                                               new ParameterValue {Name = "CompanyID"},
                                                               new ParameterValue {Name = "Title"},
                                                               new ParameterValue {Name = "CategoryID"},
                                                               new ParameterValue {Name = "SiteID"},
                                                               new ParameterValue {Name = "FromDate"},
                                                               new ParameterValue {Name = "ToDate"}
                                                           },
                                          Filename = "Risk Assessment"
                                      });

            reportDefinitions.Add(new SqlReportDefinition()
            {
                Report = SqlReportHelper.ReportType.AccidentRecord,
                ReportPath = @"/BusinessSafe_OnlineContent/AccidentRecordIndividual",
                Parameters = new List<ParameterValue>
                                                           {
                                                               new ParameterValue {Name = "AccidentRecordId"}
                                                           },
                Filename = "Accident Record"
            });

	       reportDefinitions.Add(new SqlReportDefinition()
	            {
	                Report = SqlReportHelper.ReportType.AccidentRecords,
                    ReportPath = @"/BusinessSafe_OnlineContent/AccidentRecordsAll",
	                Parameters = new List<ParameterValue>
	                                                           {
	                                                                new ParameterValue {Name = "AccidentRecordIds"},
                                                                    new ParameterValue {Name = "StartDate"},
                                                                    new ParameterValue {Name = "EndDate"},
                                                                    new ParameterValue {Name = "SiteID"}
	                                                           },
	                Filename = "Accident Records"
	            });
			           
				
			reportDefinitions.Add(new SqlReportDefinition()
            {
                Report = SqlReportHelper.ReportType.HazardousSubstancesInventory,
                ReportPath = @"/BusinessSafe_OnlineContent/HazardousSubstancesInventory",
                
                Parameters = new List<ParameterValue>
                                 {
                                     new ParameterValue { Name = "SiteID" }                                     
                                 },

                Filename = "Hazardous Substances Inventory"
            });

             reportDefinitions.Add(new SqlReportDefinition()
	            {
	                Report = SqlReportHelper.ReportType.AccidentRecords_AnalysisReport,
	                ReportPath = @"/BusinessSafe_OnlineContent/BSO025_AccidentLog",                  

	                Parameters = new List<ParameterValue>
	                            {
	                                new ParameterValue {Name = "DateStart"},
                                    new ParameterValue {Name = "DateEnd"},
                                    new ParameterValue {Name = "SiteID"}
	                            },
	                Filename = "AccidentRecords_AnalysisReport"
	            });

             reportDefinitions.Add(new SqlReportDefinition()
             {
                 Report = SqlReportHelper.ReportType.TaskStatus,
                 ReportPath = @"/BusinessSafe_OnlineContent/BSO021_TaskStatus",

                 Parameters = new List<ParameterValue>
	                            {	                                                                   
                                    new ParameterValue {Name = "SiteID"},
                                    new ParameterValue {Name = "StartDate"},
                                    new ParameterValue {Name = "EndDate"}
	                            },
                 Filename = "TaskStatus"
             });

             reportDefinitions.Add(new SqlReportDefinition()
             {
                 Report = SqlReportHelper.ReportType.TaskStatus_Completed,
                 ReportPath = @"/BusinessSafe_OnlineContent/BSO021_TaskStatus(Completed)",

                 Parameters = new List<ParameterValue>
	                            {	                                                                    
                                    new ParameterValue {Name = "SiteID"},
                                    new ParameterValue {Name = "StartDate"},
                                    new ParameterValue {Name = "EndDate"}                                    
	                            },
                 Filename = "TaskStatus_Completed"
             });

             reportDefinitions.Add(new SqlReportDefinition()
             {
                 Report = SqlReportHelper.ReportType.TaskStatus_Outstanding,
                 ReportPath = @"/BusinessSafe_OnlineContent/BSO021_TaskStatus(Outstanding)",

                 Parameters = new List<ParameterValue>
	                            {	                                                                    
                                    new ParameterValue {Name = "SiteID"},
                                    new ParameterValue {Name = "StartDate"},
                                    new ParameterValue {Name = "EndDate"}                                    
	                            },
                 Filename = "TaskStatus_Outstanding"
             });



            return reportDefinitions;
        }
    }
}
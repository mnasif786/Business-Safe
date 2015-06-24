using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using BusinessSafe.WebSite.Helpers;
using BusinessSafe.WebSite.HtmlHelpers;
using BusinessSafe.WebSite.Models;

namespace BusinessSafe.WebSite.Areas.SqlReports.Helpers
{
    public class SqlReportHelper
    {
        public static string GetExportFormatString(ReportFormatType type)
        {
            switch (type)
            {
                case ReportFormatType.XML: return "XML";
                case ReportFormatType.CSV: return "CSV";
                case ReportFormatType.Image: return "IMAGE";
                case ReportFormatType.PDF: return "PDF";
                case ReportFormatType.MHTML: return "MHTML";
                case ReportFormatType.HTML4: return "HTML4.0";
                case ReportFormatType.HTML32: return "HTML3.2";
                case ReportFormatType.Excel: return "EXCEL";
                case ReportFormatType.Word: return "WORD";

                default:
                    return "PDF";
            }
        }

        public enum ReportFormatType
        {
            XML,
            CSV,
            Image,
            PDF,
            MHTML,
            HTML4,
            HTML32,
            Excel,
            Word
        }

        public enum ReportType
        {
            [Description("General Risk Assessment")]
            GRA = 1, 

            [Description("Hazard Risk Assessment")]
            HSRA = 2, 

            [Description("Personal Risk Assessment")]
            PRA = 3, 

            [Description("Fire Risk Assessment")]
            FRA = 4,

            [Description("Responsibilities")]
            Responsibilities = 5,

            [Description("Responsibilities Index")]
            Responsibilities_Index = 6,

            [Description("Accident Record")]
            AccidentRecord = 7,

            [Description("Accident Records")]
     	    AccidentRecords = 8,

            [Description("Hazardous Substances Inventory")]
            HazardousSubstancesInventory = 9,

            [Description("Task Status")]
            TaskStatus,

            [Description("Task Status Completed")]
            TaskStatus_Completed,

            [Description("Task Status Outstanding")]
            TaskStatus_Outstanding,

            [Description("Accident Records Analysis Report")]
            AccidentRecords_AnalysisReport,

            [Description("Risk Assessor Summary")]
            RiskAssessorSummary
        }

        public static FeatureSwitches GetSqlReportFeatureSwitch(RiskAssessmentType raType)
        {
            return featureSwitches[raType];
        }

        public static ReportType GetSqlReportType(RiskAssessmentType raType)
        {
            return reportTypes[raType];
        }

        public static NetworkCredential GetNetworkCredential()
        {
            if (ConfigurationManager.AppSettings["SQLReportsDomain"] != null && ConfigurationManager.AppSettings["SQLReportsUserName"] != null && ConfigurationManager.AppSettings["SQLReportsPassword"] != null)
            {
                return new NetworkCredential
                {
                    Domain = ConfigurationManager.AppSettings["SQLReportsDomain"],
                    UserName = EncryptionHelper.Decrypt(ConfigurationManager.AppSettings["SQLReportsUserName"]),
                    Password = EncryptionHelper.Decrypt(ConfigurationManager.AppSettings["SQLReportsPassword"])
                };
            }

            throw new ArgumentException("Network Credentials must not be null");
        }


        private static readonly Dictionary<RiskAssessmentType, SqlReportHelper.ReportType> reportTypes = new Dictionary<RiskAssessmentType, SqlReportHelper.ReportType>()
                                                                                             {
                                                                                                 { RiskAssessmentType.GRA, SqlReportHelper.ReportType.GRA },
                                                                                                 { RiskAssessmentType.HSRA, SqlReportHelper.ReportType.HSRA },
                                                                                                 { RiskAssessmentType.PRA, SqlReportHelper.ReportType.PRA },
                                                                                                 { RiskAssessmentType.FRA, SqlReportHelper.ReportType.FRA }
                                                                                             };

        private static readonly Dictionary<RiskAssessmentType, FeatureSwitches> featureSwitches = new Dictionary<RiskAssessmentType, FeatureSwitches>()
                                                                                             {
                                                                                                 { RiskAssessmentType.GRA, FeatureSwitches.FeatureSwitch_SqlReports_For_GRA },
                                                                                                 { RiskAssessmentType.HSRA,FeatureSwitches.FeatureSwitch_SqlReports_For_HSRA},
                                                                                                 { RiskAssessmentType.PRA, FeatureSwitches.FeatureSwitch_SqlReports_For_PRA},
                                                                                                 { RiskAssessmentType.FRA, FeatureSwitches.FeatureSwitch_SqlReports_For_FRA}
                                                                                             };
    }
}
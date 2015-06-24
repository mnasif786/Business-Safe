using System;
using System.Data.SqlClient;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class InjuryDetailsSteps : BaseSteps
    {
        [BeforeScenario(@"NeedsAccidentRecordWithJurisdictionSetToROI")]
        public void CreateAccidentRecordWithJurisdictionSetToROI()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                       "[Id], [Title], [Reference], [JurisdictionId], [CompanyId], " +
                       "[Deleted], [CreatedOn], [CreatedBy]) " +
                       "VALUES (-1, 'Accident record for injury details tests', 'IP001', 2, " + // ROI = 2
                       " 55881, 0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c');");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [BeforeScenario(@"NeedsAccidentRecordWithJurisdictionSetToGB")]
        public void CreateAccidentRecordWithJurisdictionSetToGB()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                       "[Id], [Title], [Reference], [JurisdictionId], [CompanyId], " +
                       "[Deleted], [CreatedOn], [CreatedBy]) " +
                       "VALUES (-1, 'Accident record for injury details tests', 'IP001', 1, " +  // GB = 1
                       " 55881, 0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c');");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [BeforeScenario(@"NeedsAccidentRecordWithJurisdictionNotSetToROI")]
        public void CreateAccidentRecordWithJurisdictionNotSetToROI()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                       "[Id], [Title], [Reference], [JurisdictionId], [CompanyId], " +
                       "[Deleted], [CreatedOn], [CreatedBy]) " +
                       "VALUES (-1, 'Accident record for injury details tests', 'IP001', 1, 55881, " +
                       "0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c');");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"NeedsAccidentRecordWithJurisdictionSetToROI")]
        [AfterScenario(@"NeedsAccidentRecordWithJurisdictionNotSetToROI")]
        [AfterScenario(@"NeedsAccidentRecordWithJurisdictionSetToGB")]
        public void DeleteNeedsAccidentRecordWithJurisdictionSet()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[AccidentRecord] WHERE Id = -1;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [Then(@"element with id '(.*)' has value '(.*)'")]
        public void ThenElementWithIdHasValue(string elementId, string expectedValue)
        {
            var element = WebBrowser.Current.Element(Find.ById(elementId));
            Assert.IsNotNull(element, string.Format("Could not find element with id {0}", elementId));
            Assert.That(element.Text, Is.EqualTo(expectedValue),
                        string.Format("Element with Id {0} value not as expected", elementId));
        }

        [BeforeScenario(@"NeedsAccidentRecordToAddInjuryDetailsTo")]
        public void CreateAccidentRecordToAddInjuredPersonDetailsTo()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                "[Id], [Title], [Reference], [JurisdictionId], [CompanyId], " +
                "[Deleted], [CreatedOn], [CreatedBy]) " +
                "VALUES (-1, 'Accident record for injured person tests', 'IP001', 1, 55881, " +
                "0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c');");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"NeedsAccidentRecordToAddInjuryDetailsTo")]
        public void DeleteAccidentRecordToAddInjuredPersonDetailsTo()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[AccidentRecord] WHERE Id = -1;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }


        [BeforeScenario(@"NeedsAccidentRecordWithVisitorToAddInjuryDetailsTo")]
        public void CreateAccidentRecordWithVisitorToAddInjuredPersonDetailsTo()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                "[Id], [Title], [Reference],[PersonInvolvedId], [JurisdictionId], [CompanyId], " +
                "[Deleted], [CreatedOn], [CreatedBy]) " +
                "VALUES (-1, 'Accident record for visitor injured person tests', 'IP001',2, 1, 55881, " +
                "0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c');");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"NeedsAccidentRecordWithVisitorToAddInjuryDetailsTo")]
        public void DeleteAccidentRecordWithVisitorToAddInjuredPersonDetailsTo()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[AccidentRecord] WHERE Id = -1;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }


        [BeforeScenario(@"NeedsAccidentRecordWithEmployeeToAddInjuryDetailsTo")]
        public void CreateAccidentRecordWithEmployeeToAddInjuredPersonDetailsTo()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                "[Id], [Title], [Reference],[PersonInvolvedId], [JurisdictionId], [CompanyId], " +
                "[Deleted], [CreatedOn], [CreatedBy]) " +
                "VALUES (-1, 'Accident record for visitor injured person tests', 'IP001',1, 1, 55881, " +
                "0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c');");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"NeedsAccidentRecordWithEmployeeToAddInjuryDetailsTo")]
        public void DeleteAccidentRecordWithEmployeeToAddInjuredPersonDetailsTo()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[AccidentRecord] WHERE Id = -1;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }
    }
}

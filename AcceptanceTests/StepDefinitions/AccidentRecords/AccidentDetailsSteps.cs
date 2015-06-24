using System.Data.SqlClient;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.AccidentRecords
{
    [Binding]
    public class AccidentDetailsSteps : BaseSteps
    {
        [BeforeScenario(@"NeedsAccidentRecordToAddAccidentDetailsTo")]
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

        [AfterScenario(@"NeedsAccidentRecordToAddAccidentDetailsTo")]
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

        [Then(@"Error Message '(.*)' Contains:")]
        public void ThenErrorMessagContains(string errorDivId, Table table)
        {
            var errorDiv = GetElement(FindValidationErrorsList, "validation-summary-errors alert alert-error", 10000);
            Assert.True(errorDiv.Exists, "Could not find error list");
            foreach (var tableRow in table.Rows)
            {
                var errorDivContent = errorDiv.InnerHtml.ToLower();
                Assert.That(errorDivContent.Contains(tableRow[0].ToLower()), Is.True, string.Format("Could not find error message {0}", tableRow[0]));
            }


            WebBrowser.Current.WaitForComplete();
        }

        private Element FindValidationErrorsList(string errorListClassName)
        {
            return WebBrowser.Current.Div(Find.ByClass(errorListClassName));
        }
    }
}

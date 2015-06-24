using System.Data.SqlClient;
using System.Text;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.AccidentRecords
{
    [Binding]
    public class InjuredPersonSteps : BaseSteps
    {
        [BeforeScenario(@"NeedsAccidentRecordToAddInjuredPersonDetailsTo")]
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

        [AfterScenario(@"NeedsAccidentRecordToAddInjuredPersonDetailsTo")]
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
    }
}

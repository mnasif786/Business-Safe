using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Data.SqlClient;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.AccidentRecords
{
    [Binding]
    public class NextStepsSteps : BaseSteps
    {
        [BeforeScenario(@"NeedsAccidentRecordThatDoesntShowNextSteps")]
        public void CreateAccidentRecordThatDoesntShowNextSteps()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                "[Id], [Title], [Reference], [JurisdictionId], [CompanyId], " +
                "[Deleted], [CreatedOn], [CreatedBy]) " +
                "VALUES (-2, 'Accident record for injured person tests', 'IP001', 1, 55881, " +
                "0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c');");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"NeedsAccidentRecordThatDoesntShowNextSteps")]
        public void DeleteAccidentRecordThatDoesntShowNextSteps()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[AccidentRecord] WHERE Id = -2;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [BeforeScenario(@"NeedsAccidentRecordThatDoesShowNextSteps")]
        public void CreateAccidentRecordThatDoesShowNextSteps()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] ON;");
            sql.Append("INSERT [dbo].[AccidentRecord] (" +
                "[Id], [Title], [Reference], [JurisdictionId], [CompanyId], " +
                "[SeverityOfInjuryId], [InjuredPersonWasTakenToHospital], [LengthOfTimeUnableToCarryOutWorkId], [InjuredPersonAbleToCarryOutWorkId], " +
                "[Deleted], [CreatedOn], [CreatedBy]) " +
                "SELECT -3, 'Accident record for injured person tests', 'IP001', 1, 55881, " +
                "1, 1, 1, 1, " +
                "0, '2020-01-01 09:00:00:000', N'16ac58fb-4ea4-4482-ac3d-000d607af67c' WHERE NOT EXISTS (SELECT * FROM dbo.AccidentRecord AS ar WHERE ar.Id = -3);");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[AccidentRecord] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"NeedsAccidentRecordThatDoesntShowNextSteps")]
        public void DeleteAccidentRecordThatDoesShowNextSteps()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[AccidentRecord] WHERE Id = -3;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }
    }
}

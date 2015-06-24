using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Data.SqlClient;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class SearchGeneralRiskAssessmentSteps : BaseSteps
    {
        [BeforeScenario(@"RequiresSearchGRAData")]
        public static void InsertSearchGraData()
        {
            var sql = new StringBuilder();
            sql.Append(
               "UPDATE [User] SET SiteId = 23 WHERE ClientId = 31028 ");

            sql.AppendLine(
                "DECLARE @InsertedId bigint ");

            sql.AppendLine(
                "INSERT INTO RiskAssessment ([Title], [Reference], [AssessmentDate], [ClientId], [CreatedOn], [CreatedBy], [SiteId], [StatusId]) " +
                "VALUES ('Search GRAs Test 01', 'SGRA01', '2012-06-01 10:00:00.000', 31028, '2012-05-01 10:00:00.000', 'e8735020-0be6-46ac-bb74-13908e75ccdb', 50, 1) ");

            sql.AppendLine(
                "SET @InsertedId = SCOPE_IDENTITY() ");

            sql.AppendLine(
                "INSERT INTO MultiHazardRiskAssessment ([Id], [Location], [TaskProcessDescription]) " +
                "VALUES (@InsertedId, 'Test location', 'Test process description') ");

            sql.AppendLine(
                "INSERT INTO GeneralRiskAssessment ([Id]) " +
                "VALUES (@InsertedId) ");

            sql.AppendLine(
                "INSERT INTO RiskAssessment ([Title], [Reference], [AssessmentDate], [ClientId], [CreatedOn], [CreatedBy], [SiteId], [StatusId]) " +
                "VALUES ('Search GRAs Test 02', 'SGRA02', '2012-06-02 10:00:00.000', 31028, '2012-05-02 10:00:00.000', 'e8735020-0be6-46ac-bb74-13908e75ccdb', 43, 1) ");

            sql.AppendLine(
                "SET @InsertedId = SCOPE_IDENTITY() ");

            sql.AppendLine(
                "INSERT INTO MultiHazardRiskAssessment ([Id], [Location], [TaskProcessDescription]) " +
                "VALUES (@InsertedId, 'Test location', 'Test process description') ");

            sql.AppendLine(
                "INSERT INTO GeneralRiskAssessment ([Id]) " +
                "VALUES (@InsertedId) ");

            sql.AppendLine(
                "INSERT INTO RiskAssessment ([Title], [Reference], [AssessmentDate], [ClientId], [CreatedOn], [CreatedBy], [SiteId], [StatusId]) " +
                "VALUES ('Search GRAs Test 03', 'SGRA03', '2012-06-03 10:00:00.000', 31028, '2012-05-03 10:00:00.000', 'e8735020-0be6-46ac-bb74-13908e75ccdb', 45, 1) ");

            sql.AppendLine(
                "SET @InsertedId = SCOPE_IDENTITY() ");

            sql.AppendLine(
                "INSERT INTO MultiHazardRiskAssessment ([Id], [Location], [TaskProcessDescription]) " +
                "VALUES (@InsertedId, 'Test location', 'Test process description') ");

            sql.AppendLine(
                "INSERT INTO GeneralRiskAssessment ([Id]) " +
                "VALUES (@InsertedId) ");

            sql.AppendLine(
                "INSERT INTO RiskAssessment ([Title], [Reference], [AssessmentDate], [ClientId], [CreatedOn], [CreatedBy], [SiteId], [StatusId]) " +
                "VALUES ('Search GRAs Test 04', 'SGRA04', '2012-06-04 10:00:00.000', 31028, '2012-05-04 10:00:00.000', 'e8735020-0be6-46ac-bb74-13908e75ccdb', 227, 1) ");

            sql.AppendLine(
                "SET @InsertedId = SCOPE_IDENTITY() ");

            sql.AppendLine(
                "INSERT INTO MultiHazardRiskAssessment ([Id], [Location], [TaskProcessDescription]) " +
                "VALUES (@InsertedId, 'Test location', 'Test process description') ");

            sql.AppendLine(
                "INSERT INTO GeneralRiskAssessment ([Id]) " +
                "VALUES (@InsertedId) ");

            sql.AppendLine(
                "INSERT INTO RiskAssessment ([Title], [Reference], [AssessmentDate], [ClientId], [CreatedOn], [CreatedBy], [SiteId], [StatusId]) " +
                "VALUES ('Search GRAs Test 05', 'SGRA05', '2012-06-05 10:00:00.000', 31028, '2012-05-05 10:00:00.000', 'e8735020-0be6-46ac-bb74-13908e75ccdb', 180, 1) ");

            sql.AppendLine(
                "SET @InsertedId = SCOPE_IDENTITY() ");

            sql.AppendLine(
                "INSERT INTO MultiHazardRiskAssessment ([Id], [Location], [TaskProcessDescription]) " +
                "VALUES (@InsertedId, 'Test location', 'Test process description') ");

            sql.AppendLine(
                "INSERT INTO GeneralRiskAssessment ([Id]) " +
                "VALUES (@InsertedId) ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"RequiresSearchGRAData")]
        public void ResetUserPermissions()
        {
            var sql = new StringBuilder();
            sql.Append(
                "UPDATE [User] SET PermissionsApplyToAllSites = 0 WHERE ClientId = 31028 ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
            //    runSQLCommand(sql.ToString(), conn);
            }
        }
    }
}

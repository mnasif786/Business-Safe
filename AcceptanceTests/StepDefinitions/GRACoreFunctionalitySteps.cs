using System.Data.SqlClient;
using System.Text;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class GRACoreFunctionalitySteps : BaseSteps
    {
        
        [BeforeScenario(@"GRA_core_functionality")]
        [BeforeFeature(@"GRA_core_functionality")]
        [AfterScenario(@"GRA_core_functionality")]
        [AfterFeature(@"GRA_core_functionality")]
        public static void DeleteRowsCreatedInTests()
        {
            BaseSteps.DeleteRowsCreatedInTests();
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM GeneralRiskAssessment Where Id > " + LastRiskAssessmentId);

            sql.AppendLine("DELETE FROM RiskAssessmentEmployee WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM RiskAssessmentsNonEmployees WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM RiskAssessmentPeopleAtRisk WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM MultiHazardRiskAssessmentHazard WHERE RiskAssessmentId > " + LastRiskAssessmentId);

            sql.AppendLine("DELETE FROM RiskAssessmentReview WHERE RiskAssessmentId > " + LastRiskAssessmentId);

            sql.AppendLine("DELETE FROM [Document] WHERE Id IN ( SELECT [Id] FROM [BusinessSafe].[dbo].[RiskAssessmentDocument] WHERE RiskAssessmentId > " + LastRiskAssessmentId + ")");
            sql.AppendLine("DELETE FROM RiskAssessmentDocument WHERE RiskAssessmentId > " + LastRiskAssessmentId);

            sql.AppendLine("DELETE FROM [Task] WHERE MultiHazardRiskAssessmentHazardId IN ( SELECT [RiskAssessmentId] FROM [BusinessSafe].[dbo].[MultiHazardRiskAssessmentHazard] WHERE RiskAssessmentId > " + LastRiskAssessmentId + ")");
            
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        [BeforeScenario(@"requiresNonEmployeeDaveSmith")]
        public void CreateNonEmployeeDaveSmith()
        {
            var sql = new StringBuilder();
            sql.AppendLine("INSERT INTO [dbo].[NonEmployee] ([Name],[Company],[Position],[ClientId],[RiskAssessmentId],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy]) VALUES('Dave Smith','','',55881,NULL,0,getdate(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C',NULL,NULL)");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        [AfterScenario(@"requiresNonEmployeeDaveSmith")]
        public void DeleteNonEmployeeDaveSmith()
        {
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM [dbo].[NonEmployee] WHERE Name = 'Dave Smith'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }
    }
}
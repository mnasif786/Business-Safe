using System.Data.SqlClient;
using System.Text;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class PRACoreFunctionalitySteps : BaseSteps
    {

        [BeforeScenario(@"PRA_core_functionality")]
        [BeforeFeature(@"PRA_core_functionality")]
        [AfterScenario(@"PRA_core_functionality")]
        [AfterFeature(@"PRA_core_functionality")]
        public static void DeleteRowsCreatedInTests()
        {
            var sql = new StringBuilder();
            // delete any new Personal Risk Assessments
            sql.AppendLine("DELETE FROM PersonalRiskAssessment Where Id > 59 ");
            sql.AppendLine("DELETE FROM MultiHazardRiskAssessment Where Id > 59 ");
            sql.AppendLine("DELETE FROM RiskAssessment Where Id > 59 ");

            // amend/delete any additions relating PRA under test
            sql.AppendLine("UPDATE RiskAssessment SET [Title] = 'Core Functionality Personal Risk Assessment' WHERE Id = 56");
            sql.AppendLine("UPDATE RiskAssessment SET [AssessmentDate] = NULL WHERE Id = 56");
            sql.AppendLine("UPDATE RiskAssessment SET [SiteId] = 378 WHERE Id = 56");
            sql.AppendLine("UPDATE RiskAssessment SET [RiskAssessorEmployeeId] = NULL WHERE Id = 56");
            sql.AppendLine("UPDATE RiskAssessment SET [RiskAssessorId] = NULL WHERE Id = 56");
            sql.AppendLine("DELETE FROM RiskAssessmentReview WHERE RiskAssessmentId = 56");
            sql.AppendLine("DELETE FROM MultiHazardRiskAssessmentHazard WHERE RiskAssessmentId = 56");
            sql.AppendLine("DELETE FROM [BusinessSafe].[dbo].[Document] WHERE Id IN ( SELECT [Id] FROM [BusinessSafe].[dbo].[RiskAssessmentDocument] WHERE RiskAssessmentId= 56 )");
            
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }
    }
}
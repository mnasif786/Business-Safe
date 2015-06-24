using System.Data.SqlClient;
using System.Text;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class FRACoreFunctionalitySteps : BaseSteps
    {
        
        [BeforeScenario(@"FRA_core_functionality")]
        [BeforeFeature(@"FRA_core_functionality")]
        [AfterScenario(@"FRA_core_functionality")]
        [AfterFeature(@"FRA_core_functionality")]
        public static void DeleteRowsCreatedInTests()
        {
            BaseSteps.DeleteRowsCreatedInTests();
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM FireRiskAssessment Where Id > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM Document WHERE Id > 9 ");
            sql.AppendLine("DELETE FROM Hazard WHERE Id > 24");
            sql.AppendLine("DELETE FROM PeopleAtRisk WHERE Id > 9");

            sql.AppendLine("DELETE FROM RiskAssessmentEmployee WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM RiskAssessmentsNonEmployees WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM RiskAssessmentPeopleAtRisk WHERE RiskAssessmentId > " + LastRiskAssessmentId);

            sql.AppendLine("DELETE FROM SignificantFinding WHERE FireAnswerId IN (SELECT Id FROM Answer WHERE FireRiskAssessmentChecklistId IN (SELECT Id FROM FireRiskAssessmentChecklist WHERE FireRiskAssessmentId > " + LastRiskAssessmentId + "))");
            sql.AppendLine("DELETE FROM Answer WHERE FireRiskAssessmentChecklistId IN (SELECT Id FROM FireRiskAssessmentChecklist WHERE FireRiskAssessmentId > " + LastRiskAssessmentId + ")");

            sql.AppendLine("DELETE FROM FireRiskAssessmentSourceOfIgnitions WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM FireRiskAssessmentFireSafetlyControlMeasures WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM FireRiskAssessmentSourceOfFuels WHERE RiskAssessmentId > " + LastRiskAssessmentId); ;
            sql.AppendLine("DELETE FROM RiskAssessmentPeopleAtRisk WHERE RiskAssessmentId > " + LastRiskAssessmentId);

            sql.AppendLine("DELETE FROM SourceOfIgnition WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM FireSafetyControlMeasure WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM SourceOfFuel WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM PeopleAtRisk WHERE RiskAssessmentId > " + LastRiskAssessmentId);

            sql.AppendLine("DELETE FROM SignificantFinding");
            sql.AppendLine("DELETE FROM RiskAssessmentReview WHERE RiskAssessmentId > " + LastRiskAssessmentId);
            sql.AppendLine("DELETE FROM [Document] WHERE Id IN ( SELECT [Id] FROM [BusinessSafe].[dbo].[RiskAssessmentDocument] WHERE RiskAssessmentId > " + LastRiskAssessmentId + ")");
            sql.AppendLine("DELETE FROM RiskAssessmentDocument WHERE RiskAssessmentId = 55");

            sql.AppendLine("DELETE FROM [Task] WHERE MultiHazardRiskAssessmentHazardId IN ( SELECT [RiskAssessmentId] FROM [BusinessSafe].[dbo].[MultiHazardRiskAssessmentHazard] WHERE RiskAssessmentId > " + LastRiskAssessmentId + ")");

            sql.AppendLine("DELETE FROM MultiHazardRiskAssessmentHazard Where RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM PeopleAtRisk WHERE RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM RiskAssessmentPeopleAtRisk WHERE RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM dbo.FireRiskAssessmentFireSafetlyControlMeasures WHERE RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM SourceOfFuel WHERE RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM PeopleAtRisk WHERE RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM FireRiskAssessmentSourceOfIgnitions WHERE RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM FireRiskAssessmentFireSafetlyControlMeasures WHERE RiskAssessmentId = 55");
            sql.AppendLine("DELETE FROM FireRiskAssessmentSourceOfFuels WHERE RiskAssessmentId = 55"); ;
            sql.AppendLine("DELETE FROM RiskAssessmentPeopleAtRisk WHERE RiskAssessmentId = 55");
            
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Globalization;
using System.Data.SqlClient;
using BusinessSafe.AcceptanceTests.StepHelpers;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment
{
    [Binding]
    public class RemoveFurtherControlMeasureTask : BaseSteps
    {

        [Given(@"there is an hsra roccurring task with previous completed task for risk assessment '(.*)'")]
        public void AndThereIsAnHsraReoccurringTaskWithPreviousCompletedTaskForRiskAssessment(long riskAssessmentId)
        {
            var sql = new StringBuilder();


            sql.Append("INSERT INTO [Task] (");
            sql.Append("[Title], ");
            sql.Append("[Description], ");
            sql.Append("[Reference], ");
            sql.Append("[Deleted], ");
            sql.Append("[CreatedOn], ");
            sql.Append("[CreatedBy], ");
            sql.Append("[TaskAssignedToId], ");
            sql.Append("[TaskCompletionDueDate], ");
            sql.Append("[TaskStatusId], ");
            sql.Append("[TaskCategoryId], ");
            sql.Append("[TaskReoccurringTypeId], ");
            sql.Append("[TaskReoccurringEndDate], ");
            sql.Append("[Discriminator], ");
            sql.Append("[HazardousSubstanceRiskAssessmentId], ");
            sql.Append("[TaskGuid] )");
            sql.Append("VALUES (");
            //sql.Append("900, ");
            sql.Append("'Reoccurring task with completed tasks', ");
            sql.Append("'Reoccurring task with completed tasks', ");
            sql.Append("'RTCT02', ");
            sql.Append("0, ");
            sql.Append("'2012-12-13 11:15:00.000', ");
            sql.Append("'16ac58fb-4ea4-4482-ac3d-000d607af67c', ");
            sql.Append("'3ece3fd2-db29-4abd-a812-fcc6b8e621a1', ");
            sql.Append("'2012-12-20 11:15:00.000', ");
            sql.Append("0, ");
            sql.Append("6, ");
            sql.Append("1, ");
            sql.Append("'2050-01-01 10:00:00.000', ");
            sql.Append("'HazardousSubstanceRiskAssessmentFurtherControlMeasureTask', ");
            sql.AppendLine(riskAssessmentId.ToString(CultureInfo.InvariantCulture) + ", ");
            sql.AppendLine("'" + Guid.NewGuid().ToString() + "')");


            sql.Append("INSERT INTO [Task] (");
            sql.Append("[Title], ");
            sql.Append("[Description], ");
            sql.Append("[Reference], ");
            sql.Append("[Deleted], ");
            sql.Append("[CreatedOn], ");
            sql.Append("[CreatedBy], ");
            sql.Append("[TaskAssignedToId], ");
            sql.Append("[TaskCompletionDueDate], ");
            sql.Append("[TaskStatusId], ");
            sql.Append("[TaskCategoryId], ");
            sql.Append("[TaskReoccurringTypeId], ");
            sql.Append("[TaskReoccurringEndDate], ");
            sql.Append("[FollowingTaskId], ");
            sql.Append("[Discriminator], ");
            sql.Append("[HazardousSubstanceRiskAssessmentId], ");
            sql.Append("[TaskGuid] )");
            sql.Append("VALUES (");
            //sql.Append("900, ");
            sql.Append("'Reoccurring task with completed tasks', ");
            sql.Append("'Reoccurring task with completed tasks', ");
            sql.Append("'RTCT01', ");
            sql.Append("0, ");
            sql.Append("'2012-12-13 11:15:00.000', ");
            sql.Append("'16ac58fb-4ea4-4482-ac3d-000d607af67c', ");
            sql.Append("'3ece3fd2-db29-4abd-a812-fcc6b8e621a1', ");
            sql.Append("'2012-12-20 11:15:00.000', ");
            sql.Append("1, ");
            sql.Append("6, ");
            sql.Append("1, ");
            sql.Append("'2050-01-01 10:00:00.000', ");
            sql.Append("(SELECT SCOPE_IDENTITY()), ");
            sql.Append("'HazardousSubstanceRiskAssessmentFurtherControlMeasureTask', ");
            sql.AppendLine(riskAssessmentId.ToString(CultureInfo.InvariantCulture) + ", ");
            sql.AppendLine("'" + Guid.NewGuid().ToString() + "')");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);

            }
        }
    }
}

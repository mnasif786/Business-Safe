using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment
{
    [Binding]
    public class MarkFurtherControlMeasureAsNoLongerRequiredSteps : BaseSteps
    {
        [Given(@"A reoccuring further control measure task exisits for hazardous substance risk assessment '(.*)'")]
        public void GivenAReoccuringFurtherControlMeasureTasksExisitsForCompanyAndHazardousSubstanceRiskAssessment(long riskAssessmentId)
        {
            var sql = new StringBuilder();

            //sql.AppendLine("SET IDENTITY_INSERT [Task] ON ");
            sql.Append("INSERT INTO [Task] (");
            //sql.Append("[Id], ");
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
            sql.Append("[TaskGuid],  ");
            sql.Append("[SendTaskNotification],  ");
            sql.Append("[SendTaskCompletedNotification],  ");
            sql.Append("[SendTaskOverdueNotification] ) ");
            sql.Append("VALUES (");
            //sql.Append("900, ");
            sql.Append("'Task To Mark As No Longer Required', ");
            sql.Append("'No Longer Required Test', ");
            sql.Append("'No Longer Required Test', ");
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
            sql.Append(riskAssessmentId.ToString(CultureInfo.InvariantCulture) + ", ");
            sql.Append("'" + Guid.NewGuid().ToString() +"',");
            sql.Append("0, ");
            sql.Append("0, ");
            sql.Append("0) ");
            //sql.AppendLine("SET IDENTITY_INSERT [Task] OFF ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);

            }
        }

        [Given(@"I select confirm mark as no longer required")]
        [When(@"I select confirm mark as no longer required")]
        public void WhenISelectConfirmMarkAsNoLongerRequired()
        {
            System.Threading.Thread.Sleep(1000);

            var div =
                WebBrowser.Current.Div(Find.ByClass("ui-dialog ui-widget ui-widget-content ui-corner-all ui-draggable"));

            var button = div.Buttons[0];
            button.Click();
        }

        [Then(@"the further action task with title '(.*)' should be no longer required")]
        public void TheFurtherActionTaskWithTitleShouldBeNoLongerRequired(string title)
        {
            Thread.Sleep(2000);

            foreach (var row in WebBrowser.Current.TableRows.ToList())
            {
                if (row.TableCells.Count() > 2)
                {
                    if (row.TableCells[1].Text.Trim() == title.Trim())
                    {
                        Assert.That(row.TableCells[6].Text.Trim() == "No Longer Required");
                        return;
                    }
                }
            }
        }
    }
}

using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using TableRow = WatiN.Core.TableRow;
using System.Globalization;
using System;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class RemoveFurtherActionTaskSteps : BaseSteps
    {
        [BeforeScenario("RemoveFurtherActionTask")]
        [AfterScenario("RemoveFurtherActionTask")]
        public static void ResetFurtherActionTask()
        {
            var sql = new StringBuilder();
            sql.Append("Update Task ");
            sql.Append("Set Deleted = 0, TaskStatusId = 0 ");
            sql.Append("Where Id = '11' or Id = '12' or Id = '20'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }
        }

        [Given(@"I am on the risk assessment index page for company '(.*)'")]
        public void GivenIAmOnTheRiskAssessmentIndexPageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate("GeneralRiskAssessments/RiskAssessment?companyId=" + companyId);
        }

        [Given(@"I have clicked on the edit risk assessment link for id '(.*)'")]
        [When(@"I have clicked on the edit risk assessment link for id '(.*)'")]
        public void GivenIHaveClickedOnTheEditRiskAssessmentLinkForId39(int riskAssessmentId)
        {
            Thread.Sleep(2000);
            var allEditLinks = WebBrowser.Current.Links.Filter(Find.ByClass("icon-edit")).ToList();
            var editLink = allEditLinks.Single(x => int.Parse(x.GetAttributeValue("data-id")) == riskAssessmentId);
            editLink.Click();
        }

        [Given(@"I have clicked on the view risk assessment link for id '(.*)'")]
        [When(@"I have clicked on the view risk assessment link for id '(.*)'")]
        public void GivenIHaveClickedOnTheViewRiskAssessmentLinkForId39(int riskAssessmentId)
        {
            Thread.Sleep(2000);
            var allLinks = WebBrowser.Current.Links.Where(x => x.Title == "View Risk Assessment").ToList();
            var link = allLinks.Single(x => int.Parse(x.GetAttributeValue("data-id")) == riskAssessmentId);
            link.Click();
        }

        [Given(@"I click on the further action task row for id '(.*)'")]
        [When(@"I click on the further action task row for id '(.*)'")]
        public void GivenIClickOnTheFurtherActionTaskRowForId11(int furtherActionTaskId)
        {
            Thread.Sleep(2000);
            var furtherActionTaskRow = WebBrowser.Current.TableRows.Single(x => HasDataIdAttribute(furtherActionTaskId, x));
            furtherActionTaskRow.TableCells.ElementAt(1).Click();
            ScenarioContextHelpers.SetFurtherActionTaskRow(furtherActionTaskRow);
        }



        private static bool HasDataIdAttribute(int furtherActionTaskId, TableRow x)
        {
            return x.GetAttributeValue("data-id") != null && int.Parse(x.GetAttributeValue("data-id")) == furtherActionTaskId;
        }

        [When(@"I press 'Remove' button on the further action task row")]
        public void WhenIPressRemoveButtonOnTheFurtherActionTaskRow()
        {
            Thread.Sleep(2000);
            var furtherActionTaskRow = ScenarioContextHelpers.GetFurtherActionTaskRow();

            var button = furtherActionTaskRow.Elements.Filter(Find.ByClass("btn btn-danger btn-remove-further-action-task")).FirstOrDefault();

            Assert.IsNotNull(button, "Could not find remove button");

            button.Click();


        }

        [When(@"I press 'Edit' button on the further action task row")]
        public void WhenIPressEditButtonOnTheFurtherActionTaskRow()
        {
            Thread.Sleep(2000);
            var furtherActionTaskRow = ScenarioContextHelpers.GetFurtherActionTaskRow();

            var button = furtherActionTaskRow.Elements.Filter(Find.ByClass("btn btn-edit-further-action-task")).FirstOrDefault();

            Assert.IsNotNull(button, "Could not find edit button");

            button.Click();


        }

        [Then(@"the further action task with id '(.*)' should be deleted")]
        public void ThenTheFurtherActionTaskWithId11ShouldBeDeleted(int furtherActionTaskId)
        {
            Thread.Sleep(4000);

            var sql = new StringBuilder();
            sql.Append("Select * From Task ");
            sql.Append("Where Id = '" + furtherActionTaskId + "' And Deleted = 1 ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    using (var sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        Assert.True(sqlDataReader.HasRows);
                    }
                }
            }
        }

        [Given(@"there is a gra roccurring task with previous completed task for risk assessment hazard '(.*)'")]
        public void AndThereIsAGraReoccurringTaskWithPreviousCompletedTaskForRiskAssessmentHazard(long riskAssessmentHazardId)
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
            sql.Append("[MultiHazardRiskAssessmentHazardId], ");
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
            sql.Append("'MultiHazardRiskAssessmentFurtherControlMeasureTask', ");
            sql.Append(riskAssessmentHazardId.ToString(CultureInfo.InvariantCulture) + ", ");
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
            sql.Append("[MultiHazardRiskAssessmentHazardId], ");
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
            sql.Append("'MultiHazardRiskAssessmentFurtherControlMeasureTask', ");
            sql.AppendLine(riskAssessmentHazardId.ToString(CultureInfo.InvariantCulture) + ", ");
            sql.AppendLine("'" + Guid.NewGuid().ToString() + "')");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);

            }
        }

        [AfterScenario("changesTaskTitleAndDescriptionForTask18")]
        public void ResetTaskEighteen()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                var sql = new StringBuilder("UPDATE Task");
                sql.Append(" SET Title = 'Edit Task Test', Description = 'Edit Task Test' WHERE Id = 18");
                conn.Open();

                runSQLCommand(sql.ToString(), conn);
            }
        }
    }
}

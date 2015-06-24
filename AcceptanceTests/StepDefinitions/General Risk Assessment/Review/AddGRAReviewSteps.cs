using System;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using NUnit.Framework;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment.Review
{
    [Binding]
    public class AddGRraReviewSteps : BaseSteps
    {
        [AfterScenario("createsARiskAssessmentReviewRow")]
        public void TearDown()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var totalRows = (int)runSQLCommand("SELECT COUNT(id) FROM [dbo].[Task] WHERE [Discriminator] = 'RiskAssessmentReviewTask'", conn);
                for (var i = 0; i < totalRows; i++)
                {
                    var taskId = runSQLCommand("SELECT id FROM [dbo].[Task] WHERE [Discriminator] = 'RiskAssessmentReviewTask'", conn);
                    var sql = new StringBuilder();
                    sql.Append("DELETE FROM [TaskDocument] WHERE [TaskId] = " + taskId);
                    sql.Append("DELETE FROM [Task] WHERE [Id] = " + taskId);
                    runSQLCommand(sql.ToString(), conn);
                }
                runSQLCommand("DELETE FROM [dbo].[RiskAssessmentReview]", conn);
            }
        }

        [Then(@"the add review icon should be displayed")]
        public void ThenTheAddReviewIconShouldBeDisplayed()
        {
            var addReviewLink = WebBrowser.Current.Link(Find.ById("AddReview"));

            Assert.That(addReviewLink, Is.Not.Null);
            Assert.That(addReviewLink.Style.Display, Is.Not.EqualTo("none"));
        }

        [Then(@"the review should be saved")]
        public void ThenTheReviewShouldBeSaved()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var sql = "SELECT COUNT(id) FROM RiskAssessmentReview";
                using (var command = new SqlCommand(sql, conn))
                {
                    var result = command.ExecuteScalar();
                    Assert.That(result, Is.EqualTo(1));
                }
            }
        }

        [Then(@"the review should be completed")]
        public void ThenTheReviewShouldBeCompleted()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var sql = "SELECT COUNT(id) FROM RiskAssessmentReview Where CompletedDate IS NOT NULL";
                using (var command = new SqlCommand(sql, conn))
                {
                    var result = command.ExecuteScalar();
                    Assert.That(result, Is.EqualTo(1));
                }
            }
        }

        [Then(@"a success message should be displayed")]
        public void ThenASuccessMessageShouldBeDisplayed()
        {
            var alert = WebBrowser.Current.Div(Find.ById("TempDataAlert"));

            Assert.That(alert, Is.Not.Null);
            Assert.That(alert.Style.Display, Is.Not.EqualTo("none"));
        }

        [Then(@"RiskAssessmentReviewTask '(.*)' should not exist in task list")]
        public void ThenRiskAssessmentReviewTaskShouldNotExistInTaskList(string taskReviewName)
        {
            Assert.IsFalse(GraReviewRowPresent(taskReviewName));
        }

        [Then(@"RiskAssessmentReviewTask '(.*)' should exist in task list")]
        public void RiskAssessmentReviewTaskShouldExistInTaskList(string taskReviewName)
        {
            Assert.IsTrue(GraReviewRowPresent(taskReviewName));
        }

        [When(@"complete review task is clicked for task with description '(.*)'")]
        public void WhenCompleteReviewTaskIsClickedForTaskWithDescriptionGRAReview(string description)
        {
            var tableDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));

            Assert.IsTrue(tableDiv.Tables.Any(), "Could not find the task list table");
            
            var table = tableDiv.Tables[0];
            var row = table.TableRows.First(t => t.TableCells[4].Text == description);

            foreach (Element image in row.Elements)
            {
                if (image.ClassName != null && image.ClassName.Contains("icon-ok"))
                {
                    image.Click();

                    break;
                }
            }
        }

        [Then(@"I should be redirected to the general risk assessment page")]
        public void ThenIShouldBeRedirectedToTheGeneralRiskAssessmentPage()
        {
            const string expectedUrl = "/generalriskassessments/summary?";

            Assert.That(WebBrowser.Current.Url.ToLower(), Contains.Substring(expectedUrl));
        }

        private bool GraReviewRowPresent(string taskTitle)
        {
            var taskListDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            var taskListTable = taskListDiv.Tables[0];

            foreach (WatiN.Core.TableRow currentRow in taskListTable.TableRows)
            {
                foreach (TableCell currentCell in currentRow.TableCells)
                {
                    if (currentCell.Text == taskTitle)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        [When(@"I press 'EditButton' for the only risk assessment review")]
        public void WhenIPressEditButtonForThOnlyRiskAssessmentReview()
        {
            var div = WebBrowser.Current.Div(Find.ById("RiskAssessmentReviewsGrid"));
            var table = div.Tables[0];
            var row = table.TableRows[0];
            var cell = row.TableCells[4];
            var link = cell.Link(Find.ByClass("editRiskAssessmentReview icon-edit"));
            link.Click();
        }

        [Then(@"the editied review should be saved")]
        public void TheEditieReviewShouldBeSaved()
        {
            var div = WebBrowser.Current.Div(Find.ById("RiskAssessmentReviewsGrid"));
            var table = div.Tables[0];
            var row = table.TableRows[0];
            var dueDateCell = row.TableCells[0];
            var assignedToCell = row.TableCells[1];
            Assert.That(dueDateCell.Text == "27/09/2024");
            Assert.That(assignedToCell.Text == "Glen Ross");
        }
    }
}
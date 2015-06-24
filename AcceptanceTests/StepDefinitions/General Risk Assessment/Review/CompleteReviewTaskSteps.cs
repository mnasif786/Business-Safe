using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;
using System.Threading;
using TableRow = WatiN.Core.TableRow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.GeneralRiskAssessment.Review
{
    [Binding]
    public class CompleteReviewTaskSteps : BaseSteps
    {
        [BeforeScenario("requiresRiskAssessmentReviewSettingUp")]
        public void Setup()
        {
            setupReviewAndTaskForEmployee();
        }

        [BeforeScenario("requiresRiskAssessmentReviewAssignedToKimHowardSettingUp")]
        public void SetupForKimHoward()
        {
            setupReviewAndTaskForEmployee("A433E9B2-84F6-4AD7-A89C-050E914DFF01");
        }

        [BeforeScenario("requiresRiskAssessmentReviewAssignedToRussellWilliamsSettingUp")]
        public void SetupForRussellWilliams()
        {
            setupReviewAndTaskForEmployee("D2122FFF-1DCD-4A3C-83AE-E3503B394EB4");
        }

        /// <summary>
        /// creates risk assessment review and associated task assigned to supplied employee id
        /// </summary>
        /// <param name="employeeId">defaults to 8929BAC1-5403-4837-A72F-A077AA0C4E81 'John Conner'</param>
        private void setupReviewAndTaskForEmployee(string employeeId = "8929BAC1-5403-4837-A72F-A077AA0C4E81")
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                Console.WriteLine(
                    "Creating Task and Review for scenarios with 'requiresRiskAssessmentReviewSettingUp'");
                conn.Open();
                var sql = new StringBuilder();
                sql.AppendLine("INSERT INTO [BusinessSafe].[dbo].[RiskAssessmentReview]");
                sql.AppendLine("VALUES");
                sql.AppendLine("(39");
                sql.AppendLine(",null");
                sql.AppendLine(",0");
                sql.AppendLine(",getdate()");
                sql.AppendLine(",'16AC58FB-4EA4-4482-AC3D-000D607AF67C'");
                sql.AppendLine(",null");
                sql.AppendLine(",null");
                sql.AppendLine(",dateadd(year, 1, getdate())");
                sql.AppendLine(",'" + employeeId + "'");
                sql.AppendLine(",null");
                sql.AppendLine(",null);");
                sql.AppendLine("select scope_identity();");
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    var newReviewId = command.ExecuteScalar();

                    sql = new StringBuilder();
                    sql.AppendLine("INSERT INTO [BusinessSafe].[dbo].[Task]");
                    sql.AppendLine(" (");
                    sql.AppendLine("MultiHazardRiskAssessmentHazardId, ");
                    sql.AppendLine("Title, ");
                    sql.AppendLine("Description, ");
                    sql.AppendLine("Reference, ");
                    sql.AppendLine("Deleted, ");
                    sql.AppendLine("CreatedOn, ");
                    sql.AppendLine("CreatedBy, ");
                    sql.AppendLine("LastModifiedOn, ");
                    sql.AppendLine("LastModifiedBy, ");
                    sql.AppendLine("TaskAssignedToId, ");
                    sql.AppendLine("TaskCompletionDueDate, ");
                    sql.AppendLine("TaskStatusId, ");
                    sql.AppendLine("TaskCompletedDate, ");
                    sql.AppendLine("TaskCompletedComments, ");
                    sql.AppendLine("TaskCategoryId, ");
                    sql.AppendLine("TaskReoccurringTypeId, ");
                    sql.AppendLine("TaskReoccurringEndDate, ");
                    sql.AppendLine("FollowingTaskId, ");
                    sql.AppendLine("OriginalTaskId, ");
                    sql.AppendLine("Discriminator, ");
                    sql.AppendLine("RiskAssessmentReviewId, ");
                    sql.AppendLine("HazardousSubstanceRiskAssessmentId, ");
                    sql.AppendLine("TaskGuid, ");
                    sql.AppendLine("SendTaskNotification, ");
                    sql.AppendLine("SendTaskCompletedNotification, ");
                    sql.AppendLine("SendTaskOverdueNotification ");
                    sql.AppendLine(") ");
                    sql.AppendLine("VALUES");
                    sql.AppendLine("(null");
                    sql.AppendLine(",'RiskAssessmentReview_Creation_01'");
                    sql.AppendLine(",'GRA Review'");
                    sql.AppendLine(",'RiskAssessmentReview_Creation_01'");
                    sql.AppendLine(",0");
                    sql.AppendLine(",getdate()");
                    sql.AppendLine(",'16AC58FB-4EA4-4482-AC3D-000D607AF67C'");
                    sql.AppendLine(",null");
                    sql.AppendLine(",null");
                    sql.AppendLine(",'" + employeeId + "'");
                    sql.AppendLine(",dateadd(year, 1, getdate())");
                    sql.AppendLine(",0");
                    sql.AppendLine(",null");
                    sql.AppendLine(",null");
                    sql.AppendLine(",3");
                    sql.AppendLine(",0");
                    sql.AppendLine(",null");
                    sql.AppendLine(",null");
                    sql.AppendLine(",null");
                    sql.AppendLine(",'RiskAssessmentReviewTask'");
                    sql.AppendLine("," + newReviewId + "");
                    sql.AppendLine(",null");
                    sql.AppendLine(",'" + Guid.NewGuid() + "'");
                    sql.AppendLine(",0");
                    sql.AppendLine(",0");
                    sql.AppendLine(",0)");

                    using (var taskCommand = new SqlCommand(sql.ToString(), conn))
                    {
                        taskCommand.ExecuteScalar();
                    }
                }
            }
        }

        [AfterScenario("altersTaskStatusOfRiskAssessment39")]
        public void ResetStatusesForRiskAssessment39AndTasks()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var sql = new StringBuilder("UPDATE [Task] SET [TaskStatusId] = 0 WHERE id = 11 ");
                sql.AppendLine("UPDATE [Task] SET [TaskStatusId] = 0 WHERE id = 12 ");
                sql.AppendLine("UPDATE [Task] SET [TaskStatusId] = 0 WHERE id = 13 ");
                sql.AppendLine("UPDATE [Task] SET [TaskStatusId] = 1 WHERE id = 14 ");
                sql.AppendLine("UPDATE [Task] SET [TaskStatusId] = 0 WHERE id = 15 ");
                sql.AppendLine("UPDATE [RiskAssessment] SET [StatusId] = 1 WHERE id = 39 ");

                runSQLCommand(sql.ToString(), conn);
            }
        }

        [Given(@"I have clicked on the complete review link for a review")]
        [When(@"I have clicked on the complete review link for a review")]
        public void WhenIHaveClickedOnTheCompleteReviewLinkForAReview()
        {
            Thread.Sleep(4000);
            var tableDiv = WebBrowser.Current.Div(Find.ById("RiskAssessmentReviewsGrid"));
            var table = tableDiv.Tables[0];
            var row = table.TableRows.FirstOrDefault();
            //var row = table.TableRows.FirstOrDefault(t => t.TableCells[1].Text == "John Conner");
            var link = row.Link(Find.ByTitle("Complete Review"));
            var taskId = link.GetAttributeValue("data-id");

            foreach (Element image in row.Elements)
            {
                if (image.ClassName != null && image.ClassName.Contains("icon-check"))
                {
                    if (image.Parent.GetAttributeValue("data-id") == taskId)
                    {
                        image.Click();
                        Thread.Sleep(4000);
                        break;
                    }
                }
            }
        }

        [When(@"I click the button with id '(.*)'")]
        public void WhenIClickTheButtonWithId(string buttonId)
        {
            Button button = WebBrowser.Current.Buttons.Filter(Find.ById(buttonId)).FirstOrDefault();
            button.Click();
        }



        [Then(@"the review is saved as complete")]
        public void ThenTheReviewIsSavedAsComplete()
        {
            var riskAssessmentId = GetCurrentRiskAssessmentId();
            var sql = new StringBuilder();
            sql.Append(
                "Select COUNT(T.ID) From dbo.RiskAssessmentReview RAR INNER JOIN dbo.Task T ON RAR.ID = T.RiskAssessmentReviewId ");
            sql.Append("WHERE RiskAssessmentId = '" + riskAssessmentId + "' AND RAR.Comments = 'hello world' ");
            sql.Append("AND CONVERT(VARCHAR(10),RAR.CompletedDate,111) = CONVERT(VARCHAR(10),T.TaskCompletedDate,111)"); // task completed date is now in offset format


            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    var result = command.ExecuteScalar();
                    Assert.That(result, Is.EqualTo(1));
                }
            }
        }

        [Then(@"the next review is created")]
        public void ThenTheNextReviewIsCreated()
        {
            Thread.Sleep(4000);
            var riskAssessmentReviewsGrid = WebBrowser.Current.Div(Find.ById("RiskAssessmentReviewsGrid"));
            var riskAssessmentReviewsTable = riskAssessmentReviewsGrid.Tables[0];
            var newRiskAssessmentReviewRow = riskAssessmentReviewsTable.TableRows[0];
            Assert.That(newRiskAssessmentReviewRow.TableCells[0].Text, Is.EqualTo("01/01/2050"));
        }

        [Then(@"validation error is displayed warning archive must be checked or next review date must be entered")]
        public void validationErrorIsDisplayedWarningArchiveMustBeCheckedOrNextReviewDateMustBeEntered()
        {
            Thread.Sleep(4000);
            var validationDiv = WebBrowser.Current.Div(Find.ByClass("validation-summary-errors"));
            Assert.That(validationDiv, Is.Not.Null);
            var ul = validationDiv.Elements[0];
            Assert.That(ul.OuterText, Is.EqualTo("Either a valid Next Review Date must be entered or Archive must be checked."));
        }

        [Then(@"RiskAssessmentReviewTask '(.*)' status should be '(.*)'")]
        public void ThenRiskAssessmentReviewTaskStatusShouldBe(string taskTitle, string expectedStatus)
        {
            var graReviewRowFound = FindTableRowWithCellContents(taskTitle, expectedStatus) != null;
            Assert.IsTrue(graReviewRowFound, "review task status is not '" + taskTitle + "'");
        }

        private TableRow FindTableRowWithCellContents(string taskReference, string expectedStatus)
        {
            var taskListDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            var taskListTable = taskListDiv.Tables[0];

            for (var i = 0; i < taskListTable.TableRows.Count(); i++)
            {
                const int taskReferenceColumnIndex = 1;
                const int taskStatusColumnIndex = 8;

                if (taskListTable.TableRows[i].TableCells[taskReferenceColumnIndex].Text == taskReference && 
                    taskListTable.TableRows[i].TableCells[taskStatusColumnIndex].Text == expectedStatus)
                {
                    return taskListTable.TableRows[i];

                }

            }
            Assert.Fail("Could not find table row with cell contents of: '" + taskReference + "'");
            return null;
        }

        [Then(@"risk assessment should have a status of archived")]
        public void ThenRiskAssessmentShouldHaveAStatusOfArchived()
        {
            var span = WebBrowser.Current.Span(Find.ByClass("label label-info archived-label"));
            var text = span.Text;
            Assert.AreEqual(text, "Archived");
        }

        [Then(@"the review I just completed is viewable")]
        public void ThenTheReviewIJustCompletedIsViewable()
        {
            Thread.Sleep(4000);
            var riskAssessmentReviewsGrid = WebBrowser.Current.Div(Find.ById("RiskAssessmentReviewsGrid"));
            var riskAssessmentReviewsTable = riskAssessmentReviewsGrid.Tables[0];
            var newRiskAssessmentReviewRow = riskAssessmentReviewsTable.TableRows[1];
            Assert.That(newRiskAssessmentReviewRow.TableCells[2].Text, Is.EqualTo(DateTime.Now.ToString("dd/MM/yyyy")), "Could not find a review that has been completed today");
            Assert.That(newRiskAssessmentReviewRow.TableCells[4].Link(Find.ByClass("viewRiskAssessmentDocument")).Exists, "view icon for recently completed review not found");
        }

        [Then(@"confirmation for delete should be shown")]
        public void ThenConfirmationForDeleteShouldBeShown()
        {

        }
    }
}

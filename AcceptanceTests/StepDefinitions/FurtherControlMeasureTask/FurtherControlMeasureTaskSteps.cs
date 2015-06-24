using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using StructureMap;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using System.Dynamic;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.FurtherControlMeasureTask
{
    [Binding]
    public class FurtherControlMeasureTaskSteps : BaseSteps
    {

        [AfterScenario("usesBackgroundFCMCreation")]
        [BeforeScenario("usesBackgroundFCMCreation")]
        public void TearDown()
        {
            deleteTaskWithReference("RecurFCM01");
            deleteTaskWithReference("RecurFCM02");
            deleteTaskWithReference("HST");
        }

        private void deleteTaskWithReference(string reference)
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var totalRows = (int)runSQLCommand("SELECT Count(id) FROM [dbo].[Task] WHERE [Reference] = '" + reference + "'", conn);
                for (var i = 0; i < totalRows; i++)
                {
                    var taskId = runSQLCommand("SELECT id FROM [dbo].[Task] WHERE [Reference] = '" + reference + "'", conn);
                    var sql = new StringBuilder();
                    sql.Append("DELETE FROM [TaskDocument] WHERE [TaskId] = " + taskId);
                    sql.Append("DELETE FROM [Task] WHERE [Id] = " + taskId);
                    runSQLCommand(sql.ToString(), conn);
                }
            }
        }

        [Given("I have the following tasks:")]
        public void GivenIHaveTheFollowingEmployeesForCompany(TechTalk.SpecFlow.Table table)
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var createdFurtherControlMeasureTask = new List<ExpandoObject>();
                foreach (var row in table.Rows)
                {
                    dynamic task = new ExpandoObject();
                    task.MultiHazardRiskAssessmentHazardId = string.IsNullOrEmpty(row["MultiHazardRiskAssessmentHazardId"]) ? "NULL" : row["MultiHazardRiskAssessmentHazardId"];
                    task.Title = row["Title"];
                    task.Description = row["Description"];
                    task.Reference = row["Reference"];
                    task.Deleted = row["Deleted"];
                    task.CreatedOn = row["CreatedOn"];
                    task.CreatedBy = row["CreatedBy"];
                    task.TaskAssignedToId = row["TaskAssignedToId"];
                    task.TaskCompletionDueDate = row["TaskCompletionDueDate"];
                    task.TaskStatusId = row["TaskStatusId"];
                    task.TaskCompletedDate = row["TaskCompletedDate"];
                    task.TaskCompletedComments = row["TaskCompletedComments"];
                    task.TaskCategoryId = row["TaskCategoryId"];
                    task.TaskReoccurringTypeId = row["TaskReoccurringTypeId"];
                    task.TaskReoccurringEndDate = row["TaskReoccurringEndDate"];
                    task.Discriminator = row["Discriminator"];
                    task.TaskGuid = row["TaskGuid"];
                    task.SendTaskNotification = row["SendTaskNotification"];
                    task.SendTaskCompletedNotification = row["SendTaskCompletedNotification"];
                    task.SendTaskOverdueNotification = row["SendTaskOverdueNotification"];

                    if (row.ContainsKey("HazardousSubstanceRiskAssessmentId") && row["HazardousSubstanceRiskAssessmentId"] != "")
                    {
                        task.HazardousSubstanceRiskAssessmentId = row["HazardousSubstanceRiskAssessmentId"];
                    }
                    else
                    {
                        task.HazardousSubstanceRiskAssessmentId = "NULL";
                    }

                    string sql =
                        string.Format(
                            "SET DATEFORMAT DMY " +
                            "INSERT INTO [dbo].[Task](" +
                            "[MultiHazardRiskAssessmentHazardId]," +
                            "[Title]," +
                            "[Description]," +
                            "[Reference]," +
                            "[Deleted]," +
                            "[CreatedOn]," +
                            "[CreatedBy]," +
                            "[TaskAssignedToId]," +
                            "[TaskCompletionDueDate]," +
                            "[TaskStatusId]," +
                            "[TaskCompletedDate]," +
                            "[TaskCompletedComments]," +
                            "[TaskCategoryId]," +
                            "[TaskReoccurringTypeId]," +
                            "[TaskReoccurringEndDate]," +
                            "[Discriminator]," +
                            "[HazardousSubstanceRiskAssessmentId]," +
                            "[TaskGuid], " +
                            "[SendTaskNotification]," +
                            "[SendTaskCompletedNotification]," +
                            "[SendTaskOverdueNotification] " +
                            ") VALUES (" +
                            task.MultiHazardRiskAssessmentHazardId + ", " +
                            "'" + task.Title + "', " +
                            "'" + task.Description + "', " +
                            "'" + task.Reference + "', " +
                            task.Deleted + ", " +
                            "'" + task.CreatedOn.Replace("-", "") + "', " +
                            "'" + task.CreatedBy + "', " +
                            "'" + task.TaskAssignedToId + "', " +
                            "'" + task.TaskCompletionDueDate.Replace("-", "") + "', " +
                            task.TaskStatusId + ", " +
                            task.TaskCompletedDate.Replace("-", "") + ", " +
                            "'" + task.TaskCompletedComments + "', " +
                            task.TaskCategoryId + ", " +
                            task.TaskReoccurringTypeId + ", " +
                            "'" + task.TaskReoccurringEndDate.Replace("-", "") + "', " +
                            "'" + task.Discriminator + "', " +
                            task.HazardousSubstanceRiskAssessmentId + ", " +
                            "'" + task.TaskGuid + "', " +
                            task.SendTaskNotification + ", " +
                            task.SendTaskCompletedNotification + ", " +
                            task.SendTaskOverdueNotification + ") ");

                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.ExecuteScalar();
                    }

                    createdFurtherControlMeasureTask.Add(task);
                }
            }
        }

        [Given("Complete task is clicked for '(.*)'")]
        public void CompleteTaskIsClickedFor(string FcmRef)
        {
            WebBrowser.Current.WaitForComplete();
            var tableDiv = WebBrowser.Current.Div(Find.ById("TaskGrid"));
            var table = tableDiv.Tables[0];
            var row = table.TableRows.Single(t => t.TableCells[1].Text == FcmRef);
            var link = row.Link(Find.ByTitle("Complete Task"));
            var taskId = link.GetAttributeValue("data-id");
            link.Click();
            WebBrowser.Current.WaitForComplete();
        }

        [Given("Reassign task is clicked for '(.*)'")]
        public void ReassignTaskIsClickedFor(string FcmRef)
        {
            WebBrowser.Current.WaitForComplete();
            var tableDiv = WebBrowser.Current.Div(Find.ById("TaskGrid"));
            var table = tableDiv.Tables[0];
            var row = table.TableRows.Single(t => t.TableCells[1].Text == FcmRef);
            var link = row.Link(Find.ByTitle("Re-assign"));
            var taskId = link.GetAttributeValue("data-id");
            link.Click();
            WebBrowser.Current.WaitForComplete();
        }

        [Given("'(.*)' check box is ticked '(.*)'")]
        [When("'(.*)' check box is ticked '(.*)'")]
        [Then("'(.*)' check box is ticked '(.*)'")]
        public void CompleteCheckBosIsTicked(string checkboxId, bool isTicked)
        {
            var completeCheckBox = WebBrowser.Current.CheckBox(Find.ById(checkboxId));
            completeCheckBox.Checked = isTicked;
        }

        [Given("Javascript for clicking check bok has fired")]
        public void JavaScriptForClickingCheckBoxHasFired()
        {
            WebBrowser.Current.Eval("new completeTaskmanager.initialize().setCompleteEnabled(true);");
        }

        [Given("Complete button is enabled")]
        public void CompleteButtonIsEnabled()
        {
            WebBrowser.Current.Eval("$(\"#TaskSaveButton\").removeAttr('disabled')");
            WebBrowser.Current.Eval("$(\"#TaskSaveButton\").removeClass('disabled')");
        }

        [When("Complete is clicked")]
        public void CompleteIsClicked()
        {
            var completeButton = WebBrowser.Current.Button(Find.ById("TaskSaveButton"));
            completeButton.Click();
        }

        [Then("FCM Task should have due date of 19/10/2012")]
        public void FcmTaskShouldHaveDueDateOf19102012()
        {
            Thread.Sleep(4000);
            var tableDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            var table = tableDiv.Tables[0];
            var row = table.TableRows.Single(t => t.TableCells[1].Text == "RecurFCM01");
            Assert.AreEqual("19/10/2012", row.TableCells[7].Text);
        }

        [Then(@"Document should be saved as a completed document against the fcm task")]
        public void ThenDocumentShouldBeSavedAsACompletedDocumentAgainstTheFcmTask()
        {
            var riskAssessment = ObjectFactory.GetInstance<IGeneralRiskAssessmentRepository>().GetByIdAndCompanyId(39, 55881);
            var riskAssessmentHazard = riskAssessment.FindRiskAssessmentHazard(29);
            var furtherControlMeasureTask =
                riskAssessmentHazard.FurtherControlMeasureTasks.FirstOrDefault(
                    fcmt =>
                    fcmt.Title == "Test FCM 2" & 
                    fcmt.Description == "Description 2" & 
                    fcmt.Reference == "RecurFCM02");

            var document =
                furtherControlMeasureTask.Documents.FirstOrDefault(
                    d =>
                    d.Filename == "Completed Text File.txt" & d.DocumentOriginType == DocumentOriginType.TaskCompleted);

            Assert.That(document.Id, Is.Not.Null);
        }

        [Then(@"the task '(.*)' for company '(.*)' should be completed")]
        public void ThenTaskShouldBeCompleted(string reference, long company)
        {
            Thread.Sleep(2000);
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var totalRows = (int)runSQLCommand("SELECT Count(id) FROM [dbo].[Task] WHERE [Reference] = '" + reference + "' And [TaskStatusId] = 1", conn);
                Assert.That(totalRows, Is.EqualTo(1));
            }
        }

        [Then(@"the task list should not list a task with a reference of '(.*)'")]
        public void ThenTheTaskListShouldNotListATaskWithAReferenceOf(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the task '(.*)' for company '(.*)' should be assigned to '(.*)'")]
        public void ThenTheTaskForCompanyShouldBeAssignedTo(string reference, long company, string assignedToEmployeeId)
        {
            Thread.Sleep(1000);
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var sql = string.Format("SELECT Count(id) FROM [dbo].[Task] WHERE [Reference] = '{0}' And [TaskAssignedToId] = '{1}'", reference, assignedToEmployeeId);
                var totalRows = (int)runSQLCommand(sql, conn);
                Assert.That(totalRows, Is.EqualTo(1));
            }
        }


    }
}

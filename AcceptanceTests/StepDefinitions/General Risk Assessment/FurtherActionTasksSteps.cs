using System;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = WatiN.Core.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class FurtherActionTasksSteps : BaseSteps
    {
        [When(@"I have entered mandatory further action task data")]
        public void WhenIHaveEnteredMandatoryFurtherActionTaskData()
        {
            FormSteps.EnterValueIntoField("Reference", "Reference");
            FormSteps.EnterValueIntoField("Title", "Title");
            FormSteps.EnterValueIntoField("Description", "Description");
            FormSteps.EnterValueIntoField("Kim Howard ( Business Analyst )", "TaskAssignedTo");
            FormSteps.EnterValueIntoField("01/01/2015", "TaskCompletionDueDate");
            const string javascriptToRun = "$('#TaskAssignedToId').val('A433E9B2-84F6-4AD7-A89C-050E914DFF01');";
            WebBrowser.Current.Eval(javascriptToRun);

            dynamic furtherActionTaskAdding = new ExpandoObject();
            furtherActionTaskAdding.HazardId = 1;
            furtherActionTaskAdding.Reference = "Reference";
            furtherActionTaskAdding.Title = "Title";
            furtherActionTaskAdding.Description = "Description";
            furtherActionTaskAdding.TaskAssignedToId = "A433E9B2-84F6-4AD7-A89C-050E914DFF01";

            ScenarioContextHelpers.SetFurtherActionTaskAddingToRiskAssessment(furtherActionTaskAdding);
        }

        [When(@"I press IsReoccurring checkbox")]
        public void WhenIPressIsReoccurringCheckbox()
        {
            var checkbox = WebBrowser.Current.CheckBox(Find.ById("IsRecurring"));
            checkbox.Click();

            Thread.Sleep(1000);
        }

        [When(@"I have entered mandatory further control measure task data")]
        public void WhenIHaveEnteredMandatoryReoccurringFurtherControlMeasureTaskData()
        {
            FormSteps.EnterValueIntoField("Reoccurring-Reference", "Reference");
            FormSteps.EnterValueIntoField("Reoccurring-Title", "Title");
            FormSteps.EnterValueIntoField("Reoccurring-Description", "Description");
            FormSteps.EnterValueIntoField("Kim Howard ( Business Analyst )", "TaskAssignedTo");
            FormSteps.EnterValueIntoField("A433E9B2-84F6-4AD7-A89C-050E914DFF01", "TaskAssignedToId");
            FormSteps.EnterValueIntoField("Weekly", "TaskReoccurringType");
            FormSteps.EnterValueIntoField("1", "TaskReoccurringTypeId");
            FormSteps.EnterValueIntoField("1", "IsRecurring");
            FormSteps.EnterValueIntoField("01/01/2015", "FirstDueDate");
            FormSteps.EnterValueIntoField("01/01/2025", "TaskReoccurringEndDate");

            dynamic furtherActionTaskAdding = new ExpandoObject();
            furtherActionTaskAdding.HazardId = 1;
            furtherActionTaskAdding.Reference = "Reoccurring-Reference";
            furtherActionTaskAdding.Title = "Reoccurring-Title";
            furtherActionTaskAdding.Description = "Reoccurring-Description";
            furtherActionTaskAdding.TaskAssignedToId = "A433E9B2-84F6-4AD7-A89C-050E914DFF01";
            furtherActionTaskAdding.TaskReoccurringType = "Weekly";
            furtherActionTaskAdding.TaskCompletionDueDate = "01/01/2015";
            furtherActionTaskAdding.TaskReoccurringEndDate = "01/01/2025";

            ScenarioContextHelpers.SetFurtherActionTaskAddingToRiskAssessment(furtherActionTaskAdding);
        }

        [Then(@"the reoccurring further control measure task should be saved")]
        public void ThenTheReoccurringFurtherControlMeasureTaskShouldBeSaved()
        {
            var furtherActionTask = ScenarioContextHelpers.GetFurtherActionTaskAddingToRiskAssessment();

            var riskAssessmentId = GetCurrentRiskAssessmentId();
            var sql = new StringBuilder();
            sql.Append("Select COUNT(*) From MultiHazardRiskAssessmentHazard MHRAH INNER JOIN dbo.Task T ON MHRAH.ID = T.MultiHazardRiskAssessmentHazardId WHERE RiskAssessmentId = '" + riskAssessmentId + "' AND Title = '" + furtherActionTask.Title + "'");

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

        [Then(@"the further action task should be saved")]
        public void ThenTheFurtherActionTaskShouldBeSaved()
        {
            Thread.Sleep(3000);

            var riskAssessmentId = GetCurrentRiskAssessmentId();

            var furtherActionTask = ScenarioContextHelpers.GetFurtherActionTaskAddingToRiskAssessment();
            var sql = new StringBuilder();
            sql.Append("SELECT TOP 1 Id FROM MultiHazardRiskAssessmentHazard ");
            sql.Append("WHERE RiskAssessmentId = '" + riskAssessmentId + "' ");
            sql.Append("AND HazardId = '" + furtherActionTask.HazardId + "' ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    var id = command.ExecuteScalar();
                    sql = new StringBuilder();
                    sql.Append("SELECT * FROM Task ");
                    
                    sql.Append("WHERE Title = '" + furtherActionTask.Title + "' ");
                    sql.Append("AND Description = '" + furtherActionTask.Description + "' ");
                    sql.Append("AND Reference = '" + furtherActionTask.Reference + "' ");
                    sql.Append("AND TaskAssignedToId = '" + furtherActionTask.TaskAssignedToId + "' ");
                    if (!string.IsNullOrEmpty("" + id))
                    {
                        sql.Append("AND MultiHazardRiskAssessmentHazardId = '" + id + "' ");
                    }

                    using (var command1 = new SqlCommand(sql.ToString(), conn))
                    {
                        using (var reader = command1.ExecuteReader())
                        {
                            Assert.True(reader.HasRows);
                        }
                    }
                }
            }
        }

        [Then(@"Created Date should be displayed")]
        public void CreatedDateShouldBeDisplayed()
        {
            var createdOnColumnIndex = 4;

            var cssClass = "table table-striped further-action-task-table";
            var furtherActionTaskTable = GetElement(FindFurtherActionTableByCssClass, cssClass, 3000) as Table;

            var row = furtherActionTaskTable.TableRows[0];
            var cell = row.TableCells[createdOnColumnIndex];

            bool isCorrectCreatedDate;

            try
            {
                var date = DateTime.ParseExact(cell.Text.Trim(), "dd/MM/yyyy", null);

                if (date == DateTime.Now.Date)
                    isCorrectCreatedDate = true;
                else
                    isCorrectCreatedDate = false;
            }
            catch
            {
                isCorrectCreatedDate = false;
            }

            Assert.IsTrue(isCorrectCreatedDate);
        }

        private Table FindFurtherActionTableByCssClass(string cssClass)
        {
            return WebBrowser.Current.Table(Find.ByClass(cssClass));
        }

        [BeforeScenario("requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated")]
        public void SetupCompletedAndAnOutstandingFCMTasks()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                Console.WriteLine("Creating completed and one outstanding FCM tasks for requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated tag");
                conn.Open();

                var sql = new StringBuilder();
                // build completed tasks

                // TODO : Change for just insert to specify the fields inserting into to

                const string baseFcmSql =
                    "INSERT INTO [BusinessSafe].[dbo].[Task] ([MultiHazardRiskAssessmentHazardId], [Title], [Description], [Reference], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [TaskAssignedToId], " +
                    "[TaskCompletionDueDate], [TaskStatusId], [TaskCompletedDate], [TaskCompletedComments], [TaskCategoryId], [TaskReoccurringTypeId], [TaskReoccurringEndDate], [FollowingTaskId], [OriginalTaskId], " +
                    "[Discriminator], [TaskGuid] ) " +
                    "VALUES (29,'recurring_task_{0}','recurring_task_{0}','recurring_task_{0}',0,getdate(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C',getdate(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C','D2122FFF-1DCD-4A3C-83AE-E3503B394EB4',getdate(),1,getdate(),'this was completed on its creation',3,1,getdate(),1,1,'MultiHazardRiskAssessmentFurtherControlMeasureTask', newid());";
                for (var i = 1; i <= 5; i++)
                {
                    sql.Append(string.Format(baseFcmSql, i));
                }
                sql.Append(string.Format("INSERT INTO [BusinessSafe].[dbo].[Task] ([MultiHazardRiskAssessmentHazardId], [Title], [Description], [Reference], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [TaskAssignedToId], " +
                    "[TaskCompletionDueDate], [TaskStatusId], [TaskCompletedDate], [TaskCompletedComments], [TaskCategoryId], [TaskReoccurringTypeId], [TaskReoccurringEndDate], [FollowingTaskId], [OriginalTaskId], " +
                    "[Discriminator], [TaskGuid] ) " +
                    "VALUES(29,'recurring_task_{0}','recurring_task_{0}','recurring_task_{0}',0,getdate(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C',null,null,'D2122FFF-1DCD-4A3C-83AE-E3503B394EB4',getdate(),0,null,null,3,1,getdate(),1,1,'MultiHazardRiskAssessmentFurtherControlMeasureTask', newid())", 6));

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    command.ExecuteScalar();
                }
            }
        }

        [AfterScenario("requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated")]
        public void TeardownCompletedAndAnOutstandingFCMTasks()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var totalRows = (int)runSQLCommand("SELECT COUNT(id) FROM [dbo].[Task] WHERE [Reference] LIKE 'recurring_task_%'", conn);
                for (var i = 0; i < totalRows; i++)
                {
                    var taskId = runSQLCommand("SELECT id FROM [dbo].[Task] WHERE [Reference] LIKE 'recurring_task_%'", conn);
                    var sql = new StringBuilder();
                    sql.Append("DELETE FROM [TaskDocument] WHERE [TaskId] = " + taskId);
                    sql.Append("DELETE FROM [Task] WHERE [Id] = " + taskId);
                    runSQLCommand(sql.ToString(), conn);
                }
            }
        }

        [BeforeScenario("requiresBunchOfTasksForClientId31028")]
        public void SetupTeardownTasksFor31028()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                Console.WriteLine("Creating completed and one outstanding FCM tasks for requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated tag");
                conn.Open();

                var sql = new StringBuilder();
                // build completed tasks
                const string baseFcmSql =
                    "INSERT INTO [BusinessSafe].[dbo].[Task] VALUES(29,'recurring_task_{0}','recurring_task_{0}','recurring_task_{0}',0,getdate(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C',getdate(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C','D2122FFF-1DCD-4A3C-83AE-E3503B394EB4',getdate(),1,getdate(),'this was completed on its creation',3,1,getdate(),1,1,'MultiHazardRiskAssessmentFurtherControlMeasureTask',null, null);";
                for (var i = 1; i <= 5; i++)
                {
                    sql.Append(string.Format(baseFcmSql, i));
                }
                sql.Append(string.Format("INSERT INTO [BusinessSafe].[dbo].[Task] VALUES(29,'recurring_task_{0}','recurring_task_{0}','recurring_task_{0}',0,getdate(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C',null,null,'D2122FFF-1DCD-4A3C-83AE-E3503B394EB4',getdate(),0,null,null,3,1,getdate(),1,1,'MultiHazardRiskAssessmentFurtherControlMeasureTask',null, null)", 6));

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    command.ExecuteScalar();
                }
            }
        }

        [AfterScenario("requiresBunchOfTasksForClientId31028")]
        public void TeardownTasksFor31028()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var totalRows = (int)runSQLCommand("SELECT COUNT(id) FROM [dbo].[Task] WHERE [Reference] LIKE 'recurring_task_%'", conn);
                for (var i = 0; i < totalRows; i++)
                {
                    var taskId = runSQLCommand("SELECT id FROM [dbo].[Task] WHERE [Reference] LIKE 'recurring_task_%'", conn);
                    var sql = new StringBuilder();
                    sql.Append("DELETE FROM [TaskDocument] WHERE [TaskId] = " + taskId);
                    sql.Append("DELETE FROM [Task] WHERE [Id] = " + taskId);
                    runSQLCommand(sql.ToString(), conn);
                }
            }
        }

        [Then(@"FCM task '(.*)' should have visibility of '(.*)'")]
        public void ThenFCMTaskShouldHaveVisibilityOf(string taskTitle, string isVisible)
        {
            Thread.Sleep(2000);
            Table fcmTable =
                WebBrowser.Current.Tables.Filter(Find.ByClass("table table-striped further-action-task-table")).
                    FirstOrDefault();
            Assert.IsNotNull(fcmTable, "Further Control Measure table not found");
            bool rowFound = false;

            for (int i = 0; i < fcmTable.TableRows.Count(); i++)
            {
                var colValue = fcmTable.TableRows[i].TableCells[1].Text;
                if (colValue.StartsWith(taskTitle))
                    rowFound = true;
            }
            if (isVisible == "true")
            {
                Assert.IsTrue(rowFound);
            }
            else
            {
                Assert.IsFalse(rowFound);
            }
        }

        [Then(@"I click on the further action task row for the task '(.*)'")]
        [When(@"I click on the further action task row for the task '(.*)'")]
        public void ThenIClickOnTheFurtherActionTaskRowForThatTask(string taskTitle)
        {
            Thread.Sleep(2000);
            var table = WebBrowser.Current.Tables.Filter(Find.ByClass("table table-striped further-action-task-table")).
                    FirstOrDefault();
            var taskRowFound = false;
            foreach (WatiN.Core.TableRow row in table.TableRows)
            {
                TableCell taskTitleCell = row.TableCells.Skip(1).First();
                if (taskTitleCell.InnerHtml.Contains(taskTitle))
                {
                    taskRowFound = true;
                    taskTitleCell.Click();
                }
            }
            Assert.IsTrue(taskRowFound, string.Format("Could not find row for {0}", taskTitle));
        }
    }
}

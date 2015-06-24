using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.TaskList
{
    [Binding]
    public class TaskListSteps: BaseSteps
    {
        [BeforeScenario("DeleteTaskFromTaskList")]
        [AfterScenario("DeleteTaskFromTaskList")]
        public static void AfterDeleteTaskFromTaskListScenario()
        {
            var sql = new StringBuilder();
            sql.Append("Update Task ");
            sql.Append("Set [Deleted] = 0 ");
            sql.Append("Where Id in (16, 17, 18, 19, 20)");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [Then(@"the task lists results table should contain the following data:")]
        public void ThenTheResultShouldContainRowWithTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                dynamic task = new ExpandoObject();

                task.Ref = row["Task Reference"].Length > 0 ? row["Task Reference"] : " ";
                task.TaskCategory = row["Task Category"];
                task.Title = row["Title"];
                task.Description = row["Description"];
                task.AssignedTo = row["Assigned To"];
                task.CreatedDate = row["Created Date"];
                task.DueDate = row["Due Date"];
                task.Completed = row["Completed"];

                var searchResultsTable = WebBrowser.Current.Tables.First();

                Assert.NotNull(searchResultsTable.FindRow(task.Ref, 1),             string.Format("Could not find cell containing '{0}', in column '{1}'", task.Ref, 1));
                Assert.NotNull(searchResultsTable.FindRow(task.TaskCategory, 2), string.Format("Could not find cell containing '{0}', in column '{1}'", task.TaskCategory, 2));
                Assert.NotNull(searchResultsTable.FindRow(task.Title, 3), string.Format("Could not find cell containing '{0}', in column '{1}'", task.Title, 3));
                Assert.NotNull(searchResultsTable.FindRow(task.Description, 4), string.Format("Could not find cell containing '{0}', in column '{1}'", task.Description, 4));
                Assert.NotNull(searchResultsTable.FindRow(task.AssignedTo, 5), string.Format("Could not find cell containing '{0}', in column '{1}'", task.AssignedTo, 5));
                Assert.NotNull(searchResultsTable.FindRow(task.CreatedDate, 6), string.Format("Could not find cell containing '{0}', in column '{1}'", task.CreatedDate, 6));
                Assert.NotNull(searchResultsTable.FindRow(task.DueDate, 7), string.Format("Could not find cell containing '{0}', in column '{1}'", task.DueDate, 7));
                Assert.NotNull(searchResultsTable.FindRow(task.Completed, 8), string.Format("Could not find cell containing '{0}', in column '{1}'", task.Completed, 8));
            }
        }

        [Then(@"the completed task lists results table should contain the following data:")]
        public void ThenTheCompletedResultShouldContainRowWithTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                dynamic task = new ExpandoObject();

                task.Ref = row["Task Reference"].Length > 0 ? row["Task Reference"] : " ";
                task.TaskCategory = row["Task Category"];
                task.Title = row["Title"];
                task.Description = row["Description"];
                task.AssignedTo = row["Assigned To"];
                task.CompletedBy = row["Completed By"];
                task.CompletedDate = row["Completed Date"];
                task.DueDate = row["Due Date"];
                task.Completed = row["Status"];

                var searchResultsTable = WebBrowser.Current.Tables.First();

                AssertColumnValue(searchResultsTable, task.Ref, 1);
                AssertColumnValue(searchResultsTable, task.TaskCategory, 2);
                AssertColumnValue(searchResultsTable, task.Title, 3);
                AssertColumnValue(searchResultsTable, task.Description, 4);
                AssertColumnValue(searchResultsTable, task.AssignedTo, 5);
                AssertColumnValue(searchResultsTable, task.CompletedBy, 6);
                AssertColumnValue(searchResultsTable, task.CompletedDate, 7);
                AssertColumnValue(searchResultsTable, task.DueDate, 8);
                AssertColumnValue(searchResultsTable, task.Completed, 9);
                }
        }

        private static void AssertColumnValue(WatiN.Core.Table searchResultsTable, string property, int columnIndex)
        {
            Assert.NotNull(searchResultsTable.FindRow(property, columnIndex),
                string.Format("Could not find cell containing '{0}', in column '{1}'", property, columnIndex));
        }

        [When(@"I have clicked on the delete task link for id '(.*)'")]
        public void WhenIHaveClickedOnTheDeleteTaskLinkForId16(int taskId)
        {
            Thread.Sleep(4000);
            bool elementFound = false;
            foreach (Link link in WebBrowser.Current.Links.Filter(Find.ByClass("icon-remove")))
            {
                    if (link.GetAttributeValue("data-id") == taskId.ToString())
                    {
                        link.Click();
                        elementFound = true;
                        break;
                    }
            }
            Assert.IsTrue(elementFound, "Could not find delete task link for id " + taskId);   
        }

        [Given(@"I have clicked on the delete task link for task with title '(.*)'")]
        [When(@"I have clicked on the delete task link for task with title '(.*)'")]
        public void IHaveClickedOnTheDeleteTaskLinkForTaskWithTitle(string title)
        {
            Thread.Sleep(2000);

            foreach (var row in WebBrowser.Current.TableRows.ToList())
            {
                if (row.TableCells.Count() > 4)
                {
                    if (row.TableCells[3].Text != null && row.TableCells[3].Text.Trim() == title.Trim())
                    {
                        var deleteButton = row.TableCells.ElementAt(9).Link(Find.ByTitle("Delete Task"));
                        Assert.That(deleteButton.Exists, "Could not find Delete Task button");
                        deleteButton.Click();
                        //ScenarioContextHelpers.SetFurtherActionTaskRow(row);
                        return;
                    }
                }
            }
        }

        [Then(@"the task '(.*)' should be marked as deleted")]
        public void ThenTheTask16ShouldBeMarkedAsDeleted(int taskId)
        {
            Thread.Sleep(2000);
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                    conn.Open();

                var sql = string.Format("SELECT Deleted FROM Task WHERE Id = {0}", taskId);
                var result = runSQLCommand(sql, conn);

                Assert.That(result, Is.True, "Task is not deleted");

            }
        }

        [Given(@"the '(.*)' summary should be '(.*)'")]
        public void ThenTheStatusWrapperRedSummaryShouldBe2(string elementClass, string value)
        {
            System.Threading.Thread.Sleep(1000);
            var span = WebBrowser.Current.Span(Find.ByClass(string.Format("statusWrapperGeneral {0}", elementClass)));
            Assert.That(span.InnerHtml, Is.EqualTo(value));
        }

        [When(@"I view the recurring task schedule for a task")]
        public void WhenIViewTheRecurringTaskScheduleForATask()
        {
            var button = WebBrowser.Current.Span(Find.ByClass("label label-important label-reoccurring-task"));
            button.DoubleClick();
        }

        [Given(@"I am viewing tasks for EmployeeId '(.*)'")]
        public void GivenIAmViewingTasksForEmployeeId(string employeeId)
        {
            WebBrowser.Driver.Navigate(string.Format("/TaskList/TaskList/Find?EmployeeId{0}", employeeId));
        }

        [Then(@"the task list results table should contain '(.*)' rows")]
        public void ThenTheTaskListResultsTableShouldContainRows(int numberOfRows)
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();

            Assert.AreEqual(numberOfRows,searchResultsTable.TableRows.Count());
        }

    }
}

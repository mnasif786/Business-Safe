using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Application.Contracts.RiskAssessments;
using BusinessSafe.Application.Contracts.TaskList;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = WatiN.Core.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ReassignFurtherActionTasks
{
    [Binding]
    public class ReassignFurtherActionTasksSteps : BaseSteps
    {
        [AfterScenario("ReassigningFurtherActionTasks")]
        public static void ResetFurtherActionTasksInKnownState()
        {
            var sql = new StringBuilder();
            sql.Append("Update Task ");
            sql.Append("Set [TaskAssignedToId] = 'A433E9B2-84F6-4AD7-A89C-050E914DFF01' "); //Kim Howard
            sql.Append("Where Id = '11'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }

            sql = new StringBuilder();
            sql.Append("Update Task ");
            sql.Append("Set [TaskAssignedToId] = '3ece3fd2-db29-4abd-a812-fcc6b8e621a1', TaskStatusID = 0 "); //Barry Brown
            sql.Append("Where Id = '17'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }

           // ObjectFactory.GetInstance<IBusinessSafeSessionManager>().CloseSession();
        }

        [Given(@"I am on the responsibility planner page for company '(.*)'")]
        public void GivenIAmOnTheResponsibilityPlannerPageForCompany55881(long companyId)
        {
            GotoHomePage();
            WebBrowser.Driver.Navigate("?companyId=" + companyId);
        }

        [Then(@"the reassign task screen should be shown")]
        [Then(@"the relevant employee tasks should be shown")]
        public void ThenTheRelevantEmployeeTasksShouldBeShown()
        {
            SleepIfEnvironmentIsPotentiallySlow(8000);
        }

        [When(@"I have clicked on the reassign task link for id '(.*)'")]
        public void WhenIHaveClickedOnTheReassignTaskLinkForId11(int taskId)
        {
            Thread.Sleep(2000);
            bool elementFound = false;
            foreach (Link image in WebBrowser.Current.Links.Filter(Find.ByClass("icon-share")))
            {
                    if (image.GetAttributeValue("data-id") == taskId.ToString())
                    {
                        image.Click();
                        elementFound = true;
                        break;
                    }
            }
            Assert.IsTrue(elementFound, "Could not find re-assign task link for id " + taskId);

            
        }

        [Given(@"I have entered 'Barry Brown \( Team leader \)' into the Reassign field")]
        public void GivenIHaveEnteredBarryBrownTeamLeaderIntoTheReassignField()
        {
            Thread.Sleep(4000);
            const string javascriptToRun = "$('#ReassignTaskToId').val('3ece3fd2-db29-4abd-a812-fcc6b8e621a1');";
            WebBrowser.Current.Eval(javascriptToRun);
        }

        [Then(@"the task '(.*)' should no longer be assigned to '(.*)'")]
        public void ThenTheTask11ShouldNoLongerBeAssignedToKimHoward(long taskId, string noLongerAssignedTo)
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var taskFound = GetTaskAssignedTo(taskId, noLongerAssignedTo, conn);

                Assert.IsNull(taskFound, "Task " + taskId + " is still assigned to " + noLongerAssignedTo);
            }
        }

        [Then(@"the task '(.*)' should be assigned to '(.*)'")]
        public void ThenTheTask11ShouldBeAssignedToBarryBrownTeamLeader(long taskId, string assignedTo)
        {


            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var taskFound = GetTaskAssignedTo(taskId, assignedTo, conn);

                Assert.IsNotNull(taskFound, "Task " + taskId + " is not assigned to " + assignedTo);
            }


            //var furtherActionTask = ObjectFactory.GetInstance<IFurtherControlMeasureTaskRepository>().GetById(taskId);
            //Assert.That(furtherActionTask.TaskAssignedTo.FullName, Is.EqualTo(assignedTo));
        }

        [Then(@"the TaskDialog popup element is visible")]
        public void ThenTheTaskDialogPopupElementIsVisible()
        {
            Thread.Sleep(2000);
            Element elem = WebBrowser.Current.Elements.Filter(Find.ById("TaskDialog")).FirstOrDefault();
            Assert.IsNotNull(elem, "Element with id TaskDialog could not be found");
            Assert.That(elem.Style.Display, Is.Not.EqualTo("none"), "Element with id TaskDialog is supposed to be visible");
        }

        [When(@"I press '(.*)' button on the '(.*)' row")]
        public void WhenIPressReassignButtonOnTheRecurring_Task_Row(string buttonName, string taskTitle)
        {
            SleepIfEnvironmentIsPotentiallySlow(5000);
            WatiN.Core.TableRow furtherActionTaskRow = null;
            bool rowFound = false;

            Table fcmTable = WebBrowser.Current.Tables.Filter(Find.ByClass("table table-striped further-action-task-table")).FirstOrDefault();
            Assert.IsNotNull(fcmTable, "Further Control Measure table not found");

            for (int i = 0; i < fcmTable.TableRows.Count(); i++)
            {
                var colValue = fcmTable.TableRows[i].TableCells[1].Text;
                if (colValue.StartsWith(taskTitle))
                {
                    rowFound = true;
                    furtherActionTaskRow = fcmTable.TableRows[i];
                    break;
                }
            }

            Assert.IsTrue(rowFound, string.Format("Further Control Measure row for {0} not found", taskTitle));

            if (furtherActionTaskRow != null)
            {
                var className = string.Empty;
                switch (buttonName)
                {
                    case "Reassign":
                        className = ElementCssHelper.ClassFor(Elements.FcmReassignButton);
                        break;
                    case "Edit":
                        className = ElementCssHelper.ClassFor(Elements.EditFcmButton);
                        break;
                }

                Assert.That(className, Is.Not.EqualTo(string.Empty), string.Format("Button '{0}' has not been set in ElementCssHelper", buttonName));

                var button = furtherActionTaskRow.Elements.Filter(Find.ByClass(className)).FirstOrDefault();

                Assert.IsNotNull(button, string.Format("Could not find button with class '{0}'", className));

                button.Click();
                Thread.Sleep(1000);
            }
        }



        private static object GetTaskAssignedTo(long taskId, string noLongerAssignedTo, SqlConnection conn)
        {
            var nameParts = noLongerAssignedTo.Split(' ');

            var employeeSql = string.Format("(SELECT id FROM employee WHERE Forename = '{0}' AND Surname = '{1}')",
                nameParts[0],
                nameParts[1]);
            var sql = new StringBuilder();
            sql.Append(string.Format("SELECT id FROM Task WHERE Id = {0} AND [TaskAssignedToId] = {1}", taskId, employeeSql));

            return runSQLCommand(sql.ToString(), conn);
        }

        [Then(@"the task '(.*)' retrieved from the db should be assigned to '(.*)'")]
        public void ThenTheTaskRetrievedFromTheDbShouldBeAssignedTo(int taskId, string expectedAssignedToFullName)
        {
            var repository = ObjectFactory.GetInstance<ITasksRepository>();
            var task = repository.GetById(taskId);
            Assert.That(task.TaskAssignedTo.FullName, Is.EqualTo(expectedAssignedToFullName));

        }


    }
}

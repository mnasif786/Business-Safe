using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.Repository;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using TableRow = WatiN.Core.TableRow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ReassignFurtherActionTasks
{
    [Binding]
    public class BulkReassignFurtherActionTaskFromTaskListSteps: BaseSteps
    {
        [Then(@"the task list should be in bulk reassign mode")]
        public void ThenTheTaskListShouldBeInBulkReassignMode()
        {
            
        }

        [Given(@"I tick task '(.*)' in the task list")]
        public void GivenITickTaskTestFCM1InTheTaskList(string task)
        {
            Thread.Sleep(2000);
            var table = WebBrowser.Current.Tables.First();

            foreach (TableRow row in table.TableRows)
            {
                TableCell taskTitleCell = row.TableCells.Skip(3).Take(1).First();
                if(taskTitleCell.InnerHtml.Contains(task))
                {
                    CheckBox reassignCheckBox = row.CheckBoxes.First();
                    reassignCheckBox.Click();
                    break;
                }
            }
        }


        [Given(@"I select '(.*)' with id '(.*)' to reassign the tasks to in the task list")]
        public void GivenISelectKimHowardWithIdA433E9B2_84F6_4Ad7_A89C_050E914Dff01ToReassignTheTasksToInTheTaskList(string employee, string employeeId)
        {
            Thread.Sleep(2000);
            var selectBox = WebBrowser.Current.SelectLists.FirstOrDefault(x => x.Id == "BulkReassignToId");
            if (selectBox != null)
            {
                selectBox.Select(employee);
                // trigger any events listening for this select box to change
                WebBrowser.Current.Eval("$('#BulkReassignToId').change();");
                Thread.Sleep(2000);
            }
        }


        [Then(@"task '(.*)' is assigned to '(.*)' in the task list")]
        public void ThenTaskTestFCM6IsAssignedToKimHoward(string task, string employeeName)
        {
            Thread.Sleep(2000);
            
            var employeeId = GetEmployeeIdByName(employeeName);
            
            var sql = new StringBuilder();
            sql.Append("SELECT Count(*) FROM Task WHERE Title = '" + task + "' AND TaskAssignedToId = '" + employeeId + "'");

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
    }
}

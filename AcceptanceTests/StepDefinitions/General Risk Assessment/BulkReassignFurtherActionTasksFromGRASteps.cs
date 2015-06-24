using System.Data.SqlClient;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = WatiN.Core.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class BulkReassignFurtherActionTasksFromGraSteps : BaseSteps
    {
        [AfterFeature("BulkReassigningFurtherActionTasks")]
        public static void TearDown()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var totalRows = (int)runSQLCommand("SELECT COUNT(id) FROM [dbo].[Task] WHERE [Reference] LIKE 'Bulk_Reassign_%'", conn);
                for (var i = 0; i < totalRows; i++)
                {
                    var taskId = runSQLCommand("SELECT id FROM [dbo].[Task] WHERE [Reference] LIKE 'Bulk_Reassign_%'", conn);
                    var sql = new StringBuilder();
                    sql.Append("DELETE FROM [TaskDocument] WHERE [TaskId] = " + taskId);
                    sql.Append("DELETE FROM [Task] WHERE [Id] = " + taskId);
                    runSQLCommand(sql.ToString(), conn);
                }
            }
        }


        [Given(@"I tick task '(.*)'")]
        public void GivenITickTasks45And6(string reqTaskName)
        {
            var checkboxFound = false;
            foreach (Element elem in WebBrowser.Current.Elements.Filter(Find.ByClass("bulk-reassign-checkbox")))
            {
                var taskName = elem.Parent.PreviousSibling.PreviousSibling.InnerHtml;
                if ((taskName.Contains(reqTaskName)))
                {
                    checkboxFound = true;
                    elem.Click();
                    break;
                }
            }
            Assert.IsTrue(checkboxFound, "Checkbox for task " + reqTaskName + " not found");
        }

        [Given(@"I select '(.*)' with id '(.*)' to reassign the tasks to")]
        public void GivenISelectKimHowardToReassignTheTasksTo(string employeeName, string employeeId)
        {
            // clicking UI employee anchor tag doesn't trigger correct events!
            //Button ddlButton = WebBrowser.Current.Buttons.Filter(Find.ByTitle("Show All Items")).First();
            //ddlButton.Click();
            //Thread.Sleep(1000);
            //IEnumerable<Element> employeeAnchorTags = WebBrowser.Current.Elements.Filter(Find.ByClass("ui-corner-all"));
            //foreach (Link employee in employeeAnchorTags)
            //{
            //    if (employee.InnerHtml.Contains(employeeName))
            //    {
            //        employee.Click();
            //        employee.Parent.Click();
            //        Thread.Sleep(1000);
            //    }
            //}
            string javascriptToRun = "$('#bulkReassignTaskToId').val('" + employeeId + "');$('#bulkReassignTaskTo').val('" + employeeName + "');";
            WebBrowser.Current.Eval(javascriptToRun);
        }

        [Then(@"task '(.*)' is assigned to '(.*)'")]
        public void ThenTheTaskIsAssignedToEmployee(string expectedTaskName, string expectedEmployeeName)
        {
            foreach (Element elem in WebBrowser.Current.Elements.Filter(Find.ByClass("bulk-reassign-checkbox")))
            {
                var taskName = elem.Parent.PreviousSibling.PreviousSibling.InnerHtml;
                var employeeName = elem.Parent.PreviousSibling.InnerHtml;
                if ((taskName.Contains(expectedTaskName)))
                {
                    Assert.That(employeeName.Contains(expectedEmployeeName));
                    break;
                }
            }
        }

        [Then(@"task '(.*)' is not assigned to '(.*)'")]
        public void ThenTheTaskIsNotAssignedToEmployee(string expectedTaskName, string expectedEmployeeName)
        {
            foreach (Element elem in WebBrowser.Current.Elements.Filter(Find.ByClass("bulk-reassign-checkbox")))
            {
                var taskName = elem.Parent.PreviousSibling.PreviousSibling.InnerHtml;
                var employeeName = elem.Parent.PreviousSibling.InnerHtml;
                if ((taskName.Contains(expectedTaskName)))
                {
                    Assert.IsFalse(employeeName.Contains(expectedEmployeeName));
                    break;
                }
            }
        }

        [Then(@"a popup warning me that Barry is not a user")]
        public void ThenAPopupWarningMeThatBarryIsNotAUser()
        {
            Element elem = WebBrowser.Current.Elements.Filter(Find.ByClass("employee-not-user-alert-message")).First();
            Assert.That(elem.ClassName.Contains("hide"), Is.Not.True);
        }

        [Then(@"task '(.*)' has a checkbox '(.*)'")]
        public void ThenTaskHasACheckbox(string taskTitle, string hasACheckbox)
        {
            foreach (Table table in WebBrowser.Current.Tables.Filter(Find.ByClass("further-action-task-table")))
            {
                foreach (TableCell cell in table.TableCells)
                {
                    if (cell.InnerHtml.Contains(taskTitle))
                    {
                        Element checkBoxCell = cell.NextSibling.NextSibling;
                        if (hasACheckbox == "true")
                        {
                            Assert.IsTrue(checkBoxCell.InnerHtml.Contains("<input type=\"checkbox\""));
                        }
                        else
                        {
                            Assert.IsFalse(checkBoxCell.InnerHtml.Contains("<input type=\"checkbox\""));
                        }

                    }
                }
            }
        }
    }
}

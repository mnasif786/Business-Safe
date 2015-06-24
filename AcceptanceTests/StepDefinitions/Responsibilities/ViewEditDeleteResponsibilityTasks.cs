using System.Linq;
using BusinessSafe.AcceptanceTests.StepHelpers;

using TechTalk.SpecFlow;
using NUnit.Framework;
using WatiN.Core;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Threading;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Responsibilities
{
    [Binding]
    public class ViewEditDeleteResponsibilityTasks : BaseSteps
    {
        [BeforeScenario(@"NeedsResponsibilityToViewEditDelete")]
        public void CreateResponsibilityTasksToViewEditDelete()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [Responsibility] ON;");
            sql.Append("INSERT INTO [Responsibility] (" +
                       "[Id], [ResponsibilityCategoryId], [Title], [Description], [SiteId], " +
                       "[ResponsibilityReasonId], [OwnerId], [InitialTaskReoccurringTypeId], [CompanyId], " +
                       "[Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy]) " +
                       "VALUES (-1, 1, N'Responsibility For Testing', N'Responsibility For Testing', 371, " +
                       "1, 'a433e9b2-84f6-4ad7-a89c-050e914dff01', 1, 55881, " +
                       "0, CAST(0x0000A1F500EF9CA0 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A1F500EF9CA0 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c') ");
            sql.AppendLine("SET IDENTITY_INSERT [Responsibility] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        [AfterScenario(@"NeedsResponsibilityToViewEditDelete")]
        public void DeleteResponsibilityTasksToViewEditDelete()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [Responsibility] WHERE Id in (-1);");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        [Given(@"I click to view the first responsibility")]
        public void IClickToViewTheFirstResponsibility()
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitiesGrid"));
            var table = form.Tables.First();
            var row = table.TableRows.First();
            var viewButton = row.TableCells[10].Link(Find.ByTitle("View Responsibility"));
            viewButton.Click();
        }


        [Given(@"I click to delete the first responsibility")]
        public void IClickToDeleteTheFirstResponsibility()
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitiesGrid"));
            var table = form.Tables.First();
            var row = table.TableRows.First();
            var viewButton = row.TableCells[10].Link(Find.ByTitle("Delete Responsibility"));
            viewButton.Click();
        }

        [Given(@"I click to edit the first responsibility")]
        public void IClickToEditTheFirstResponsibility()
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitiesGrid"));
            var table = form.Tables.First();
            var row = table.TableRows.First();
            var viewButton = row.TableCells[10].Link(Find.ByTitle("Edit Responsibility"));
            viewButton.Click();
        }

        [Given("I click confirm")]
        [When("I click confirm")]
        public void IClickConfirm()
        {
            Thread.Sleep(1000);
            var div = WebBrowser.Current.Div(Find.ById("delete-responsibility-dialog"));
            Thread.Sleep(1000);
            var button = (div.NextSibling as Div).Buttons.First();
            Thread.Sleep(1000);
            button.Click();
            Thread.Sleep(1000);
        }

        [Then(@"The first record in the responsibility table should be:")]
        public void TheFirstRecordINTheResponbsibilityTableShouldBe(TechTalk.SpecFlow.Table table)
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitiesGrid"));
            var displayedTable = form.Tables.First();
            var categoryExpected = table.Rows[0]["Category"].Trim();
            var categoryDisplayed = displayedTable.TableRows[0].TableCells[0].Text.Trim();
            var titleExpected = table.Rows[0]["Title"].Trim();
            var titleDisplayed = displayedTable.TableRows[0].TableCells[1].Text.Trim();
            var descriptionExpected = table.Rows[0]["Description"].Trim();
            var descriptionDisplayed = displayedTable.TableRows[0].TableCells[2].Text.Trim();
            var siteExpected = table.Rows[0]["Site"].Trim();
            var siteDisplayed = displayedTable.TableRows[0].TableCells[3].Text.Trim();
            var reasonExpected = table.Rows[0]["Reason"].Trim();
            var reasonDisplayed = displayedTable.TableRows[0].TableCells[4].Text.Trim();
            var responsibilityOwnerExpected = table.Rows[0]["Responsibility Owner"].Trim();
            var responsibilityOwnerDisplayed = displayedTable.TableRows[0].TableCells[5].Text.Trim();
            var statusExpected = table.Rows[0]["Status"].Trim();
            var statusDisplayed = displayedTable.TableRows[0].TableCells[6].Text.Trim();
            var frequencyExpected = table.Rows[0]["Frequency"].Trim();
            var frequencyDisplayed = displayedTable.TableRows[0].TableCells[8].Text.Trim();
            var completionDueDateExpected = table.Rows[0]["Completion Due Date"].Trim();
            var completionDueDatesDisplayed = displayedTable.TableRows[0].TableCells[9].Text.Trim();

            Assert.AreEqual(categoryExpected, categoryDisplayed);
            Assert.AreEqual(titleExpected, titleDisplayed);
            Assert.AreEqual(descriptionExpected, descriptionDisplayed);
            Assert.AreEqual(siteExpected, siteDisplayed);
            Assert.AreEqual(reasonExpected, reasonDisplayed);
            Assert.AreEqual(responsibilityOwnerExpected, responsibilityOwnerDisplayed);
            Assert.AreEqual(statusExpected, statusDisplayed);
            Assert.AreEqual(frequencyExpected, frequencyDisplayed);
            Assert.AreEqual(completionDueDateExpected, completionDueDatesDisplayed);
        }
    }
}

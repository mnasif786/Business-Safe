using System.Linq;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Domain.Entities;
using StructureMap;
using TechTalk.SpecFlow;
using NUnit.Framework;
using WatiN.Core;
using System;
using NHibernate.Linq;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Responsibilities
{
    [Binding]
    public class AddResponsibilityTasks : BaseSteps
    {
        [Given(@"I am on the responsibility page for responsibility with id '(.*)' and company '(.*)'")]
        public void GivenIAmOnTheResponsibilityPageForResponsibilityWithId(int responsibilityId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Responsibilities/Responsibility/Edit?responsibilityId={0}&companyId={1}", responsibilityId, companyId));
            WebBrowser.Current.WaitForComplete(5000);
        }

        [Then(@"responsibility task table should contain the following data:")]
        public void ThenResponsibilityTaskTableShouldConatinTheFollowingData(TechTalk.SpecFlow.Table table)
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitiesTaskGrid"));
            var displayedTable = form.Tables.First();

            if (table.Rows.Count != displayedTable.TableRows.Length)
            {
                Assert.Inconclusive(string.Format("The expected number of rows is {0} but the actual count is {1}. This is probably due to an additonal item that has been created in a previous test.", table.Rows.Count, displayedTable.TableRows.Count()));
            }

            for (var i = 0; i < table.Rows.Count; i++)
            {
                var title = table.Rows[i]["Title"].Trim();
                Assert.AreEqual(title, displayedTable.TableRows[i].TableCells[1].Text.Trim());

                var description = table.Rows[i]["Description"].Trim();
                Assert.AreEqual(description, displayedTable.TableRows[i].TableCells[2].Text.Trim());

                var assignedTo = table.Rows[i]["Assigned To"].Trim();
                Assert.AreEqual(assignedTo, displayedTable.TableRows[i].TableCells[3].Text.Trim());

                var site = table.Rows[i]["Site"].Trim();
                Assert.AreEqual(site, displayedTable.TableRows[i].TableCells[4].Text.Trim());

                var created = table.Rows[i]["Created"].Trim();
                if (created == "DateTime.Now")
                {
                    created = DateTime.Now.ToString("dd/MM/yyyy");
                    Assert.AreEqual(created, displayedTable.TableRows[i].TableCells[5].Text.Trim());
                }
                else
                {
                    Assert.AreEqual(created, displayedTable.TableRows[i].TableCells[5].Text.Trim());
                }

                var dueDate = table.Rows[i]["Due Date"].Trim();
                Assert.AreEqual(dueDate, displayedTable.TableRows[i].TableCells[6].Text.Trim());

                var status = table.Rows[i]["Status"].Trim();
                Assert.AreEqual(status, displayedTable.TableRows[i].TableCells[7].Text.Trim());
            }
        }

        [Then(@"A Responsibility has been created with title '(.*)'")]
        public void ThenAResponsibilityHasBeenCreatedWithTitle(string title)
        {           
            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
                .Query<Responsibility>()
                .Where(x => x.Title == title)
                .Any();
               
            Assert.IsTrue(result);
        }


        [Then(@"The following record should exist in the responsibility table:")]
        public void TheFollowingRecordShouldExistInTheResponsibilityTable(TechTalk.SpecFlow.Table table)
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitiesGrid"));
            var displayedTable = form.Tables.First();
            var foundRow = false;

            for (var i = 0; i < displayedTable.TableRows.Count(); i++)
            {
                var categoryExpected = table.Rows[0]["Category"].Trim();
                var categoryDisplayed = displayedTable.TableRows[i].TableCells[0].Text.Trim();
                var titleExpected = table.Rows[0]["Title"].Trim();
                var titleDisplayed = displayedTable.TableRows[i].TableCells[1].Text.Trim();
                var descriptionExpected = table.Rows[0]["Description"].Trim();
                var descriptionDisplayed = displayedTable.TableRows[i].TableCells[2].Text.Trim();
                var siteExpected = table.Rows[0]["Site"].Trim();
                var siteDisplayed = displayedTable.TableRows[i].TableCells[3].Text.Trim();
                var reasonExpected = table.Rows[0]["Reason"].Trim();
                var reasonDisplayed = displayedTable.TableRows[i].TableCells[4].Text.Trim();
                var responsibilityOwnerExpected = table.Rows[0]["Responsibility Owner"].Trim();
                var responsibilityOwnerDisplayed = displayedTable.TableRows[i].TableCells[5].Text.Trim();
                var statusExpected = table.Rows[0]["Status"].Trim();
                var statusDisplayed = displayedTable.TableRows[i].TableCells[6].Text.Trim();
                var frequencyExpected = table.Rows[0]["Frequency"].Trim();
                var frequencyDisplayed = displayedTable.TableRows[i].TableCells[8].Text.Trim();
                var completionDueDateExpected = table.Rows[0]["Completion Due Date"].Trim();
                var completionDueDatesDisplayed = displayedTable.TableRows[i].TableCells[9].Text.Trim();

                if(categoryExpected == categoryDisplayed
                    && titleExpected == titleDisplayed
                    && descriptionExpected == descriptionDisplayed
                    && siteExpected == siteDisplayed
                    && reasonExpected == reasonDisplayed
                    && responsibilityOwnerExpected == responsibilityOwnerDisplayed
                    && statusExpected == statusDisplayed
                    && frequencyExpected == frequencyDisplayed
                    && completionDueDateExpected == completionDueDatesDisplayed)
                {
                    foundRow = true;
                }
            }

            Assert.IsTrue(foundRow);
        }

        [Then(@"first row does not have title '(.*)'")]
        public void TheFollowingRecordShouldNotExistInTheResponsibilityTable(string title)
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitiesGrid"));
            var displayedTable = form.Tables.First();
            var row = displayedTable.TableRows.First();

            Assert.That(row.TableCells[1].Text.Trim(), Is.Not.EqualTo(title));
        }
    }
}

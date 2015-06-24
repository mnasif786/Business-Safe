using System.Linq;
using System.Threading;
using TechTalk.SpecFlow;
using NUnit.Framework;
using BusinessSafe.AcceptanceTests.StepHelpers;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.HazardousSubstances.Inventory
{
    [Binding]
    public class ViewInventorySteps : BaseSteps
    {
        [Given(@"I am on the hazardous substance inventory page for company '(.*)'")]
        public void GivenIAmOnTheHazardousSubstanceInventoryPageForCompany(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("HazardousSubstanceInventory?companyId={0}", companyId));
        }

        [Then(@"the Hazardous Substances table should contain the following data:")]
        public void ThenTheHazardousSubstancesTableShouldContainTheFollowingData(Table table)
        {
            Thread.Sleep(2000);
            var searchResultsTable = WebBrowser.Current.Tables.First();
            Assert.AreEqual(table.Rows.Count, searchResultsTable.TableRows.Count());

            for (int i = 0; i < table.Rows.Count; i++)
            {

                Assert.AreEqual(table.Rows[i]["Substance Name"], searchResultsTable.TableRows[i].TableCells[1].Text);
                //Assert.AreEqual(table.Rows[i]["Substance Ref"], searchResultsTable.TableRows[i].TableCells[2].Text);
                Assert.AreEqual(table.Rows[i]["Supplier"].Trim(), searchResultsTable.TableRows[i].TableCells[2].Text.Trim());
                Assert.AreEqual(table.Rows[i]["Date Of SDS Sheet"], searchResultsTable.TableRows[i].TableCells[3].Text);
                Assert.AreEqual(table.Rows[i]["Hazard Classification"].Trim(), searchResultsTable.TableRows[i].TableCells[4].Text.Trim());
                Assert.AreEqual(table.Rows[i]["Risk No And Phrases"].Trim(), searchResultsTable.TableRows[i].TableCells[5].Text.Trim());
                Assert.AreEqual(table.Rows[i]["Safety No And Phrases"].Trim(), searchResultsTable.TableRows[i].TableCells[6].Text.Trim());
                Assert.AreEqual(table.Rows[i]["Usage"].Trim(), searchResultsTable.TableRows[i].TableCells[7].Text.Trim());
            }
        }
    }
}
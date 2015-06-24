using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Documents
{
    [Binding]
    public class SearchAddedDocumentsSteps : Steps
    {
        [Then(@"the added document results table should contain the following data:")]
        public void ThenTheAddedDocumentResultsTableShouldContainTheFollowingData(TechTalk.SpecFlow.Table table)
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();
            Assert.AreEqual(table.Rows.Count, searchResultsTable.TableRows.Length);
            int titleColumnIndex = 1;

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string title = table.Rows[i]["Title"];

                Assert.AreEqual(title, searchResultsTable.TableRows[i].TableCells[titleColumnIndex].Text);
            }
        } 
    }
}

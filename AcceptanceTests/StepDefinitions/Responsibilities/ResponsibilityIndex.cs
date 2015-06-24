using System.Linq;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepHelpers;

using TechTalk.SpecFlow;

using WatiN.Core;

using TableRow = WatiN.Core.TableRow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Responsibilities
{
    [Binding]
    public class ResponsibilityIndex : BaseSteps
    {
        const string responsibilitiesGridContainerId = "ResponsibilitiesGrid";
        const int titleCellIndex = 1;
        const int actionButtonsCellIndex = 10;

        [When(@"I click to delete the responsibility with title '(.*)'")]
        public void WhenIClickToDeleteTheResponsibilityWithTitle(string title)
        {

            var row = FindRowWithTitle(title);
            var deleteButton = row.TableCells[actionButtonsCellIndex].Link(Find.ByClass("icon-remove"));
            Thread.Sleep(500);
            deleteButton.Click();
        }

        [Given(@"I click to reinstate the responsibility with title '(.*)'")]
        [When(@"I click to reinstate the responsibility with title '(.*)'")]
        public void WhenIClickToReinstateTheResponsibilityWithTitle(string title)
        {
            var row = FindRowWithTitle(title);
            var reinstateButton = row.TableCells[actionButtonsCellIndex].Link(Find.ByClass("reinstate icon-share"));
            Thread.Sleep(500);
            reinstateButton.Click();
        }

        private static TableRow FindRowWithTitle(string title)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById(responsibilitiesGridContainerId));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var row = table.TableRows.Single(x => x.TableCells[titleCellIndex].Text == title);
            Thread.Sleep(500);
            return row;
        }
    }
}

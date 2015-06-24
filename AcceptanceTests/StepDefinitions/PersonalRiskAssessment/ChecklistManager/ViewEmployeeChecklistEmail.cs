using System;
using System.Linq;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

using WatiN.Core;


namespace BusinessSafe.AcceptanceTests.StepDefinitions.PersonalRiskAssessment.ChecklistManager
{
    [Binding]
    public class ViewEmployeeChecklistEmail : BaseSteps
    {
        [When(@"I click on the view employee checklist email for first checklist")]
        public void WhenIClickOnTheViewEmployeeChecklistEmailForSChecklist()
        {
            var gridContainer = WebBrowser.Current.Div(Find.ById("EmployeeChecklists"));
            var table = gridContainer.Tables.First();
            var requestedRow = table.TableRows.FirstOrDefault();

            Assert.IsNotNull(requestedRow, string.Format("Could not find any rows"));

            var viewButton = requestedRow.TableCells.LastOrDefault().Link(Find.ByClass("icon-search"));

            Assert.IsNotNull(viewButton, "Could not find corresponding view link");

            viewButton.Click();
        }

        [When(@"I click on the view employee checklist email for id '(.*)'")]
        public void WhenIClickOnTheViewEmployeeChecklistForId(Guid id)
        {
            var gridContainer = WebBrowser.Current.Div(Find.ById("EmployeeChecklists"));
            var table = gridContainer.Tables.First();
            
            foreach(var row in table.TableRows)
            {
                var link = (row as WatiN.Core.TableRow).TableCellsDirectChildren[7].Links[0];

                if (link.GetAttributeValue("data-id").ToLower() == id.ToString().ToLower())
                {
                    link.Click();
                    return;
                }
            }

            throw new Exception("Could not find link");
        }

        [Then(@"The checklist link has a url of '(.*)'")]
        public void TheChecklistLinkHasAUsrlOf(string url)
        {
            var link = WebBrowser.Current.Link(Find.ById("ChecklistUrl"));
            var href = link.GetAttributeValue("href");
            Assert.That(href.Contains(url));
        }
    }
}

using System.Linq;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.BUGS
{
    [Binding]
    public class _1671BugSteps:BaseSteps
    {
        [Given(@"I am working on Hazards")]
        public void GivenIAmWorkingOnHazards()
        {
            var secondTabLink = WebBrowser.Current.Links.First(x => x.InnerHtml == "Risk Assessments");
            secondTabLink.Click();

            ScenarioContextHelpers.SetCompanyDefaultFormWorkingOnName("hazards");
        }


        [Then(@"should not be able to edit system defaults")]
        public void ThenShouldNotBeAbleToEditSystemDefaults()
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var table = form.Table(Find.First());
            var lastRow = table.TableRows[table.TableRows.Length - 1];
            var lastCell = lastRow.TableCells.Last();
            Assert.That(lastCell.Links.Length == 0);
        }

        [Then(@"should not be able to delete system defaults")]
        public void ThenShouldNotBeAbleToDeleteSystemDefaults()
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var table = form.Table(Find.First());
            var lastRow = table.TableRows[table.TableRows.Length - 1];
            var lastCell = lastRow.TableCells.Last();
            Assert.That(lastCell.Links.Length == 0);
        }

    }
}

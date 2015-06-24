using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ResponsibilityPlanner
{
    [Binding]
    public class CompleteResponsibilityTaskSteps : BaseSteps
    {
        [Given(@"User opens the Close Task Page for FirstCreatedResponsibilityTaskId")]
        [When(@"User opens the Close Task Page for FirstCreatedResponsibilityTaskId")]
        public void WhenUserOpensTheCloseTaskPageForId1()
        {
            var responsibilityTaskId = ScenarioContextHelpers.GetFirstCreatedResponsibilityTaskId();
            WebBrowser.Current.Link(Find.ByElement(x => x.GetAttributeValue("data-id") == responsibilityTaskId.ToString())).Click();
            ScenarioContextHelpers.SetResponsibilityTaskId(responsibilityTaskId);
        }

        [Then(@"Then Complete Responsibility Task page should open correctly")]
        public void ThenThenPageShouldOpenCorrectly()
        {
            Thread.Sleep(2000);
            var responsibilityTaskId = ScenarioContextHelpers.GetResponsibilityTaskId();
            var hiddenTaskIdField = WebBrowser.Current.TextField(Find.ById(x => x == "hiddentaskid"));
            Assert.That(hiddenTaskIdField.Exists, Is.True);
            Assert.That(hiddenTaskIdField.Value, Is.EqualTo(responsibilityTaskId.ToString()));
            
        }
    }
}

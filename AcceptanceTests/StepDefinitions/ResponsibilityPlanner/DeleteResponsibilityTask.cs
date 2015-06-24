using System.Text.RegularExpressions;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ResponsibilityPlanner
{
    [Binding]
    public class DeleteResponsibilityTask : BaseSteps
    {
        [When(@"User clicks on Delete Task button for Id FirstCreatedResponsibilityTaskId")]
        public void WhenUserClicksOnDeleteTaskButton()
        {
            var responsibilityTaskId = ScenarioContextHelpers.GetFirstCreatedResponsibilityTaskId();
            WebBrowser.Current.Link(
                Find.ByElement(
                    x =>
                        x.GetAttributeValue("data-id") == responsibilityTaskId.ToString() &&
                            x.ClassName == "gridIcon iconDelete")).Click();
        }

        [Then(@"Modal confirmation should open correctly")]
        public void ThenModalConfirmationShouldOpenCorrectly()
        {
            Div popup = WebBrowser.Current.Div(Find.ById(new Regex("deleteTaskConfirm$")));
            Assert.That(WatiNHelper.IsDisplayed(popup), Is.True);

            WebBrowser.Current.Close();
        }

        [Given(@"Modal confirmation is open for id SecondCreatedResponsibilityTaskId")]
        public void GivenModalConfirmationIsOpenForId2()
        {
            var responsibilityTaskId = ScenarioContextHelpers.GetSecondCreatedResponsibilityTaskId();
            WebBrowser.Current.Link(
                Find.ByElement(
                    x =>
                        x.GetAttributeValue("data-id") == responsibilityTaskId.ToString() &&
                            x.ClassName == "gridIcon iconDelete")).Click();
            WebBrowser.Driver.CaptureScreenShot("GivenModalConfirmationIsOpenForId2");
        }

        [When(@"User clicks on cancel button")]
        public void WhenUserClicksOnCancelButton()
        {
            var button =
                WebBrowser.Current.Div(d => d.ClassName == "ui-dialog-buttonset").Button(
                    b => b.InnerHtml.Contains("Cancel"));
            WebBrowser.Driver.CaptureScreenShot("WhenUserClicksOnCancelButton");
            button.Click();
        }

        [Then(@"Modal confirmation should close")]
        public void ThenModalConfirmationShouldClose()
        {
            Div popup = WebBrowser.Current.Div(Find.ById(new Regex("deleteTaskConfirm$")));
            Assert.That(WatiNHelper.IsDisplayed(popup), Is.False);
            WebBrowser.Driver.CaptureScreenShot("ThenModalConfirmationShouldClose");
            WebBrowser.Current.Close();
        }
      
    }
}
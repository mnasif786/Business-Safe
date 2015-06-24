using System.Linq;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ValidationMessageSteps: Steps
    {
        [Then(@"the '(.*)' error message is displayed")]
        public void ThenTheErrorMessageIsDisplayed(string errorMessage)
        {
            var div = WebBrowser.Current.Divs.FirstOrDefault(d => d.ClassName == "validation-summary-errors");
            Assert.True(div.InnerHtml.Contains(errorMessage), string.Format("Error message '{0}' not displayed", errorMessage));
        }

        [Then(@"the '(.*)' error message is not displayed")]
        public void ThenTheErrorMessageIsNotDisplayed(string errorMessage)
        {
            var div = WebBrowser.Current.Divs.FirstOrDefault(d => d.ClassName == "validation-summary-errors");
            Assert.False(div.InnerHtml.Contains(errorMessage), string.Format("Error message '{0}' displayed", errorMessage));
        }

        [Then(@"the notice '(.*)' should be displayed")]
        public void ThenTheNoticeShouldBeDisplayed(string message)
        {
            var div = WebBrowser.Current.Divs.FirstOrDefault(d => d.ClassName == "alert alert-success");
            Assert.True(div.InnerHtml.Contains(message), string.Format("Message '{0}' not displayed", message));
        }

    }
}
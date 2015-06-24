using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Fire_Risk_Assessment.Premises_Information
{
    [Binding]
    public class SleepingAccommodationSteps
    {
        [Given(@"The javascript for clicking sleeping accommodation provided yes is fired")]
        [When(@"The javascript for clicking sleeping accommodation provided yes is fired")]
        public void TheJavascriptForClickingSleepingAccommodationProvidedYesIsFired()
        {
            WebBrowser.Current.Eval("$(\"#PremisesProvidesSleepingAccommodationTrue\").click();");
        }

        [Given(@"The javascript for clicking sleeping accommodation provided no is fired")]
        [When(@"The javascript for clicking sleeping accommodation provided no is fired")]
        public void TheJavascriptForClickingSleepingAccommodationProvidedNoIsFired()
        {
            WebBrowser.Current.Eval("$(\"#PremisesProvidesSleepingAccommodationFalse\").click();");
        }

        [Then(@"I am redirected to the index page for the fire risk assessment for risk assessment id '(.*)' and company id '(.*)'")]
        public void ThenIAmRedirectedToTheIndexPageForTheFireRiskAssessment(long riskAssessmentId, long companyId)
        {
            var currentUrl = WebBrowser.Current.Url.ToLower();
            var expectedUrlPortion = string.Format("fireriskassessments?riskassessmentid={0}&companyid={1}", riskAssessmentId, companyId);
            Assert.That(currentUrl.Contains(expectedUrlPortion), string.Format("URL {0}, does not contain {1}", currentUrl, expectedUrlPortion));
        }
    }
}

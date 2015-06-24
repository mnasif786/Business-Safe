using System;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class GeneralRiskAssessmentSummarySteps : BaseSteps
    {
        [Then(@"I am redirected to the summary page for the new risk assessment")]
        public void ThenIAmRedirectedToTheSummaryPageForTheNewRiskAssessment()
        {
            var currentUrl = WebBrowser.Current.Url.ToLower();
            var expectedUrlPortion = "/GeneralRiskAssessments/summary?riskAssessmentId=".ToLower();
            Assert.That(currentUrl.Contains(expectedUrlPortion), string.Format("URL {0}, does not contain {1}", currentUrl, expectedUrlPortion));
        }
    }
}

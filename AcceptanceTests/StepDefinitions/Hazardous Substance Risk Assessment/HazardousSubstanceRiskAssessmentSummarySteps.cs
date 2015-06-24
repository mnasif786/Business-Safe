using System;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment
{
    [Binding]
    public class HazardousSubstanceRiskAssessmentSummarySteps
    {
        [Then(@"I am redirected to the summary page for the new hazardous substance risk assessment")]
        public void ThenIAmRedirectedToTheSummaryPageForTheNewHazardousSubstanceRiskAssessment()
        {
            var currentUrl = WebBrowser.Current.Url.ToLower();
            var expectedUrlPortion = "/HazardousSubstanceRiskAssessments/summary?hazardoussubstanceriskAssessmentId=".ToLower();
            Assert.That(currentUrl.Contains(expectedUrlPortion), string.Format("URL {0}, does not contain {1}", currentUrl, expectedUrlPortion));
     
        }
    }
}

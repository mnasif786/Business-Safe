using System;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment;
using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

using WatiN.Core;

using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Inventory
{
    [Binding]
    public class InventoryItemHasLinkBackToAssociatedRiskAssessmentSteps : BaseSteps
    {
        [When(@"I click on the view risk assessment for the hazardous substance with id '(.*)'")]
        public void WhenIClickOnTheViewRiskAssessmentForTheHazardousSubstanceWithId(int hazardousSubstanceId)
        {
            var table = WebBrowser.Current.Div(Find.ById("HazardousSubstancesGrid")).Tables.First();
            Assert.IsNotNull(table, "Could not find div#HazardousSubstancesGrid table");
            var linkFound = false;
            foreach(Element link in table.Links)
            {
                if(link.GetValue("data-id") == hazardousSubstanceId.ToString())
                {
                    linkFound = true;
                    var theLink = link as Link;
                    theLink.Click();
                    break;
                }
            }
            Assert.IsTrue(linkFound, string.Format("Could not find view associated risk assessment for hazardous substance with id {0}",
                        hazardousSubstanceId));
        }

        [Then(@"the hazardous substance risk assessment id is '(.*)'")]
        public void ThenTheHazardousSubstanceRiskAssessmentIdIs(int riskAssessmentId)
        {
            Element input = WebBrowser.Current.Element(Find.ById("RiskAssessmentId"));
            Assert.That(input.GetValue("value"), Is.EqualTo(riskAssessmentId.ToString()), string.Format("Could not find element #HazardousSubstanceRiskAssessmentId with value of {0}", riskAssessmentId));
        }
    }
}

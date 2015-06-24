using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.CompanyDefaults
{
    [Binding]
    public class RiskAssessorsSteps : BaseSteps
    {
        [Given(@"I am on the risk assessors tab")]
        public void GivenIAmOnTheRiskAssessorsTab()
        {
            new FormSteps().GivenIPressLinkWithId("risk-assessors");
        }
    }
}

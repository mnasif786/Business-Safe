using System;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.PersonalRiskAssessment.ChecklistGenerator
{
    [Binding]
    public class SendToSingleEmployeeSteps : BaseSteps
    {
        [Given(@"I click the singleEmployee radiobutton")]
        public void GivenIClickTheSingleEmployeeRadiobutton()
        {
            WebBrowser.Current.Eval("$('#singleEmployee').click();");
        }
    }
}

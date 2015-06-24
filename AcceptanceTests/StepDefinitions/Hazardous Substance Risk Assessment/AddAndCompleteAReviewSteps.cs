using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment
{
    [Binding]
    public class AddAndCompleteAReviewSteps : BaseSteps
    {
        [Given(@"I click on the complete review icon")]
        public void IHaveClickedOnTheCompleteReviewIcon()
        {
            WebBrowser.Current.Links.Filter(x => x.ClassName == "completeReview icon-ok").First().Click();
        }
    }
}

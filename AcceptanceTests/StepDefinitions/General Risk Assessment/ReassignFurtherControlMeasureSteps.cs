using System.Linq;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class ReassignFurtherControlMeasureSteps : BaseSteps
    {
        [Given(@"I press 'Reassign' button on the further action task row")]
        public void WhenIPressReassignButtonOnTheFurtherActionTaskRow()
        {
            Thread.Sleep(2000);
            var furtherActionTaskRow = ScenarioContextHelpers.GetFurtherActionTaskRow();


            var button = furtherActionTaskRow.Elements.Filter(Find.ByClass(ElementCssHelper.ClassFor(Elements.FcmReassignButton))).FirstOrDefault();

            Assert.IsNotNull(button, "Could not find re assign button");

            button.Click();
        }
    }
}

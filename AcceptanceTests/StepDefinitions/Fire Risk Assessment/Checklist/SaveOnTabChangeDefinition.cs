using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Fire_Risk_Assessment.Checklist
{
    [Binding]
    public class SaveOnTabChangeDefinition
    {
        [Given("I click '(.*)' for radio button for question with index '(.*)' on section '(.*)'")]
        [When("I click '(.*)' for radio button for question with index '(.*)' on section '(.*)'")]
        public void WhenIPressAdd(string val, long questionIndex, long sectionId)
        {
            var section = WebBrowser.Current.Element(Find.ById("Section_" + sectionId.ToString()));
            //TODO: finish
        }
    }
}

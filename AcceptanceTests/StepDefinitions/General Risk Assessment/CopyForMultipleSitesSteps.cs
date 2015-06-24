using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using WatiN.Core;
using BusinessSafe.AcceptanceTests.StepHelpers;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class CopyForMultipleSitesSteps
    {
        [Given(@"I click on the copy button for risk assessment with id '(.*)'")]
        public void GivenIClickOnTheCopyButtonForRiskAssessmentWithId(int id)
        {
            var editButtons = WebBrowser.Current.Links.Filter(Find.ByClass("copy-risk-assessment-link icon-repeat"));
            foreach (Link link in editButtons)
            {
                var dataId = link.GetAttributeValue("data-id");
                if (dataId == id.ToString())
                {
                    link.Click();
                    break;
                }
            }
        }

        [Given(@"I check site with id '(.*)'")]
        public void GivenICheckSiteWithId (int id)
        {
            var checkBoxes = WebBrowser.Current.CheckBoxes.Filter(Find.ByClass("siteMultiSelectCheckbox")).Filter(Find.ByValue(id.ToString()));
            checkBoxes.First().Click();
        }

        [When(@"I click confirm multiple copy")]
        public void WhenIClickConfirmMultipleCopy()
        {
            var confirm = WebBrowser.Current.Button(Find.ByText("Confirm"));
            //var form = WebBrowser.Current.Form(Find.ById("formCopyMultipleSitesRiskAssessment"));
            //var confirm = form.Button(Find.ByClass("btn btn-primary"));
            confirm.Click();
        }
    }
}

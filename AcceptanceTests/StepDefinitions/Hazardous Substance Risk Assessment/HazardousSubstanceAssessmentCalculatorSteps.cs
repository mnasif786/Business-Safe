using System;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment
{
    [Binding]
    public class HazardousSubstanceAssessmentCalculatorSteps : BaseSteps
    {
        [Then(@"the work approach is '(.*)'")]
        public void ThenTheWorkApproachIs(string expectedWorkApproach)
        {
            var workApproach = WebBrowser.Current.Element(Find.ById("WorkApproach"));
            Assert.IsNotNull(workApproach, "Could not find element with id 'WorkApproach'");
            Assert.That(workApproach.InnerHtml, Is.EqualTo(expectedWorkApproach));
        }
        
        [Then(@"the GuidanceNotes href is '(.*)'")]
        public void ThenTheGuidanceNotesHrefIs(string expectedHref)
        {
            var guidanceNotesLink = WebBrowser.Current.Link(Find.ById("GuidanceNotes"));
            Assert.IsNotNull(guidanceNotesLink, "Could not find element with id 'GuidanceNotes'");
            Assert.That(guidanceNotesLink.Url, Is.EqualTo(expectedHref));
        }
    }
}

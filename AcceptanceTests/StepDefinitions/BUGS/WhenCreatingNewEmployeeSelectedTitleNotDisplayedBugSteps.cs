using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using TechTalk.SpecFlow;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.BUGS
{
    [Binding]
    public class BugWhenCreatingNewEmployeeSelectedTitleNotDisplayed : BaseSteps
    {
        [Then(@"the previously selected Title should be displayed")]
        public void ThenThePreviouslySelectedTitleShouldBeDisplayed()
        {
            // test html select menu - this doesn't capture the any selected options in IE!
            Thread.Sleep(2000);
            var dropDownList = WebBrowser.Current.SelectList(Find.ById("NameTitle"));
            Assert.That(dropDownList.SelectedOption.Value, Is.EqualTo("Mr"));

            // test ui decoration
            //Element selectedValue = WebBrowser.Current.Elements.Filter(x => x.ClassName == "ui-selectmenu-status" && x.Parent.Id == "NameTitle-button").FirstOrDefault();
            //Assert.That(selectedValue.InnerHtml, Is.EqualTo("Mr"));
        }
    }
}

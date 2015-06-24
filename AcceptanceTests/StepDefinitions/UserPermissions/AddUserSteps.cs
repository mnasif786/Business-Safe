using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.UserPermissions
{
    [Binding]
    public class AddUserSteps : BaseSteps
    {
        [Then(@"the validation field is visible with message '(.*)'")]
        public void ThenTheValidationFieldIsVisibleWithMessage(string message)
        {
            var validationDiv = WebBrowser.Current.Div(Find.ByClass("validation-summary-errors"));
            Assert.IsTrue(validationDiv.Exists);
            Assert.That(validationDiv.InnerHtml.Contains(message));
        }
    }
}

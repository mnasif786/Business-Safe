using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ResponsibilityPlanner
{
    [Binding]
    public class NotifyPeninsulaClientSteps: BaseSteps
    {
        [Given(@"The user is on Company Details Page")]
        public void GivenTheUserIsOnCompanyDetailsPage()
        {
            GotoCompanyDetailsPage(55881);
        }
        
        [When(@"User click on (.*) button with id (.*)")]
        public void WhenUserClickOnSaveButton(string buttonName, string buttonId)
        {
            var notifyAdminButton = WebBrowser.Current.Button(Find.ById(buttonId.Trim()));
            Assert.That(notifyAdminButton.Exists, Is.True);
            notifyAdminButton.Click();
        }

        [Then(@"Then an email is sent to admin")]
        public void ThenThenAnEmailIsSentToAdmin()
        {
            WebBrowser.Current.WaitForComplete();
            var findSuccessDiv = WebBrowser.Current.Div(Find.ByClass("alert alert-success"));
            Assert.That(findSuccessDiv.Exists, Is.True);
        }

        [Given(@"textboxes on the page are invisible and labels are visible")]
        public void GivenTextboxesOnThePageAreInvisibleAndLabelsAreVisible()
        {
            VerifyTextBoxAreInvisibleAndLabelsAreVisible();
        }

        [Then(@"textboxes on the page are invisible and labels are visible")]
        public void ThenTextboxesOnThePageAreInvisibleAndLabelsAreVisible()
        {
            VerifyTextBoxAreInvisibleAndLabelsAreVisible();
        }

        private static void VerifyTextBoxAreInvisibleAndLabelsAreVisible()
        {
            var companyNametextBox = WebBrowser.Current.TextField(Find.ById("CompanyName"));
            var address1TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine1"));
            var address2TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine2"));
            var address3TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine3"));
            var address4TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine4"));
            var postcodeTextBox = WebBrowser.Current.TextField(Find.ById("PostCode"));
            var telephoneTextBox = WebBrowser.Current.TextField(Find.ById("Telephone"));
            var websiteTextBox = WebBrowser.Current.TextField(Find.ById("Website"));
            var maincontactTextBox = WebBrowser.Current.TextField(Find.ById("MainContact"));

            Assert.That(companyNametextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(address1TextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(address2TextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(address3TextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(address4TextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(postcodeTextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(telephoneTextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(websiteTextBox.ClassName, Is.EqualTo("hide"));
            Assert.That(maincontactTextBox.ClassName, Is.EqualTo("hide"));
        }

        [Then(@"labels are made invisible and textboxes are made visible")]
        public void ThenThenLabelsAreMadeInvisibleAndTextboxesAreMadeVisible()
        {
            var companyNametextBox = WebBrowser.Current.TextField(Find.ById("CompanyName"));
            var address1TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine1"));
            var address2TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine2"));
            var address3TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine3"));
            var address4TextBox = WebBrowser.Current.TextField(Find.ById("AddressLine4"));
            var postcodeTextBox = WebBrowser.Current.TextField(Find.ById("PostCode"));
            var telephoneTextBox = WebBrowser.Current.TextField(Find.ById("Telephone"));
            var websiteTextBox = WebBrowser.Current.TextField(Find.ById("Website"));
            var maincontactTextBox = WebBrowser.Current.TextField(Find.ById("MainContact"));

            Assert.That(companyNametextBox.ClassName, Is.EqualTo(null));
            Assert.That(address1TextBox.ClassName, Is.EqualTo(null));
            Assert.That(address2TextBox.ClassName, Is.EqualTo(null));
            Assert.That(address3TextBox.ClassName, Is.EqualTo(null));
            Assert.That(address4TextBox.ClassName, Is.EqualTo(null));
            Assert.That(postcodeTextBox.ClassName, Is.EqualTo(null));
            Assert.That(telephoneTextBox.ClassName, Is.EqualTo(null));
            Assert.That(websiteTextBox.ClassName, Is.EqualTo(null));
            Assert.That(maincontactTextBox.ClassName, Is.EqualTo(null));
        }

        [When(@"User remove the text from text box with id (.*)")]
        public void WhenUserRemoveTheTextFromTextBoxWithIdAddressLine1(string textBoxId)
        {
            var textBox = WebBrowser.Current.TextField(Find.ById(textBoxId.Trim()));

            ScenarioContext.Current.Add("AddressLine1", textBox.Text);

            textBox.Clear();
        }

        [Then(@"Error message is displayed")]
        public void ThenErrorMessageIsDisplayed()
        {
            WebBrowser.Current.WaitForComplete();

            var validationSummary = WebBrowser.Current.Div(Find.ByClass("validation-summary-errors"));
            
            Assert.That(validationSummary.Exists, Is.True);
        }

        [Then(@"textboxes display original values")]
        public void ThenTextboxesDisplayOriginalValues()
        {
            var textBox = WebBrowser.Current.TextField(Find.ById("AddressLine1"));
            var originalvalue = ScenarioContext.Current.Get<string>("AddressLine1");

            Assert.That(textBox.Text, Is.EqualTo(originalvalue));
        }

        [When(@"click the cancel link")]
        public void WhenClickTheCancelLink()
        {
            WebBrowser.Current.Link(Find.ByText("Cancel")).Click();
        }

    }
}

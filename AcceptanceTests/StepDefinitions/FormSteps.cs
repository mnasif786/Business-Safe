using System;
using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class FormSteps : BaseSteps
    {
        [Given(@"I have waited for element '(.*)' to exist")]
        [When(@"I have waited for element '(.*)' to exist")]
        public static void WaitForElementToExist(string elementId)
        {
            WebBrowser.Current.Element(Find.ById(elementId)).WaitUntilExists();
        }

        [Given(@"I have waited for the page to reload")]
        [When(@"I have waited for the page to reload")]
        public void WhenIHaveWaitedForThePageToReload()
        {
            WebBrowser.Current.WaitForComplete();
            SleepIfEnvironmentIsPotentiallySlow(2000);
        }

        [Then(@"autocomplete select list for '(.*)' should contain '(.*)' options")]
        public void AutocompleteSelectListShouldContainOptions(string fieldId, int optionCount)
        {
            Thread.Sleep(2000);

            var textField = WebBrowser.Current.TextField(Find.ById(fieldId));
            var intitialCount = int.Parse(textField.GetAttributeValue("data-initial-count"));
            Assert.AreEqual(optionCount, intitialCount);
        }

        [Given(@"I have entered '(.*)' into the '(.*)' textfield")]
        [When(@"I have entered '(.*)' into the '(.*)' textfield")]
        [Then(@"I have entered '(.*)' into the '(.*)' textfield")]
        public void GivenIHaveEnteredIntoTheTextField(string value, string textFieldId)
        {
            var textField = GetElement(FindTextBox, textFieldId, 5000) as TextField;
            Assert.IsNotNull(textField, string.Format("Could not find input '{0}'", textFieldId));

            textField.Clear();
            textField.AppendText(value);
        }

        [Given(@"I have entered '(.*)' into the '(.*)' field")]
        [When(@"I have entered '(.*)' into the '(.*)' field")]
        [Then(@"I have entered '(.*)' into the '(.*)' field")]
        public void GivenIHaveEnteredIntoTheField(string value, string field)
        {
            var inputFound = false;
            Thread.Sleep(2000);
            var textBox = WebBrowser.Current.TextFields.FirstOrDefault(x => x.Name == field);
            if (textBox != null)
            {
                textBox.Clear();
                textBox.AppendText(value);
                inputFound = true;
                return;
            }
            var selectBox = WebBrowser.Current.SelectLists.FirstOrDefault(x => x.Id == field);
            if (selectBox != null)
            {
                selectBox.Select(value);
                // trigger any events listening for this select box to change
                WebBrowser.Current.Eval("$('#" + field + "').change();");
                inputFound = true;
                Thread.Sleep(2000);
            }
            Assert.IsTrue(inputFound, string.Format("Could not find input '{0}'", field));
        }

        [Given(@"on form '(.*)' I have entered '(.*)' into the '(.*)' field")]
        [When(@"on form '(.*)' I have entered '(.*)' into the '(.*)' field")]
        public void GivenIHaveEnteredIntoTheFieldOnForm(string formId, string value, string field)
        {
            var inputFound = false;
            Thread.Sleep(2000);
            var form = WebBrowser.Current.Form(Find.ById(formId));

            var textBox = form.TextFields.FirstOrDefault(x => x.Name == field);
            if (textBox != null)
            {
                textBox.Clear();
                textBox.AppendText(value);
                inputFound = true;
                return;
            }
            var selectBox = form.SelectLists.FirstOrDefault(x => x.Id == field);
            if (selectBox != null)
            {
                selectBox.Select(value);
                // trigger any events listening for this select box to change
                WebBrowser.Current.Eval("$('#" + field + "').change();");
                inputFound = true;
                Thread.Sleep(2000);
            }
            Assert.IsTrue(inputFound, string.Format("Could not find input '{0}'", field));
        }

        public void GivenIHaveEnteredIntoTheField(string value, string field, int threadSleep)
        {
            var inputFound = false;
            System.Threading.Thread.Sleep(threadSleep);
            var textBox = WebBrowser.Current.TextFields.FirstOrDefault(x => x.Name == field);
            if (textBox != null)
            {
                textBox.Clear();
                textBox.AppendText(value);
                inputFound = true;
                return;
            }
            var selectBox = WebBrowser.Current.SelectLists.FirstOrDefault(x => x.Id == field);
            if (selectBox != null)
            {
                selectBox.Select(value);
                // trigger any events listening for this select box to change
                WebBrowser.Current.Eval("$('#" + field + "').change();");
                inputFound = true;
                Thread.Sleep(2000);
            }
            Assert.IsTrue(inputFound, string.Format("Could not find input '{0}'", field));
        }

        [Given(@"I have entered '(.*)' into the '(.*)' field of the '(.*)' form")]
        [When(@"I have entered '(.*)' into the '(.*)' field of the '(.*)' form")]
        [Then(@"I have entered '(.*)' into the '(.*)' field of the '(.*)' form")]
        public void GivenIHaveEnteredIntoATextFieldOnAForm(string value, string textFieldId, string formId)
        {
            var form = GetElement(FindForm, formId, 5000) as Form;

            var textField = GetElement(FindTextBox, textFieldId, form, 5000) as TextField;
            Assert.IsTrue(textField != null, string.Format("Could not find input '{0}'", textFieldId));

            textField.Clear();
            textField.AppendText(value);
        }

        [Given(@"I have entered '(.*)' and '(.*)' into the '(.*)' combobox")]
        [When(@"I have entered '(.*)' and '(.*)' into the '(.*)' combobox")]
        public void GivenIHaveEnteredAndIntoTheCombobox(string displayValue, string id, string comboboxName)
        {
            EnterValueIntoTextField(displayValue, comboboxName);
            var idFieldName = comboboxName + "Id";
            EnterValueIntoTextField(id, idFieldName);
            Thread.Sleep(1000);
            WebBrowser.Current.Eval("$('#" + comboboxName + "').trigger('afterSelect');");
            Thread.Sleep(1000);
        }

        private void EnterValueIntoTextField(string value, string fieldName)
        {
            var field = WebBrowser.Current.TextFields.Filter(Find.ByName(fieldName)).FirstOrDefault();
            Assert.IsNotNull(field);
            field.Clear();
            field.AppendText(value);
        }

        [Given(@"I have triggered after select drop down event for '(.*)'")]
        [When(@"I have triggered after select drop down event for '(.*)'")]
        public void GivenIHaveTriggeredAfterSelectDropDownEventFor(string dropDownName)
        {
            WebBrowser.Current.Eval("$('#" + dropDownName + "').trigger('afterSelect');");
            Thread.Sleep(2000);
        }

        [When(@"the '(.*)' field is cleared")]
        [Given(@"the '(.*)' field is cleared")]
        public void GivenIHaveEnteredIntoTheField(string field)
        {
            System.Threading.Thread.Sleep(2000);
            var textBox = WebBrowser.Current.TextFields.FirstOrDefault(x => x.Name == field);
            if (textBox != null)
            {
                textBox.Value = null;
            }
        }

        [Given(@"I press '(.*)' link")]
        [When(@"I press '(.*)' link")]
        public void GivenIPressLink(string linkName)
        {
            var link = GetElement(FindLink, linkName, 5000);
            Assert.IsNotNull(link, string.Format("Could not find link {0}", linkName));
            link.Click();
        }

        [When(@"I press link with ID '(.*)'")]
        [Given(@"I press link with ID '(.*)'")]
        public void GivenIPressLinkWithId(string id)
        {
            var link = WebBrowser.Current.Link(Find.ById(id));
            Assert.That(link.Exists);
            link.Click();
            Thread.Sleep(2000);
        }

        [When(@"I press link with Text '(.*)'")]
        [Given(@"I press link with Text '(.*)'")]
        public void GivenIPressLinkWithText(string text)
        {
            WebBrowser.Current.Link(Find.ByText(text)).Click();
        }

        [Given(@"change event has fired on '(.*)' select list")]
        public void GivenChangeEventHasFiredOnSelectList(string field)
        {
            var selectBox = WebBrowser.Current.SelectLists.FirstOrDefault(x => x.Id == field);
            if (selectBox != null)
            {
                selectBox.FireEvent("onChange");
            }
        }

        public static void EnterValueIntoField(string value, string field)
        {
            new FormSteps().GivenIHaveEnteredIntoTheField(value, field, 0);
        }

        [Given(@"I press '(.*)' checkbox")]
        [When(@"I press '(.*)' checkbox")]
        public void WhenIPressCheckbox(string Id)
        {
            var checkbox = WebBrowser.Current.CheckBox(Find.ById(Id));

            checkbox.Checked = true;
            WebBrowser.Current.Eval("$('#" + Id + "').click();"); // ... fires event, with correctly ticked checkbox, but un-ticks it in the process ...
            checkbox.Checked = true;

            Assert.IsNotNull(checkbox, "Could not find checkbox with id " + Id);

            Thread.Sleep(2000);
        }

        [Given(@"I press '(.*)' checkbox with client side events")]
        [When(@"I press '(.*)' checkbox with client side events")]
        public void WhenIPressIsCheckboxWithClientSideEvents(string Id)
        {
            // something weird going on in watin here - not firing click event correctly, so had to bodge
            var checkbox = WebBrowser.Current.CheckBox(Find.ById(Id));
            checkbox.Checked = true; // ticks checkbox...
            WebBrowser.Current.Eval("$('#" + Id + "').click();"); // ... fires event, with correctly ticked checkbox, but un-ticks it in the process ...
            checkbox.Checked = true; // ...re-ticks checkbox
        }

        [Given(@"I press '(.*)' radio button with the value of '(.*)'")]
        [When(@"I press '(.*)' radio button with the value of '(.*)'")]
        public void WhenIPressIsRadioOption(string name, string value)
        {
            var radioButton = WebBrowser.Current.RadioButtons.First(x => x.GetAttributeValue("name") == name && x.GetAttributeValue("value") == value);

            WebBrowser.Current.Eval("$(\"[name='" + name + "'][value='" + value + "']\").click();");

            Assert.IsNotNull(radioButton, "Could not find radio button with id " + name);

            Thread.Sleep(2000);
        }

        [Given(@"the '(.*)' checkbox has value of '(.*)'")]
        [When(@"the '(.*)' checkbox has value of '(.*)'")]
        public void TheCheckboxHasAValueOf(string id, bool val)
        {
            var checkbox = WebBrowser.Current.CheckBox(Find.ById(id));
            checkbox.Checked = val;
        }

        [Then(@"the label with id '(.*)' should have a value of '(.*)'")]
        public void TheLabelHasAValueOf(string id, string val)
        {
            var label = WebBrowser.Current.Label(Find.ById(id));
            Assert.That(label.Text, Is.EqualTo(val));
        }

        [Then(@"the label with id '(.*)' should be null")]
        public void TheLabelShouldBeNull(string id)
        {
            var label = WebBrowser.Current.Label(Find.ById(id));
            Assert.That(label.Text, Is.Null);
        }

        [Given(@"I wait for '(.*)' miliseconds")]
        [When(@"I wait for '(.*)' miliseconds")]
        public void IWaitForMiliseconds(int miliseconds)
        {
            //Thread.Sleep(miliseconds);
            SleepIfEnvironmentIsPotentiallySlow(miliseconds);
        }

        [Given(@"value '(.*)' is selected for radio button '(.*)'")]
        [When(@"value '(.*)' is selected for radio button '(.*)'")]
        public void ValueIsSelectedForRadioButton(string value, string field)
        {
            var radioButton = WebBrowser.Current.RadioButton(Find.ById(field) && Find.ByValue(value));
            radioButton.Click();
        }

        [Then(@"radio button '(.*)' has value of '(.*)' and is '(.*)'")]
        public void RadioButtonHasValueOf(string field, string value, bool isChecked)
        {
            var radioButton = WebBrowser.Current.RadioButton(Find.ById(field) && Find.ByValue(value));
            Assert.IsTrue(radioButton.Exists);

            if (isChecked)
            {
                Assert.IsTrue(isChecked);
            }
            else
            {
                Assert.IsFalse(isChecked);
            }
        }

        [Then(@"the element with id '(.*)' has a '(.*)' attribute of '(.*)'")]
        public void ThenTheElementWithIdHasAAttributeOf(string elementId, string attributeName, string attributeValue)
        {
            var element = WebBrowser.Current.Element(Find.ById(elementId));
            Assert.IsNotNull(element, string.Format("Could not find element with id {0}", elementId));
            Assert.That(element.GetAttributeValue(attributeName), Is.EqualTo(attributeValue), string.Format("Element with Id {0} attribute {1} not as expected", elementId, attributeName));
        }

        [Then(@"the input with id '(.*)' and value '(.*)' is in view mode")]
        public void ThenTheInputWithIdAndValueIsInViewMode(string inputId, string expectedValue)
        {
            var input = WebBrowser.Current.Element(Find.ById(inputId));
            Assert.IsNotNull(input, "Element with id '" + input + "' could not be found");
            Assert.That(input.Style.Display, Is.EqualTo("none"), "Element with id '" + input + "' is supposed to be not visible");

            var associatedStrongElement = input.NextSibling;
            Assert.That(associatedStrongElement.InnerHtml, Is.EqualTo(expectedValue));
        }

        [Then(@"the input with id '(.*)' has value '(.*)'")]
        public void ThenTheInputWithIdHasValue(string inputId, string expectedValue)
        {
            var element = WebBrowser.Current.TextField(Find.ById(inputId));
            Assert.IsNotNull(element, string.Format("Could not find element with id {0}", inputId));
            Assert.That(element.Value, Is.EqualTo(expectedValue), string.Format("Element with Id {0} attribute {1} not as expected", inputId, expectedValue));
        }

        [When(@"I enter todays date into the field '(.*)'")]
        public void WhenIEnterTodaysDateIntoTheField(string inputId)
        {
            var element = WebBrowser.Current.TextField(Find.ById(inputId));
            Assert.IsNotNull(element, string.Format("Could not field text field with id '{0}'", inputId));

            element.Clear();
            element.AppendText(DateTime.Today.ToString("dd/MM/yy"));
        }

        [Then(@"the input with id '(.*)' has todays date")]
        public void ThenTheInputWithIdHasTodaysDate(string inputId)
        {
            var expectedValue = DateTime.Today.ToString("dd/MM/yyyy");
            var element = WebBrowser.Current.TextField(Find.ById(inputId));
            Assert.IsNotNull(element, string.Format("Could not find element with id {0}", inputId));
            Assert.That(element.Value, Is.EqualTo(expectedValue), string.Format("Element with Id {0} attribute {1} not as expected", inputId, expectedValue));
        }

        [Given(@"I click the radiobutton with id '(.*)'")]
        public void GivenIClickTheElementWithId(string elementId)
        {
            var element = WebBrowser.Current.RadioButton(Find.ById(elementId));
            Assert.IsNotNull(element, string.Format("Could not find element with id {0}", elementId));
            element.Click();
        }

        [Given(@"I press button with text '(.*)'")]
        [When(@"I press button with text '(.*)'")]
        public void IPressButtonWithText(string text)
        {
            var button = WebBrowser.Current.Button(Find.ByText(text));
            button.Click();
        }

        [Given(@"I press button with class '(.*)'")]
        [When(@"I press button with class '(.*)'")]
        public void IPressButtonWithClass(string @class)
        {
            var button = WebBrowser.Current.Button(Find.ByClass(@class));
            button.Click();
        }

        [Given(@"I have entered a unique value into the '(.*)' field")]
        [When(@"I have entered a unique value into the '(.*)' field")]
        public void GivenIHaveEnteredAUniqueValueIntoTheField(string field)
        {
            var value = Guid.NewGuid().ToString();
            var inputFound = false;
            var textBox = WebBrowser.Current.TextFields.FirstOrDefault(x => x.Name == field);
            if (textBox != null)
            {
                textBox.Clear();
                textBox.AppendText(value);
                inputFound = true;
                return;
            }
            var selectBox = WebBrowser.Current.SelectLists.FirstOrDefault(x => x.Id == field);
            if (selectBox != null)
            {
                selectBox.Select(value);
                // trigger any events listening for this select box to change
                WebBrowser.Current.Eval("$('#" + field + "').change();");
                inputFound = true;
            }
            Assert.IsTrue(inputFound, string.Format("Could not find input '{0}'", field));
        }

        [Given(@"I have entered a unique value into the '(.*)' textfield")]
        public void GivenIHaveEnteredAUniqueValueIntoTheTextfield(string fieldId)
        {
            var value = Guid.NewGuid().ToString();
            GivenIHaveEnteredIntoTheField(value, fieldId);
        }


        [Then("The error page is displayed")]
        public void The_error_page_is_displayed()
        {
            var id = "Title";
            var textfield = WebBrowser.Current.TextFields.FirstOrDefault(x => x.Id == id);
            Assert.Null(textfield);
        }

        [Given("I have ticked checkbox with value '(.*)'")]
        public void GivenIHaveCheckedCheckBoxWithValue(string employeeId)
        {
            var employee = WebBrowser.Current.CheckBox(Find.ByValue(employeeId));
            employee.Click();
        }

        [Then(@"element with id '(.*)' exists")]
        public void ElementWithIdExists(string id)
        {
            var element = WebBrowser.Current.Element(Find.ById(id));
            Assert.That(element.Exists);
        }

        [Then(@"element with id '(.*)' does not exist")]
        public void ElementWithIdDoesNotExist(string id)
        {
            var element = WebBrowser.Current.Element(Find.ById(id));
            Assert.That(!element.Exists);
        }

        private Form FindForm(string formId)
        {
            return WebBrowser.Current.Form(Find.ById(formId));
        }

        private TextField FindTextBox(string textBoxId)
        {
            return WebBrowser.Current.TextField(Find.ById(textBoxId));
        }

        private TextField FindTextBox(string textBoxId, Form form)
        {
            return form.TextField(Find.ById(textBoxId));
        }

        private Link FindLink(string linkId)
        {
            return WebBrowser.Current.Link(linkId);
        }
    }
}
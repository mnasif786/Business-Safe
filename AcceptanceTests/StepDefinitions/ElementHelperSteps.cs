using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using System;
using NUnit.Framework;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ElementHelperSteps: BaseSteps
    {
        [Then(@"the element with id '(.*)' has visibility of '(.*)'")]
        [When(@"the element with id '(.*)' has visibility of '(.*)'")]
        public void ThenTheElementWithIdVisibilityIs(string elemId, string isExpectedToBeVisible)
        {
            Thread.Sleep(2000);
            var jsCommand = String.Format("$('#{0}').is(':visible');", elemId);
            var isVisible = WebBrowser.Current.Eval(jsCommand) == "true";

            var elem = WebBrowser.Current.Elements.Filter(Find.ById(elemId)).FirstOrDefault();

            CheckVisbility(isExpectedToBeVisible, isVisible, elem, elemId, "id");
        }

        [Then(@"the element with class '(.*)' has visibility of '(.*)'")]
        public static Element ThenTheElementWithClassVisibilityIs(string elemClass, string isExpectedToBeVisible)
        {
            var jsCommand = String.Format("$('.{0}').is(':visible');", elemClass.Replace(' ', '.'));
            var isVisible = WebBrowser.Current.Eval(jsCommand) == "true";

            var elem = WebBrowser.Current.Elements.Filter(Find.ByClass(elemClass)).FirstOrDefault();

            CheckVisbility(isExpectedToBeVisible, isVisible, elem, elemClass, "class");

            return elem;
        }

        private static void CheckVisbility(string isExpectedToBeVisible, bool isVisible, Element elem, string elemId, string selector)
        {
            if (isExpectedToBeVisible == "true")
            {
                Assert.IsNotNull(elem, string.Format("Element with {0} '{1}' could not be found", selector, elemId));
                Assert.That(isVisible.ToString().ToLower, Is.EqualTo("true"), string.Format("Element with {0} '{1}' is supposed to be visible", selector, elemId));
            }
            else
            {
                if (elem != null)
                {
                    Assert.That(isVisible.ToString().ToLower, Is.Not.EqualTo("true"), string.Format("Element with {0} '{1}' is not supposed to be visible", selector, elemId));
                }
            }
        }

        [Then(@"the text box with id '(.*)' should contain '(.*)'")]
        public void ThenTheTextBoxWithIdShouldContain(string id, string contents)
        {
            
            var textBox = WebBrowser.Current.TextField(Find.ById(id));
            Assert.IsNotNull(textBox, string.Format("Could not find TextField with id '{0}'", id));
            Assert.AreEqual(contents, textBox.Text, string.Format("Element with id '{0}' does not contain '{1}'", id, contents));
        }

        [Then(@"the element with id '(.*)' should contain '(.*)'")]
        public void ThenTheElementWithIdShouldContain(string id, string contents)
        {
            var element = WebBrowser.Current.Element(Find.ById(id));
            Assert.IsNotNull(element, string.Format("Could not find Element with id '{0}'", id));
            Assert.IsTrue(element.Text.Contains(contents), string.Format("Element with id '{0}' does not contain '{1}'", id, contents));
        }

        [Then(@"the element with class '(.*)' should contain '(.*)'")]
        public void ThenTheElementWithClassShouldContain(string elemClass, string contents)
        {
            var element = WebBrowser.Current.Elements.Filter(Find.ByClass(elemClass)).FirstOrDefault();
            Assert.IsNotNull(element, string.Format("Could not find Element with class '{0}'", elemClass));
            Assert.IsTrue(element.Text.Contains(contents), string.Format("Element with id '{0}' does not contain '{1}'", elemClass, contents));
        }

        [Then(@"the element with id '(.*)' contains null")]
        public void ThenTheElementWithIdContainsNull(string id)
        {
            var element = WebBrowser.Current.Element(Find.ById(id));
            Assert.IsNotNull(element, string.Format("Could not find Element with id '{0}'", id));
            Assert.IsNull(element.Text, string.Format("Element with id '{0}' is not null.", id));
        }

        [Then(@"the element with id '(.*)' does not exist")]
        public void ThenThenElementWithIdDoesNotExist(string id)
        {
            var exists = WebBrowser.Current.Elements.Exists(id);
            Assert.IsFalse(exists, string.Format("Found Element with id '{0}'", id));
        }


        [Then(@"the element with id '(.*)' exist")]
        public void ThenThenElementWithIdExists(string id)
        {
            var exists = WebBrowser.Current.Elements.Exists(id);
            Assert.IsTrue(exists, string.Format("Could not find Element with id '{0}'", id));
        }

        [Then(@"the hidden field with id '(.*)' should have value '(.*)'")]
        public void ThenTheHiddenFieldWithIdShouldContain(string id, string contents)
        {
            var element = WebBrowser.Current.Element(Find.ById(id));
            Assert.IsNotNull(element, string.Format("Could not find Element with id '{0}'", id));
            Assert.That(element.GetValue("value"), Is.EqualTo(contents), string.Format("Hidden field with id '{0}' does not contain '{1}'", id, contents));
        }

        [Then(@"the hidden field with id '(.*)' has null value")]
        public void ThenTheHiddenFieldWithIdShouldBeNull(string id)
        {
            var element = WebBrowser.Current.Element(Find.ById(id));
            Assert.IsNotNull(element, string.Format("Could not find Element with id '{0}'", id));
            Assert.IsNull(element.GetValue("value"), string.Format("Hidden field with id '{0}' is not null", id));
        }
    }
}
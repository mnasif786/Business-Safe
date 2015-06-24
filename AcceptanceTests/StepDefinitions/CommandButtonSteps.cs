using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using NUnit.Framework;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class CommandButtonSteps : BaseSteps
    {
        [Given(@"I press button with css class '(.*)' first")]
        [When(@"I press button with css class '(.*)' first")]
        public void WhenIPressButtonWithCssClassFirst(string cssClass)
        {
            Thread.Sleep(2000);
            var buttons = FindButtonsByClass(cssClass);
            var button = buttons.First();
            button.Click();
        }

        [Given(@"I press button with css class '(.*)' last")]
        [When(@"I press button with css class '(.*)' last")]
        public void WhenIPressButtonWithCssClassLast(string cssClass)
        {
            Thread.Sleep(2000);
            var buttons = FindButtonsByClass(cssClass);
            var button = buttons.Last();
            button.Click();
        }

        private ButtonCollection FindButtonsByClass(string cssClass)
        {
            var buttons = WebBrowser.Current.Buttons.Filter(Find.ByClass(cssClass));
            return buttons;
        }

        [Given(@"I press '(.*)' button")]
        [When(@"I press '(.*)' button")]
        public void WhenIPressButton(string buttonId)
        {
            Element element = GetElement(FindButtonById, buttonId, 5000);
            Assert.NotNull(element, string.Format("Could not find button or link with id {0}", buttonId));
            element.Click();

            Thread.Sleep(2000); // todo: when removing this; be aware that any steps following a button press will be expecting effect of this button press to be available instantly
        }

        private Element FindButtonById(string buttonId)
        {
            return WebBrowser.Current.Buttons.Filter(Find.ById(buttonId)).FirstOrDefault() ?? (Element)WebBrowser.Current.Links.Filter(Find.ById(buttonId)).FirstOrDefault();
        }

        public static void PressButton(string buttonName)
        {
            new CommandButtonSteps().WhenIPressButton(buttonName);
        }

        [Then(@"the '(.*)' button visibility is '(.*)'")]
        public void ThenTheButtonWithIdVisibilityIs(string buttonId, string isVisible)
        {
            Element elem = WebBrowser.Current.Elements.Filter(Find.ById(buttonId)).FirstOrDefault();
            if (isVisible == "true")
            {
                Assert.IsNotNull(elem);
                Assert.That(elem.Style.Display, Is.Not.EqualTo("none"));
            }
            else
            {
                if (elem != null)
                {
                    Assert.That(elem.Style.Display, Is.EqualTo("none"));
                }

            }
        }
    }
}
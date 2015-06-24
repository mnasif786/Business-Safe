using System.Linq;
using TechTalk.SpecFlow;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.BUGS
{
    [Binding]
    public class _1836BugSteps : BaseSteps
    {

        [Given(@"I have clicked on 'employeesMultiSelect'")]
        public void GivenIHaveClickedOnEmployeesMultiSelect()
        {
            WebBrowser.Current.WaitForComplete();
            var selectList = WebBrowser.Current.SelectLists.Single(x => x.Id == "employeesMultiSelect");
            Assert.That(selectList.Exists, string.Format("Could not find select list {0}", "employeesMultiSelect"));

            selectList.SelectByValue("a433e9b2-84f6-4ad7-a89c-050e914dff01");

            WebBrowser.Current.Eval("$('#employeesMultiSelect option:first').attr('selected', 'selected').click().parent().focus();");
            WebBrowser.Current.WaitForComplete();
        }

        [Then(@"the remove button should be displayed")]
        public void ThenTheRemoveButtonShouldBeDisplayed()
        {
            WebBrowser.Current.WaitForComplete();
            var button = WebBrowser.Current.Buttons.Single(x => x.Id == "removeEmployeesBtn");
            Assert.That(button.ClassName.Contains("hide"), Is.False);
        }
    }
}

using System.Dynamic;
using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using TableRow = WatiN.Core.TableRow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Users
{
    [Binding]
    public class DeleteUserSteps : BaseSteps
    {
        [Given(@"I am on the add user page for company '(.*)'")]
        public void GivenIAmOnTheCreateEmployeePageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Users/AddUsers?companyId={0}", companyId));
            dynamic addingUser = new ExpandoObject();
            addingUser.CompanyId = companyId;
            Thread.Sleep(2000);
        }

        [Given(@"UserId select list change event is fired")]
        public void UserIdSelectListChangeEvenIsFired()
        {
            WebBrowser.Current.Eval("var changeUserId = function(){$('#EmployeeId').change(); }; changeUserId();");
        }



        [Then(@"the save success notification box should be displayed")]
        public void ThenTheSaveSuccessNotificationBoxShouldBDisplayed()
        {
            var saveSuccessNotification = WebBrowser.Current.Div(Find.ById("SaveSuccessNotification"));
            Assert.IsTrue(saveSuccessNotification.Exists);
        }

        [When(@"I click to delete the user with reference '(.*)'")]
        public void WhenIDeleteTheUserWithReference(string reference)
        {
            Element element = GetElement(FindUserReference, reference, 5000);
            var row = element as TableRow;
            var deleteButton = row.TableCells[6].Link(Find.ById("DeleteUserIconLink"));
            deleteButton.Click();
        }

        [Then(@"the user with reference '(.*)' is visible")]
        public void ThenTheRiskPhraseIsVisible(string reference)
        {
            var row = FindUserReference(reference);
            Assert.IsTrue(row.TableCells[0].InnerHtml.Contains(reference));
        }

        private TableRow FindUserReference(string reference)
        {
            var tableDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            var table = tableDiv.Tables[0];
            var row = table.TableRows.Single(x => x.TableCells[0].Text == reference);
            return row;
        }
    }
}

using System.Dynamic;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.SearchUsers
{
    [Binding]
    public class SearchUsersSteps : BaseSteps
    {
        [Given(@"I am on the search users page for company '(.*)'")]
        public void GivenIAmOnTheSearchUsersPageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate("Users/ViewUsers?companyId=" + companyId);
        }

        [Given(@"I have checked show deleted users")]
        public void GivenIHaveCheckedShowDeletedUsers()
        {
            var showDeletedLink = WebBrowser.Current.Link("showDeletedLink");
            Assert.That(showDeletedLink.Exists, "Could not find showDeletedLink");
            showDeletedLink.Click();
        }

        [Then(@"the user results table should contain the following data:")]
        public void ThenTheResultShouldContainRowWithTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                dynamic user = new ExpandoObject();

                user.Ref = row["Ref"];
                user.Forename = row["Forename"];
                user.Surname = row["Surname"];
                user.JobTitle = row["Job Title"];
                user.Site = row["Site"];
                user.Role = row["Role"];

                var searchResultsTable = WebBrowser.Current.Tables.First();

                Assert.NotNull(searchResultsTable.FindRow(user.Ref, 0));
                Assert.NotNull(searchResultsTable.FindRow(user.Forename, 1));
                Assert.NotNull(searchResultsTable.FindRow(user.Surname, 2));
                Assert.NotNull(searchResultsTable.FindRow(user.JobTitle, 3));
                Assert.NotNull(searchResultsTable.FindRow(user.Site, 4));
                Assert.NotNull(searchResultsTable.FindRow(user.Role, 5));
            }
        }
    }
}
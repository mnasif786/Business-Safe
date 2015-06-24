using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.CompanyDefaults
{
    [Binding]
    public class CompanyDefaultsSteps : BaseSteps
    {

        [AfterScenario("NonEmployee")]
        public static void AfterCompanyDefaultsFeature()
        {
        }

        [AfterScenario("AddingRiskAssessor")]
        [BeforeScenario("AddingRiskAssessor")]
        public static void AfterAddingRiskAssessor()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [RiskAssessor] WHERE [Id] > 5 ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        [Given(@"I am on the Company default page")]
        public void GivenIAmOnTheCompanyDefaultPage()
        {
            WebBrowser.Driver.Navigate("/Company/CompanyDefaults?companyId=55881");
        }

        [Given(@"I have no '(.*)' for company")]
        public void GivenIHaveNoDefaultsForCompany(string defaultType)
        {
            var companyId = ScenarioContextHelpers.GetCompanyId();
            var sql = new StringBuilder();
            sql.Append("DELETE FROM NonEmployee WHERE ClientId = '" + companyId + "'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }



        [Given(@"I have added a Organisational Unit Classification Company Default")]
        public void GivenIHaveAddedAOrganisationalUnitClassificationCompanyDefault()
        {
            CreateAOrganisationalUnitClassificationCompanyDefault();
        }

        [Given(@"I have added a Non Employee Company Default")]
        public void GivenIHaveAddedANonEmployeeCompanyDefault()
        {
            Thread.Sleep(2000);
            CreateANonEmployeeCompanyDefault();
        }

        [Given(@"I am working on Non Employee")]
        public void GivenIAmWorkingOnNonEmployee()
        {
            ScenarioContextHelpers.SetCompanyDefaultFormWorkingOnName("non-employees");
        }

        [Given(@"I am working on Organisational Unit Classification")]
        public void GivenIAmWorkingOnOrganisationalUnitClassification()
        {
            ScenarioContextHelpers.SetCompanyDefaultFormWorkingOnName("organisational-unit-classification");
        }

        [Given(@"I have entered exact'(.*)'")]
        public void GivenIHaveEnteredExactNewNonEmployee(string nonEmployeeName)
        {
            Thread.Sleep(4000);
            var newNonEmployeeName = nonEmployeeName;
            var nonEmployeeForm = WebBrowser.Current.Form(Find.ById(x => x == "addNewNonEmployeeForm" || x == "editNewNonEmployeeForm"));

            var nameTextField = nonEmployeeForm.TextFields.First();
            nameTextField.Clear();
            nameTextField.AppendText(newNonEmployeeName);
            ScenarioContextHelpers.AddedCompanyDefault(newNonEmployeeName);
        }

        [Given(@"I have entered '(.*)'")]
        public void GivenIHaveEnteredNewNonEmployee(string nonEmployeeName)
        {
            Thread.Sleep(4000);
            var newNonEmployeeName = nonEmployeeName + DateTime.Now.ToLongTimeString();
            var nonEmployeeForm = WebBrowser.Current.Form(Find.ById(x => x == "addNewNonEmployeeForm" || x == "editNewNonEmployeeForm"));
            var nameTextField = nonEmployeeForm.TextField(Find.ById(x => x == "Name"));
            nameTextField.Clear();
            nameTextField.AppendText(newNonEmployeeName);
            ScenarioContextHelpers.AddedCompanyDefault(newNonEmployeeName);
        }

        [Given(@"I select 'update' command button")]
        [Given(@"I select 'create' command button")]
        public void GivenISelectCreateCommandButton()
        {
            Thread.Sleep(2000);
            var nonEmployeeForm = WebBrowser.Current.Form(Find.ById(x => x == "addNewNonEmployeeForm" || x == "editNewNonEmployeeForm" || x == "updateNonEmployeeBtn"));
            nonEmployeeForm.Buttons.First().Click();
        }

        private void CreateAOrganisationalUnitClassificationCompanyDefault()
        {
            GivenIAmOnTheCompanyDefaultPage();
            WhenIEnterTestClarificationIntoTheAddTextbox("New Added Classification");
            WhenISelectAddCommandButton("add");
            ThenTestClarificationShouldBeCreatedAsAOrganisationalUnitClassificationCompanyDefault("created");
        }

        private void CreateANonEmployeeCompanyDefault()
        {
            GivenIAmOnTheCompanyDefaultPage();
            WhenISelectAddNewNonEmployeeCommandButton();
            GivenIHaveEnteredNewNonEmployee("new non employee woo");
            GivenISelectCreateCommandButton();
        }

        [Given(@"I edit the new Non Employee Company Default")]
        public void GivenIEditTheNewNonEmployeeCompanyDefault()
        {
            Thread.Sleep(2000);
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var table = form.Table(Find.First());
            var lastRow = table.TableRows[table.TableRows.Length - 1];
            var lastCell = lastRow.TableCells.Last();
            var editLink = lastCell.Links.First();
            editLink.Click();
        }


        [When(@"I delete the new Non Employee Company Default")]
        public void WhenIDeleteTheNewNonEmployeeCompanyDefault()
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var table = form.Table(Find.First());
            var lastRow = table.TableRows[table.TableRows.Length - 1];
            var lastCell = lastRow.TableCells.Last();
            var deleteLink = lastCell.Links.Last();
            deleteLink.Click();
        }

        [Then(@"should have Matching Names displayed")]
        public void ThenShouldHaveMatchingNamesDisplayed()
        {
            Thread.Sleep(4000);

            var addNewNonEmployeeForm = WebBrowser.Current.Form(Find.ById(x => x == "addNewNonEmployeeForm"));
            var validationDiv = addNewNonEmployeeForm.Div(Find.ByClass("matchingNamesDisplay"));
            Assert.That(validationDiv.Exists);

            validationDiv.Element(x => x.InnerHtml.Contains("We have the found the following potential matches;"));
        }


        [When(@"I delete the new Organisational Unit Classification Company Default")]
        public void WhenIDeleteTheNewOrganisationalUnitClassificationCompanyDefault()
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var table = form.Table(Find.First());
            var lastRow = table.TableRows[table.TableRows.Length - 1];
            lastRow.Links[lastRow.Links.Length - 1].Click();
        }

        [Given(@"I select 'add new non employee' command button")]
        [When(@"I select 'add new non employee' command button")]
        public void WhenISelectAddNewNonEmployeeCommandButton()
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var addNewNonEmployee = form.Button(Find.First());
            addNewNonEmployee.Click();
        }


        [When(@"I enter '(.*)' into the add textbox")]
        public void WhenIEnterTestClarificationIntoTheAddTextbox(string text)
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var inputField = form.TextField(Find.First());
            inputField.AppendText(text);

            ScenarioContextHelpers.AddedCompanyDefault(text);
        }


        [When(@"I select '(add|edit)' command button")]
        public void WhenISelectAddCommandButton(string buttonAction)
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var button = form.Button(Find.First());
            button.Click();
        }


        [When(@"I edit the new Organisational Unit Classification Company Default to '(.*)'")]
        public void WhenIEditTheNewOrganisationalUnitClassificationCompanyDefaultToTestClarificationModified(string edit)
        {
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var table = form.Table(Find.First());
            var lastRow = table.TableRows[table.TableRows.Length - 1];
            lastRow.Link(Find.First()).Click();

            var inputField = form.TextField(Find.First());
            inputField.Clear();
            inputField.AppendText(edit);

            ScenarioContextHelpers.AddedCompanyDefault(edit);

            var button = form.Button(Find.First());
            button.Click();
        }

        [When(@"I confirm delete")]
        public void WhenIConfirmDelete()
        {
            var dialogDiv = WebBrowser.Current.Div(x => x.ClassName == "ui-dialog-buttonset");
            dialogDiv.Button(Find.First()).Click();
        }


        [Then(@"should have '(created|edited)' the Organisational Unit Classification Company Default")]
        public void ThenTestClarificationShouldBeCreatedAsAOrganisationalUnitClassificationCompanyDefault(string actionType)
        {
            AssertLastRowInTheTableMatchesTheAddEditedCompanyDefault();
        }

        [Then(@"should have the new Non Employee form display")]
        public void ThenShouldHaveTheNewNonEmployeeFormDisplay()
        {

        }


        [Then(@"should have deleted the Organisational Unit Classification Company Default")]
        public void ThenShouldHaveDeletedTheOrganisationalUnitClassificationCompanyDefault()
        {

        }

        private static void AssertLastRowInTheTableMatchesTheAddEditedCompanyDefault()
        {
            Thread.Sleep(2000);
            var addedDefault = ScenarioContextHelpers.GetAddedDefault();
            var form = WebBrowser.Current.Form(ScenarioContextHelpers.GetCompanyDefaultFormWorkingOnName());
            var table = form.Table(Find.First());
            var lastRow = table.TableRows[table.TableRows.Length - 1];
            Assert.That(lastRow.TableCells.First().Text, Is.EqualTo(addedDefault));
        }

        [Then(@"should have 'updated' the Non Employee Company Default")]
        [Then(@"should have 'created' the Non Employee Company Default")]
        public void ThenShouldHaveCreatedTheNonEmployeeCompanyDefault()
        {
            Thread.Sleep(4000);

            var addedDefault = ScenarioContextHelpers.GetAddedDefault();
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var count = runSQLCommand("SELECT COUNT(id) FROM [NonEmployee] WHERE Name = '" + addedDefault + "'", conn);
                Assert.That(count, Is.EqualTo(1));
            }
        }

        [Then(@"should have deleted the Non Employee Company Default")]
        public void ThenShouldHaveDeletedTheNonEmployeeCompanyDefault()
        {
            Thread.Sleep(4000);

            var addedDefault = ScenarioContextHelpers.GetAddedDefault();
            var companyId = ScenarioContextHelpers.GetCompanyId();
            var sql = new StringBuilder();
            sql.Append("SELECT Count(*) FROM NonEmployee WHERE ClientId = '" + companyId + "' AND Name = '" + addedDefault + "' AND DELETED = 1");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    var result = command.ExecuteScalar();
                    Assert.That(result, Is.EqualTo(1));
                }
            }
        }


        [When(@"I click on edit for the first new non employee")]
        public void WhenIClickOnEditForTheFirstNewNonEmployee()
        {
            Thread.Sleep(2000);
            var links = WebBrowser.Current.Links.Filter(Find.ByClass("icon-edit editNonEmployees"));
            var link = links.ElementAt(1);
            Assert.IsNotNull(link, "Could not find first .editNonEmployees link");
            link.Click();
            Thread.Sleep(2000);
        }

        [Given(@"I click on the add risk asssessor button")]
        public void WhenIClickOnTheAddRiskAssessorButton()
        {
            var button = WebBrowser.Current.Button(Find.ByClass("btn addRiskAssessor btn-success"));
            button.Click();
        }

        [Then(@"the risk assessors table should contain the following data:")]
        public void ThenTheRisklAssessorsTableShouldContainTheFollowingData(TechTalk.SpecFlow.Table table)
        {
            var form = WebBrowser.Current.Div(Find.ById("riskAssessorsDiv"));
            var displayedTable = form.Tables.First();
            Assert.AreEqual(table.Rows.Count, displayedTable.TableRows.Length );

            for (var i = 0; i < table.Rows.Count; i++)
            {
                var forename = table.Rows[i]["Forename"].Trim();
                Assert.AreEqual(forename, displayedTable.TableRows[i].TableCells[0].Text.Trim());

                var surname = table.Rows[i]["Surname"].Trim();
                Assert.AreEqual(surname, displayedTable.TableRows[i].TableCells[1].Text.Trim());

                var site = table.Rows[i]["Site"].Trim();
                Assert.AreEqual(site, displayedTable.TableRows[i].TableCells[2].Text.Trim());

                var doNotSendTaskOverdueNotifications = table.Rows[i]["Overdue"].Trim();
                var doNotSendTaskOverdueNotificationsCheckbox = displayedTable.TableRows[i].TableCells[3].CheckBox(Find.ByName("DoNotSendOverDueNotifications"));
                Assert.AreEqual(bool.Parse(doNotSendTaskOverdueNotifications), doNotSendTaskOverdueNotificationsCheckbox.Checked);

                var doNotSendCompletedNotifications = table.Rows[i]["Completed"].Trim();
                var doNotSendCompletedNotificationsCheckbox = displayedTable.TableRows[i].TableCells[4].CheckBox(Find.ByName("DoNotSendCompletedNotifications"));
                Assert.AreEqual(bool.Parse(doNotSendCompletedNotifications), doNotSendCompletedNotificationsCheckbox.Checked);

                var doNotSendDueNotifications = table.Rows[i]["Due"].Trim();
                var doNotSendDueNotificationsCheckbox = displayedTable.TableRows[i].TableCells[5].CheckBox(Find.ByName("DoNotSendDueNotifications"));
                Assert.AreEqual(bool.Parse(doNotSendDueNotifications), doNotSendDueNotificationsCheckbox.Checked);
            }
        }

        [Then(@"the risk assessor table should contain a risk assessor with surname '(.*)'")]
        public void ThenTheRiskAssessorTableShouldContainARiskAssessorWithSurname(string surname)
        {
            var form = WebBrowser.Current.Div(Find.ById("riskAssessorsDiv"));
            var displayedTable = form.Tables.First();
    
            Assert.IsTrue(displayedTable.TableRows.SelectMany(row => row.TableCells)
                .Any(cell => cell.Text == surname), "Could not find cell with value of " + surname);

        }


    }
}

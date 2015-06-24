using System;
using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;
using System.Data.SqlClient;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class NonEmployeeSteps : BaseSteps
    {
        protected static object runSQLCommand(string sql, SqlConnection conn)
        {
            object val;
            using (var command = new SqlCommand(sql, conn))
            {
                val = command.ExecuteScalar();
            }
            return val;
        }

        [Given(@"We have the cleared non employees for company '(.*)'")]
        public void GivenWeHaveTheFollowingNonEmployees(long companyId)
        {
            //const string deleteSql = "DELETE FROM [BusinessSafe].[dbo].[NonEmployee] WHERE ClientId = :ClientId And Name != 'Dave Smith' ";

            //var businessSafeSessionFactory = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>();
            
            //businessSafeSessionFactory
            //    .GetSession()
            //    .CreateSQLQuery(deleteSql)
            //    .SetParameter("ClientId", companyId)
            //    .UniqueResult<long>();

            string deleteSql = "DELETE FROM [BusinessSafe].[dbo].[NonEmployee] WHERE ClientId = " + companyId.ToString()  + " And Name != 'Dave Smith' ";

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(deleteSql, conn);
            }
        }

        [Given(@"We have the following non employees:")]
        public void GivenWeHaveTheFollowingNonEmployees(Table table)
        {
            //const string insertSql = "INSERT INTO [BusinessSafe].[dbo].[NonEmployee] ([Name] ,[Company] ,[Position],[ClientId] ,[RiskAssessmentId]) VALUES (:Name, :Company, :Position, :ClientId, :RiskAssessmentId)";

            //var businessSafeSessionFactory = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>();
            
            //foreach (var row in table.Rows)
            //{
            //    string name = row["Name"];
            //    string company = row["Company"];
            //    string position = row["Position"];
            //    long clientId = long.Parse(row["LinkToCompanyId"] != "" ? row["LinkToCompanyId"] : "0");
            //    long riskAssessment = long.Parse(row["LinkToRiskAssessment"] != "" ? row["LinkToRiskAssessment"] : "0");

            //    var session = businessSafeSessionFactory.GetSession();
            //    session
            //       .CreateSQLQuery(insertSql)
            //       .SetParameter("Name", name)
            //       .SetParameter("Company", company)
            //       .SetParameter("Position", position)
            //       .SetParameter("ClientId", clientId)
            //       .SetParameter("RiskAssessmentId", riskAssessment)
            //       .UniqueResult<long>();

            //    session.Flush();
            //}

            
            
            foreach (var row in table.Rows)
            {
                string insertSql = "INSERT INTO [BusinessSafe].[dbo].[NonEmployee] ([Name] ,[Company] ,[Position],[ClientId] ,[RiskAssessmentId]) " +
                                   "VALUES ('" + 
                                   row["Name"] + "', '" + 
                                   row["Company"] + "', '" +
                                   row["Position"] + "', " +
                                   long.Parse(row["LinkToCompanyId"] != "" ? row["LinkToCompanyId"] : "0") + ", " +
                                   long.Parse(row["LinkToRiskAssessment"] != "" ? row["LinkToRiskAssessment"] : "0") + ")";

                using (var conn = new SqlConnection(BusinessSafeConnectionString))
                {
                    conn.Open();
                    runSQLCommand(insertSql, conn);
                }
            }
        }

        [Given(@"I am on the risk assessment page for company '(.*)'")]
        public void GivenIAmOnTheRiskAssessmentPageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("GeneralRiskAssessments/RiskAssessment/Create?companyId={0}", companyId));
        }


        [Given(@"I have created a new risk assessment")]
        public void GivenIHaveCreatedANewRiskAssessment()
        {
            Thread.Sleep(2000);
            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "Title"));
            textField.Clear();
            textField.TypeText("New Risk Assessment " + DateTime.Now.ToLongTimeString());

            textField = WebBrowser.Current.TextField(Find.ById(x => x == "Reference"));
            textField.Clear();
            textField.TypeText("Test Reference");

            var btn = WebBrowser.Current.Button(Find.ById(x => x == "createSummary"));
            btn.Click();

            textField = WebBrowser.Current.TextField(Find.ById(x => x == "SiteId"));
            textField.Clear();
            textField.TypeText("379");

            textField = WebBrowser.Current.TextField(Find.ById(x => x == "RiskAssessorId"));
            textField.Clear();
            textField.TypeText("1");

            btn = WebBrowser.Current.Button(Find.ById(x => x == "saveButton"));
            btn.Click();
        }

        [When(@"I have entered '(.*)' into the non employee search field")]
        public void WhenIHaveEnteredDoesNotExistIntoTheNonEmployeeField(string nameToSearch)
        {
            Thread.Sleep(1000);
            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "nonEmployeesName"));
            textField.Clear();
            textField.TypeText(nameToSearch);
            textField.KeyDown();
            ScenarioContextHelpers.SetSearchedNonEmployee(nameToSearch);
        }
        
        [Given(@"I have entered '(.*)' into the Name field")]
        public void GivenIHaveEnteredNewNameIntoTheNameField(string name)
        {
            var addNewNonEmployeeForm = WebBrowser.Current.Div(Find.ById(x => x == "dialogAddNonEmployee"));
            var nameTextField = addNewNonEmployeeForm.TextField(Find.ById(x => x == "Name"));
            nameTextField.Clear();
            nameTextField.AppendText(name);

            var addingNewNonEmployee = ScenarioContextHelpers.GetAddingNewNonEmployee();
            addingNewNonEmployee.Name = name;
            ScenarioContextHelpers.SetAddingNewNonEmployee(addingNewNonEmployee);
        }

        [Given(@"I have entered '(.*)' into the Company field")]
        public void GivenIHaveEnteredNewCompanyIntoTheCompanyField(string company)
        {
            var addNewNonEmployeeForm = WebBrowser.Current.Div(Find.ById(x => x == "dialogAddNonEmployee"));
            var companyTextField = addNewNonEmployeeForm.TextField(Find.ById(x => x == "Company"));
            companyTextField.Clear();
            companyTextField.AppendText(company);

            var addingNewNonEmployee = ScenarioContextHelpers.GetAddingNewNonEmployee();
            addingNewNonEmployee.Company = company;
            ScenarioContextHelpers.SetAddingNewNonEmployee(addingNewNonEmployee);

        }

        [Given(@"I have entered '(.*)' into the Position field")]
        public void GivenIHaveEnteredNewPositionIntoThePositionField(string position)
        {
            var addNewNonEmployeeForm = WebBrowser.Current.Div(Find.ById(x => x == "dialogAddNonEmployee"));
            var positionTextField = addNewNonEmployeeForm.TextField(Find.ById(x => x == "Position"));
            positionTextField.Clear();
            positionTextField.AppendText(position);

            var addingNewNonEmployee = ScenarioContextHelpers.GetAddingNewNonEmployee();
            addingNewNonEmployee.Position = position;
            ScenarioContextHelpers.SetAddingNewNonEmployee(addingNewNonEmployee);
        }

        [Given(@"I have clicked on the create new non employee button for company '(.*)'")]
        public void GivenIHaveClickedOnTheCreateNewNonEmployeeButton(long companyId)
        {
            var nonEmployeeCreating = new NonEmployeeCreating {ClientId = companyId};
            ScenarioContextHelpers.SetAddingNewNonEmployee(nonEmployeeCreating);

            var addNewNonEmployeeBtn = WebBrowser.Current.Button(Find.ById(x => x == "addNewNonEmployee"));
            addNewNonEmployeeBtn.Click();
        }


        [When(@"I have clicked create")]
        public void WhenIHaveClickedCreate()
        {
            var addNewNonEmployeeForm = WebBrowser.Current.Div(Find.ById(x => x == "dialogAddNonEmployee"));
            var createBtn = addNewNonEmployeeForm.Button(Find.ById(x => x == "createNonEmployeeBtn"));
            createBtn.Click();
        }


        [Then(@"the result should be:")]
        public void ThenTheResultShouldBe(Table table)
        {
            Thread.Sleep(4000);
            var expectedResults = table.Rows.Select(row => row["Label"]).ToList();

            var searchTerm = ScenarioContextHelpers.GetSearchedNonEmployee();

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "nonEmployeesName"));
            textField.Clear();
            textField.TypeText(searchTerm);
            textField.KeyDown();

            WebBrowser.Current.Element(x => x.ClassName == "ui-corner-all" && x.Text == expectedResults[0]);

        }

        [Then(@"the create new non employee button should be enabled")]
        public void ThenTheCreateNewNonEmployeeButtonShouldBeEnabled()
        {
            Thread.Sleep(4000);
            foreach (Button button in WebBrowser.Current.Buttons)
            {
                if(button.Id == "addNewNonEmployee")
                {
                    Assert.That(button.GetAttributeValue("disabled").ToLower(), Is.Not.EqualTo("true").And.Not.EqualTo("disabled"));
                    return;
                }
            }
            // Should not get here fail if we do!
            Assert.Fail("Could not find button addNewNonEmployee");
            
        }

        [Then(@"the new non employee should be created")]
        public void ThenTheNewNonEmployeeShouldBeCreated()
        {
            const string insertSql = "SELECT Count(*) FROM [BusinessSafe].[dbo].[NonEmployee] WHERE ClientId = :ClientId AND Name = :Name AND Position = :Position and Company = :Company";
            Thread.Sleep(1000);
            var businessSafeSessionFactory = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>();
            var addingNewNonEmployee = ScenarioContextHelpers.GetAddingNewNonEmployee();
            var result = businessSafeSessionFactory
                                       .GetSession()
                                       .CreateSQLQuery(insertSql)
                                       .SetParameter("Name", addingNewNonEmployee.Name)
                                       .SetParameter("Company", addingNewNonEmployee.Company)
                                       .SetParameter("Position", addingNewNonEmployee.Position)
                                       .SetParameter("ClientId", addingNewNonEmployee.ClientId)
                                       .UniqueResult<int>();

            Assert.That(result, Is.EqualTo(1));
        }

        [Then(@"the new non employee should be linked to the risk assessment")]
        public void ThenTheNewNonEmployeeShouldBeLinkedToTheRiskAssessment()
        {
            var addingNewNonEmployee = ScenarioContextHelpers.GetAddingNewNonEmployee();
            var nonEmployeesSelectList = WebBrowser.Current.SelectList(Find.ById(x => x == "nonEmployeesMultiSelect"));
            Assert.That(nonEmployeesSelectList.Option(addingNewNonEmployee.Name + ", " + addingNewNonEmployee.Company + ", " + addingNewNonEmployee.Position).Exists);
        }

        [Then(@"the name is required validation message should be shown")]
        public void ThenTheNameIsRequiredValidationMessageShouldBeShown()
        {
            Thread.Sleep(2000);
            var addNewNonEmployeeForm = WebBrowser.Current.Div(Find.ById(x => x == "dialogAddNonEmployee"));
            var validationDiv = addNewNonEmployeeForm.Div(Find.ById(x => x == "validationDisplay"));
            Assert.That(validationDiv.Exists);

            var element = validationDiv.Element(x => x.InnerHtml == "Name is required");
            Assert.That(element.Exists);
        }

        [Then(@"the non employee already exists validation message should be shown")]
        public void ThenTheNonEmployeeAlreadyExistsValidationMessageShouldBeShown()
        {
            Thread.Sleep(4000);
            var addNewNonEmployeeForm = WebBrowser.Current.Div(Find.ById(x => x == "dialogAddNonEmployee"));
            var validationDiv = addNewNonEmployeeForm.Div(Find.ByClass("matchingNamesDisplay"));
            Assert.That(validationDiv.Exists);

            validationDiv.Element(x => x.InnerHtml.Contains("We have the found the following potential matches;"));

        }

    }

    public class NonEmployeeCreating
    {
        public string Name;
        public string Position;
        public string Company;
        public long ClientId;
    }
}

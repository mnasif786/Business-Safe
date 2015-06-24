using System;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Employees
{
    [Binding]
    public class AddEmployeesSteps : BaseSteps
    {

        [Given(@"I am on the create employee page for company '(.*)'")]
        public void GivenIAmOnTheCreateEmployeePageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Employees/Employee?companyId={0}", companyId));
            dynamic creatingEmployee = new ExpandoObject();
            creatingEmployee.CompanyId = companyId;
            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);

            Thread.Sleep(2000);
        }


        [Given(@"I am on the view employee page for company '(.*)' and employee id '(.*)'")]
        public void GivenIAmOnTheViewEmployeePageForCompany55881(long companyId, string employeeId)
        {
            WebBrowser.Driver.Navigate(string.Format("Employees/Employee/View?companyId={0}&employeeId={1}", companyId, employeeId));
            dynamic creatingEmployee = new ExpandoObject();
            creatingEmployee.CompanyId = companyId;
            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);

            Thread.Sleep(2000);
        }

        [Then(@"the corresponding span for '(.*)' contains '(.*)'")]
        public void ThenTheCorrespondingSpanForContains(string inputId, string expectedValue)
        {
            var input = WebBrowser.Current.Element(Find.ById(inputId));
            var span = input.NextSibling;
            Assert.That(span.InnerHtml.Contains(expectedValue));
        }


        [Given(@"I have entered '(.*)' in the has disability checkbox")]
        public void GivenIHaveEnteredIntoTheHasDisabilityField(string hasdisability)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();

            var checkedValue = bool.Parse(hasdisability);
            var checkbox = WebBrowser.Current.CheckBox(Find.ById("HasDisability"));
            checkbox.Checked = checkedValue;
            creatingEmployee.HasDisability = checkedValue;

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' with id '(.*)' in the site dropdown")]
        public void GivenIHaveEnteredIntoTheSiteField(string site, string siteId)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();

            var dropDownList = WebBrowser.Current.TextField(Find.ById("Site"));
            if (site != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(site);
                creatingEmployee.SiteId = siteId;
            }

            dropDownList = WebBrowser.Current.TextField(Find.ById("SiteId"));
            if (site != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(siteId);
            }
            else
            {
                dropDownList.Clear();
            }

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }


        [Given(@"I have entered '(.*)' with id '(.*)' in the nationality dropdown")]
        public void GivenIHaveEnteredIntoTheNationalityField(string nationality, string nationalityId)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();

            var dropDownList = WebBrowser.Current.TextField(Find.ById("Nationality"));
            if (nationality != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(nationality);
                creatingEmployee.NationalityId = nationalityId;
            }

            dropDownList = WebBrowser.Current.TextField(Find.ById("NationalityId"));
            if (nationality != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(nationalityId);
            }
            else
            {
                dropDownList.Clear();
            }
            
            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' with id '(.*)' in the employment status dropdown")]
        public void GivenIHaveEnteredIntoTheEmploymentStatusField(string status, string statusId)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();

            var dropDownList = WebBrowser.Current.TextField(Find.ById("EmploymentStatus"));
            if (status != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(status);
                creatingEmployee.EmploymentStatusId = statusId;
            }

            dropDownList = WebBrowser.Current.TextField(Find.ById("EmploymentStatusId"));
            if (status != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(statusId);
            }
            else
            {
                dropDownList.Clear();
            }

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }


        [Given(@"I have entered '(.*)' with id '(.*)' in the country dropdown")]
        public void GivenIHaveEnteredIntoTheCountryField(string country, string countryId)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();

            var dropDownList = WebBrowser.Current.TextField(Find.ById("Country"));
            if (country != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(country);
                creatingEmployee.GotContactDetails = true;
                creatingEmployee.CountryId = countryId;
            }


            dropDownList = WebBrowser.Current.TextField(Find.ById("CountryId"));
            if (country != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(countryId);
            }
            else
            {
                dropDownList.Clear();
            }

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the title field")]
        public void GivenIHaveEnteredIntoTheTitleField(string title)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.Title = title;

            
            var dropDownList = WebBrowser.Current.TextField(Find.ById("NameTitle"));
            if (title != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(title);
            }
            else
            {
                dropDownList.Clear();
            }
            dropDownList = WebBrowser.Current.TextField(Find.ById("NameTitleId"));
            if (title != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(title);
            }
            else
            {
                dropDownList.Clear();
            }



            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the previous surname field")]
        public void GivenIHaveEnteredIntoThePreviousSurnameField(string previousSurname)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.PreviousSurname = previousSurname;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "PreviousSurname"));
            textField.Clear();
            textField.TypeText(previousSurname);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the middle name field")]
        public void GivenIHaveEnteredIntoTheMiddleNameField(string middleName)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.MiddleName = middleName;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "MiddleName"));
            textField.Clear();
            textField.TypeText(middleName);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the date of birth field")]
        public void GivenIHaveEnteredDateIntoTheDateOfBirthField(string dateOfBirth)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            DateTime result;
            if (DateTime.TryParse(dateOfBirth, out result))
            {
                creatingEmployee.DateOfBirth = DateTime.Parse(dateOfBirth);
            }
            
            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "DateOfBirth"));
            textField.Clear();
            textField.TypeText(dateOfBirth);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the employee reference field")]
        public void GivenIHaveEnteredNewEmployeeReferenceIntoTheEmployeeReferenceField(string employeeReference)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.EmployeeReference = employeeReference;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "EmployeeReference"));
            textField.Clear();
            textField.TypeText(employeeReference);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the forename field")]
        public void GivenIHaveEnteredBobIntoTheForenameField(string forename)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.Forename = forename;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "Forename"));
            textField.Clear();
            textField.TypeText(forename);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }



        [Given(@"I have entered '(.*)' in the disability description field")]
        public void GivenIHaveEnteredIntoTheDisabilityDescriptionField(string description)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.DisabilityDescription = description;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "DisabilityDescription"));
            textField.TypeText(description);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' in the ni number field")]
        public void GivenIHaveEnteredIntoTheNINumberField(string niNumber)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.NINumber = niNumber;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "NINumber"));
            textField.Clear();
            textField.TypeText(niNumber);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the surname field")]
        public void GivenIHaveEnteredSmithIntoTheSurnameField(string surname)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.Surname = surname;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "Surname"));
            textField.Clear();
            textField.TypeText(surname);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the postcode field")]
        public void GivenIHaveEnteredSmithIntoThePostCodeField(string postcode)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.PostCode = postcode;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "Postcode"));
            textField.Clear();
            textField.TypeText(postcode);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [Given(@"I have entered '(.*)' into the relationship field")]
        public void GivenIHaveEnteredIntoTheRelationshipField(string relationship)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.Relationship = relationship;

            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "Relationship"));
            textField.Clear();
            textField.TypeText(relationship);

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }


        [Given(@"I have clicked on the '(.*)' header")]
        [Then(@"I have clicked on the '(.*)' header")]
        public void GivenIHaveClickedOnTheAdditionalPersonalDetailsHeader(string header)
        {
            var link = WebBrowser.Current.Link(Find.ById(header + "-link"));
            link.Click();
        }

        [Given(@"I have selected '(.*)' in the sex dropdown")]
        public void GivenIHaveSelectedMaleInTheSexDropdown(string sex)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            creatingEmployee.Sex = sex;

            var dropDownList = WebBrowser.Current.TextField(Find.ById("Sex"));
            if (sex != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(sex);
            }
            else
            {
                dropDownList.Clear();
            }
            dropDownList = WebBrowser.Current.TextField(Find.ById("SexId"));
            if (sex != "-- Select Option --")
            {
                dropDownList.Clear();
                dropDownList.AppendText(sex);
            }
            else
            {
                dropDownList.Clear();
            }

            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);
        }

        [When(@"I have clicked save")]
        public void WhenIHaveClickedSave()
        {
            var saveBtn = WebBrowser.Current.Button(Find.ById("SaveEmployeeButton"));
            saveBtn.Click();
            Thread.Sleep(2000);
        }

        [When(@"I have clicked emergency contact save")]
        public void WhenIHaveClickedEmergencyContactDetailsSave()
        {
            var saveBtn = WebBrowser.Current.Button(Find.ById("EmergencyContactDetailsSaveButton"));
            saveBtn.Click();
        }

        [Then(@"the employee should be saved")]
        public void ThenTheEmployeeShouldBeSaved()
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            
            var sql = new StringBuilder();

            sql.Append("SELECT * FROM Employee WHERE Forename = '" + creatingEmployee.Forename + "' AND Surname = '" + creatingEmployee.Surname + "' AND EmployeeReference='" + creatingEmployee.EmployeeReference + "' AND Sex = '" + creatingEmployee.Sex + "'");

            SqlDataReader result;
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    result = command.ExecuteReader();
                    Assert.That(result.HasRows,Is.True);

                    result.Read();

                    #region "Dyanmic Properties Check"
                    try
                    {
                        if (creatingEmployee.Title != null)
                        {
                            Assert.That(result["Title"] as string, Is.EqualTo(creatingEmployee.Title));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.MiddleName != null)
                        {
                            Assert.That(result["MiddleName"] as string, Is.EqualTo(creatingEmployee.MiddleName));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.PreviousSurname != null)
                        {
                            Assert.That(result["PreviousSurname"] as string, Is.EqualTo(creatingEmployee.PreviousSurname));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.DateOfBirth != null)
                        {
                            Assert.That(result.GetDateTime(result.GetOrdinal("DateOfBirth")).ToShortDateString(), Is.EqualTo(creatingEmployee.DateOfBirth.ToShortDateString()));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.NationalityId != null)
                        {
                            Assert.That(result.GetInt32(result.GetOrdinal("NationalityId")).ToString(), Is.EqualTo(creatingEmployee.NationalityId));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.HasDisability != null)
                        {
                            Assert.That(result.GetBoolean(result.GetOrdinal("HasDisability")), Is.EqualTo(creatingEmployee.HasDisability));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.DisabilityDescription != null)
                        {
                            Assert.That(result["DisabilityDescription"] as string, Is.EqualTo(creatingEmployee.DisabilityDescription));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.NINumber != null)
                        {
                            Assert.That(result["NINumber"] as string, Is.EqualTo(creatingEmployee.NINumber));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.SiteId != null)
                        {
                            Assert.That(result.GetInt64(result.GetOrdinal("SiteId")).ToString(), Is.EqualTo(creatingEmployee.SiteId.ToString()));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.EmploymentStatusId != null)
                        {
                            Assert.That(result.GetInt32(result.GetOrdinal("EmploymentStatusId")).ToString(), Is.EqualTo(creatingEmployee.EmploymentStatusId.ToString()));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }
                    #endregion




                    try
                    {
                        if (creatingEmployee.GotContactDetails)
                        {
                            HasSavedEmployeeContactDetails(result);
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                }
            }

        }

        private void HasSavedEmployeeContactDetails(SqlDataReader dataReader)
        {
            dynamic creatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();


            var sql = new StringBuilder();
            var employeeId = dataReader.GetGuid(dataReader.GetOrdinal("Id"));

            sql.Append("SELECT * FROM EmployeeContactDetails WHERE EmployeeId = '" + employeeId + "'");

            SqlDataReader result;
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    result = command.ExecuteReader();
                    Assert.That(result.HasRows, Is.True);

                    result.Read();

                    
                    try
                    {
                        if (creatingEmployee.CountryId != null)
                        {
                            Assert.That(result.GetInt32(result.GetOrdinal("CountryId")).ToString(), Is.EqualTo(creatingEmployee.CountryId));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }

                    try
                    {
                        if (creatingEmployee.PostCode != null)
                        {
                            Assert.That(result.GetString(result.GetOrdinal("PostCode")), Is.EqualTo(creatingEmployee.PostCode));
                        }
                    }
                    catch (RuntimeBinderException)
                    { }
                }
            }
            
        }

        [Then(@"the emergency contact details table should contain the following data:")]
        public void ThenTheResultShouldContainRowWithTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                dynamic expectedDetail = new ExpandoObject();

                expectedDetail.Title = row["Title"].Trim();
                expectedDetail.Forename = row["Forename"].Trim();
                expectedDetail.Surname = row["Surname"].Trim();
                expectedDetail.Relationship = row["Relationship"].Trim();
                expectedDetail.PreferredTelephone = row["Preferred Telephone"].Trim();

                var tableDiv = WebBrowser.Current.Div(Find.ById("emergencyContactDetailsGrid"));
                var searchResultsTableRow = tableDiv.Tables[0].TableRows[0];

                dynamic actualDetail = new ExpandoObject();

                actualDetail.Title = searchResultsTableRow.TableCells[0].Text.Trim();
                actualDetail.Forename = searchResultsTableRow.TableCells[1].Text.Trim();
                actualDetail.Surname = searchResultsTableRow.TableCells[2].Text.Trim();
                actualDetail.Relationship = searchResultsTableRow.TableCells[3].Text.Trim();
                actualDetail.PreferredTelephone = searchResultsTableRow.TableCells[4].Text.Trim();


                Assert.NotNull(actualDetail.Title, string.Format("Could not find cell containing '{0}', in column '{1}'", expectedDetail.Title, 1));
                Assert.NotNull(actualDetail.Forename, string.Format("Could not find cell containing '{0}', in column '{1}'", expectedDetail.Forename, 2));
                Assert.NotNull(actualDetail.Surname, string.Format("Could not find cell containing '{0}', in column '{1}'", expectedDetail.Surname, 3));
                Assert.NotNull(actualDetail.Relationship, string.Format("Could not find cell containing '{0}', in column '{1}'", expectedDetail.Relationship, 4));
                Assert.NotNull(actualDetail.PreferredTelephone, string.Format("Could not find cell containing '{0}', in column '{1}'", expectedDetail.PreferredTelephone, 5));
            }
        }

        [Then(@"validation message should be displayed saying '(.*)' is required")]
        public void ThenValidationMessageShouldBeDisplayedSayingEmployeeReferenceIsRequired(string validationMessageField)
        {
            var validationDiv = WebBrowser.Current.Div(Find.ByClass("validation-summary-errors"));
            Assert.That(validationDiv.InnerHtml.ToLower().Contains(string.Format("{0} is required", validationMessageField)));
        }

        [Then(@"validation message should be displayed saying 'not valid for DateOfBirth.'")]
        public void ThenValidationMessageShouldBeDisplayedSayingNotValidDateOfBirth()
        {
            var validationDiv = WebBrowser.Current.Div(Find.ByClass("validation-summary-errors"));
            Assert.That(validationDiv.InnerHtml.ToLower().Contains("not valid for dateofbirth"));
        }
    }
}

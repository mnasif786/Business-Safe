using System;
using System.Collections.Generic;
using System.Dynamic;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ReinstateDeletedEmployees
{
    [Binding]
    public class ReinstateDeletedEmployeeSteps : BaseSteps
    {
        [When(@"I press 'Reassign' link for '(.*)' '(.*)'")]
        public void WhenIPressDeleteLinkForBobSmith(string forename, string surname)
        {
            var employeeId = GetEmployeeId(forename, surname);

            foreach (Element image in WebBrowser.Current.Links.Filter(Find.ByClass("icon-share")))
            {
                if (image.GetAttributeValue("data-id") == employeeId.ToString())
                {
                    image.Click();
                    break;
                }
            }
        }

        [Given(@"I have checked show deleted employees")]
        public void GivenIHaveCheckedShowDeletedEmployees()
        {
            var showDeletedLink = WebBrowser.Current.Link("showDeletedLink");
            showDeletedLink.Click();
        }

        [Then(@"confirmation for reinstate deleted employee should be shown")]
        public void ThenConfirmationForReinstateDeletedEmployeeShouldBeShown()
        {

        }

        private static Guid GetEmployeeId(string forename, string surname)
        {
            IEnumerable<ExpandoObject> existingEmployees = ScenarioContextHelpers.GetCreatedEmployees();
            var employeeId = Guid.Empty;
            foreach (dynamic employee in existingEmployees)
            {
                if (employee.Forename == forename && employee.Surname == surname)
                {
                    employeeId = employee.EmployeeId;
                    break;
                }
            }
            return employeeId;
        }
    }
}

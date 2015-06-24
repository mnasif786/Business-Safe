using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.DeleteEmployees
{
    [Binding]
    public class DeleteEmployeeSteps : BaseSteps
    {
        [When(@"I press 'Delete' link for '(.*)' '(.*)'")]
        public void WhenIPressDeleteLinkForBobSmith(string forename, string surname)
        {
            var employeeId = GetEmployeeId(forename, surname);

            foreach (Element image in WebBrowser.Current.Links.Filter(Find.ByClass("icon-remove")))
            {
                    if (image.GetAttributeValue("data-id") == employeeId.ToString())
                    {
                        image.Click();
                        break;
                    }
            }
        }

        private static Guid GetEmployeeId(string forename, string surname)
        {
            if (forename == "Barry" && surname == "Brown")
            {
                return Guid.Parse("3ECE3FD2-DB29-4ABD-A812-FCC6B8E621A1");
            }

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

        [Then(@"'(.*)' '(.*)' should then be deleted")]
        public void ThenTheEmployeeShouldThenBeDeleted(string forename, string surname)
        {
            Thread.Sleep(2000);
            var employeeId = GetEmployeeId(forename, surname);
            var employee = ObjectFactory.GetInstance<IEmployeeRepository>().GetById(employeeId);
            Assert.True(employee.Deleted);
        }

        [Then(@"'(.*)' '(.*)' should then not be deleted")]
        public void ThenTheEmployeeShouldThenNotBeDeleted(string forename, string surname)
        {
            Thread.Sleep(4000);
            var employeeId = GetEmployeeId(forename, surname);
            var employee = ObjectFactory.GetInstance<IEmployeeRepository>().GetById(employeeId);
            Assert.False(employee.Deleted);
        }

        [Then(@"message should display showing can not delete employee as got outstanding tasks")]
        public void ThenMessageShouldDisplayShowingCanNotDeleteEmployeeAsGotOutstandingTasks()
        {
            const string dialogID = "ui-dialog-title-dialogCannotRemoveEmployee";

            var messageDialog = GetElement(FindMessageDialogById, dialogID, 5000);

            Assert.That(messageDialog.Enabled, Is.True);
        }

        private Element FindMessageDialogById(string dialogID)
        {
            return WebBrowser.Current.Div(Find.ById(dialogID));
        }
    }
}

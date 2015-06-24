using System;
using System.Dynamic;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepHelpers;

using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Users
{
    [Binding]
    public class ViewUserSteps
    {
        [Given(@"I am on the view user page for company '(.*)' and employee id '(.*)'")]
        public void GivenIAmOnTheViewUserPageForCompanyAndEmployeeId(int companyId, string employeeId)
        {
            WebBrowser.Driver.Navigate(string.Format("Users/ViewUsers/ViewUser?companyId={0}&employeeId={1}", companyId, employeeId));
            dynamic creatingEmployee = new ExpandoObject();
            creatingEmployee.CompanyId = companyId;
            ScenarioContextHelpers.SetEmployeeCreatingUpdating(creatingEmployee);

            Thread.Sleep(2000);
        }
    }
}

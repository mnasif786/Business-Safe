using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Employees
{
    [Binding]
    public class UpdateEmployeeSteps: BaseSteps
    {
        [AfterFeature("UpdatingEmployee")]
        public static void AfterUpdatingEmployeeFeature()
        {
            
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[Employee] Where [Forename] = 'Bob' AND [Surname] = 'Smith'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }
        }

        [Given("I have setup the employee 'Bob Smith' for scenario")]
        public void GivenIHaveSetupTheEmployeesForScenario()
        {
            var employeeId = Guid.NewGuid();

            var sql = new StringBuilder();
            sql.Append("INSERT INTO [dbo].[Employee]([Id],[Forename],[Surname],[Sex] ,[EmployeeReference],[ClientId],[Deleted] ,[CreatedOn]) VALUES ('" + employeeId +  "' ,'Bob' ,'Smith' ,'Male' ,'Test Reference' ,55881 ,0 ,GetDate()) ");
            
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }

            dynamic updatingEmployee = new ExpandoObject();
            updatingEmployee.EmployeeId = employeeId;
            ScenarioContextHelpers.SetEmployeeCreatingUpdating(updatingEmployee);
            
        }


        [Given("I am on the update employee page for company '(.*)' for employee 'Bob Smith'")]
        public void GivenIAmOnTheUpdateEmployeePageForCompanyAndEmployee(long companyId)
        {
            var updatingEmployee = ScenarioContextHelpers.GetEmployeeCreatingUpdating();
            WebBrowser.Driver.Navigate(string.Format("Employees/Employee/Index?companyId={0}&employeeId={1}", companyId, updatingEmployee.EmployeeId));
            Thread.Sleep(2000);
        }
    }
}

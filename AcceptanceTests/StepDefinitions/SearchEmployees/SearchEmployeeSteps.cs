using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.SearchEmployees
{
    [Binding]
    public class SearchEmployeeSteps : BaseSteps
    {
        private static readonly Dictionary<string, long> Sites = new Dictionary<string, long> { { "Aberdeen", 378 }, { "Barnsley", 379 } };

        [BeforeScenario("DeletingEmployee")]
        public static void BeforeDeleteEmployeeFeature()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var sql = "Update [dbo].[Employee] Set Deleted = 0 Where Id = '3ECE3FD2-DB29-4ABD-A812-FCC6B8E621A1'";
                    using (var command = new SqlCommand(sql.ToString(), conn))
                    {
                        command.ExecuteScalar();
                    }
            }
        }

        [AfterScenario("SearchingEmployee", "DeletingEmployee", "ReinstatingDeletedEmployee")]
        public static void AfterSearchEmployeeFeature()
        {

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var createdEmployees = ScenarioContextHelpers.GetCreatedEmployees();
                foreach (dynamic createdEmployee in createdEmployees)
                {
                    var sql = "DELETE FROM [dbo].[Employee] Where [Forename] = '" + createdEmployee.Forename + "' AND [Surname] = '" + createdEmployee.Surname + "'";
                    using (var command = new SqlCommand(sql.ToString(), conn))
                    {
                        command.ExecuteScalar();
                    }
                }
            }
        }

        [Given("I have the following employees for company '(.*)':")]
        public void GivenIHaveTheFollowingEmployeesForCompany(long companyId, Table table)
        {   
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var createdEmployees = new List<ExpandoObject>();
                foreach (var row in table.Rows)
                {
                    dynamic employee = new ExpandoObject();
                    employee.EmployeeId = Guid.NewGuid();
                    employee.EmployeeReference = row["Employee Reference"];
                    employee.Forename = row["Forename"];
                    employee.Surname = row["Surname"];
                    employee.JobTitle = row["Job Title"];
                    employee.Deleted = row.ContainsKey("Deleted") && bool.Parse(row["Deleted"]);
                    employee.SiteId = Sites[row["Site"]];

                    

                    string sql =
                        string.Format(
                            "INSERT INTO [dbo].[Employee]([Id],[Sex],[CreatedOn],[EmployeeReference],[Forename],[Surname],[JobTitle],[SiteId], [ClientId], [Deleted]) VALUES ( '{0}' ,'Male', GetDate(), '{1}', '{2}','{3}','{4}',{5},{6}, '{7}') ",
                            employee.EmployeeId,
                            employee.EmployeeReference,
                            employee.Forename,
                            employee.Surname,
                            employee.JobTitle,
                            employee.SiteId,
                            companyId,
                            employee.Deleted);

                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.ExecuteScalar();
                    }

                    createdEmployees.Add(employee);
                }
                ScenarioContextHelpers.SetCreatedEmployees(createdEmployees);
            }
        }

        [Given(@"I am on the search employee page for company '(.*)'")]
        public void GivenIAmOnTheSearchEmployeePageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate("Employees/EmployeeSearch?companyId=" + companyId);
        }

        [Then(@"the result should contain row with the following:")]
        public void ThenTheResultShouldContainRowWithTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                dynamic employee = new ExpandoObject();
                employee.Forename = row["Forename"];
                employee.Surname = row["Surname"];
                employee.JobTitle = row["Job Title"];
                employee.Site = row["Site"];
                var searchResultsTable  = WebBrowser.Current.Tables.First();
                Assert.NotNull(searchResultsTable.FindRow(employee.Forename, 0), string.Format("Could not find {0}", employee.Forename));
                Assert.NotNull(searchResultsTable.FindRow(employee.Surname, 1), string.Format("Could not find {0}", employee.Surname));
                Assert.NotNull(searchResultsTable.FindRow(employee.JobTitle, 2), string.Format("Could not find {0}", employee.JobTitle));
                Assert.NotNull(searchResultsTable.FindRow(employee.Site, 3), string.Format("Could not find {0}", employee.Site));
            }
        }

        

    }
}

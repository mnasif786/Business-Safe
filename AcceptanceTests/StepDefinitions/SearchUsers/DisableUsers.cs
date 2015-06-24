using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.SearchUsers
{
    [Binding]
    public class DisableUsers : BaseSteps
    {
        
        [BeforeScenario("DisableUser")]
        [AfterScenario("DisableUser", "ReinstateUser")]
        public static void UndeleteUserKimHoward()
        {
            string sql = string.Format(@"Update [User] Set Deleted = 0 Where UserId = '{0}'", GetUserId("Kim", "Howard"));
            Console.WriteLine(sql);

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }
        }

        [BeforeScenario("ReinstateUser")]
        public static void DeleteUserKimHoward()
        {
            string sql = string.Format(@"Update [User] Set Deleted = 1 Where UserId = '{0}'", GetUserId("Kim", "Howard"));

            Console.WriteLine(sql);

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }
        }

        [When(@"I press 'Delete' link for user with name '(.*)' '(.*)'")]
        public void WhenIPressDeleteLinkForUser(string forename, string surname)
        {
            string userId = GetUserId(forename, surname);
            var linkElement = Find.ByElement(x => x.GetAttributeValue("data-id") == userId.ToLower() && x.ClassName == "icon-remove");

            WebBrowser.Current.Link(linkElement).Click();
        }

        [Then(@"the user '(.*)' '(.*)' should be deleted")]
        public void ThenTheUserKimHowardShouldBeDeleted(string forename, string surname)
        {
            Thread.Sleep(5000);
            string userId = GetUserId(forename, surname);
            string sql = string.Format(@"Select * From [User] Where UserId = '{0}' And Deleted = 1", userId);

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (var sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        Assert.True(sqlDataReader.HasRows);
                    }
                }
            }
        }

        [When(@"I press 'Reinstate' link for user with name '(.*)' '(.*)'")]
        public void WhenIPressReinstateLinkForUser(string forename, string surname)
        {
            string userId = GetUserId(forename, surname);
            var linkElement = Find.ByElement(x => x.GetAttributeValue("data-id") == userId.ToLower() && x.ClassName == "reinstateIcon");

            var link = WebBrowser.Current.Link(linkElement);
            Assert.That(link.Exists, string.Format("Could not find re-instate link for {0} {1}", forename, surname));
            
            link.Click();
        }

        [Then(@"the user '(.*)' '(.*)' should be reinstated")]
        public void ThenTheUserForenameSurnameShouldBeReinstated(string forename, string surname)
        {
            Thread.Sleep(5000);
            string userId = GetUserId(forename, surname);
            string sql = string.Format(@"Select * From [User] Where UserId = '{0}' And Deleted = 0", userId);

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (var sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        Assert.True(sqlDataReader.HasRows);
                    }
                }
            }
        }

        public static string GetUserId(string forename, string surname)
        {
            if (forename == "Kim" && surname == "Howard")
                return "289B10DC-A589-4475-977C-DA525421A19C";

            return string.Empty;
        }
    }
}
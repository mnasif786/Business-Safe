using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.User_Roles
{
    [Binding]
    public class UserRolesSteps : BaseSteps
    {
        [AfterScenario("UserRoles")]
        public static void BeforeUserRoles()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var sql = "SELECT RoleId FROM [dbo].[Role] Where [Description] = 'Test Role'";

                var roleIds = new List<Guid>();
                using (var command = new SqlCommand(sql, conn))
                {
                    var sqlResult = command.ExecuteScalar();
                    var roleId = sqlResult is Guid ? (Guid)sqlResult : new Guid();
                    roleIds.Add(roleId);
                }

                if (roleIds == null) return;

                foreach (var id in roleIds)
                {
                    sql = "DELETE FROM [dbo].[RolesPermissions] Where [RoleId] = '" + id + "'";
                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.ExecuteScalar();
                    }

                    sql = "DELETE FROM [dbo].[Role] Where [RoleId] = '" + id + "'";
                    using (var command = new SqlCommand(sql, conn))
                    {
                        command.ExecuteScalar();
                    }
                }

                // Delete any prior permissions associated with the role "TestRoleWithUsers" so that we can ensure acceptance tests for checking it saves ok work
                sql = "DELETE FROM [dbo].[RolesPermissions] Where [RoleId] = 'AB4117B1-C31A-4303-9EEA-5C28C6A7B009'";

                using (var command = new SqlCommand(sql, conn))
                {
                    command.ExecuteScalar();
                }
            }
        }

        [Given(@"I have entered '(.*)' Id into the '(.*)' field")]
        public void GivenIHaveEnteredIdIntoTheField(string roleName, string roleField)
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var sql = "SELECT RoleId FROM [dbo].[Role] Where [Description] = '" + roleName + "' ORDER BY CreatedOn Desc";
                
                using (var command = new SqlCommand(sql, conn))
                {
                    var sqlResult = command.ExecuteScalar();
                    new FormSteps().GivenIHaveEnteredIntoTheField(sqlResult.ToString(), roleField);
                }
            }
        }


        [Given(@"I have created 'Test Role'")]
        public void GivenIHaveCreatedTestRole()
        {
            GivenIHaveNavigatedToAddUserRolesPage();
            FormSteps.EnterValueIntoField("Test Role", "RoleName");
            GivenIHaveCheckedViewCompanyDetailsPermissionCheckbox("ViewCompanyDetails");
            GivenIHaveCheckedViewCompanyDetailsPermissionCheckbox("EditCompanyDetails");
            CommandButtonSteps.PressButton("SaveUserRoleButton");
        }


        [Given(@"I have navigated to user roles page")]
        public void GivenIHaveNavigatedToUserRolesPage()
        {
            var companyId = ScenarioContextHelpers.GetCompanyId();
            WebBrowser.Driver.Navigate("/Users/UserRoles?companyId=" + companyId);
            Thread.Sleep(2000);
        }

        [Then(@"the permissions checkboxes should not be enabled")]
        public void ThenThePermissionsCheckboxesShouldNotBeEnabled()
        {
            Debug.WriteLine("Getting Permissions");
            Thread.Sleep(2000);

            foreach (CheckBox checkbox in WebBrowser.Current.CheckBoxes)
            {
                var disabledAttr = checkbox.GetAttributeValue("disabled").ToLower();
                Assert.That(disabledAttr, Is.EqualTo("true").Or.EqualTo("disabled"));
            }
        }

        [Given(@"I have navigated to add user roles page")]
        public void GivenIHaveNavigatedToAddUserRolesPage()
        {
            var companyId = ScenarioContextHelpers.GetCompanyId();
            WebBrowser.Driver.Navigate("/Users/UserRoles/New?companyId=" + companyId);
            Thread.Sleep(4000);
        }

        [Given(@"I have checked '(.*)' permission checkbox")]
        [When(@"I have checked '(.*)' permission checkbox")]
        public void GivenIHaveCheckedViewCompanyDetailsPermissionCheckbox(string permission)
        {
            Debug.WriteLine("Getting check box");
            Thread.Sleep(4000);
            foreach (CheckBox checkBox in WebBrowser.Current.CheckBoxes)
            {
                if (checkBox.OuterHtml.Contains(permission))
                {
                    checkBox.Click();
                    break;
                }
            }


            var permissions = ScenarioContextHelpers.GetUserRolePermissions();
            
            if(!permissions.Contains(permission))
                permissions.Add(permission);

            ScenarioContextHelpers.SetUserRolePermissions(permissions);
        }

        [Then(@"the user role '(.*)' should be edited")]
        [Then(@"the user role '(.*)' should be created")]
        public void ThenTheUserRoleShouldBeCreated(string roleName)
        {
            var currentCompanyId = GetCurrentCompanyId();

            var sql = new StringBuilder();
            sql.Append("Select COUNT(*) From dbo.Role R INNER JOIN dbo.RolesPermissions RP ON R.RoleId = RP.RoleID WHERE R.ClientId = '" + currentCompanyId + "' AND R.Description = '" + roleName + "'");


            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    var result = command.ExecuteScalar();
                    var expected = ScenarioContextHelpers.GetUserRolePermissions().Count;
                    Assert.That(result, Is.EqualTo(expected));
                }
            }
        }

        [Then(@"the user role '(.*)' should be deleted")]
        public void ThenTheUserRoleTestRoleShouldBeDeleted(string roleName)
        {
            Thread.Sleep(2000);
            
            var currentCompanyId = GetCurrentCompanyId();

            var sql = new StringBuilder();
            sql.Append("Select COUNT(*) From dbo.Role WHERE ClientId = '" + currentCompanyId + "' AND Name = '" + roleName + "' AND Deleted = 1");
            
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

        [Then(@"the '(.*)' modal confirmation should open '(.*)'")]
        public void ThenModalConfirmationShouldOpen(string dialogId, bool closeBrowser)
        {
            Element dialogBox =
                WebBrowser.Current.Elements.FirstOrDefault(
                    x => x.GetAttributeValue("aria-labelledby") == "ui-dialog-title-" + dialogId);

            Assert.That(dialogBox.Style.Display, Is.Not.EqualTo("none"));
        }

        [Then(@"the '(.*)' modal confirmation dialog should close")]
        public void ThenModalConfirmationShouldClose(string dialogId)
        {
            Element dialogBox =
                WebBrowser.Current.Elements.FirstOrDefault(
                    x => x.GetAttributeValue("aria-labelledby") == "ui-dialog-title-" + dialogId);

            Assert.That(dialogBox.Style.Display, Is.EqualTo("none"));
        }
    }
}
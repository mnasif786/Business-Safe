using System;
using System.Data.SqlClient;
using System.Linq;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.UserPermissions
{
    [Binding]
    public class UserPermissionsSteps : BaseSteps
    {
        [Then(@"the '(.*)' link should not be available")]
        [Then(@"the '(.*)' menu item should not be available")]
        public void ThenTheSitesMenuItemShouldNotBeAvailable(string linkId)
        {
            var siteLink = WebBrowser.Current.Link(Find.ById(linkId));
            Assert.IsFalse(siteLink.Exists);
        }

        [Then(@"the '(.*)' link should be available")]
        [Then(@"the '(.*)' menu item should be available")]
        public void ThenTheSitesMenuItemShouldBeAvailable(string linkId)
        {
            var siteLink = WebBrowser.Current.Link(Find.ById(linkId));
            Assert.IsTrue(siteLink.Exists);
        }

        [Given(@"I have navigated straight to the '(.*)' url")]
        [When(@"I have navigated straight to the '(.*)' url")]
        public void WhenIHaveNavigatedStraightToTheSitesUrl(string url)
        {
            var companyId = ScenarioContextHelpers.GetCompanyId();

            if (url.ToLower() == "sites")
            {
                WebBrowser.Driver.Navigate("Sites");
            }
            else if (url.ToLower() == "view employees")
            {
                WebBrowser.Driver.Navigate("/Employees/EmployeeSearch?companyId=" + companyId);
            }
            else if (url.ToLower() == "add edit general risk assessment")
            {
                WebBrowser.Driver.Navigate("GeneralRiskAssessments?companyId=" + companyId);
            }
            else if (url.ToLower() == "edit employee")
            {
                WebBrowser.Driver.Navigate("/Employees/Employee/Index?companyId=55881&employeeId=086838fc-76c0-4bf7-afd7-9b0d53372d7b");
            }
            else if(url.ToLower() == "user search")
            {
                WebBrowser.Driver.Navigate("/Users/ViewUsers?companyId=" + companyId);
            }
        }

        [Then(@"the '(.*)' command should be available")]
        public void ThenTheIconEditCommandShouldBeAvailable(string iconClassName)
        {
            System.Threading.Thread.Sleep(4000);
            var commands = WebBrowser.Current.Links.Where(ic => ic.ClassName == iconClassName);

            Assert.That(commands.Count(), Is.GreaterThan(0));
        }

        [Then(@"the '(.*)' command should not be available")]
        public void ThenTheIconViewCommandShouldNotBeAvailable(string iconClassName)
        {
            System.Threading.Thread.Sleep(4000);
            var commands = WebBrowser.Current.Links.Where(ic => ic.ClassName == iconClassName);

            Assert.That(commands.Count(), Is.EqualTo(0));
        }

        [Given(@"I have clicked on the '(.*)' command for the first employee record")]
        public void GivenIHaveClickedOnTheIconEditCommandForTheFirstEmployeeRecord(string iconClassName)
        {
            System.Threading.Thread.Sleep(4000);
            foreach (Element image in WebBrowser.Current.Elements)
            {
                if (image.ClassName != null && image.ClassName.Contains("icon-edit"))
                {
                    image.Click();
                    break;
                }
            }
        }

        [Then(@"the '(.*)' on the employee edit screen should be available")]
        public void ThenTheSaveBtnOnTheEmployeeEditScreenShouldBeAvailable(string buttonId)
        {
            System.Threading.Thread.Sleep(4000);
            var commandBtn = WebBrowser.Current.Button(Find.ById(buttonId));
            Assert.IsTrue(commandBtn.Exists);
        }


        [Then(@"I should be redirected to Peninsula login page")]
        public void ThenIShouldBeRedirectedToPeninsulaLoginPage()
        {
            Assert.That(WebBrowser.Current.Url.ToLower(), Contains.Substring("signin/index"));
        }

        [Given(@"I have clicked the '(.*)'")]
        public void GivenIHaveClickedTheViewSitesLink(string linkId)
        {
            WebBrowser.Current.Link(Find.ById(linkId)).Click();
        }

        [Given(@"I have clicked the '(.*)' in the site tree structure")]
        public void GivenIHaveClickedTheMainSiteInTheSiteTreeStructure(string siteName)
        {
            WebBrowser.Current.Divs.First(d => d.ClassName == "linked-site" && d.GetAttributeValue("data-id").Length > 0).Click();
        }

        [Then(@"the '(.*)' should be available")]
        public void ThenTheSaveSiteDetailsButtonIsShouldBeAvailable(string buttonId)
        {
            System.Threading.Thread.Sleep(4000);
            var button = WebBrowser.Current.Button(Find.ById(buttonId));
            Assert.IsTrue(button.Exists);
        }

        [AfterScenario("ResetRussellWilliamsSiteId")]
        public void ResetRussellWilliamsSiteId()
        {
            const string sql = "UPDATE [User] SET SiteId = 371 WHERE UserId = '16AC58FB-4EA4-4482-AC3D-000D607AF67C'";
            Console.WriteLine(sql);
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql, conn);
            }
        }
    }
}
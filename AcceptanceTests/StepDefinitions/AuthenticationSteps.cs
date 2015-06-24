using System;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class AuthenticationSteps: BaseSteps
    {
        [Given(@"I have logged in as company with id '(.*)' as a '(.*)'")]
        [When(@"I have logged in as company with id '(.*)' as a '(.*)'")]
        public void GivenIHaveLoggedInAsCompanyWithId(long companyId, string userRole)
        {
            var userId = GetUserIdForCompany(companyId, userRole);
            ScenarioContextHelpers.SetCompanyId(companyId);
            GotToAutoLogInController(companyId, userId);
        }

        [Given(@"I have logged in as company with id '(.*)' as Barry Scott")]
        [When(@"I have logged in as company with id '(.*)' as Barry Scott")]
        public void GivenIHaveLoggedInAsBarryScott(long companyId)
        {
            var userId = new Guid("1ED907A9-FA3B-460B-8885-67500D82C119");
            ScenarioContextHelpers.SetCompanyId(companyId);
            GotToAutoLogInController(companyId, userId);
        }

        private static void GotToAutoLogInController(long companyId, Guid userId)
        {
            var loginUrl = string.Format("AutoLogInFromPeninsula/Index?companyId={0}&userId={1}", companyId, userId);
            WebBrowser.Driver.Navigate(loginUrl);
        }

        [Given(@"I have logged in as company with id '(.*)'")]
        public void GivenIHaveLoggedInAsCompanyWithId(long companyId)
        {
            var userId = GetUserIdForCompany(companyId, "UserAdmin");
            ScenarioContextHelpers.SetCompanyId(companyId);
            GotToAutoLogInController(companyId, userId);
        }

        [Then(@"the Log Out menu item should be available")]
        public void ThenTheLogOutMenuItemShouldBeAvailable()
        {
            var logOutLink = WebBrowser.Current.Link(Find.ById("LogOutLink"));
            Assert.IsTrue(logOutLink.Exists);
        }

        private Guid GetUserIdForCompany(long companyId, string userRole)
        {
            if (companyId == 55881 && userRole == "UserAdmin")
                return Guid.Parse("16ac58fb-4ea4-4482-ac3d-000d607af67c");

            if (companyId == 55881 && userRole == "General User")
                return Guid.Parse("E7385B71-ABFC-400A-8FB0-CC58ACA78E38");

            if (companyId == 55881 && userRole == "Health and Safety Manager")
                return Guid.Parse("289B10DC-A589-4475-977C-DA525421A19C");

            if (companyId == 24072)
                return Guid.Parse("817927d0-ed72-44f9-bc20-fc9e26909754");

            if (companyId == 37634)
                return Guid.Parse("91f0e64a-7c04-4d89-a336-56c82d810652");

            if (companyId == 31028)
                return Guid.Parse("E8735020-0BE6-46AC-BB74-13908E75CCDB");

            if (companyId == 30128)
                return Guid.Parse("6BDA9CE4-18DD-49AC-8A2C-3D9CFC510181");

            if (companyId == 55255)
                return Guid.Parse("4FE70604-8393-4355-BA4A-7C454BEF158D");
            

            throw new Exception("User Id not defined for test company " + companyId);
        }
    }
}
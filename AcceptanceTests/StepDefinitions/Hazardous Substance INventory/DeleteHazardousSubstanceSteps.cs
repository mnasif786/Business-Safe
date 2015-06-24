using System.Data.SqlClient;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.HazardousSubstances
{
    [Binding]
    public class DeleteHazardousSubstanceSteps : BaseSteps
    {
        
        [AfterScenario("DeletingHazardousSubstance")]
        [BeforeScenario("DeletingHazardousSubstance")]
        public static void BeforeDeleteHazardousSubstanceFeature()
        {
            Thread.Sleep(4000);
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var sql = "Update [dbo].[HazardousSubstance] Set Deleted = 0 Where Id IN (1,3) ";
                    using (var command = new SqlCommand(sql.ToString(), conn))
                    {
                        command.ExecuteScalar();
                    }
            }
        }

        [Given(@"I am on the search hazardous substances page for company '(.*)'")]
        public void GivenIAmOnTheSearchHazardousSubstancesPageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("HazardousSubstanceInventory?companyId={0}", companyId));
        }

        [When(@"I press 'Delete' link for hazardous substance with id '(.*)'")]
        public void WhenIPressDeleteLinkForTestHazardousSubstance3(string hazardousSubstanceId)
        {
            Thread.Sleep(2000);
            var removeIconFound = false;

            var cssClass = ElementCssHelper.ClassFor(Elements.HazardousSubstanceRemoveButton);
            var removeLinks = WebBrowser.Current.Links.Filter(Find.ByClass(cssClass));
            foreach (Link link in removeLinks)
            {
                if (link.GetAttributeValue("data-id") == hazardousSubstanceId)
                {
                    link.Click();
                    removeIconFound = true;
                    break;
                }
            }

            Assert.IsTrue(removeIconFound, string.Format("Could not find remove button for hazardous substance with id {0}", hazardousSubstanceId));
        }

        [Given(@"I press 'Reinstate' link for hazardous substance with id '(.*)'")]
        [When(@"I press 'Reinstate' link for hazardous substance with id '(.*)'")]
        public void WhenIPressReinstateLinkForTestHazardousSubstance3(string hazardousSubstanceId)
        {
            Thread.Sleep(2000);
            var reinstateIconFound = false;

            var cssClass = ElementCssHelper.ClassFor(Elements.HazardousSubstanceReinstateButton);
            var reinstateLinks = WebBrowser.Current.Links.Filter(Find.ByClass(cssClass));
            foreach (Link link in reinstateLinks)
            {
                if (link.GetAttributeValue("data-id") == hazardousSubstanceId)
                {
                    link.Click();
                    reinstateIconFound = true;
                    break;
                }
            }

            Assert.IsTrue(reinstateIconFound, string.Format("Could not find reinstate button for hazardous substance with id {0}", hazardousSubstanceId));
        }

        [Then(@"the hazardous substance with id '(.*)' should then be deleted")]
        public void ThenTheHazardousSubstanceNamedTestHazardousSubstance3ShouldThenBeDeleted(string hazardousSubstanceId)
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                string sql =
                    string.Format(
                        "SELECT Count(*) FROM HazardousSubstance WHERE Id = '{0}' AND DELETED = 1", hazardousSubstanceId);

                var result = runSQLCommand(sql, conn);
                Assert.That(result, Is.EqualTo(1));

                // reset test hazsub 3...
                //var stringBuilder = string.Format("UPDATE HazardousSubstance SET DELETED =0 WHERE Id = '{0}' ", hazardousSubstanceId);

                //runSQLCommand(stringBuilder, conn);
            }
        }

    }
}

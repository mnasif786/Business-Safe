using System;
using System.Data.SqlClient;
using System.Text;

using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Domain.Entities;

using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.PersonalRiskAssessment
{
    [Binding]
    public class CreateSteps : BaseSteps
    {
        [Given(@"I am redirected to the summary page for the new personal risk assessment")]
        [Then(@"I am redirected to the summary page for the new personal risk assessment")]
        public void ThenIAmRedirectedToTheSummaryPageForTheNewPersonalRiskAssessment()
        {
            var currentUrl = WebBrowser.Current.Url.ToLower();
            var expectedUrlPortion = "/PersonalRiskAssessments/summary?riskAssessmentId=".ToLower();
            Assert.That(currentUrl.Contains(expectedUrlPortion), string.Format("URL {0}, does not contain {1}", currentUrl, expectedUrlPortion));

        }

        [Then(@"the DateOfAssessment text box should contain todays date")]
        public void TheDateOfAssessmentTextBoxShouldContainTodaysDate()
        {
            var dateOfAssessment = WebBrowser.Current.TextField(Find.ById("DateOfAssessment"));
            Assert.That(dateOfAssessment.Text, Is.EqualTo(DateTime.Now.ToString("dd/MM/yyyy")));
        }

        [When(@"I try to hack the url as Barry Scott to view sensitive personal risk assessment")]
        public void I_try_to_hack_the_url_as_Barry_Scott_to_view_sensitive_personal_risk_assessment()
        {
            WebBrowser.Driver.Navigate("PersonalRiskAssessments/Summary?riskAssessmentId=58&companyId=55881");
        }

        [BeforeScenario("Personal_Risk_Assessments")]
        public void EnsureKnownStatePRAsAreUndeleted()
        {
            var sql = new StringBuilder();
            sql.AppendLine("update [RiskAssessment] set deleted = 0 where id in ( select id from [PersonalRiskAssessment] ) and id <= " + LastRiskAssessmentId);
            
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }
    }
}

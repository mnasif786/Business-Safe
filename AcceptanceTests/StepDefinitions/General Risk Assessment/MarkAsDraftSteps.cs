using System.Data.SqlClient;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Data.Repository;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class MarkAsDraftSteps: BaseSteps
    {
        [Given(@"the new risk assessment is not marked as draft")]
        public void GivenTheNewRiskAssessmentIsNotMarkedAsDraft()
        {
            Thread.Sleep(4000);

            var sql = new StringBuilder();
            sql.AppendLine("UPDATE RiskAssessment Set StatusId = 1 WHERE ID = '" + GetCurrentRiskAssessmentId() + "' ");


            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }

        }

        [When(@"reload the current page")]
        public void WhenReloadTheCurrentPage()
        {
            WebBrowser.Current.Refresh();
        }


        [When(@"I have clicked 'Draft'")]
        public void WhenIHaveClickedDraft()
        {
            var checkBox = WebBrowser.Current.CheckBox(Find.ById("IsDraft"));
            checkBox.Click();
            Thread.Sleep(4000);
        }

        [Given(@"I have loaded the risk assessment")]
        public void GivenIHaveLoadedTheRiskAssessment()
        {
            var currentRiskAssessmentId = GetCurrentRiskAssessmentId();
            var currentCompanyId = GetCurrentCompanyId();
            WebBrowser.Driver.Navigate("/GeneralRiskAssessments/PremisesInformation?riskAssessmentId=" + currentRiskAssessmentId + "&CompanyId=" + currentCompanyId);
        }


        [Then(@"the risk assessment should be marked as draft")]
        public void ThenTheRiskAssessmentShouldBeMarkedAsDraft()
        {
            var riskAssessmentId = GetCurrentRiskAssessmentId();
            var sql = new StringBuilder();
            sql.Append("SELECT Count(*) FROM RiskAssessment WHERE Id = '" + riskAssessmentId + "' AND StatusId = '" + (int)RiskAssessmentStatus.Draft + "'");

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

        [Then(@"the risk assessment should be marked as live")]
        public void ThenTheRiskAssessmentShouldBeMarkedAsLive()
        {
            var riskAssessmentId = GetCurrentRiskAssessmentId();
            var sql = new StringBuilder();
            sql.Append("SELECT Count(*) FROM RiskAssessment WHERE Id = '" + riskAssessmentId + "' AND StatusId = '" + (int)RiskAssessmentStatus.Live + "'");

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

    }
}

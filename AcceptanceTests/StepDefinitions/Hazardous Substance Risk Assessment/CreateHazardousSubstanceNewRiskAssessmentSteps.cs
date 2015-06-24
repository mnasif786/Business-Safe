using System.Data.SqlClient;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.HazardousSubstances
{
    [Binding]
    public class CreateHazardousSubstanceNewRiskAssessmentSteps : BaseSteps
    {
        [BeforeScenario(@"@HazardousSubstanceRiskAssessments")]
        [AfterScenario(@"@HazardousSubstanceRiskAssessments")]
        public static void TearDownSystemUnderTest()
        {
            var sql = new StringBuilder();
            sql.AppendLine("DELETE FROM RiskAssessment WHERE Reference = 'Test Reference'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        [Given(@"I am on the create hazardous substance risk assessment page for company '(.*)'")]
        [Given(@"I am on description tab with company Id '(.*)'")]
        public void GivenIAmOnPremisesInformationTabWithCompanyId(int companyId)
        {
            WebBrowser.Driver.Navigate("HazardousSubstanceRiskAssessments/RiskAssessment/Create?companyId=" + companyId);
        }

        [Then(@"a new hazardous substance risk assessment should be created with reference '(.*)'")]
        public void ThenANewHazardousSubstanceRiskAssessmentShouldBeCreatedWithReferenceTestReference(string reference)
        {
            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
               .CreateSQLQuery("SELECT Count(Id) FROM RiskAssessment where reference = '" + reference + "'")
               .UniqueResult();

            Assert.That(result, Is.EqualTo(1));
        }


        [Then(@"the hazardous substance risk assessment is updated with date of assessment set to '(.*)'")]
        public void ThenTheNewRiskAssessmentHasCorrectValues(string assessmentDate)
        {
            var riskAssessmentId = WebBrowser.Current.Element(Find.ById("HazardousSubstanceRiskAssessmentId")).GetValue("value");

            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
                .CreateSQLQuery("SELECT CONVERT(VARCHAR(10), AssessmentDate, 103) AS [DD/MM/YYYY] FROM RiskAssessment WHERE Id = '" + riskAssessmentId + "' ")
                .UniqueResult();
            
            Assert.That(result, Is.EqualTo(assessmentDate));
        }

        [Given(@"I have loaded the hazardous substance risk assessment")]
        public void GivenIHaveLoadedTheRiskAssessment()
        {
            var currentRiskAssessmentId = GetCurrentRiskAssessmentId();
            var currentCompanyId = GetCurrentCompanyId();
            WebBrowser.Driver.Navigate("HazardousSubstanceRiskAssessments/Description?hazardousSubstanceRiskAssessmentId=" + currentRiskAssessmentId + "&CompanyId=" + currentCompanyId);
        }

        [Then(@"the hazardous substance risk assessment should be marked as draft")]
        public void ThenTheRiskAssessmentShouldBeMarkedAsDraft()
        {
            Thread.Sleep(4000);

            var sql = new StringBuilder();
            sql.AppendLine("Select StatusId FROM RiskAssessment WHERE ID = '" + GetCurrentRiskAssessmentId() + "' ");
            

            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
                            .CreateSQLQuery(sql.ToString())
                            .UniqueResult();

            Assert.That(result, Is.EqualTo((int)RiskAssessmentStatus.Draft));
        }

        [Then(@"the hazardous substance risk assessment should be marked as live")]
        public void ThenTheRiskAssessmentShouldBeMarkedAsLive()
        {
            Thread.Sleep(4000);

            var sql = new StringBuilder();
            sql.AppendLine("Select StatusId FROM RiskAssessment WHERE ID = '" + GetCurrentRiskAssessmentId() + "' ");


            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
                            .CreateSQLQuery(sql.ToString())
                            .UniqueResult();

            Assert.That(result, Is.EqualTo((int)RiskAssessmentStatus.Live));
        }

        [Then(@"I should be redirected to the hazardous substance risk assessment index page")]
        public void ThenIShouldBeRedirectedToTheHazardousSubstanceRiskAssessmentIndexPage()
        {
            const string expectedUrl = "HazardousSubstanceRiskAssessments?";
            Assert.That(WebBrowser.Current.Url.ToLower(), Contains.Substring(expectedUrl.ToLower()));
        }

        [Then(@"the risk phrase '(.*)' is visible")]
        public void ThenTheRiskPhraseIsVisible(string riskPhrase)
        {
            Element riskPhraseList = WebBrowser.Current.Element(Find.ById("riskPhrases"));
            Assert.That(riskPhraseList.InnerHtml.Contains(riskPhrase), string.Format("Could not find risk phrase {0}", riskPhrase));
        }

        [Then(@"Hazard group '(.*)' is selected")]
        public void HazardGroupIsSelected(string hazardGroupCode)
        {
            Thread.Sleep(1000);
            var radioButton = WebBrowser.Current.RadioButton(Find.ById("HazardGroup") && Find.ByValue("B"));
            Assert.IsTrue(radioButton.Checked);
        }

        [Then(@"the control measure '(.*)' should be saved to the hazardous substance risk assessment")]
        public void ThenTheControlMeasureTestControlMeasureShouldBeSavedToTheHazardousSubstanceRiskAssessment(string controlMeasureText)
        {
            Thread.Sleep(2000);
            Assert.True(GetControlMeasureExistResult(controlMeasureText));
        }

        [Then(@"the control measure '(.*)' should be removed from the hazardous substance risk assessment")]
        public void ThenTheControlMeasureShouldBeRemovedFromTheHazardousSubstanceRiskAssessment(string controlMeasureText)
        {
            Thread.Sleep(2000);
            ElementHelperSteps.ThenTheElementWithClassVisibilityIs("controlMeasureText", "false");
        }

        private bool GetControlMeasureExistResult(string controlMeasureText)
        {
            var riskAssessmentId = GetCurrentRiskAssessmentId();
            var sql = new StringBuilder();

            sql.Append("Select * From HazardousSubstanceRiskAssessmentControlMeasure ");
            sql.Append("Where HazardousSubstanceRiskAssessmentId = '" + riskAssessmentId + "' ");
            sql.Append("And ControlMeasure = '" + controlMeasureText + "'");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    using (var sqlDataReader = command.ExecuteReader())
                    {
                        return sqlDataReader.HasRows;
                    }
                }
            }
        }
    }
}

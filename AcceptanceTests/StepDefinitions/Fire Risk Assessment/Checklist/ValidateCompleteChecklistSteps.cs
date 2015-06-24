using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Fire_Risk_Assessment.Checklist
{
    [Binding]
    public class ValidateCompleteChecklistSteps : BaseSteps
    {

        [BeforeScenario("ChangesFRA55Checklist")]
        [AfterScenario("ChangesFRA55Checklist")]
        public void TearDownDatabase()
        {
            var sql = new StringBuilder("UPDATE Answer SET YesNoNotApplicableResponse = 3 ");
            sql.AppendLine("WHERE FireRiskAssessmentChecklistId = (");
            sql.AppendLine("SELECT id FROM FireRiskAssessmentChecklist WHERE FireRiskAssessmentId = 55");
            sql.AppendLine(")");
            
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [Then(@"the error message '(.*)' is displayed for question '(.*)'")]
        public void ThenTheErrorMessageIsDisplayedForQuestion(string errorMessage, int questionId)
        {
            var questionDiv = WebBrowser.Current.Divs
                .Filter(Find.ByClass("question span"))
                .SingleOrDefault(x => x.GetAttributeValue("data-question-id") == questionId.ToString());

            Assert.That(questionDiv.Exists, string.Format("Could not find question div with id {0}", questionId));

            var errorSpan = questionDiv.Spans.Filter(Find.ByClass("field-validation-error")).SingleOrDefault();
            Assert.That(errorSpan.Exists, string.Format("Could not find error message for question with id {0}", questionId));

            Assert.That(errorSpan.InnerHtml.Contains(errorMessage));

        }

        [Given(@"I select '(.*)' for questions '(.*)' to '(.*)'")]
        public void GivenISelectForQuestionsTo(string idPrefix, int start, int finish)
        {
            for(var i = start; i <= finish; i++)
            {
                var radioButton = WebBrowser.Current.RadioButton(Find.ById(string.Format("{0}_{1}", idPrefix, i)));
                radioButton.Click();
            }
        }

    }
}
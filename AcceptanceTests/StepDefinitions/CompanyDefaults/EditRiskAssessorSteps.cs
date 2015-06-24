using System.Data.SqlClient;
using System.Text;

using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.CompanyDefaults
{
    [Binding]
    public class EditRiskAssessorSteps : BaseSteps
    {
        [AfterScenario("ChangesKnownRiskAssessors")]
        public void ResetRiskAssessors()
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE RiskAssessor ");
            sql.AppendLine("SET SiteId = NULL, ");
            sql.AppendLine("    HasAccessToAllSites = 1, ");
            sql.AppendLine("    DoNotSendTaskOverdueNotifications = 1, ");
            sql.AppendLine("    DoNotSendTaskCompletedNotifications = 1, ");
            sql.AppendLine("    DoNotSendReviewDueNotification = 1 ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        [Given(@"I click on the edit button for risk assessor with id '(.*)'")]
        public void GivenIClickOnTheEditButtonForRiskAssessorWithId(int id)
        {
            var editButtons = WebBrowser.Current.Links.Filter(Find.ByClass("icon-edit edit-risk-assessor"));
            foreach(Link link in editButtons)
            {
                var dataId = link.GetAttributeValue("data-id");
                if (dataId == id.ToString())
                {
                    link.Click();
                    break;
                }
            }
        }
    }
}

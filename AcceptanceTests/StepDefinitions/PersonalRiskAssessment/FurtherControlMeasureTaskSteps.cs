using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;
using TechTalk.SpecFlow;

using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.PersonalRiskAssessment
{
    [Binding]
    public class FurtherControlMeasureTaskSteps: BaseSteps
    {
        [Then(@"the personal risk assessment further control measure task should be created with the title '(.*)'")]
        public void ThenThePersonalRiskAssessmentFurtherControlMeasureTaskShouldBeCreated(string title)
        {
            var riskAssessmentId = GetCurrentRiskAssessmentId();
            var sql = new StringBuilder();
            sql.Append("Select COUNT(*) From MultiHazardRiskAssessmentHazard MHRAH INNER JOIN dbo.Task T ON MHRAH.ID = T.MultiHazardRiskAssessmentHazardId WHERE RiskAssessmentId = '" + riskAssessmentId + "' AND Title = '" + title + "'");

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

        [Then(@"the personal risk assessment further control measure task should be removed with the title '(.*)'")]
        public void ThenThePersonalRiskAssessmentFurtherControlMeasureTaskShouldBeRemovedCreated(string title)
        {
            var row = GetElement(GetFurtherControlMeasureTaskRowByTitle, new[] { title }, 3000);
            Assert.That(row, Is.Null);
        }

        [Given(@"I click on the further action task row for task with title '(.*)'")]
        [When(@"I click on the further action task row for task with title '(.*)'")]
        public void GivenIClickOnTheFurtherActionTaskRowForTaskWithTitle(string title)
        {
            var row = (WatiN.Core.TableRow)GetElement(GetFurtherControlMeasureTaskRowByTitle, new[] { title }, 3000); //GetFurtherControlMeasureTaskRowByTitle(title);
            row.TableCells.ElementAt(1).Click();
            ScenarioContextHelpers.SetFurtherActionTaskRow(row);
        }

        private Element GetFurtherControlMeasureTaskRowByTitle(object[] args)
        {
            return WebBrowser.Current.TableRows.ToList()
                .Where(row => row.TableCells.Count() > 2)
                .FirstOrDefault(row => row.TableCells[1].Text != null && row.TableCells[1].Text.Trim() == args[0].ToString().Trim());
        }

        [AfterScenario("deletesPRATask43")]
        public void ReinstateTaskPRARA2()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand("UPDATE dbo.Task SET DELETED = 0 WHERE Id = 43  ", conn);
            }
        }

        [AfterScenario("changesTitleOfPRATask43")]
        public void ResetTitleTaskPRARA2()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand("UPDATE dbo.Task SET Title = 'PRA RA 2' WHERE Id = 43 ", conn);
            }
        }

        [Then(@"the personal risk assessment further control measure task with the description '(.*)' should have title '(.*)'")]
        public void ThenThePersonalRiskAssessmentFurtherControlMeasureTaskWithTheDescriptionShouldHaveTitle(string description, string title)
        {
            const int titleColumnIndex = 1;
            const int descriptionColumnIndex = 2;
            foreach (var row in WebBrowser.Current.TableRows.ToList())
            {
                if (row.TableCells.Count() > 2)
                {
                    if (row.TableCells[descriptionColumnIndex].Text != null && row.TableCells[descriptionColumnIndex].Text.Trim() == description.Trim())
                    {
                        Assert.That(row.TableCells[titleColumnIndex].Text.Trim(), Is.EqualTo(title.Trim()));
                        return;
                    }
                }
            }
        }

    }
}
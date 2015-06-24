using System.Data.SqlClient;
using System.Dynamic;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using Table = TechTalk.SpecFlow.Table;
using TableRow = TechTalk.SpecFlow.TableRow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment.Archive
{
    [Binding]
    public class GeneralRiskAssessmentArchiveViewSteps : BaseSteps
    {
        [BeforeScenario("requiresArchivedGeneralRiskAssessments")]
        public void Setup()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                var insertSql = new StringBuilder("INSERT INTO [BusinessSafe].[dbo].[RiskAssessment] ");
                insertSql.Append("(Title, Reference, AssessmentDate, ClientId, Deleted, CreatedOn, CreatedBy, SiteId, RiskAssessorId, StatusId)");
                insertSql.AppendLine("VALUES ('Test Archived RA {0}','Test Archived RA {0}','2016-06-23 00:00:00',55881,0,getdate(),");
                insertSql.AppendLine("'16AC58FB-4EA4-4482-AC3D-000D607AF67C',378,");
                insertSql.AppendLine("2 ,2) ");
                insertSql.AppendLine("SET @InsertedId = SCOPE_IDENTITY() ");
                insertSql.AppendLine(" INSERT INTO [BusinessSafe].[dbo].[MultiHazardRiskAssessment] (Id, Location, TaskProcessDescription) ");
                insertSql.AppendLine(" VALUES (@InsertedId, 'a location' , 'a description') ");
                insertSql.AppendLine(" INSERT INTO [BusinessSafe].[dbo].[GeneralRiskAssessment] (Id) ");
                insertSql.AppendLine(" VALUES (@InsertedId)");

                var sql = new StringBuilder("DECLARE @InsertedId bigint ");
                sql.AppendLine((string.Format(insertSql.ToString(), "01")));
                sql.AppendLine((string.Format(insertSql.ToString(), "02")));
                sql.AppendLine((string.Format(insertSql.ToString(), "03")));
                sql.AppendLine((string.Format(insertSql.ToString(), "04")));
                sql.AppendLine((string.Format(insertSql.ToString(), "05")));

                runSQLCommand(sql.ToString(), conn);
            }
        }

        [Then(@"the risk assessment table should contain the following data:")]
        public void ThenTheRiskAssessmentTableShouldContainTheFollowingData(Table table)
        {
            graTableContainsExpected(table);
        }

        [Then(@"the risk assessment table should contain the following data output has only one cell:")]
        public void ThenTheRiskAssessmentTableShouldContainTheFollowingDataWithOnlyFirstCell(Table table)
        {
            graTableContainsExpected(table, true);
        }

        private void graTableContainsExpected(Table table, bool onlyCheckFirstCell = false)
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();

            for (var i = 0; i < table.Rows.Count; i++)
            {
                dynamic gra = new ExpandoObject();

                TableRow row = table.Rows[i];
                gra.Ref = row["Reference"];
                gra.Title = row["Title"];
                gra.Description = row["Site"];
                gra.AssignedTo = row["Assigned To"];
                gra.Status = row["Status"];
                gra.CompletionDueDate = row["Completion Due Date"];

                var outputRow = searchResultsTable.TableRows[i];
                Assert.That(outputRow.TableCells[0].InnerHtml.Replace("&nbsp;", " ").Trim(), Is.EqualTo(gra.Ref.Trim()));
                if (!onlyCheckFirstCell)
                {
                    Assert.That(outputRow.TableCells[1].InnerHtml.Replace("&nbsp;", " ").Trim(), Is.EqualTo(gra.Title));
                    Assert.That(outputRow.TableCells[2].InnerHtml.Replace("&nbsp;", " ").Trim(), Is.EqualTo(gra.Description));
                    Assert.That(outputRow.TableCells[3].InnerHtml.Replace("&nbsp;", " ").Trim(), Is.EqualTo(gra.AssignedTo));
                    Assert.That(outputRow.TableCells[4].InnerHtml.Replace("&nbsp;", " ").Trim(), Is.EqualTo(gra.Status));
                    Assert.That(outputRow.TableCells[5].InnerHtml.Replace("&nbsp;", " ").Trim(), Is.EqualTo(gra.CompletionDueDate));
                }
            }
        }
    }
}

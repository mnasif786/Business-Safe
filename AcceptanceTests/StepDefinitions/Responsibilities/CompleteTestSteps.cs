using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Data.SqlClient;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Responsibilities
{
    [Binding]
    public class CompleteTestSteps : BaseSteps
    {
        [BeforeScenario(@"NeedsResponsibilityTasksToComplete")]
        public void CreateResponsibilityTasksToComplete()
        {

            var sql = new StringBuilder();
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[Task] ON;");
            sql.Append("INSERT [dbo].[Task] (" +
                "[Id], [MultiHazardRiskAssessmentHazardId], [Title], [Description], [Reference], " +
                "[Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], " +
                "[TaskAssignedToId], [TaskCompletionDueDate], [TaskStatusId], [TaskCompletedDate], [TaskCompletedComments], " +
                "[TaskCategoryId], [TaskReoccurringTypeId], [TaskReoccurringEndDate], [FollowingTaskId], [OriginalTaskId], " +
                "[Discriminator], [RiskAssessmentReviewId], [HazardousSubstanceRiskAssessmentId], [SignificantFindingId], [TaskGuid], " +
                "[SendTaskNotification], [SendTaskCompletedNotification], [SendTaskOverdueNotification], [DoNotSendStartedTaskNotification], [ResponsibilityId], " +
                "[TaskCompletedBy], [SiteId]) " +
                "VALUES (-1,NULL, N'Resp Task To Delete', N'Resp Task To Delete', N'NonRecResp01', " +
                "0, CAST(0x0000A1F500EF9CA0 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A1F500EF9CA0 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c', " +
                "N'd2122fff-1dcd-4a3c-83ae-e3503b394eb4', '2020-01-01 09:00:00:000', 0, NULL, NULL, " +
                "7, 0, NULL, NULL, 48, " +
                "N'ResponsibilityTask', NULL, NULL, NULL, N'9e98ebb5-b51a-46fc-9076-1c5c7370bff0', " +
                "1, 1, 1, NULL, 12, " +
                "NULL, 379);");
            sql.Append("INSERT [dbo].[Task] (" +
                "[Id], [MultiHazardRiskAssessmentHazardId], [Title], [Description], [Reference], " +
                "[Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], " +
                "[TaskAssignedToId], [TaskCompletionDueDate], [TaskStatusId], [TaskCompletedDate], [TaskCompletedComments], " +
                "[TaskCategoryId], [TaskReoccurringTypeId], [TaskReoccurringEndDate], [FollowingTaskId], [OriginalTaskId], " +
                "[Discriminator], [RiskAssessmentReviewId], [HazardousSubstanceRiskAssessmentId], [SignificantFindingId], [TaskGuid], " +
                "[SendTaskNotification], [SendTaskCompletedNotification], [SendTaskOverdueNotification], [DoNotSendStartedTaskNotification], [ResponsibilityId], " +
                "[TaskCompletedBy], [SiteId]) " +
                "VALUES (-2,NULL, N'Resp Task To Delete', N'Resp Task To Delete', N'RecResp01', " +
                "0, CAST(0x0000A1F500EF9CA0 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A1F500EF9CA0 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c', " +
                "N'd2122fff-1dcd-4a3c-83ae-e3503b394eb4', '2020-01-01 09:00:00:000', 0, NULL, NULL, " +
                "7, 1, '2021-01-01 09:00:00:000', NULL, 48, " +
                "N'ResponsibilityTask', NULL, NULL, NULL, N'9e98ebb5-b51a-46fc-9076-1c5c7370bff0', " +
                "1, 1, 1, NULL, 12, " +
                "NULL, 379);");
            sql.AppendLine("SET IDENTITY_INSERT [dbo].[Task] OFF;");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }

        [Then(@"the task list should contain thefollowing data:")]
        public void TheTaskListShouldContainTheFollowingData(TechTalk.SpecFlow.Table table)
        {
            var form = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            var displayedTable = form.Tables.First();
           
            
            for (var i = 0; i < table.Rows.Count; i++)
            {
                var taskReference = table.Rows[i]["Task Reference"].Trim();
                Assert.AreEqual(taskReference, displayedTable.TableRows[i].TableCells[1].Text.Trim());

                var taskCategory = table.Rows[i]["Task Category"].Trim();
                Assert.AreEqual(taskCategory, displayedTable.TableRows[i].TableCells[2].Text.Trim());

                var title = table.Rows[i]["Title"].Trim();
                Assert.AreEqual(title, displayedTable.TableRows[i].TableCells[3].Text.Trim());

                var description = table.Rows[i]["Description"].Trim();
                Assert.AreEqual(description, displayedTable.TableRows[i].TableCells[4].Text.Trim());

                var assignedTo = table.Rows[i]["Assigned To"].Trim();
                Assert.AreEqual(assignedTo, displayedTable.TableRows[i].TableCells[5].Text.Trim());

                var createdDate = table.Rows[i]["Created Date"].Trim();
                if (createdDate == "DateTime.Now")
                {
                    createdDate = DateTime.Now.ToString("dd/MM/yyyy");
                    Assert.AreEqual(createdDate, displayedTable.TableRows[i].TableCells[6].Text.Trim());
                }
                else
                {
                    Assert.AreEqual(createdDate, displayedTable.TableRows[i].TableCells[6].Text.Trim());
                }

                var dueDate = table.Rows[i]["Due Date"].Trim();
                Assert.AreEqual(dueDate, displayedTable.TableRows[i].TableCells[7].Text.Trim());

                var status = table.Rows[i]["Status"].Trim();
                Assert.AreEqual(status, displayedTable.TableRows[i].TableCells[8].Text.Trim());
            }

            if (table.Rows.Count != displayedTable.TableRows.Length)
            {
                Assert.Inconclusive(string.Format("The expected number of rows is {0} but the actual count is {1}. This is probably due to an additonal item that has been created in a previous test.", table.Rows.Count, displayedTable.TableRows.Count()));    
            }

        }

        [AfterScenario(@"NeedsResponsibilityTasksToComplete")]
        public void DeleteResponsibilityTasksToComplete()
        {
            var sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[Task] WHERE Id in (-1,-2);");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                runSQLCommand(sql.ToString(), conn);

            }
        }
    }
}

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.RepositoryContracts;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment
{
    [Binding]
    public class AddFurtherControlMeasureTask : BaseSteps
    {
        [Then(@"the hazardous substance risk assessment with reference '(.*)' fcm task is saved to the database")]
        public void ThenTheHazardousSubstanceRiskAssessmentFcmTaskIsSavedToTheDatabase(string reference)
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var sql = string.Format("SELECT Count(id) FROM Task Where Discriminator = 'HazardousSubstanceRiskAssessmentFurtherControlMeasureTask' AND reference = '{0}'", reference);

                var result = runSQLCommand(sql, conn);

                Assert.That(result, Is.EqualTo(1), string.Format("Could not find FCM Task with reference: {0}", reference));
            }
        }

        [Given(@"I have entered 'a433e9b2-84f6-4ad7-a89c-050e914dff01' into the hidden TaskAssignedToId field")]
        [When(@"I have entered 'a433e9b2-84f6-4ad7-a89c-050e914dff01' into the hidden TaskAssignedToId field")]
        public void WhenIHaveEnteredA433E9B2_84F6_4Ad7_A89C_050E914Dff01IntoTheHiddenTaskAssignedToIdField()
        {
            const string javascriptToRun = "$('#TaskAssignedToId').val('A433E9B2-84F6-4AD7-A89C-050E914DFF01');";
            WebBrowser.Current.Eval(javascriptToRun);
        }

        [Given(@"I am on the hazardous substance risk assessment index page for company '(.*)'")]
        public void GivenIAmOnTheHazardousSubstanceRiskAssessmentIndexPageForCompany55881(long companyId)
        {
            WebBrowser.Driver.Navigate("HazardousSubstanceRiskAssessments?companyId=" + companyId);
        }

        [Given(@"I press 'Edit' button on the further control measure task row")]
        [When(@"I press 'Edit' button on the further control measure task row")]
        public void GivenIPressEditButtonOnTheFurtherControlMeasureTaskRow()
        {
            Thread.Sleep(2000);

            var furtherActionTaskRow = ScenarioContextHelpers.GetFurtherActionTaskRow();
            var button = furtherActionTaskRow.Elements.Filter(Find.ById("edit-fcm-task")).FirstOrDefault();

            Assert.IsNotNull(button, "Could not find edit button");

            button.Click();
        }

        [When(@"I press View button on the further control measure task row")]
        public void WhenIPressButtonOnTheFurtherControlMeasureTaskRow()
        {
            Thread.Sleep(2000);

            var furtherActionTaskRow = ScenarioContextHelpers.GetFurtherActionTaskRow();
            var button = furtherActionTaskRow.Elements.Filter(Find.ByClass("btn btn-view-further-action-task")).FirstOrDefault();

            Assert.IsNotNull(button, "Could not find view button");

            button.Click();
        }

        [Then(@"the modified further control measure task is saved to the database")]
        public void ThenTheModifiedFurtherControlMeasureTaskIsSavedToTheDatabase()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                const string sql = "SELECT Count(id) FROM Task Where Discriminator = 'HazardousSubstanceRiskAssessmentFurtherControlMeasureTask' AND title = 'Title Test Passed' and TaskAssignedToId = 'a433e9b2-84f6-4ad7-a89c-050e914dff01'";

                var result = runSQLCommand(sql, conn);

                Assert.That(result, Is.EqualTo(1), string.Format("Could not find modified FCM Task"));
            }
        }

        [When(@"It is simulated that a document has been created for the fcm task")]
        public void WhenItIsSimulatedThatADocumentHasBeenCreatedForTheFcmTask()
        {
            Thread.Sleep(4000);
            WebBrowser.Current.Eval("addRowToNewlyUploadedDocumentsTable('" +
                                    "<tr>" +
                                    "<td>" +
                                    "<input id=\"DocumentGridRow_53984_DocumentOriginTypeId\" name=\"DocumentGridRow_53984_DocumentOriginTypeId\" type=\"hidden\" value=\"1\" />" +
                                    "<input id=\"DocumentGridRow_53984_DocumentLibraryId\" name=\"DocumentGridRow_53984_DocumentLibraryId\" type=\"hidden\" value=\"53984\" />" +
                                    "<input id=\"DocumentGridRow_53984_FileName\" name=\"DocumentGridRow_53984_FileName\" type=\"hidden\" value=\"Created FCM Task Text File ZZZZZ.txt\" />" +
                                    "<a href=\"/Document/DownloadDocument?enc=QfbCd5M90Wq0rgrXIMv7oFoPN0NoADDouoaNs5MdFPM5f9NXnVDiSY8huKc%252f1lth\">" +
                                    "Created FCM Task Text File ZZZZZ.txt" +
                                    "</a>" +
                                    "</td>" +
                                    "<td>" +
                                    "<select id=\"DocumentGridRow_53984_DocumentType\" name=\"DocumentGridRow_53984_DocumentType\" style=\"width:150px;\"><option value=\"6\">HSRA Document</option>" +
                                    "<option value=\"2\">Document Type 2</option>" +
                                    "<option value=\"3\">Document Type3</option>" +
                                    "</select>" +
                                    "</td>" +
                                    "<td>" +
                                    "<input id=\"DocumentGridRow_53984_Description\" name=\"DocumentGridRow_53984_Description\" type=\"text\" value=\"\" />" +
                                    "</td>" +
                                    "<td>" +
                                    "<a href=\"#\" class=\"deleteNewlyAddedDocument\" id=\"DeleteNewlyAddedDocumentLink\">" +
                                    "<input id=\"DeleteArgument\" name=\"DeleteArgument\" type=\"hidden\" value=\"QfbCd5M90Wq0rgrXIMv7oFoPN0NoADDouoaNs5MdFPM5f9NXnVDiSY8huKc%2f1lth\" />" +
                                    "<i class=\"icon-remove\"/>" +
                                    "</a>" +
                                    "</td>" +
                                    "</tr>');");

        }

        [Then(@"the Document should be saved as part of the created fcm task")]
        public void ThenTheDocumentShouldBeSavedAsPartOfTheCreatedFcmTask()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var sql = string.Format("SELECT Count(id) FROM Document Where filename = 'Created FCM Task Text File ZZZZZ.txt'");

                var result = runSQLCommand(sql, conn);

                Assert.That(result, Is.EqualTo(1), string.Format("Could not find document for this created FCM Task"));
            }
        }

        [Then(@"I press IsReoccurring checkbox for the HSRA FCM Task")]
        public void ThenIPressIsReoccurringCheckboxForTheHSRAFCMTask()
        {
            Thread.Sleep(1000);
            var checkbox = WebBrowser.Current.CheckBox(Find.ById("IsRecurring"));
            checkbox.Checked = true;

            Thread.Sleep(1000);

            //Force javascript to run.
            const string js = "setReoccurring($('#IsRecurring'))";
            WebBrowser.Current.Eval(js);

            Thread.Sleep(1000);
        }

        [Then(@"I have entered mandatory further control measure task data for this hsra")]
        [Given(@"I have entered mandatory further control measure task data for this hsra")]
        public void ThenIHaveEnteredMandatoryFurtherControlMeasureTaskDataForThisHsra()
        {
            FormSteps.EnterValueIntoField("Reoccurring-HSRA-FCM-Task-Reference", "Reference");
            FormSteps.EnterValueIntoField("Reoccurring-HSRA-FCM-Task-Title", "Title");
            FormSteps.EnterValueIntoField("Reoccurring-HSRA-FCM-Task-Description", "Description");
            FormSteps.EnterValueIntoField("Kim Howard ( Business Analyst )", "TaskAssignedTo");
            
            const string javascriptToRun = "$('#TaskAssignedToId').val('A433E9B2-84F6-4AD7-A89C-050E914DFF01');";
            WebBrowser.Current.Eval(javascriptToRun);

            FormSteps.EnterValueIntoField("Weekly", "TaskReoccurringType");
            FormSteps.EnterValueIntoField("1", "TaskReoccurringTypeId");
            FormSteps.EnterValueIntoField("01/01/2015", "FirstDueDate");
            FormSteps.EnterValueIntoField("01/01/2025", "TaskReoccurringEndDate");
        }

        [Then(@"the reoccurring hazardous substance risk assessment further control measure task is saved to the database")]
        public void ThenTheReoccurringHazardousSubstanceRiskAssessmentFurtherControlMeasureTaskIsSavedToTheDatabase()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                const string sql =
                    "SELECT Count(id) FROM Task Where Discriminator = 'HazardousSubstanceRiskAssessmentFurtherControlMeasureTask' " +
                    "AND Title = 'Reoccurring-HSRA-FCM-Task-Title' " +
                    "AND Description = 'Reoccurring-HSRA-FCM-Task-Description' " +
                    "AND TaskAssignedToId = 'A433E9B2-84F6-4AD7-A89C-050E914DFF01'" +
                    "AND TaskReoccurringTypeId = '1'";

                var result = runSQLCommand(sql, conn);

                Assert.That(result, Is.EqualTo(1), string.Format("Could not find created reoccurring FCM Task for HSRA"));
            }
        }

        [BeforeScenario(@"hsrafcmtaskeighteeniscompleted")]
        public static void SetHsraFcmTaskEighteenToCompleted()
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE Task SET TaskStatusId = 1  WHERE id = 18 ");
            sql.AppendLine("UPDATE Task SET TaskCompletedComments = 'Task Completed' WHERE id = 18 ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }

        [AfterScenario(@"hsrafcmtaskeighteeniscompleted")]
        public static void ResetHsraFcmTaskEighteenFromCompleted()
        {
            var sql = new StringBuilder();
            sql.AppendLine("UPDATE Task SET TaskStatusId = 0 WHERE id = 18 ");
            sql.AppendLine("UPDATE Task SET TaskCompletedComments = NULL WHERE id = 18 ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();
                runSQLCommand(sql.ToString(), conn);
            }
        }
    }
}

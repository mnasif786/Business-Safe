using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using TechTalk.SpecFlow;
using WatiN.Core;
using WatiN.Core.DialogHandlers;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Documents
{
    [Binding]
    public class DocumentsSteps : BaseSteps
    {
        [Given(@"I am on the added documents page for company '(.*)'")]
        public void GivenIAmOnTheDocumentLibraryPageForCompany(long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Documents/AddedDocumentsLibrary?companyId={0}", companyId));
        }

        [Given(@"I am on the BusinessSafe Document Library system page for company '(.*)'")]
        public void GivenIAmOnTheBusinessSafeSystemDocumentLibraryPageForCompany(long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Documents/BusinessSafeSystemDocumentsLibrary?companyId={0}", companyId));
        }

        [Given(@"I am on the Reference Documents Library system page for company '(.*)'")]
        public void GivenIAmOnTheReferenceDocumentsLibraryPageForCompany(long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Documents/ReferenceDocumentsLibrary?companyId={0}", companyId));
        }

        [Then(@"The Document Type drop down contains the correct types:")]
        public void ThenTheDocumentTypeDropDownContainsTheCorrectTypes(TechTalk.SpecFlow.Table table)
        {

            WebBrowser.Current.Eval("$('#DocumentType').next('.btn').click()");
            
            
            var itemsCount = WebBrowser.Current.Eval("$('li','.ui-autocomplete:visible').length");
            Assert.AreEqual(table.Rows.Count.ToString(), itemsCount);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string documentType = table.Rows[i]["DocumentType"];

                var result = WebBrowser.Current.Eval("$('li:contains(\"" + documentType + "\")','.ui-autocomplete:visible').length");

                Assert.IsTrue(result == "1");
            }
        }

        [Given(@"I click Browse")]
        public void WhenIClickBrowse()
        {
            var frame = WebBrowser.Current.Frame(Find.ById("DocumentUploadFrame"));
            var fileUpload = frame.FileUpload(Find.ById("File"));
            fileUpload.Click();
        }

        //DOES NOT WORK IN IE8.
        [Given(@"File is set to '(.*)'")]
        public void FileIsSetTo(string filePath)
        {
            var frame = WebBrowser.Current.Frame(Find.ById("DocumentUploadFrame"));
            var fileUpload = frame.FileUpload(Find.ById("File"));
            fileUpload.Set(filePath);
        }

        [When("Upload is clicked")]
        public void UploadIsClicked()
        {
            var frame = WebBrowser.Current.Frame(Find.ById("DocumentUploadFrame"));
            var submit = frame.Button(Find.ById("DocumentUploadSubmit"));
            submit.Click();
        }

        [When("It is simulated that a document has been uploaded")]
        [Given("It is simulated that a document has been uploaded")]
        public void WhenItIsSimulatedThatADocumentHasBeenUploaded()
        {
            Thread.Sleep(4000);
            WebBrowser.Current.Eval("addRowToNewlyUploadedDocumentsTable('" +
                                    "<tr>" +
                                    "<td>" +
                                    "<input id=\"DocumentGridRow_53984_DocumentLibraryId\" name=\"DocumentGridRow_53984_DocumentLibraryId\" type=\"hidden\" value=\"53984\" />" +
                                    "<input id=\"DocumentGridRow_53984_FileName\" name=\"DocumentGridRow_53984_FileName\" type=\"hidden\" value=\"Test Text File 05.txt\" />" +
                                    "<a href=\"/Document/DownloadDocument?enc=QfbCd5M90Wq0rgrXIMv7oFoPN0NoADDouoaNs5MdFPM5f9NXnVDiSY8huKc%252f1lth\">" +
                                    "Test Text File 05.txt" +
                                    "</a>" +
                                    "</td>" +
                                    "<td>" +
                                    "<select id=\"DocumentGridRow_53984_DocumentType\" name=\"DocumentGridRow_53984_DocumentType\" style=\"width:150px;\"><option value=\"1\">GRA Document</option>" +
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

        [When("It is simulated that a document has been uploaded when completing fcm task")]
        [Given("It is simulated that a document has been uploaded when completing fcm task")]
        public void WhenItIsSimulatedThatADocumentHasBeenUploadedWhenCompletingFcmTask()
        {
            Thread.Sleep(4000);
            WebBrowser.Current.Eval("addRowToNewlyUploadedDocumentsTable('" +
                                    "<tr>" +
                                    "<td>" +
                                    "<input id=\"DocumentGridRow_53984_DocumentOriginTypeId\" name=\"DocumentGridRow_53984_DocumentOriginTypeId\" type=\"hidden\" value=\"1\" />" +
                                    "<input id=\"DocumentGridRow_53984_DocumentLibraryId\" name=\"DocumentGridRow_53984_DocumentLibraryId\" type=\"hidden\" value=\"53984\" />" +
                                    "<input id=\"DocumentGridRow_53984_FileName\" name=\"DocumentGridRow_53984_FileName\" type=\"hidden\" value=\"Completed Text File.txt\" />" +
                                    "<a href=\"/Document/DownloadDocument?enc=QfbCd5M90Wq0rgrXIMv7oFoPN0NoADDouoaNs5MdFPM5f9NXnVDiSY8huKc%252f1lth\">" +
                                    "Completed Text File.txt" +
                                    "</a>" +
                                    "</td>" +
                                    "<td>" +
                                    "<select id=\"DocumentGridRow_53984_DocumentType\" name=\"DocumentGridRow_53984_DocumentType\" style=\"width:150px;\"><option value=\"1\">GRA Document</option>" +
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

        [Then("DocumentsToIncludeTable contains one row for Test Test File 05.txt")]
        public void ThenDocumentsToIncludeTableContainsOneRowForTestTestFile05Txt()
        {
            var documentsToIncludeTable = WebBrowser.Current.Table(Find.ById("DocumentsToIncludeTable"));
            Assert.AreEqual(2, documentsToIncludeTable.TableRows.Count());
            Assert.AreEqual("Test Text File 05.txt", documentsToIncludeTable.TableRows[1].TableCells[0].Text);
        }

        [When("The document in the table is deleted")]
        public void WhenTheDocumentInTheTableIsDeleted()
        {
            var documentsToIncludeTable = WebBrowser.Current.Table(Find.ById("DocumentsToIncludeTable"));
            var deleteNewlyAddedDocumentLink = documentsToIncludeTable.TableRows[1].Link(Find.ById("DeleteNewlyAddedDocumentLink"));
            deleteNewlyAddedDocumentLink.Click();
        }

        [Then("DocumentsToIncludeTable contains no rows")]
        public void ThenDocumentsToIncludeTableContainsNoRows()
        {
            var documentsToIncludeTable = WebBrowser.Current.Table(Find.ById("DocumentsToIncludeTable"));
            Assert.AreEqual(1, documentsToIncludeTable.TableRows.Count());
        }

        [Then(@"the document should be saved to the risk assessment")]
        public void ThenTheDocumentShouldBeSavedToTheRiskAssessment()
        {
            Thread.Sleep(4000);

            var result = GetRiskAssessmentDocumentCountForFile();
            Assert.That(result, Is.EqualTo(1));
        }

        private int GetRiskAssessmentDocumentCountForFile()
        {
            var riskAssessmentId = GetCurrentRiskAssessmentId();
            var sql = new StringBuilder();
            sql.Append(
                "SELECT COUNT(*) FROM [BusinessSafe].[dbo].[Document] D INNER JOIN [BusinessSafe].[dbo].[RiskAssessmentDocument] RAD ON D.ID = RAD.ID WHERE RAD.RiskAssessmentID = '" +
                riskAssessmentId + "' AND FileName = 'Test Text File 05.txt' AND D.[Deleted] = 0 ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    conn.Open();
                    var result = command.ExecuteScalar();
                    return (int) result;
                }
            }
        }

        [Then(@"the document should not be on the risk assessment")]
        public void ThenTheDocumentShouldNotBeOnTheRiskAssessment()
        {
            Thread.Sleep(4000);

            var result = GetRiskAssessmentDocumentCountForFile();
            Assert.That(result, Is.EqualTo(0));
        }

        [Given(@"the document in the attached documents table is deleted")]
        public void GivenTheDocumentInTheAttachedDocumentsTableIsDeleted()
        {
            var documentsToIncludeTable = WebBrowser.Current.Tables.First();
            var tableRow = documentsToIncludeTable.TableRows.First();
            foreach (CheckBox checkBox in tableRow.CheckBoxes)
            {
                checkBox.Click();
                break;
            }
        }

    }
}

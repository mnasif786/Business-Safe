using System.Data.SqlClient;
using System.Linq;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

using WatiN.Core;

using Table = WatiN.Core.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Documents.AddedDocuments
{
    [Binding]
    public class AddDocumentToAddedDocumentsSteps : BaseSteps
    {
        [AfterScenario("createsAddedDocument")]
        public void TearDownAddedDocument()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var docId =
                    runSQLCommand(
                        "SELECT id FROM Document WHERE Title = 'test title' AND Description = 'test description' AND DocumentTypeId = 2",
                        conn);
                runSQLCommand("DELETE FROM AddedDocument WHERE Id = " + docId, conn);
                runSQLCommand("DELETE FROM Document WHERE Id = " + docId, conn);
            }
        }

        [When("It is simulated that an added document has been uploaded")]
        [Given("It is simulated that an added document has been uploaded")]
        public void WhenItIsSimulatedThatAnAddedDocumentHasBeenUploaded()
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
                                    "<select id=\"DocumentGridRow_53984_DocumentType\" name=\"DocumentGridRow_53984_DocumentType\" style=\"width:150px;\"><option value=\"378\">GRA Document</option>" +
                                    "<option value=\"2\">Checklist</option>" +
                                    "<option value=\"3\">Procedures</option>" +
                                    "</select>" +
                                    "</td>" +
                                    "<td>" +
                                    "<select id=\"DocumentGridRow_53984_Site\" name=\"DocumentGridRow_53984_Site\" style=\"width:150px;\"><option value=\"378\">Aberdeen</option>" +
                                    "<option value=\"379\">Barnsley</option>" +
                                    "</select>" +
                                    "</td>" +
                                    "<td>" +
                                    "<input id=\"DocumentGridRow_53984_Title\" name=\"DocumentGridRow_53984_Title\" type=\"text\" value=\"\" />" +
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

        [When(@"I Select '(.*)' as my document type")]
        [Given(@"I Select '(.*)' as my document type")]
        public void WhenISelectAsMyDocumentType(string docType)
        {
            SelectList docTypeDdl = GetEditAddedDocumentTable().SelectLists.FirstOrDefault();
            Assert.IsNotNull(docTypeDdl, "Could not find Document Type DDL");

            docTypeDdl.Select(docType);
        }

        [When(@"I Select '(.*)' as my site")]
        [Given(@"I Select '(.*)' as my site")]
        public void WhenISelectAsMySite(string site)
        {
            var table = WebBrowser.Current.Tables.Filter(Find.ById("DocumentsToIncludeTable")).FirstOrDefault();
            Assert.IsNotNull(table, "Could not find added documents table");

            SelectList docTypeDdl = GetEditAddedDocumentTable().SelectLists.Skip(1).FirstOrDefault();
            Assert.IsNotNull(docTypeDdl, "Could not find Site DDL");

            docTypeDdl.Select(site);
        }

        [When(@"I enter '(.*)' as my title")]
        [Given(@"I enter '(.*)' as my title")]
        public void WhenIEnterAsMyTitle(string title)
        {
            TextField titleField = GetEditAddedDocumentTable().TextFields.FirstOrDefault();
            Assert.IsNotNull(titleField, "Could not find Title textfield");

            titleField.Value = title;
        }

        [When(@"it enter '(.*)' as my description")]
        [Given(@"it enter '(.*)' as my description")]
        public void WhenItEnterTestDescriptionAsMyDescription(string description)
        {
            TextField descField = GetEditAddedDocumentTable().TextFields.FirstOrDefault();
            Assert.IsNotNull(descField, "Could not find Description textfield");

            descField.Value = description;
        }

        private static Table GetEditAddedDocumentTable()
        {
            var table = WebBrowser.Current.Tables.Filter(Find.ById("DocumentsToIncludeTable")).FirstOrDefault();
            Assert.IsNotNull(table, "Could not find added documents table");
            return table;
        }

        [Then(@"the file is saved to the database")]
        public void ThenTheFileIsSavedToTheDatabase()
        {
            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                var docId =
                    runSQLCommand(
                        "SELECT id FROM Document WHERE Title = 'test title' AND Description = 'test description' AND DocumentTypeId = 2",
                        conn);
                var totalAddedDocs =
                    runSQLCommand("SELECT Count(id) FROM AddedDocument WHERE SiteId = 379 AND Id = " + docId, conn);

                Assert.That(docId, Is.GreaterThan(0), "Base Document not found");
                Assert.That(totalAddedDocs, Is.EqualTo(1), "AddedDocument not found");
            }
        }

        [Then(@"the file is shown at the top of the table")]
        public void ThenTheFileIsShownAtTheTopOfTheTable()
        {
            var div = WebBrowser.Current.Divs.Filter(Find.ById("DocumentsGrid")).First();
            Table docTable = div.Tables.First();
            var expectedNewRow = docTable.TableRows.First();

            Assert.That(expectedNewRow.TableCells.ElementAt(0).InnerHtml.Contains("Checklist"));
            Assert.That(expectedNewRow.TableCells.ElementAt(2).InnerHtml.Contains("test description"));
        }
    }
}

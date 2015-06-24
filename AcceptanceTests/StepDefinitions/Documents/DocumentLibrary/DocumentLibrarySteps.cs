using System.Dynamic;

using BusinessSafe.AcceptanceTests.StepHelpers;

using NUnit.Framework;

using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Documents.DocumentLibrary
{
    [Binding]
    public class DocumentLibrarySteps
    {
        [Given(@"I am on the document library page for company '(.*)' and documentGroup '(.*)'")]
        public void GivenIAmOnTheDocumentLibraryPageForCompany55881(long companyId, string documentGroup)
        {
            WebBrowser.Driver.Navigate(string.Format("Documents/ReferenceDocumentsLibrary?companyId={0}&documentGroup={1}", companyId, documentGroup));
        }

        [Given(@"I am on the businesssafe system document library page for company '(.*)' and documentGroup '(.*)'")]
        public void GivenIAmOnTheBusinesssafeSystemDocumentLibraryPageForCompanyAndDocumentGroup(int p0, string p1)
        {
            WebBrowser.Driver.Navigate(string.Format("Documents/BusinessSafeSystemDocumentsLibrary?companyId=55881"));
        }

        [Then(@"the document results table should contain the following data:")]
        public void ThenTheResultShouldContainRowWithTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                dynamic document = new ExpandoObject();

                document.DocumentType = row["Document Type"];
                document.Title = row["Title"]; 
                document.DocumentSubType = row["Document SubType"];
                document.Description = row["Description"];

                var searchResultsTable = WebBrowser.Current.Tables.First();

                Assert.NotNull(searchResultsTable.FindRow(document.DocumentType, 0));
                Assert.NotNull(searchResultsTable.FindRow(document.DocumentSubType, 1));
                Assert.NotNull(searchResultsTable.FindRow(document.Title, 2));
                Assert.NotNull(searchResultsTable.FindRow(document.Description, 3));
            }
        }

        [Then(@"the BS system document results table should contain the following data:")]
        public void ThenTheBSSystemResultShouldContainRowWithTheFollowing(Table table)
        {
            foreach (var row in table.Rows)
            {
                dynamic document = new ExpandoObject();

                document.DocumentType = row["Document Type"];
                document.Title = row["Title"];

                var searchResultsTable = WebBrowser.Current.Tables.First();

                Assert.NotNull(searchResultsTable.FindRow(document.DocumentType, 0));
                Assert.NotNull(searchResultsTable.FindRow(document.Title, 1));
            }
        }    
    }
}
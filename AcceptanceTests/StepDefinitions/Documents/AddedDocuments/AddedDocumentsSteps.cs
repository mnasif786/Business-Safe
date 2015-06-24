using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;

using NUnit.Framework;

using StructureMap;

using TechTalk.SpecFlow;

using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Documents.AddedDocuments
{
    [Binding]
    public class AddedDocumentsSteps: BaseSteps
    {

        [BeforeScenario("DeletingAddedDocument")]
        [AfterScenario("DeletingAddedDocument")]
        public static void BeforeScenario()
        {
            Thread.Sleep(2000);
            string sql = @"Update [Document] Set Deleted = 0 Where Id = '2'";

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                using (var command = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    command.ExecuteScalar();
                }
            }
        }

        [When(@"I press 'Delete' link for Document with id of '(.*)'")]
        public void WhenIPressDeleteLinkForDocumentWithIdOf1(string documentId)
        {
            var linkElement = Find.ByElement(x => x.GetAttributeValue("data-id") == documentId && x.ClassName == "DeleteDocumentIconLink");
            WebBrowser.Current.Link(linkElement).Click();
        }

        [Then(@"Document with id of '(.*)' should be deleted")]
        public void ThenDocumentWithIdOf1ShouldBeDeleted(long documentId)
        {
            const string insertSql = "SELECT Count(*) FROM [BusinessSafe].[dbo].[Document] WHERE Id = :Id AND Deleted = 1";
            Thread.Sleep(1000);
            var businessSafeSessionFactory = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>();
            var result = businessSafeSessionFactory
                                       .GetSession()
                                       .CreateSQLQuery(insertSql)
                                       .SetParameter("Id", documentId)
                                       .UniqueResult<int>();
            Assert.That(result, Is.EqualTo(1));
        }

        [Then(@"should not be a delete link for a document with id of '(.*)'")]
        public void ThenShouldNotBeADeleteLinkForADocumentWithIdOf(string documentId)
        {
            var linkElement = WebBrowser.Current.Links.FirstOrDefault(x => x.GetAttributeValue("data-id") == documentId && x.ClassName == "DeleteDocumentIconLink");
            Assert.IsNull(linkElement);
        }

        [Then(@"should see document row for a document with id of '(.*)'")]
        public void ThenShouldSeeDocumentRowForADocumentWithIdOf1(string documentId)
        {
            var linkElement = WebBrowser.Current.Links.FirstOrDefault(x => x.GetAttributeValue("data-id") == documentId && x.ClassName == "ViewDocumentIconLink");
            Assert.IsNotNull(linkElement, "Could not find document with id " + documentId);
        }

        [Then(@"the added document results table should contain the risk assessment report")]
        public void ThenTheAddedDocumentResultsTableShouldContainTheRiskAssessmentReport()
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();

            const int titleColumnIndex = 1;
            var formattedDate = DateTime.Today.ToString("dd_MM_yyyy");
            var title = string.Format("Acceptance Test Risk Assessment_Acceptance Test Risk Assessment_{0}.txt", formattedDate);

            Assert.AreEqual(title, searchResultsTable.TableRows[0].TableCells[titleColumnIndex].Text);
        }

    }
}

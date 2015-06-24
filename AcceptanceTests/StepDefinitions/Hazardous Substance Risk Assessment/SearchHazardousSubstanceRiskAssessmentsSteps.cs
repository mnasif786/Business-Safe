using System;
using System.Linq;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Hazardous_Substance_Risk_Assessment
{
    public class SearchHazardousSubstanceRiskAssessmentsSteps : BaseSteps
    {

        [Then(@"the Hazardous Substance Risk Assessments table should contain the following data")]
        public void ThenTheHazardousSubstanceRiskAssessmentsTableShouldContainTheFollowingData(Table table)
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                Assert.AreEqual(table.Rows[i]["Reference"], searchResultsTable.TableRows[i].TableCells[0].Text);
                Assert.AreEqual(table.Rows[i]["Title"], searchResultsTable.TableRows[i].TableCells[1].Text);
                Assert.AreEqual(table.Rows[i]["Site"].Trim(), searchResultsTable.TableRows[i].TableCells[2].Text.Trim());
                Assert.AreEqual(table.Rows[i]["Assigned To"].Trim(), searchResultsTable.TableRows[i].TableCells[3].Text.Trim());
                Assert.AreEqual(table.Rows[i]["Status"], searchResultsTable.TableRows[i].TableCells[4].Text);
                Assert.AreEqual(table.Rows[i]["Completion Due Date"], searchResultsTable.TableRows[i].TableCells[5].Text);
            }

            if (table.Rows.Count != searchResultsTable.TableRows.Count())
            {

                searchResultsTable.TableRows.ToList().ForEach(tr =>
                                                                  {
                                                                      Console.WriteLine("Ref:{0}, Title{1}", tr.TableCells[0].Text, tr.TableCells[1].Text);
                                                                  });
                Assert.Inconclusive(string.Format("The expected number of rows is {0} but the actual count is {1}. This is probably due to an additonal RA that has been created in a previous test.", table.Rows.Count, searchResultsTable.TableRows.Count()));
            }

        }

        [Then(@"the Hazardous Substance Risk Assessments table should contains data")]
        public void ThenTheHazardousSubstanceRiskAssessmentsTableShouldContainsData()
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();
            Assert.That(searchResultsTable.TableRows.Any(),"The search results table is empty.");
        }

    }
}

using System.Linq;
using TechTalk.SpecFlow;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;
using NUnit.Framework;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.Sites
{
    [Binding]
    public class RestrictGeneralRiskAssessmentsBySitesSteps : BaseSteps
    {
        [Then(@"sites select list should contain '(.*)' options")]
        public void SitesSelectListShouldContainOptions(int optionCount)
        {
            Thread.Sleep(1000);

            var sitesSelectList = WebBrowser.Current.SelectList(Find.ById("SelectedSiteId"));
            Assert.AreEqual(optionCount, sitesSelectList.Options.Count());

        }



        [Then(@"the (general|hazardous substances) risk assessments table should contain the following data:")]
        public void TheGeneralRiskAssessmentsTableShouldContainTheFollowingData(string type, TechTalk.SpecFlow.Table table)
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();
            Assert.AreEqual(table.Rows.Count, searchResultsTable.TableRows.Count());

            foreach (var row in table.Rows)
            {
                Assert.NotNull(searchResultsTable.FindRow(row["Reference"], 0));
            }
        }

        [Then(@"the (general|hazardous substances) risk assessments table should not contain the following data:")]
        public void TheGeneralRiskAssessmentsTableShouldNotContainTheFollowingData(string type, TechTalk.SpecFlow.Table table)
        {
            var searchResultsTable = WebBrowser.Current.Tables.First();
            Assert.AreEqual(table.Rows.Count, searchResultsTable.TableRows.Count());

            foreach (var row in table.Rows)
            {
                Assert.Null(searchResultsTable.FindRow(row["Reference"], 0));
            }
        }


        [Then(@"sites select list should contain the folowing data:")]
        public void SitesSelectListShouldContainTheFolowingData(TechTalk.SpecFlow.Table table)
        {
            Thread.Sleep(1000);

            WebBrowser.Current.Eval("$('#Site').next('.btn').click()");


            var itemsCount = WebBrowser.Current.Eval("$('li','.ui-autocomplete:visible').length");
            Assert.AreEqual(table.Rows.Count.ToString(), itemsCount);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                string site = table.Rows[i]["SiteName"];

                if (!string.IsNullOrEmpty(site) && site != "Mkt Harb'ro")
                {
                    var result = WebBrowser.Current.Eval("$('li:contains(\"" + site + "\")','.ui-autocomplete:visible').length");
                    Assert.IsTrue(result == "1");
                }
            }
        }

    }
}

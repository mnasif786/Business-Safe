using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.AccidentRecords
{
    [Binding]
    public class AccidentRecordSearchSteps : BaseSteps
    {       
        private static bool CheckNthRow(Table table, int n, WatiN.Core.Table displayedTable)
        {
            bool foundRow = false;

            var refExpected = table.Rows[0]["Ref"].Trim();
            var refDisplayed = displayedTable.TableRows[n].TableCells[0].Text.Trim();

            var titleExpected = table.Rows[0]["Title"].Trim();
            var titleDisplayed = displayedTable.TableRows[n].TableCells[1].Text.Trim();

            var descriptionExpected = table.Rows[0]["Description"].Trim();
            var descriptionDisplayed = displayedTable.TableRows[n].TableCells[2].Text.Trim();

            var injuredPersonExpected = table.Rows[0]["Injured Person"].Trim();
            var injuredpersonDisplayed = displayedTable.TableRows[n].TableCells[3].Text.Trim();

            var severityExpected = table.Rows[0]["Severity"].Trim();
            var severityDisplayed = displayedTable.TableRows[n].TableCells[4].Text.Trim();

            var siteExpected = table.Rows[0]["Site"].Trim();
            var siteDisplayed = displayedTable.TableRows[n].TableCells[5].Text.Trim();

            var reportedByExpected = table.Rows[0]["Reported By"].Trim();
            var reportedByDisplayed = displayedTable.TableRows[n].TableCells[6].Text.Trim();

            var dateOfAccidentExpected = table.Rows[0]["Date Of Accident"].Trim();
            var dateOfAccidentDisplayed = displayedTable.TableRows[n].TableCells[8].Text.Trim();

            var dateCreatedExpected = table.Rows[0]["Date Created"].Trim();
            var dateCreatedDisplayed = displayedTable.TableRows[n].TableCells[9].Text.Trim();


            if (refExpected == refDisplayed
                && titleExpected == titleDisplayed
                && descriptionExpected == descriptionDisplayed
                && injuredPersonExpected == injuredpersonDisplayed
                && severityExpected == severityDisplayed
                && siteExpected == siteDisplayed
                && reportedByExpected == reportedByDisplayed
                && dateOfAccidentExpected == dateOfAccidentDisplayed
                && dateCreatedExpected == dateCreatedDisplayed
                )
            {
                foundRow = true;
            }

            return foundRow;
        }

        [Then(@"The following record should exist in the accident record table:")]
        public void TheFollowingRecordShouldExistInTheAccidentRecordTable(TechTalk.SpecFlow.Table table)
        {
            var form = WebBrowser.Current.Div(Find.ById("AccidentRecordsGrid"));
            var displayedTable = form.Tables.First();
            var foundRow = false;

            for (var i = 0; i < displayedTable.TableRows.Count(); i++)
            {
                if (CheckNthRow(table, i, displayedTable))
                {
                    foundRow = true;
                }
            }

            Assert.IsTrue(foundRow);
        }

        [Then(@"The following record should be the first one returned in the accident record table:")]
        public void TheFollowingRecordShouldBeTheFirstOneReturnedInTheAccidentRecordTable(TechTalk.SpecFlow.Table table)
        {
            var form = WebBrowser.Current.Div(Find.ById("AccidentRecordsGrid"));
            var displayedTable = form.Tables.First();                     
            Assert.IsTrue( CheckNthRow(table, 0, displayedTable) );
        }

    }
}

using System;
using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.PersonalRiskAssessment.ChecklistGenerator
{
    [Binding]
    public class GenerateMultipleEmployeeChecklistsSteps
    {
        [Given(@"I click the multipleEmployees radiobutton")]
        public void GivenIClickTheMultipleEmployeesRadiobutton()
        {
            WebBrowser.Current.Eval("$('#multipleEmployees').click();");
        }
        
        
        //[When(@"I press button '(.*)'")]
        //public void WhenIPressButton(string p0)
        //{
        //    ScenarioContext.Current.Pending();
        //}
        
        [Then(@"Employees selected is (.*)")]
        public void ThenEmployeesSelectedIs(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the multiple selected employees table should contain the following data in correct order:")]
        public void TheMultipleSelectedEmployeesTableShouldContainTheFollowingData(TechTalk.SpecFlow.Table expectedTable)
        {
            var div = WebBrowser.Current.Div(Find.ById("SelectedEmployeesGrid"));
            var displayedTable = div.Tables.First();

            Assert.That(displayedTable.TableRows.Count(), Is.EqualTo(expectedTable.RowCount));

            for (var i = 0; i < expectedTable.Rows.Count; i++)
            {
                var title = expectedTable.Rows[i]["Name"].Trim();
                Assert.AreEqual(title, displayedTable.TableRows[i].TableCells[0].Text.Trim());

                var description = expectedTable.Rows[i]["Email"].Trim();

                var displayedEmail = displayedTable.TableRows[i].TableCells[1].Text != null
                                         ? displayedTable.TableRows[i].TableCells[1].Text.Trim()
                                         : "";

                Assert.AreEqual(description, displayedEmail);
            }
        }
    }
}

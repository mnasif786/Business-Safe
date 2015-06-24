using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class DeleteRiskAssessmentSteps : Steps
    {
        [Given(@"I have created a new risk assessment for deletion")]
        public void GivenIHaveCreatedANewRiskAssessmentForDeletion()
        {
            Thread.Sleep(2000);
            var textField = WebBrowser.Current.TextField(Find.ById(x => x == "Title"));
            textField.Clear();
            textField.TypeText("New Risk Assessment For Deletion");

            textField = WebBrowser.Current.TextField(Find.ById(x => x == "Reference"));
            textField.Clear();
            textField.TypeText("DEL001");

            var btn = WebBrowser.Current.Button(Find.ById(x => x == "createSummary"));
            btn.Click();

            textField = WebBrowser.Current.TextField(Find.ById(x => x == "SiteId"));
            textField.Clear();
            textField.TypeText("379");

            textField = WebBrowser.Current.TextField(Find.ById(x => x == "RiskAssessorId"));
            textField.Clear();
            textField.TypeText("1");

            btn = WebBrowser.Current.Button(Find.ById(x => x == "saveButton"));
            btn.Click();
        }

        [Given(@"I have entered mandatory further action task data into the risk assessment for deletion")]
        public void WhenIHaveEnteredMandatoryFurtherActionTaskDataIntoTheRiskAssessmentForDeleteion()
        {
            FormSteps.EnterValueIntoField("DEL002", "Reference");
            FormSteps.EnterValueIntoField("Delete 2", "Title");
            FormSteps.EnterValueIntoField("Delete 2", "Description");
            FormSteps.EnterValueIntoField("Glen Ross ( HR Analyst )", "TaskAssignedTo");
            FormSteps.EnterValueIntoField("01/01/2015", "TaskCompletionDueDate");
            const string javascriptToRun = "$('#TaskAssignedToId').val('9D24AE1A-6645-45FC-9D50-8FC70BABEB89');";
            WebBrowser.Current.Eval(javascriptToRun);

            dynamic furtherActionTaskAdding = new ExpandoObject();
            furtherActionTaskAdding.HazardId = 1;
            furtherActionTaskAdding.Reference = "DEL002";
            furtherActionTaskAdding.Title = "Delete 2";
            furtherActionTaskAdding.Description = "Delete 2";
            furtherActionTaskAdding.TaskAssignedToId = "9D24AE1A-6645-45FC-9D50-8FC70BABEB89";

            ScenarioContextHelpers.SetFurtherActionTaskAddingToRiskAssessment(furtherActionTaskAdding);
        }

        [When(@"I click to delete the risk assessment with reference '(.*)'")]
        public void WhenIDeleteTheRiskAssessmentWithReference(string reference)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("GeneralRiskAssessmentsGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var row = table.TableRows.Single(x => x.TableCells[0].Text == "DEL001");
            Thread.Sleep(500);
            var deleteButton = row.TableCells[7].Link(Find.ByClass("deleteIcon icon-remove"));
            Thread.Sleep(500);
            deleteButton.Click();
        }

        [When(@"I click confirm button on delete")]
        public void WhenIClickConfirmButtonOnDelete()
        {
            Thread.Sleep(500);
            var confirm = WebBrowser.Current.Button(Find.ByText("Confirm"));
            confirm.Click();
        }

        [Then(@"task with reference '(.*)' should not be present")]
        public void TaskWithReferenceShouldNotBePresent(string reference)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var rows = table.TableRows.Where(x => x.TableCells[1].Text == reference);
            Thread.Sleep(500);
            Assert.AreEqual(0, rows.Count());
        }

        [Then(@"task with title '(.*)' should be present")]
        public void TaskWithReferenceShouldBePresent(string title)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var rows = table.TableRows.Where(x => x.TableCells[3].Text == title);
            Thread.Sleep(500);
            Assert.AreEqual(1, rows.Count());
        }

        [Then(@"task list has no rows")]
        public void TaskListHasNoRows()
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("ResponsibilitySaveResponsibilityTaskRequestGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            Assert.AreEqual("No records to display.", table.TableRows[0].Text);
        }



        [When(@"I click to delete the personal risk assessment with reference '(.*)'")]
        public void WhenIDeleteThePersonalRiskAssessmentWithReference(string reference)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("PersonalRiskAssessmentsGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var row = table.TableRows.Single(x => x.TableCells[0].Text == reference);
            Thread.Sleep(500);
            var deleteButton = row.TableCells[7].Link(Find.ByClass("deleteIcon icon-remove"));
            Thread.Sleep(500);
            deleteButton.Click();
        }

        [When(@"I click to delete the hazardous substance risk assessment with reference '(.*)'")]
        public void WhenIClickToDeleteTheHazardousSubstanceRiskAssessmentWithReferenceHazSub1RA1(string reference)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("HazardousSubstancesAssessmentsGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var row = table.TableRows.Single(x => x.TableCells[0].Text == reference);
            Thread.Sleep(500);
            var deleteButton = row.TableCells[7].Link(Find.ByClass("deleteIcon icon-remove"));
            Thread.Sleep(500);
            deleteButton.Click();
        }


        [When(@"I click to reinstate the personal risk assessment with reference '(.*)'")]
        public void WhenIReinstateThePersonalRiskAssessmentWithReference(string reference)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("PersonalRiskAssessmentsGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var row = table.TableRows.Single(x => x.TableCells[0].Text == reference);
            Thread.Sleep(500);
            var deleteButton = row.TableCells[7].Link(Find.ByClass("undeleteIcon icon-share"));
            Thread.Sleep(500);
            deleteButton.Click();
        }


        [When(@"I click to reinstate the hazardous substance risk assessment with reference '(.*)'")]
        public void WhenIReinstateTheHazardousSubstanceRiskAssessmentWithReference(string reference)
        {
            Thread.Sleep(500);
            var tableDiv = WebBrowser.Current.Div(Find.ById("HazardousSubstancesAssessmentsGrid"));
            Thread.Sleep(500);
            var table = tableDiv.Tables[0];
            Thread.Sleep(500);
            var row = table.TableRows.Single(x => x.TableCells[0].Text == reference);
            Thread.Sleep(500);
            var deleteButton = row.TableCells[7].Link(Find.ByClass("undeleteIcon icon-share"));
            Thread.Sleep(500);
            deleteButton.Click();
        }
    }
}

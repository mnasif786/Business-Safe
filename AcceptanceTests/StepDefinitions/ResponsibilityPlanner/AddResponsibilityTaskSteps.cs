using System.Text.RegularExpressions;

using BusinessSafe.AcceptanceTests.StepHelpers;

using TechTalk.SpecFlow;

using WatiN.Core;

using NUnit.Framework;

using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ResponsibilityPlanner
{
    [Binding]
    public class AddResponsibilityTaskSteps: BaseSteps
    {

        [Given(@"The user is on Responsibility Planner Page")]
        public void GivenTheUserIsOnResponsibilityPlannerPage()
        {
            GotoResponsibilityPlanner();
        }

        [When(@"User opens the Add Task Page")]
        public void WhenUserOpensTheAddTaskPage()
        {
            WebBrowser.Current.Link(Find.ByText("Add Task")).Click();
        }

        [Then(@"Then page should open correctly")]
        public void ThenThenPageShouldOpenCorrectly()
        {
            var taskDetailsFound = WebBrowser.Current.ContainsText(new Regex("Task Details")); //TextField(Find.ById("ResponsibilityTask_Refrence"));
            Assert.That(taskDetailsFound, Is.True);
        }

        [Given(@"The user is on Add Task Page")]
        public void GivenTheUserIsOnAddTaskPage()
        {
            GotoResponsibilityPlanner();
            WebBrowser.Current.Link(Find.ById("btnAddTask")).Click();
        }

        [Then(@"page should render all controls correctly")]
        public void ThenThenPageShouldRenderAllControlsCorrectly()
        {
            var txtRef = WebBrowser.Current.TextField(Find.ById("Reference"));
            var txtTitle = WebBrowser.Current.TextField(Find.ById("Title"));
            var txtDescription = WebBrowser.Current.TextField(Find.ById("Description"));
            var chkUrgent = WebBrowser.Current.CheckBox(Find.ById("Urgent"));

            Assert.That(txtRef.Exists, Is.True);
            Assert.That(txtTitle.Exists, Is.True);
            Assert.That(txtDescription.Exists, Is.True);
            Assert.That(chkUrgent.Exists, Is.True);
        }

        [Then(@"Task category ddl shoud be loaded")]
        public void ThenTaskCategoryDdlShoudBeLoaded()
        {
            var dropDownList = WebBrowser.Current.SelectList(Find.ById("TaskCategoryId"));
            var taskCategories = (Table)ScenarioContext.Current["TaskCategories"];

            Assert.That(dropDownList.Exists, Is.True);

            foreach (var row in taskCategories.Rows)
            {
                Assert.That(dropDownList.InnerHtml.Contains(row[1]), Is.True);
            }
            
            
        }

        [When(@"user clicks on save button")]
        public void WhenUserClicksOnSaveButton()
        {
            var taskCategoryButton = WebBrowser.Current.Button(Find.ById("SaveTask"));
            Assert.That(taskCategoryButton.Exists, Is.True);
            taskCategoryButton.Click();   
        }

        [Then(@"System shows client side validation messages")]
        public void ThenSystemShowsClientSideValidationMessages()
        {
            Assert.That(WebBrowser.Current.ContainsText("Please enter Task Title."));
            
        }

        [When(@"User enters required data")]
        public void WhenUserEntersRequiredData()
        {
            var txtRef = WebBrowser.Current.TextField(Find.ById("Reference"));
            txtRef.AppendText("001");
            var txtTitle = WebBrowser.Current.TextField(Find.ById("Title"));
            txtTitle.AppendText("Test Task");
            var taskCAtegory = WebBrowser.Current.SelectList(Find.ById("TaskCategoryId"));
            taskCAtegory.Select( "My Tasks");
        }

        [Then(@"System saves the task")]
        public void ThenSystemSavesTheTask()
        {
            Assert.That(WebBrowser.Current.ContainsText("Task has been added successfully!"));
            
        }

        [Given(@"The javascript for IsReoccuring has fired")]
        [When(@"The javascript for IsReoccuring has fired")]
        public void TheJavaScriptForIsReoccuringHasFired()
        {
            WebBrowser.Current.Eval("$('#IsRecurring').click();");
        }
    }
}

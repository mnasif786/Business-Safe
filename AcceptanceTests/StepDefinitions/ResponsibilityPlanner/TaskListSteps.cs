using System;
using System.Collections.Generic;
using BusinessSafe.AcceptanceTests.StepHelpers;
using StructureMap;
using TechTalk.SpecFlow;
using NUnit.Framework;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;
using TableRow = TechTalk.SpecFlow.TableRow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.ResponsibilityPlanner
{
    [Binding]
    public class TaskListSteps: BaseSteps
    {
        [BeforeFeature(@"Acceptance")]
        public static void SetUpSystemUnderTest()
        {
            ObjectFactory.Configure(x => x.AddRegistry(new SutRegistry()));
            
        }
        
        [Given(@"We have task categories of:")]
        public void GivenWeHaveTaskCategoriesOf(Table table)
        {
            ScenarioContext.Current.Add("TaskCategories", table);
        }

        [Given(@"We have responsiblity tasks of :")]
        public void GivenWeHaveTasks(Table table)
        {
            
        }

        [Given(@"A user navigates to application")]
        public void GivenAUserNavigatesToApplication()
        {            
            GotoResponsibilityPlanner();
        }

        

        

        [When(@"page is loaded")]
        public void WhenPageIsLoaded()
        {
            ScenarioContext.Current.Add("Title", WebBrowser.Current.Title);
        }

        [Then(@"the title should be (.*) page")]
        public void ThenTheResultShouldBeResponsibilityPlannerPage(string title)
        {
            Assert.That(ScenarioContext.Current["Title"], Is.EqualTo(title));           
        }

        [Then(@"drop down lists are populated with data:")]
        public void ThenDropDownListsArePopulatedWithData(IEnumerable<BusinessSafeDropDownList> dropDownListsToVerify)
        {
            foreach (var businessSafeDropDownList in dropDownListsToVerify)
            {                
                var dropDownList = WebBrowser.Current.SelectList(Find.ById(businessSafeDropDownList.Name));
                Assert.That(dropDownList.Exists, Is.True);

                foreach (var values in businessSafeDropDownList.ListItemValues)
                {
                    Assert.That(dropDownList.InnerHtml.Contains(values), Is.True);
                }
            }
            WebBrowser.Current.Close();
        }

        [Given(@"user click on GoToTasks for Assigned To Filter")]
        [When(@"user click on GoToTasks for Assigned To Filter")]
        public void UserClickOnGoToTasksForAssignedToFilter()
        {
            WebBrowser.Current.WaitForComplete();
            var button = WebBrowser.Current.Button(Find.ById("ResponsibilityTaskCategoryGoToResponsibilityTasks"));
            Assert.That(button.Exists, Is.True, "Could not find button#ResponsibilityTaskCategoryGoToResponsibilityTasks");
            button.Click();
        }

        [Given(@"user click on GoToPassButton for Task Category Filter")]
        public void GivenUserClickOnGoToPassButtonForTaskCategoryFilter()
        {
            ActionUserClickOnTaskCategoryGoToTasksButton();
        }



        [When(@"user click on GoToPassButton for Task Category Filter")]
        public void WhenUserClickOnGoToPassButtonForTaskCategoryFilter()
        {
            ActionUserClickOnTaskCategoryGoToTasksButton();
        }
        
        private static void ActionUserClickOnTaskCategoryGoToTasksButton()
        {
            WebBrowser.Current.WaitForComplete();

            var taskCategoryButton = WebBrowser.Current.Button(Find.ById("ResponsibilityTaskCategoryGoToResponsibilityTasks"));
            Assert.That(taskCategoryButton.Exists, Is.True);
            taskCategoryButton.Click();
        }

        [Then(@"Task list is updated with correct tasks:")]
        public void ThenTaskListIsUpdatedWithCorrectTasks(Table taskTitleTable)
        {
            WebBrowser.Current.WaitForComplete();
            var tableBody = WebBrowser.Current.GetTableBody("ResponsibilitySaveResponsibilityTaskRequestGrid");
          
            foreach (var taskTitle in taskTitleTable.Rows)
            {
                Assert.That(tableBody.InnerHtml.Contains(taskTitle[0]), Is.False);
            }
        }

        [When(@"user select (.*) from (.*) drop down list")]
        public void WhenUserSelectGeneralRiskAssessmentFromDdlResponsibilityTaskCategoryDropDownList(string dropDownText, string dropDownIdToSelect)
        {
            var dropDownList = WebBrowser.Current.SelectList(Find.ById(dropDownIdToSelect));
            
            dropDownList.Select(dropDownText);
        }

       
        [Given(@"user select (.*) from (.*) drop down list")]
        public void GivenUserSelectGeneralRiskAssessmentFromDdlResponsibilityTaskCategoryDropDownList(string dropDownText, string dropDownIdToSelect)
        {
            var dropDownList = WebBrowser.Current.SelectList(Find.ById(dropDownIdToSelect));

            dropDownList.Select(dropDownText);
        }

        

        
        [Then(@"check statuses on tasks are correct:")]
        public void ThenCheckStatusesOnTasksAreCorrect(Table taskStatuses)
        {

            var tableRowOfTask = WebBrowser.Current.GetTableBody("ResponsibilitySaveResponsibilityTaskRequestGrid");

            foreach (var taskStatuse in taskStatuses.Rows)
            {
                TableRow statuse = taskStatuse;

                var rows = tableRowOfTask.TableRows.Filter(tr => tr.InnerHtml.Contains(statuse[0]));

                Assert.That(rows.First().InnerHtml.Contains(String.Format("title={0}", taskStatuse[1])), Is.True);
            }
        }

        [Then(@"check overal statuses are correct:")]
        public void ThenCheckOveralStatusesAreCorrect(Table table)
        {
            foreach (var taskStatusOccurrences in table.Rows)
            {
                var divStatusWrapper = WebBrowser.Current.Div(Find.ById("statusWrapperTaskCategory"));
                var totalNumberOfStatusExpected = taskStatusOccurrences[1];
                var actualTotalNumberOfStatuses = divStatusWrapper.Span(Find.ByClass(String.Format("statusWrapperGeneral statusWrapper{0}", taskStatusOccurrences[0]))).Text;

                //Assert.That(actualTotalNumberOfStatuses, Is.EqualTo(totalNumberOfStatusExpected));
            }
        }

        [Then(@"Close Browser")]
        public void ThenCloseBrowser()
        {
            WebBrowser.Current.Close();
        }

    }
}
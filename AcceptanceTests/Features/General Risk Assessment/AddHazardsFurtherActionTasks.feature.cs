﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.17929
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace BusinessSafe.AcceptanceTests.Features.GeneralRiskAssessment
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Add Hazards Further Action Tasks")]
    [NUnit.Framework.CategoryAttribute("Acceptance")]
    public partial class AddHazardsFurtherActionTasksFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AddHazardsFurtherActionTasks.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Add Hazards Further Action Tasks", "In order to add further action tasks to hazards on a general risk assessments\r\nAs" +
                    " a business safe online user\r\nI want to be able to add further action tasks", ProgrammingLanguage.CSharp, new string[] {
                        "Acceptance"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 7
#line 8
 testRunner.Given("I have logged in as company with id \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add Further Action Task To Risk Assessment")]
        [NUnit.Framework.CategoryAttribute("finetune")]
        [NUnit.Framework.CategoryAttribute("Acceptance")]
        public virtual void AddFurtherActionTaskToRiskAssessment()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add Further Action Task To Risk Assessment", new string[] {
                        "finetune",
                        "Acceptance"});
#line 12
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 13
 testRunner.Given("I am on the risk assessment page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.And("I have created a new risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("I press \'hazardspeople\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("I have added \'Asbestos\' to the \'Hazards\' risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("I press \'SaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I press \'controlmeasures\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("I have waited for the page to reload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("I have waited for element \'AddFurtherActionTask1\' to exist", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.When("I press \'AddFurtherActionTask1\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
 testRunner.And("I have entered mandatory further action task data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.When("I press \'FurtherActionTaskSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("the further action task should be saved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 26
 testRunner.And("Created Date should be displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Further Action Task validation")]
        public virtual void FurtherActionTaskValidation()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Further Action Task validation", ((string[])(null)));
#line 28
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 29
 testRunner.Given("I am on the risk assessment page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 30
 testRunner.And("I have created a new risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("I press \'hazardspeople\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("I have added \'Asbestos\' to the \'Hazards\' risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And("I press \'SaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.And("I press \'controlmeasures\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.When("I press \'AddFurtherActionTask1\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.And("I wait for \'3000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("I press IsReoccurring checkbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("I have entered \'Kim Howard ( Business Analyst )\' into the \'TaskAssignedTo\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("I have entered \'a433e9b2-84f6-4ad7-a89c-050e914dff01\' into the \'TaskAssignedToId\'" +
                    " field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("I press \'FurtherActionTaskSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.Then("the \'Title is required\' error message is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 45
 testRunner.Then("the \'Description is required\' error message is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 46
 testRunner.Then("the \'Task Recurrence Frequency is required\' error message is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 47
 testRunner.Then("the \'First Due Date requires a valid date\' error message is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Add Reoccurring Further Control Measure Task To Risk Assessment And Edit it")]
        [NUnit.Framework.CategoryAttribute("Acceptance")]
        public virtual void AddReoccurringFurtherControlMeasureTaskToRiskAssessmentAndEditIt()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add Reoccurring Further Control Measure Task To Risk Assessment And Edit it", new string[] {
                        "Acceptance"});
#line 50
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 51
 testRunner.Given("I am on the risk assessment page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 52
 testRunner.And("I have created a new risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("I press \'hazardspeople\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("I have added \'Asbestos\' to the \'Hazards\' risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("I press \'SaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("I press \'controlmeasures\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.When("I press \'AddFurtherActionTask1\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 60
 testRunner.And("I wait for \'5000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.When("I press IsReoccurring checkbox", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 62
 testRunner.And("I have entered mandatory further control measure task data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
 testRunner.When("I press \'FurtherActionTaskSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 64
 testRunner.Then("the reoccurring further control measure task should be saved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 65
 testRunner.When("I click on the further action task row for the task \'Reoccurring-Title\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 66
 testRunner.And("I press \'Edit\' button on the \'Reoccurring-Title\' row", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.Then("the input with id \'TaskReoccurringEndDate\' has value \'01/01/2025\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 68
 testRunner.When("I enter todays date into the field \'FirstDueDate\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 69
 testRunner.And("I press \'FurtherActionTaskSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
 testRunner.Then("the \'First Due Date must be in the future\' error message is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Completed Recurring Tasks Do Not Display In GRA Control Measures Tab")]
        [NUnit.Framework.CategoryAttribute("requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated")]
        public virtual void CompletedRecurringTasksDoNotDisplayInGRAControlMeasuresTab()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Completed Recurring Tasks Do Not Display In GRA Control Measures Tab", new string[] {
                        "requiresCompletedAndAnOutstandingRecurringFCMTasksToBeCreated"});
#line 73
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 74
 testRunner.Given("I am on the risk assessment index page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 75
 testRunner.And("I have clicked on the edit risk assessment link for id \'39\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
 testRunner.And("I have waited for the page to reload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.And("I press \'controlmeasures\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.And("I have waited for the page to reload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.Then("FCM task \'recurring_task_1\' should have visibility of \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 80
 testRunner.Then("FCM task \'recurring_task_2\' should have visibility of \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 81
 testRunner.Then("FCM task \'recurring_task_3\' should have visibility of \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 82
 testRunner.Then("FCM task \'recurring_task_4\' should have visibility of \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 83
 testRunner.Then("FCM task \'recurring_task_5\' should have visibility of \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 84
 testRunner.Then("FCM task \'recurring_task_6\' should have visibility of \'true\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

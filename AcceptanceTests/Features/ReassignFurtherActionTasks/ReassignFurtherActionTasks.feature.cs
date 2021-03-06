﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace BusinessSafe.AcceptanceTests.Features.ReassignFurtherActionTasks
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Reassigning Further Action Tasks")]
    [NUnit.Framework.CategoryAttribute("ReassigningFurtherActionTasks")]
    public partial class ReassigningFurtherActionTasksFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ReassignFurtherActionTasks.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Reassigning Further Action Tasks", "In order to reassign further action tasks\r\nAs a business safe online user with th" +
                    "e correct user access rights\r\nI want to be able to reassign further action tasks" +
                    "", ProgrammingLanguage.CSharp, new string[] {
                        "ReassigningFurtherActionTasks"});
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
        [NUnit.Framework.DescriptionAttribute("Reassign GRA further action task from task list")]
        [NUnit.Framework.CategoryAttribute("ReassigningFurtherActionTasks")]
        public virtual void ReassignGRAFurtherActionTaskFromTaskList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Reassign GRA further action task from task list", new string[] {
                        "ReassigningFurtherActionTasks"});
#line 11
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 12
 testRunner.Given("I am on the responsibility planner page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
 testRunner.And("I have entered \'Kim Howard\' into the \'Employee\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I have entered \'A433E9B2-84F6-4AD7-A89C-050E914DFF01\' into the \'EmployeeId\' field" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("I press \'ResponsibilityTaskCategoryGoToResponsibilityTasks\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.Then("the relevant employee tasks should be shown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 17
 testRunner.When("I have clicked on the reassign task link for id \'11\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
 testRunner.Then("the reassign task screen should be shown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 19
 testRunner.And("the TaskDialog popup element is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.Given("I have entered \'Barry Brown ( Team leader )\' into the \'ReassignTaskTo\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
 testRunner.And("I have entered \'3ECE3FD2-DB29-4ABD-A812-FCC6B8E621A1\' into the \'ReassignTaskToId\'" +
                    " field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.When("I press \'FurtherActionTaskSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 23
 testRunner.Then("the task \'11\' should no longer be assigned to \'Kim Howard\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 24
 testRunner.And("the task \'11\' should be assigned to \'Barry Brown\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Reassign HSRA further action task from task list")]
        [NUnit.Framework.CategoryAttribute("ReassigningFurtherActionTasks")]
        public virtual void ReassignHSRAFurtherActionTaskFromTaskList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Reassign HSRA further action task from task list", new string[] {
                        "ReassigningFurtherActionTasks"});
#line 27
this.ScenarioSetup(scenarioInfo);
#line 7
this.FeatureBackground();
#line 28
 testRunner.Given("I am on the responsibility planner page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 29
 testRunner.And("I have entered \'Barry Brown\' into the \'Employee\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("I have entered \'3ece3fd2-db29-4abd-a812-fcc6b8e621a1\' into the \'EmployeeId\' field" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And("I press \'ResponsibilityTaskCategoryGoToResponsibilityTasks\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.Then("the relevant employee tasks should be shown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 33
 testRunner.When("I have clicked on the reassign task link for id \'17\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 34
 testRunner.Then("the reassign task screen should be shown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 35
 testRunner.And("the TaskDialog popup element is visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.Given("I have entered \'Kim Howard\' into the \'ReassignTaskTo\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 37
 testRunner.And("I have entered \'A433E9B2-84F6-4AD7-A89C-050E914DFF01\' into the \'ReassignTaskToId\'" +
                    " field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.When("I press \'FurtherActionTaskSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
 testRunner.Then("the task \'17\' should no longer be assigned to \'Barry Brown\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 40
 testRunner.And("the task \'17\' should be assigned to \'Kim Howard\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

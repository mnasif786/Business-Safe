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
namespace BusinessSafe.AcceptanceTests.Features.CompanyDefaults
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("EditRiskAssessor")]
    public partial class EditRiskAssessorFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "EditRiskAssessor.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "EditRiskAssessor", "In order to edit risk assessors to a risk assessment\r\nAs a BSO user\r\nI want to be" +
                    " able to select risk assessor from grid", ProgrammingLanguage.CSharp, ((string[])(null)));
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
#line 6
#line 7
    testRunner.Given("I have logged in as company with id \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.And("I have no \'Non Employees\' for company", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Set Site for Risk Assessor")]
        [NUnit.Framework.CategoryAttribute("ChangesKnownRiskAssessors")]
        public virtual void SetSiteForRiskAssessor()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Set Site for Risk Assessor", new string[] {
                        "ChangesKnownRiskAssessors"});
#line 11
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 12
 testRunner.Given("I am on the Company default page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
 testRunner.And("I am on the risk assessors tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I click on the edit button for risk assessor with id \'4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("\'HasAccessToAllSites\' check box is ticked \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("I have entered \'Aberdeen\' into the \'Site\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("I have entered \'378\' into the \'SiteId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.When("I press \'saveBtn\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 19
 testRunner.And("I wait for \'2000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Forename",
                        "Surname",
                        "Site",
                        "Overdue",
                        "Completed",
                        "Due"});
            table1.AddRow(new string[] {
                        "John",
                        "Conner",
                        "Aberdeen",
                        "true",
                        "true",
                        "true"});
            table1.AddRow(new string[] {
                        "Kim",
                        "Howard",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
            table1.AddRow(new string[] {
                        "Barry",
                        "Scott",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
            table1.AddRow(new string[] {
                        "Russell",
                        "Williams",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
#line 20
 testRunner.Then("the risk assessors table should contain the following data:", ((string)(null)), table1, "Then ");
#line 26
 testRunner.Given("I click on the edit button for risk assessor with id \'4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And("\'HasAccessToAllSites\' check box is ticked \'true\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("I have entered \'\' into the \'Site\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("I have entered \'\' into the \'SiteId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.When("I press \'saveBtn\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.And("I wait for \'2000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Forename",
                        "Surname",
                        "Site",
                        "Overdue",
                        "Completed",
                        "Due"});
            table2.AddRow(new string[] {
                        "John",
                        "Conner",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
            table2.AddRow(new string[] {
                        "Kim",
                        "Howard",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
            table2.AddRow(new string[] {
                        "Barry",
                        "Scott",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
            table2.AddRow(new string[] {
                        "Russell",
                        "Williams",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
#line 33
 testRunner.Then("the risk assessors table should contain the following data:", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Set Risk Assessor Notifications")]
        [NUnit.Framework.CategoryAttribute("ChangesKnownRiskAssessors")]
        public virtual void SetRiskAssessorNotifications()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Set Risk Assessor Notifications", new string[] {
                        "ChangesKnownRiskAssessors"});
#line 41
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 42
 testRunner.Given("I am on the Company default page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 43
 testRunner.And("I am on the risk assessors tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("I click on the edit button for risk assessor with id \'4\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.And("\'DoNotSendTaskOverdueNotifications\' check box is ticked \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
 testRunner.And("\'DoNotSendTaskCompletedNotifications\' check box is ticked \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
 testRunner.And("\'DoNotSendReviewDueNotification\' check box is ticked \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And("I wait for \'2000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
 testRunner.When("I press \'saveBtn\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Forename",
                        "Surname",
                        "Site",
                        "Overdue",
                        "Completed",
                        "Due"});
            table3.AddRow(new string[] {
                        "John",
                        "Conner",
                        "All Sites",
                        "false",
                        "false",
                        "false"});
            table3.AddRow(new string[] {
                        "Kim",
                        "Howard",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
            table3.AddRow(new string[] {
                        "Barry",
                        "Scott",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
            table3.AddRow(new string[] {
                        "Russell",
                        "Williams",
                        "All Sites",
                        "true",
                        "true",
                        "true"});
#line 50
 testRunner.Then("the risk assessors table should contain the following data:", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
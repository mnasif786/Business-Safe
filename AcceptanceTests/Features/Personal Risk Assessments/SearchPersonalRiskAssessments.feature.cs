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
namespace BusinessSafe.AcceptanceTests.Features.PersonalRiskAssessments
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("SearchPersonalRiskAssessments")]
    [NUnit.Framework.CategoryAttribute("Acceptance")]
    public partial class SearchPersonalRiskAssessmentsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SearchPersonalRiskAssessments.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SearchPersonalRiskAssessments", "In order to find a specific PRA\r\nAs a user with the correct permissions\r\nI want t" +
                    "o search the PRAs", ProgrammingLanguage.CSharp, new string[] {
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
#line 8
#line 9
 testRunner.Given("I have logged in as company with id \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.And("I am on the personal risk assessments page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search by site id")]
        [NUnit.Framework.CategoryAttribute("Personal_Risk_Assessments")]
        public virtual void SearchBySiteId()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search by site id", new string[] {
                        "Personal_Risk_Assessments"});
#line 12
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 13
 testRunner.Given("I have entered \'378\' into the \'SiteId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.When("I press \'Search\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reference",
                        "Title",
                        "Site",
                        "Assigned To",
                        "Status",
                        "Completion Due Date"});
            table1.AddRow(new string[] {
                        "CFPRA",
                        "Core Functionality Personal Risk Assessment",
                        "Aberdeen",
                        "",
                        "Draft",
                        ""});
            table1.AddRow(new string[] {
                        "PRA02",
                        "PRA 2",
                        "Aberdeen",
                        "Russell Williams",
                        "Live",
                        "21/04/2013"});
            table1.AddRow(new string[] {
                        "make sensitive by other user test",
                        "PRA created by Kim",
                        "Aberdeen",
                        "Kim Howard",
                        "Draft",
                        "12/06/2013"});
            table1.AddRow(new string[] {
                        "PRA01",
                        "PRA 1",
                        "Aberdeen",
                        "Russell Williams",
                        "Live",
                        "23/06/2013"});
#line 15
 testRunner.Then("the risk assessment table should contain the following data:", ((string)(null)), table1, "Then ");
#line 21
 testRunner.Given("I have entered \'\' into the \'SiteId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 22
 testRunner.Given("I have entered \'381\' into the \'SiteGroupId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 23
 testRunner.When("I press \'Search\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reference",
                        "Title",
                        "Site",
                        "Assigned To",
                        "Status",
                        "Completion Due Date"});
            table2.AddRow(new string[] {
                        "PRA04",
                        "PRA 4",
                        "Edinburgh",
                        "Russell Williams",
                        "Live",
                        "21/04/2012"});
#line 24
 testRunner.Then("the risk assessment table should contain the following data:", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search by created date")]
        public virtual void SearchByCreatedDate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search by created date", ((string[])(null)));
#line 29
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 30
 testRunner.Given("I have entered \'01/04/2012\' into the \'CreatedFrom\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 31
 testRunner.Given("I have entered \'01/04/2013\' into the \'CreatedTo\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 32
 testRunner.When("I press \'Search\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reference",
                        "Title",
                        "Site",
                        "Assigned To",
                        "Status",
                        "Completion Due Date"});
            table3.AddRow(new string[] {
                        "PRA03",
                        "PRA 3",
                        "Barnsley",
                        "Russell Williams",
                        "Live",
                        "23/06/2012"});
            table3.AddRow(new string[] {
                        "PRA02",
                        "PRA 2",
                        "Aberdeen",
                        "Russell Williams",
                        "Live",
                        "21/04/2013"});
#line 33
 testRunner.Then("the risk assessment table should contain the following data:", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Search by title")]
        public virtual void SearchByTitle()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search by title", ((string[])(null)));
#line 38
this.ScenarioSetup(scenarioInfo);
#line 8
this.FeatureBackground();
#line 39
 testRunner.Given("I have entered \'PRA TASKs\' into the \'Title\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
 testRunner.When("I press \'Search\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reference",
                        "Title",
                        "Site",
                        "Assigned To",
                        "Status",
                        "Completion Due Date"});
            table4.AddRow(new string[] {
                        "Ref: PRA TASKs1",
                        "PRA TASKs1",
                        "",
                        "",
                        "Draft",
                        "12/02/2012"});
            table4.AddRow(new string[] {
                        "Ref: PRA TASKs2",
                        "PRA TASKs2",
                        "",
                        "",
                        "Draft",
                        "12/02/2012"});
#line 41
 testRunner.Then("the risk assessment table should contain the following data:", ((string)(null)), table4, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

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
namespace BusinessSafe.AcceptanceTests.Features.HazardousSubstancesInventory
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Inventory Item Has Link Back To Associated RiskAssessment")]
    public partial class InventoryItemHasLinkBackToAssociatedRiskAssessmentFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "InventoryItemLinkBackToAssociatedRiskAssessment.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Inventory Item Has Link Back To Associated RiskAssessment", "In order to monitor our Hazardous Substances and their associated Risk Assessment" +
                    "\r\nAs a BSO user\r\nI want to be able to view related Risk Assessments for a given " +
                    "Hazardous Substance", ProgrammingLanguage.CSharp, ((string[])(null)));
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
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When a Hazardous Substance has a single Risk Assessment Then the Link goes to tha" +
            "t Risk Assessment")]
        public virtual void WhenAHazardousSubstanceHasASingleRiskAssessmentThenTheLinkGoesToThatRiskAssessment()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a Hazardous Substance has a single Risk Assessment Then the Link goes to tha" +
                    "t Risk Assessment", ((string[])(null)));
#line 10
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 11
 testRunner.Given("I am on the hazardous substance inventory page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Substance Name",
                        "Supplier",
                        "Date Of SDS Sheet",
                        "Hazard Classification",
                        "Risk No And Phrases",
                        "Safety No And Phrases",
                        "Usage"});
            table1.AddRow(new string[] {
                        "Test Hazardous Substance 1",
                        "Test Supplier 1",
                        "01/10/2012",
                        "Global",
                        "RX01, RX02",
                        "SX01, SX02",
                        "Test details of use 1"});
            table1.AddRow(new string[] {
                        "Test Hazardous Substance 2",
                        "Test Supplier 1",
                        "01/10/2012",
                        "Global",
                        "RX02, RX03",
                        "SX02, SX03",
                        "Test details of use 2"});
            table1.AddRow(new string[] {
                        "Test Hazardous Substance 3",
                        "Test Supplier 2",
                        "01/10/2012",
                        "Global",
                        "",
                        "",
                        "Test details of use 3"});
            table1.AddRow(new string[] {
                        "Toilet Cleaner",
                        "Test Supplier 2",
                        "01/10/2012",
                        "Global",
                        "RX02, RX03",
                        "SX03",
                        "Cleaning the toilets"});
#line 12
 testRunner.Then("the Hazardous Substances table should contain the following data:", ((string)(null)), table1, "Then ");
#line 18
 testRunner.When("I click on the view risk assessment for the hazardous substance with id \'2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 19
 testRunner.Then("I am redirected to the url \'HazardousSubstanceRiskAssessments/Description/View\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 20
 testRunner.And("the hazardous substance risk assessment id is \'44\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When a Hazardous Substance has several Risk Assessment Then the Link goes to a Se" +
            "arch result of its Risk Assessments")]
        [NUnit.Framework.CategoryAttribute("finetune")]
        public virtual void WhenAHazardousSubstanceHasSeveralRiskAssessmentThenTheLinkGoesToASearchResultOfItsRiskAssessments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When a Hazardous Substance has several Risk Assessment Then the Link goes to a Se" +
                    "arch result of its Risk Assessments", new string[] {
                        "finetune"});
#line 23
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 24
 testRunner.Given("I am on the hazardous substance inventory page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Substance Name",
                        "Supplier",
                        "Date Of SDS Sheet",
                        "Hazard Classification",
                        "Risk No And Phrases",
                        "Safety No And Phrases",
                        "Usage"});
            table2.AddRow(new string[] {
                        "Test Hazardous Substance 1",
                        "Test Supplier 1",
                        "01/10/2012",
                        "Global",
                        "RX01, RX02",
                        "SX01, SX02",
                        "Test details of use 1"});
            table2.AddRow(new string[] {
                        "Test Hazardous Substance 2",
                        "Test Supplier 1",
                        "01/10/2012",
                        "Global",
                        "RX02, RX03",
                        "SX02, SX03",
                        "Test details of use 2"});
            table2.AddRow(new string[] {
                        "Test Hazardous Substance 3",
                        "Test Supplier 2",
                        "01/10/2012",
                        "Global",
                        "",
                        "",
                        "Test details of use 3"});
            table2.AddRow(new string[] {
                        "Toilet Cleaner",
                        "Test Supplier 2",
                        "01/10/2012",
                        "Global",
                        "RX02, RX03",
                        "SX03",
                        "Cleaning the toilets"});
#line 25
 testRunner.Then("the Hazardous Substances table should contain the following data:", ((string)(null)), table2, "Then ");
#line 31
 testRunner.When("I click on the view risk assessment for the hazardous substance with id \'1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.Then("I am redirected to the url \'HazardousSubstanceRiskAssessments?\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reference",
                        "Title",
                        "Site",
                        "Assigned To",
                        "Status",
                        "Completion Due Date"});
            table3.AddRow(new string[] {
                        "HazSub 1 RA 1",
                        "Test Hazardous Substance 1 RA 1",
                        "Barnsley",
                        "Kim Howard",
                        "Live",
                        "29/10/2012"});
            table3.AddRow(new string[] {
                        "HazSub 1 RA 2",
                        "Test Hazardous Substance 1 RA 2",
                        "",
                        "",
                        "Live",
                        "29/10/2012"});
            table3.AddRow(new string[] {
                        "Edinburgh Hazardous Substance Risk Assessment",
                        "Edinburgh Hazardous Substance Risk Assessment",
                        "Edinburgh",
                        "",
                        "Live",
                        "29/10/2012"});
#line 33
 testRunner.Then("the Hazardous Substance Risk Assessments table should contain the following data", ((string)(null)), table3, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

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
    [NUnit.Framework.DescriptionAttribute("AddToInventory")]
    public partial class AddToInventoryFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AddToInventory.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "AddToInventory", "In order to record the hazardous substances on site\r\nAs a BSO user\r\nI want to be " +
                    "add a Hazardous Substance to my Inventory of Hazardous Substances", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("When I click Add Hazardous Substance I can save the hazardous substance then edit" +
            " it")]
        [NUnit.Framework.CategoryAttribute("createsHazardousSubstance")]
        public virtual void WhenIClickAddHazardousSubstanceICanSaveTheHazardousSubstanceThenEditIt()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I click Add Hazardous Substance I can save the hazardous substance then edit" +
                    " it", new string[] {
                        "createsHazardousSubstance"});
#line 10
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 11
 testRunner.Given("I am on the add hazardous substance page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 12
 testRunner.And("I press \'AddEditHazardousSubstanceCancelButton\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I have waited for the page to reload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.Then("I should be redirected to the inventory page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 15
 testRunner.Given("I am on the add hazardous substance page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 16
 testRunner.And("I have entered \'My completely new one\' into the \'Name\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("I have entered \'Test Supplier 1\' into the \'Supplier\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("I have entered \'1\' into the \'SupplierId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I have entered \'01/01/2012\' into the \'SdsDate\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("I selected the Global identification standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("I have selected the option label \'RX01 Test Risk Phrase 1\' from multi-select cont" +
                    "rol \'risk-phrase\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And("I have selected the option label \'RX03 Test Risk Phrase 3\' from multi-select cont" +
                    "rol \'risk-phrase\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.And("I have selected the option label \'SX01 Test Safety Phrase 1\' from multi-select co" +
                    "ntrol \'safety-phrase\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And("I have selected the option label \'SX03 Test Safety Phrase 3\' from multi-select co" +
                    "ntrol \'safety-phrase\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("I have entered \'details of my usage\' into the \'DetailsOfUse\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.And("I press \'AssessmentRequired\' radio button with the value of \'false\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.When("I press \'AddEditHazardousSubstanceSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("I should be redirected to the inventory page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
                        "My completely new one",
                        "Test Supplier 1",
                        "01/01/2012",
                        "Global",
                        "RX01, RX03",
                        "SX01, SX03",
                        "details of my usage"});
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
#line 29
 testRunner.And("the Hazardous Substances table should contain the following data:", ((string)(null)), table1, "And ");
#line 36
 testRunner.Given("I press the edit icon for hazardous substance with name \'My completely new one\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 37
 testRunner.And("I have entered \'My edited title\' into the \'Name\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And("I have deselected option label \'RX01 Test Risk Phrase 1\' from multi-select contro" +
                    "l \'risk-phrase\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("I have deselected option label \'SX03 Test Safety Phrase 3\' from multi-select cont" +
                    "rol \'safety-phrase\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.When("I press \'AddEditHazardousSubstanceSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 41
 testRunner.Then("I should be redirected to the inventory page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
                        "My edited title",
                        "Test Supplier 1",
                        "01/01/2012",
                        "Global",
                        "RX03",
                        "SX01",
                        "details of my usage"});
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
#line 42
 testRunner.And("the Hazardous Substances table should contain the following data:", ((string)(null)), table2, "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I click Add Hazardous Substance with assessment required checked I am direct" +
            "ed to risk assessment")]
        [NUnit.Framework.CategoryAttribute("createsHazardousSubstance")]
        public virtual void WhenIClickAddHazardousSubstanceWithAssessmentRequiredCheckedIAmDirectedToRiskAssessment()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I click Add Hazardous Substance with assessment required checked I am direct" +
                    "ed to risk assessment", new string[] {
                        "createsHazardousSubstance"});
#line 51
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 52
testRunner.Given("I am on the add hazardous substance page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 53
 testRunner.And("I have entered \'My completely new one\' into the \'Name\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("I have entered \'01/01/2012\' into the \'SdsDate\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("I have entered \'Test Supplier 1\' into the \'Supplier\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("I have entered \'1\' into the \'SupplierId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("I selected the Global identification standard", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And("I have entered \'details of my usage\' into the \'DetailsOfUse\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.And("I press \'AssessmentRequired\' radio button with the value of \'true\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.When("I press \'AddEditHazardousSubstanceSaveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 61
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
 testRunner.Then("I should be redirected to the create hazardous substance risk assessment page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 63
 testRunner.And("the text box with id \'Title\' should contain \'My completely new one\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
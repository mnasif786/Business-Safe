﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.225
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
    [NUnit.Framework.DescriptionAttribute("Deleting Hazardous Substance")]
    [NUnit.Framework.CategoryAttribute("DeletingHazardousSubstance")]
    public partial class DeletingHazardousSubstanceFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "DeleteHazardousSubstance.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Deleting Hazardous Substance", "In order to delete hazardous substances\r\nAs a business safe online user with the " +
                    "correct user access rights\r\nI want to be able to delete hazardous substances", ProgrammingLanguage.CSharp, new string[] {
                        "DeletingHazardousSubstance"});
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
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Delete Hazardous Substance")]
        public virtual void DeleteHazardousSubstance()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Delete Hazardous Substance", ((string[])(null)));
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("I have logged in as company with id \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.Given("I am on the search hazardous substances page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 11
 testRunner.When("I press \'Delete\' link for hazardous substance with id \'3\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 12
 testRunner.And("I wait for \'2000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I select \'yes\' on confirmation", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.Then("the hazardous substance with id \'3\' should then be deleted", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 16
 testRunner.Given("I press \'showDeletedLink\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 17
 testRunner.And("I press \'Reinstate\' link for hazardous substance with id \'3\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("I select \'yes\' on confirmation", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.When("I press \'showDeletedLink\' link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
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
#line 20
 testRunner.Then("the Hazardous Substances table should contain the following data:", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

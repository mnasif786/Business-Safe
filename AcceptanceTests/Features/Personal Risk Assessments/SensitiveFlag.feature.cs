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
    [NUnit.Framework.DescriptionAttribute("Sensitive Flag")]
    public partial class SensitiveFlagFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SensitiveFlag.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Sensitive Flag", "", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("A sensitive personal risk assessment only viewable by risk assessor")]
        [NUnit.Framework.CategoryAttribute("Acceptance")]
        [NUnit.Framework.CategoryAttribute("Personal_Risk_Assessments")]
        [NUnit.Framework.CategoryAttribute("Personal_Risk_Assessments_Sensitivity")]
        public virtual void ASensitivePersonalRiskAssessmentOnlyViewableByRiskAssessor()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A sensitive personal risk assessment only viewable by risk assessor", new string[] {
                        "Acceptance",
                        "Personal_Risk_Assessments",
                        "Personal_Risk_Assessments_Sensitivity"});
#line 12
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 13
 testRunner.Given("I am on create riskassessment page in area \'PersonalRiskAssessments\' with company" +
                    " Id \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 14
 testRunner.And("I have entered \'Test Title\' into the \'Title\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("I have entered \'Test Reference\' into the \'Reference\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.Given("I press \'createSummary\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 17
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("I am redirected to the summary page for the new personal risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I have entered \'Barnsley\' into the \'Site\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("I have entered \'379\' into the \'SiteId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("I have entered \'Barry Scott\' into the \'RiskAssessor\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And("I have entered \'1\' into the \'RiskAssessorId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.And("I have entered \'21/04/2020\' into the \'DateOfAssessment\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And("\'Sensitive\' check box is ticked \'true\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("I press \'saveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.And("I have waited for the page to reload", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.And("I have clicked the \'LogOutLink\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.When("I have logged in as company with id \'55881\' as Barry Scott", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.And("I am on the personal risk assessments page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
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
                        "PRA04",
                        "PRA 4",
                        "Edinburgh",
                        "Russell Williams",
                        "Live",
                        "21/04/2012"});
            table1.AddRow(new string[] {
                        "PRA03",
                        "PRA 3",
                        "Barnsley",
                        "Russell Williams",
                        "Live",
                        "23/06/2012"});
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
            table1.AddRow(new string[] {
                        "Test Reference",
                        "Test Title",
                        "Barnsley",
                        "Barry Scott",
                        "Draft",
                        "21/04/2020"});
#line 31
 testRunner.Then("the risk assessment table should contain the following data:", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A sensitive personal risk assessment not viewable by anyone else")]
        [NUnit.Framework.CategoryAttribute("Acceptance")]
        [NUnit.Framework.CategoryAttribute("Personal_Risk_Assessments")]
        [NUnit.Framework.CategoryAttribute("Personal_Risk_Assessments_Sensitivity")]
        [NUnit.Framework.CategoryAttribute("finetune")]
        public virtual void ASensitivePersonalRiskAssessmentNotViewableByAnyoneElse()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A sensitive personal risk assessment not viewable by anyone else", new string[] {
                        "Acceptance",
                        "Personal_Risk_Assessments",
                        "Personal_Risk_Assessments_Sensitivity",
                        "finetune"});
#line 45
 this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 46
 testRunner.Given("I am on create riskassessment page in area \'PersonalRiskAssessments\' with company" +
                    " Id \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 47
 testRunner.And("I have entered \'Test Title\' into the \'Title\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And("I have entered \'Test Reference\' into the \'Reference\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 49
 testRunner.Given("I press \'createSummary\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 50
 testRunner.And("I wait for \'1000\' miliseconds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.And("I am redirected to the summary page for the new personal risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
 testRunner.And("I have entered \'Barnsley\' into the \'Site\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
 testRunner.And("I have entered \'379\' into the \'SiteId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("I have entered \'Kim Howard\' into the \'RiskAssessor\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("I have entered \'2\' into the \'RiskAssessorId\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("I have entered \'23/06/2020\' into the \'DateOfAssessment\' field", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("\'Sensitive\' check box is ticked \'true\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And("I press \'saveButton\' button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.And("I have clicked the \'LogOutLink\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.When("I have logged in as company with id \'55881\' as Barry Scott", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 61
 testRunner.And("I am on the personal risk assessments page for company \'55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Reference",
                        "Title",
                        "Site",
                        "Assigned To",
                        "Status",
                        "Completion Due Date"});
            table2.AddRow(new string[] {
                        "CFPRA",
                        "Core Functionality Personal Risk Assessment",
                        "Aberdeen",
                        "",
                        "Draft",
                        ""});
            table2.AddRow(new string[] {
                        "PRA04",
                        "PRA 4",
                        "Edinburgh",
                        "Russell Williams",
                        "Live",
                        "21/04/2012"});
            table2.AddRow(new string[] {
                        "PRA03",
                        "PRA 3",
                        "Barnsley",
                        "Russell Williams",
                        "Live",
                        "23/06/2012"});
            table2.AddRow(new string[] {
                        "PRA02",
                        "PRA 2",
                        "Aberdeen",
                        "Russell Williams",
                        "Live",
                        "21/04/2013"});
            table2.AddRow(new string[] {
                        "make sensitive by other user test",
                        "PRA created by Kim",
                        "Aberdeen",
                        "Kim Howard",
                        "Draft",
                        "12/06/2013"});
            table2.AddRow(new string[] {
                        "PRA01",
                        "PRA 1",
                        "Aberdeen",
                        "Russell Williams",
                        "Live",
                        "23/06/2013"});
#line 62
 testRunner.Then("the risk assessment table should contain the following data:", ((string)(null)), table2, "Then ");
#line 70
 testRunner.When("I try to hack the url as Barry Scott to view sensitive personal risk assessment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
 testRunner.Then("The error page is displayed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("A non-sensitive personal risk assessment changed to sensitive by its creator")]
        [NUnit.Framework.CategoryAttribute("Personal_Risk_Assessments")]
        [NUnit.Framework.CategoryAttribute("Personal_Risk_Assessments_Sensitivity")]
        public virtual void ANon_SensitivePersonalRiskAssessmentChangedToSensitiveByItsCreator()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A non-sensitive personal risk assessment changed to sensitive by its creator", new string[] {
                        "Personal_Risk_Assessments",
                        "Personal_Risk_Assessments_Sensitivity"});
#line 75
this.ScenarioSetup(scenarioInfo);
#line 6
this.FeatureBackground();
#line 77
 testRunner.Given("I am on the summary page of Personal Risk Assessment with id \'49\' and companyId \'" +
                    "55881\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 78
 testRunner.Then("the element with id \'Sensitive\' has a \'readonly\' attribute of \'False\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 79
 testRunner.Then("the element with id \'RiskAssessor\' has a \'readonly\' attribute of \'False\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 80
 testRunner.Then("the element with id \'RiskAssessorId\' has a \'readonly\' attribute of \'False\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 81
 testRunner.Then("the element with id \'Site\' has a \'readonly\' attribute of \'False\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 82
 testRunner.Then("the element with id \'SiteId\' has a \'readonly\' attribute of \'False\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

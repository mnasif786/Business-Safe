using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class EditRiskAssessmentSteps : BaseSteps
    {
        [Given(@"I am on riskassessment '(.*)' for company '(.*)'")]
        public void GivenIAmOnRiskAssessmentForCompany(int riskAssessmentId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("GeneralRiskAssessments//Summary?riskAssessmentId={0}&companyId={1}", riskAssessmentId, companyId));
        }

        [Given(@"I am on fireriskassessment '(.*)' for company '(.*)'")]
        public void GivenIAmOnFireRiskAssessmentForCompany(int riskAssessmentId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("FireRiskAssessments//Summary?riskAssessmentId={0}&companyId={1}", riskAssessmentId, companyId));
        }

        [Given(@"I am on personalriskassessment '(.*)' for company '(.*)'")]
        public void GivenIAmOnPersonalRiskAssessmentForCompany(int riskAssessmentId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("PersonalRiskAssessments/Summary?riskAssessmentId={0}&companyId={1}", riskAssessmentId, companyId));
        }

        [Given(@"I have entered '(.*)' with id '(.*)' into the employee search field")]
        public void WhenIHaveEnteredIntoTheEmployeeSearchField(string nameToSearch, string employeeId)
        {
            Thread.Sleep(1000);

            WebBrowser.Current.Eval("$('#employeesName').val('Kim'); var e = jQuery.Event('keydown'); e.which = 32; $('#employeesName').trigger(e);");
            
            Thread.Sleep(2000);

            WebBrowser.Current.Eval("$('a.ui-corner-all:first').mouseenter().click().mouseleave();");

        }

        [Given(@"I have entered '(.*)' with id '(.*)' into the non employee search field")]
        public void WhenIHaveEnteredIntoTheNonEmployeeSearchField(string nameToSearch, string employeeId)
        {
            Thread.Sleep(1000);

            WebBrowser.Current.Eval("$('#nonEmployeesName').val('Dave'); var e = jQuery.Event('keydown'); e.which = 32; $('#nonEmployeesName').trigger(e);");

            Thread.Sleep(2000);

            WebBrowser.Current.Eval("$('a.ui-corner-all:first').mouseenter().click().mouseleave();");

        }



        [Given(@"I select 'Kim Howard' in Employees list")]
        public void GivenISelectEmployeeInSelectList()
        {
            WebBrowser.Current.Eval("$('#employeesMultiSelect option:first').attr('selected', true).focus().click();");
        }

        [Given(@"I select 'Dave Smith' in NonEmployees list")]
        public void GivenISelectNonEmployeeInSelectList()
        {
            WebBrowser.Current.Eval("$('#nonEmployeesMultiSelect option:first').attr('selected', true).focus().click();");
        }

        [Then(@"the '(.*)' select list contains '(.*)'")]
        public void ThenTheEmployeesSelectListContains(string selectList, string name)
        {
            Thread.Sleep(1000);
            Assert.True(
                WebBrowser.Current.SelectList(selectList).Options.Count(
                    x => x.InnerHtml.Contains(name)) == 1);

        }
        
        [Then(@"the '(.*)' select list should not contain '(.*)'")]
        public void ThenTheSelectListShouldNotContain(string selectList, string name)
        {
            Thread.Sleep(1000);
            Assert.True(
                WebBrowser.Current.SelectList(selectList).Options.Count(
                    x => x.InnerHtml.Contains(name)) == 0);

        }
    }
}
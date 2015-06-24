using NUnit.Framework;

using TechTalk.SpecFlow;
using BusinessSafe.AcceptanceTests.StepHelpers;
using WatiN.Core;
using System.Threading;

namespace BusinessSafe.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class NavigationSteps : Steps
    {
        [Given(@"I navigate to task list")]
        [When(@"I navigate to task list")]
        public void NavigateToTaskList()
        {
            Thread.Sleep(1000);
            WebBrowser.Current.Link(Find.ById("my-planner")).Click();
            Thread.Sleep(2000);
            WebBrowser.Current.Link(Find.ById("my-task-list")).Click();
        }

        [Given(@"I navigate to risk assessments")]
        [When(@"I navigate to risk assessments")]
        public void NavigateToGeneralRiskAssessments()
        {
            Thread.Sleep(1000);
            WebBrowser.Current.Link(Find.ById("my-businesssafe-link")).Click();
            Thread.Sleep(1000);
            WebBrowser.Current.Link(Find.ById("ViewGeneralRiskAssessmentsLink")).Click();
        }

        [Given(@"I navigate to added documents")]
        [When(@"I navigate to added documents")]
        public void NavigateToAddedDocuments()
        {
            Thread.Sleep(1000);
            WebBrowser.Current.Link(Find.ById("myDocumentationLink")).Click();
            Thread.Sleep(1000);
            WebBrowser.Current.Link(Find.ById("addedDocumentsLibraryLink")).Click();
        }

        
        [Given(@"I am on the Index for Hazardous Substances RiskAssessments for company '(.*)'")]
        [When(@"I am on the Index for Hazardous Substances RiskAssessments for company '(.*)'")]
        public void IAmOnTheIndexForHazardousSubstancesRiskAssessments(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("HazardousSubstanceRiskAssessments/RiskAssessment?companyId={0}", companyId));
        }

        [Then(@"I am redirected to the url '(.*)'")]
        public void ThenIAmRedirectedToTheUrl(string expectedUrl)
        {
            Thread.Sleep(1000);
            Assert.That(WebBrowser.Current.Url.ToLower(), Contains.Substring(expectedUrl.ToLower()));
        }

        [Given(@"I am on the add users page for company '(.*)'")]
        [When(@"I am on the add users page for company '(.*)'")]
        public void IAmOnTheAddUsersPageForCompany(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Users/AddUsers?companyId={0}", companyId));
        }

        [Given(@"I am on the add general risk assessment page for company '(.*)'")]
        [When(@"I am on the add general risk assessment page for company '(.*)'")]
        public void IAmOnTheAddGeneralRiskAssessmentPageForCompany(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("GeneralRiskAssessments/RiskAssessment/Create?companyId={0}", companyId));
        }

        [Given(@"I am on the add general risk assessment index page for company '(.*)'")]
        [When(@"I am on the add general risk assessment index page for company '(.*)'")]
        public void IAmOnTheAddGeneralRiskAssessmentIndexPageForCompany(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("GeneralRiskAssessments/RiskAssessment?companyId={0}", companyId));
        }

        [Given(@"I am on the general risk assessments page for company '(.*)'")]
        [When(@"I am on the general risk assessments page for company '(.*)'")]
        public void IAmOnTheGeneralRiskAssessmentsPageForCompany(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("GeneralRiskAssessments?companyId={0}", companyId));
        }

        [Given(@"I am on the personal risk assessments page for company '(.*)'")]
        [When(@"I am on the personal risk assessments page for company '(.*)'")]
        public void GivenIAmOnThePersonalRiskAssessmentsPageForCompany55881(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("PersonalRiskAssessments?companyId={0}", companyId));
        }


        [Given(@"I am on the general risk assessment control measures page for company '(.*)' and risk assessment '(.*)'")]
        [When(@"I am on the general risk assessment control measures page for company '(.*)' and risk assessment '(.*)'")]
        public void IAmOnTheGeneralRiskAssessmentControlMeasuresPageForCompanyAndRiskAssessment(int companyId, int riskAssessmentId)
        {
            WebBrowser.Driver.Navigate(string.Format("GeneralRiskAssessments/ControlMeasures?riskAssessmentId={0}&CompanyId={1}", riskAssessmentId, companyId));
        }

        [Given(@"I am on the add hazardous substance risk assessment page for company '(.*)'")]
        [When(@"I am on the add hazardous substance risk assessment page for company '(.*)'")]
        public void IAmOnTheAddHazardousSubstanceRiskAssessmentPageForCompany(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("HazardousSubstanceRiskAssessments/RiskAssessment/Create?companyId={0}", companyId));
        }

        [Given(@"I am on the hazardous substances control measures page for company '(.*)' and risk assessment '(.*)'")]
        [When(@"I am on the hazardous substances control measures page for company '(.*)' and risk assessment '(.*)'")]
        public void IAmOnTheHazardousSubstancesControlMeasuresPageForCompanyAndRiskAssessment(int companyId, int riskAssessmentId)
        {
            WebBrowser.Driver.Navigate(string.Format("HazardousSubstanceRiskAssessments/ControlMeasures?riskAssessmentId={0}&CompanyId={1}", riskAssessmentId, companyId));
        }

        [Given(@"I am on the risk assessment review page for risk assessment with id '(.*)' and companyId '(.*)'")]
        public void GivenIAmOnTheRiskAssessmentReviewPageForRiskAssessmentWithId(int riskAssessmentId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("GeneralRiskAssessments/Review?riskAssessmentId={0}&CompanyId={1}", riskAssessmentId, companyId));
        }


        [Given(@"I am on the add new personal risk assessment page")]
        public void GivenIAmOnTheAddNewPersonalRiskAssessmentPage()
        {
            WebBrowser.Driver.Navigate(string.Format("PersonalRiskAssessments/RiskAssessment/Create"));
        }

        [Given(@"I am on the hazards page of Personal Risk Assessment with id '(.*)' and companyId '(.*)'")]
        public void GivenIAmOnTheHazardsPageOfPersonalRiskAssessmentWithId(int praId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("PersonalRiskAssessments/Hazards?companyId={0}&riskAssessmentId={1}", companyId, praId));
        }

        [Given(@"I am on the summary page of Personal Risk Assessment with id '(.*)' and companyId '(.*)'")]
        public void GivenIAmOnTheSummaryPageOfPersonalRiskAssessmentWithId(int praId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("PersonalRiskAssessments/Summary?companyId={0}&riskAssessmentId={1}", companyId, praId));
        }

        [Given(@"I am on the checklist generator page for risk assessment '(.*)' and companyid '(.*)'")]
        public void GivenIAmOnTheChecklistGeneratorPageForRiskAssessment(long riskAssessmentId, long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("PersonalRiskAssessments/ChecklistGenerator?companyId={0}&riskAssessmentId={1}", companyId, riskAssessmentId));
        }

        [Given(@"I am on the checklist manager page for risk assessment '(.*)' and companyid '(.*)'")]
        public void GivenIAmOnTheChecklistManagerPageForRiskAssessment(long riskAssessmentId, long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("PersonalRiskAssessments/ChecklistManager?companyId={0}&riskAssessmentId={1}", companyId, riskAssessmentId));
        }

        [Given(@"I am on the Fire Risk Assessment Premises Information page with id '(.*)' and companyId '(.*)'")]
        public void GivenIAmOnTheFireRiskAssessmentPremisesInformationPageWithId(int praId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("FireRiskAssessments/PremisesInformation?companyId={0}&riskAssessmentId={1}", companyId, praId));
        }

        [Given(@"I am on the Fire Risk Assessment Hazards page with id '(.*)' and companyId '(.*)'")]
        public void GivenIAmOnTheFireRiskAssessmentHazardsPageWithId(int fraId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("FireRiskAssessments/Hazards?companyId={0}&riskAssessmentId={1}", companyId, fraId));
        }

        [Given(@"I am on the Fire Risk Assessment Summary page with id '(.*)' and companyId '(.*)'")]
        public void GivenIAmOnTheFireRiskAssessmentSummaryPageWithIdAndCompanyId(int riskAssessmentId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("FireRiskAssessments/Summary?companyId={0}&riskAssessmentId={1}", companyId, riskAssessmentId));
        }

        [Given(@"I am on the Fire Risk Assessment Checklist page with id '(.*)' and companyId '(.*)'")]
        public void GivenIAmOnTheFireRiskAssessmentChecklistPageWithIdAndCompanyId(int riskAssessmentId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("FireRiskAssessments/Checklist?companyId={0}&riskAssessmentId={1}", companyId, riskAssessmentId));
        }

        [Given(@"I am on the Create Responsibility Task page for companyId '(.*)'")]
        public void IAmOnTheCreateResponsibilityTaskPageForCompanyId(int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Responsibilities/Responsibility/Create?companyId={0}", companyId));
        }

        [Given(@"I am on page '(.*)' of the Responsibility Index page for companyId '(.*)'")]
        public void IAmOnPageTheResponsibilityIndexPageForCompanyId(int page, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("Responsibilities/Responsibility?companyId={0}&ResponsibilitiesGrid-page={1}", companyId, page));
        }

        [Given(@"I am on the injured person page for accident record '(.*)' for companyId '(.*)'")]
        [When(@"I am on the injured person page for accident record '(.*)' for companyId '(.*)'")]
        public void IAmOnTheInjuredPersonPageForAccidentRecordForCompany(long accidentRecordId, long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("AccidentReports/InjuredPerson/Index?accidentRecordId={0}&companyId={1}", accidentRecordId, companyId));
        }

        [Given(@"I am on the injury page for accident record '(.*)' for companyId '(.*)'")]
        [When(@"I am on the injury page for accident record '(.*)' for companyId '(.*)'")]
        public void GivenIAmOnTheInjuryPageForAccidentRecordForCompanyId(int accidentRecordId, int companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("AccidentReports/InjuryDetails?accidentRecordId={0}&companyId={1}", accidentRecordId, companyId));                        
        }

        [Given(@"I am on the accident details page for accident record '(.*)' for companyId '(.*)'")]
        [When(@"I am on the accidet details page for accident record '(.*)' for companyId '(.*)'")]
        public void IAmOnTheAccidentDetailsPageForAccidentRecordForCompany(long accidentRecordId, long companyId)
        {
            WebBrowser.Driver.Navigate(string.Format("AccidentReports/AccidentDetails/Index?accidentRecordId={0}&companyId={1}", accidentRecordId, companyId));
        }
		
        [Given(@"I am on the accident record index page")]
        public void IAmOnTheAccidentRecordsIndexPage()
        {
            WebBrowser.Driver.Navigate(string.Format("AccidentReports"));            
        }

        [When(@"I press back")]
        public void WhenIPressBack()
        {
            BaseSteps.SleepIfEnvironmentIsPotentiallySlow(5000);
            WebBrowser.Current.Back();            
        }
    }
}

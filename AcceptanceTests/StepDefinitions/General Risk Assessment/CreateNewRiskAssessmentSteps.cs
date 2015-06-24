using System.Linq;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NUnit.Framework;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class CreateNewRiskAssessmentSteps : BaseSteps
    {
  
        [Then(@"the control measure table should contain '(.*)'")]
        public void ThenControlMeasureTableShouldContain(string controlMeasure)
        {
            var controlMeasures = WebBrowser.Current.Divs.Filter(x => x.ClassName == "controlMeasureText").ToList();
            Assert.That(controlMeasures.First().InnerHtml, Contains.Substring(controlMeasure));
        }


        [Given(@"I am on create riskassessment page in area '(.*)' with company Id '(.*)'")]
        public void GivenIAmOnCreateRiskAssessmentWithCompanyId(string areaName, int companyId)
        {
            WebBrowser.Driver.Navigate(areaName+"/RiskAssessment/Create?companyId=" + companyId);
        }
        
        [Then(@"a new risk assessment should be created with the '(.*)' set as '(.*)'")]
        public void ThenARiskAssessmentShouldBeCreated(string field , string reference)
        {
            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
               .CreateSQLQuery(string.Format("SELECT Count(Id) FROM RiskAssessment where {0} = '{1}'", field, reference))
               .UniqueResult();

            Assert.That(result, Is.EqualTo(1));
        }
        
        [Then(@"the new risk assessment has correct values")]
        public void ThenTheNewRiskAssessmentHasCorrectValues()
        {
            var riskAssessmentId = WebBrowser.Current.Element(Find.ById("RiskAssessmentId")).GetValue("value");

            var session = ObjectFactory.GetInstance<IBusinessSafeSessionFactory>().GetSession();
            var result = session
                .CreateSQLQuery("SELECT * FROM MultiHazardRiskAssessment WHERE Id = '" + riskAssessmentId + "'")
                .UniqueResult();

            var riskAssessment = (object[])result;

            Assert.IsNotNull(riskAssessment);

            //todo: sort these out below.
            //Assert.That(riskAssessment[1].ToString().Trim(), Is.EqualTo("Test Title"));
            //Assert.That(riskAssessment[2].ToString().Trim(), Is.EqualTo("Test Reference"));
            //Assert.That(riskAssessment[12].ToString().Trim(), Is.EqualTo("location location"));
            //Assert.That(riskAssessment[13].ToString().Trim(), Is.EqualTo("test test"));
        }

        [Then(@"Error List '(.*)' Contains:")]
        public void ThenErrorListContains(string errorDivId, Table table)
        {
            var errorDiv = GetElement(FindValidationErrorsList, "validation-summary-errors alert alert-error", 10000);
            Assert.True(errorDiv.Exists, "Could not find error list");
            foreach (var tableRow in table.Rows)
            {
                var errorDivContent = errorDiv.InnerHtml.ToLower();
                Assert.That(errorDivContent.Contains(tableRow[0].ToLower()), Is.True, string.Format("Could not find error message {0}", tableRow[0]));
            }


            WebBrowser.Current.WaitForComplete();
        }

        [Then(@"the Error List Contains")]
        public void ThenTheErrorListContains(Table table)
        {
            ThenErrorListContains(null, table);
        }

        private Element FindValidationErrorsList(string errorListClassName)
        {
            return WebBrowser.Current.Div(Find.ByClass(errorListClassName));
        }
    }
}

using BusinessSafe.AcceptanceTests.StepHelpers;
using BusinessSafe.Data.NHibernate.BusinessSafe;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using StructureMap;
using TechTalk.SpecFlow;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class PremisesInformationSteps
    {
        

       
        [Then(@"the result in the combo box for risk assessors should be")]
        public void ThenTheResultInTheComboBoxForRiskAssessorsShouldBe(Table table)
        {
            var selectRiskAssessors = WebBrowser.Current.SelectList(Find.ById("RiskAssessors"));

            foreach (var riskAssessor in table.Rows)
            {
                Assert.That(selectRiskAssessors.InnerHtml.Contains(riskAssessor[0]), Is.True);    
            }

        }

    }
}

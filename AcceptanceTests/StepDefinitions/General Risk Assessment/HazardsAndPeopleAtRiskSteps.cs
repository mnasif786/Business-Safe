using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using BusinessSafe.AcceptanceTests.StepDefinitions.HazardousSubstances.Inventory;
using BusinessSafe.AcceptanceTests.StepHelpers;
using Microsoft.CSharp.RuntimeBinder;
using NHibernate.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class HazardsAndPeopleAtRiskSteps : BaseSteps
    {
        [When(@"Go to the Hazards tabs")]
        public void WhenGoToTheHazardsTabs()
        {
            var hazardLink = WebBrowser.Current.Link(Find.ById("hazardspeople"));
            hazardLink.Click();
        }

        [When(@"Make sure hazards and people sections are populated")]
        public void WhenMakeSureHazardsAndPeopleSectionsArePopulated()
        {
            Thread.Sleep(4000);
            var hazardsSelectList = WebBrowser.Current.SelectLists.Filter(Find.ById("HazardIds")).FirstOrDefault();

            var peopleAtRiskSelectList = WebBrowser.Current.SelectLists.Filter(Find.ById("PeopleAtRiskIds")).FirstOrDefault();

            Assert.That(hazardsSelectList.Options.Count(), Is.GreaterThan(1));
            Assert.That(peopleAtRiskSelectList.Options.Count(), Is.GreaterThan(1));
        }

        [Given(@"I press add hazard")]
        public void IPressAddToHazard()
        {
            var div = WebBrowser.Current.Div(Find.ById("hazards-multi-select"));
            var button = div.Button(Find.ByClass("add-to-selected btn"));
            button.Click();
        }

        [Given(@"I have added '(.*)' to the '(.*)' risk assessment")]
        [When(@"I have added '(.*)' to the '(.*)' risk assessment")]
        public void WhenIHaveAddedAsbestosToTheRiskAssessment(string value, string type)
        {
            string nameOfControl = string.Empty;
            switch(type) {
                case "Hazards":
                    nameOfControl = "hazards";
                    break;
                case "PeopleAtRisk":
                    nameOfControl = "people-at-risk";
                    break;
                case "ControlMeasures":
                    nameOfControl = "fire-safety-control-measures";
                    break;
                case "SourcesOfFuel":
                    nameOfControl = "source-of-fuel";
                    break;
                case "SourcesOfIgnition":
                    nameOfControl = "source-of-ignition";
                    break;


            }
            var stepDef = new AddToInventorySteps();
            var item = stepDef.GivenIHaveSelectedOptionLabelFromMultiSelectControl(value, nameOfControl);

                dynamic hazardAdding = new ExpandoObject();
                hazardAdding.Id = item.GetAttributeValue("data-id");
                hazardAdding.Name = value;
                ScenarioContextHelpers.SetHazardAddingToRiskAssessment(hazardAdding);
        }

        [Then(@"the hazard should be added to the general risk assessment list of hazards")]
        public void ThenTheHazardShouldBeAddedToTheGeneralRiskAssessmentListOfHazards()
        {
            var hazard = ScenarioContextHelpers.GetHazardAddingToRiskAssessment();
            var hazardSelectList = WebBrowser.Current.SelectLists.Skip(1).Take(1).First();
            Assert.True(hazardSelectList.Options.Count(x => x.Text == hazard.Name) == 1);
        }

        [Then(@"the person at risk should be added to the general risk assessment list of people at risk")]
        public void ThenThePersonAtRiskShouldBeAddedToTheGeneralRiskAssessmentListOfPeopleAtRisk()
        {
            var personAtRisk = ScenarioContextHelpers.GetHazardAddingToRiskAssessment();
            var personAtRiskSelectList = WebBrowser.Current.SelectLists.Skip(3).Take(1).First();
            Assert.True(personAtRiskSelectList.Options.Count(x => x.Text == personAtRisk.Name) == 1);
        }

        [Then(@"the hazard should be saved to the general risk assessment")]
        public void ThenTheHazardShouldBeSavedToTheGeneralRiskAssessment()
        {
            Thread.Sleep(2000);


            var riskAssessmentId = GetCurrentRiskAssessmentId();

            var hazard = ScenarioContextHelpers.GetHazardAddingToRiskAssessment();
            var sql = new StringBuilder();
            sql.Append("Select * From MultiHazardRiskAssessmentHazard ");
            sql.Append("Where RiskAssessmentId = '" + riskAssessmentId + "' ");
            sql.Append("And HazardId = '" + hazard.Id + "' ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    using (var sqlDataReader = command.ExecuteReader())
                    {
                        Assert.True(sqlDataReader.HasRows);
                    }
                }
            }
        }

        [Then(@"the hazard with id '(.*)' should be saved to the general risk assessment")]
        public void ThenTheHazardWithidShouldBeSavedToTheGeneralRiskAssessment(int id)
        {
            Thread.Sleep(2000);


            var riskAssessmentId = WebBrowser.Current.TextField(Find.ById("RiskAssessmentId")).Value; //GetCurrentRiskAssessmentId();

            var sql = new StringBuilder();
            sql.Append("Select * From MultiHazardRiskAssessmentHazard ");
            sql.Append("Where RiskAssessmentId = '" + riskAssessmentId + "' ");
            sql.Append("And HazardId = '" + id + "' ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    using (var sqlDataReader = command.ExecuteReader())
                    {
                        Assert.True(sqlDataReader.HasRows);
                    }
                }
            }
        }

        [Then(@"the person at risk with id '(.*)' should be saved to the general risk assessment")]
        public void ThenThePersonAtRiskShouldBeSavedToTheGeneralRiskAssessment(int id)
        {
            Thread.Sleep(2000);


            var riskAssessmentId = WebBrowser.Current.TextField(Find.ById("RiskAssessmentId")).Value;

            var sql = new StringBuilder();
            sql.Append("Select * From RiskAssessmentPeopleAtRisk ");
            sql.Append("Where RiskAssessmentId = '" + riskAssessmentId + "' ");
            sql.Append("And PeopleAtRiskId = '" + id + "' ");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    using (var sqlDataReader = command.ExecuteReader())
                    {
                        Assert.True(sqlDataReader.HasRows);
                    }
                }
            }
        }

        [Then(@"the display warning message should be displayed")]
        public void ThenTheDisplayWarningMessageShouldBeDisplayed()
        {
            Thread.Sleep(2000);

            var displayMessage = WebBrowser.Current.Div(Find.ById("people-at-risk-alert"));
            Assert.That(displayMessage, Is.Not.Null);
            Assert.That(displayMessage.Enabled, Is.True);
        }

        [Then(@"the hazards multi selector should have '(.*)' options")]
        public void ThenTheMultiSelectorShouldHaveOptions(int expectedOptions)
        {
            var hazardsContainer = WebBrowser.Current.Div(Find.ById("hazards-multi-select"));
            Assert.IsNotNull(hazardsContainer, "Could not find hazard multi selector");

            var totalOptions = hazardsContainer.Elements.Count(x => x.TagName == "LI");
            Assert.That(totalOptions, Is.EqualTo(expectedOptions), string.Format("hazard selector contains {0} options; expected {1}", totalOptions, expectedOptions));
        }

        [Then(@"the '(.*)' multi-select contains '(.*)' are in the selected column")]
        public void ThenThoseHazardsAreInTheSelectedColumn(string selectId, string hazardsCsv)
        {
            var hazardsContainer = WebBrowser.Current.SelectList(Find.ById(selectId + "Ids"));

            Assert.IsNotNull(hazardsContainer, string.Format("Could not find multi selector destination ", selectId));

            var expectedHazards = hazardsCsv.Split(',');
            var totalExpectedHazards = expectedHazards.Count() == 1 && expectedHazards[0] == string.Empty
                ? 0
                : expectedHazards.Count();

            var totalOptions = hazardsContainer.Options.Count();
            Assert.That(totalOptions, Is.EqualTo(totalExpectedHazards), string.Format("multi selector destination contains {0} options; expected {1}", totalOptions, totalExpectedHazards));
            for (var i = 0; i < totalOptions; i++)
            {
                Assert.That(hazardsContainer.Options[i].Text, Is.EqualTo(expectedHazards[i]));

            }
        }

        [Then(@"the selected hazards list contains '(.*)'")]
        public void ThenTheSelectedHazardsContainsHazard(string hazardsCsv)
        {
            var expectedHazards = hazardsCsv.Split(',');
            var totalExpectedHazards = expectedHazards.Count() == 1 && expectedHazards[0] == string.Empty? 0: expectedHazards.Count();

            var selectedHazards = WebBrowser.Current.Elements.Filter(e => e.Parent != null && e.Parent.Id == "selectedHazards" && e.TagName == "LI");
            
            //Verify the count of selected hazards is correct
            Assert.That(selectedHazards.Count(), Is.EqualTo(totalExpectedHazards), string.Format("multi selector destination contains {0} options; expected {1}", selectedHazards, totalExpectedHazards));

            //Verify that the hazard text is correct
            for (var i = 0; i < selectedHazards.Count(); i++)
            {
                Assert.That(selectedHazards[i].Text, Is.EqualTo(expectedHazards[i]));
            }
        }

        [Given(@"I have added '(.*)' to the hazards list")]
        public void GivenIHaveAddedToTheHazardsList(string hazard)
        {
            var availableHazards = WebBrowser.Current.Elements.Filter(e => e.Parent != null && e.Parent.Id == "availableHazards" && e.TagName == "LI");
            var availableHazardItem = availableHazards.First(x => x.Text == hazard);
            var addSelectedHazardButton = WebBrowser.Current.Button("addSelectedHazards");

            availableHazardItem.Click();
            Thread.Sleep(500);
            addSelectedHazardButton.Click();
        }

        
    }
}

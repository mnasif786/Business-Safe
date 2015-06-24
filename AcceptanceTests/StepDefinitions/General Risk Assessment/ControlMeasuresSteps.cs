using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using BusinessSafe.AcceptanceTests.StepHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace BusinessSafe.AcceptanceTests.StepDefinitions.General_Risk_Assessment
{
    [Binding]
    public class ControlMeasuresSteps : BaseSteps
    {
        [When(@"I press button with text '(.*)'")]
        public void WhenIPressButtonWithTextAddControlMeasure(string buttonText)
        {
            var button = WebBrowser.Current.Buttons.FirstOrDefault(x => x.InnerHtml.Contains(buttonText));

            if (button != null)
                button.Click();
        }

        [Given(@"I press button Add Control Measure")]
        [When(@"I press button Add Control Measure")]
        public void WhenIPressButtonAddControlMeasure()
        {
            Thread.Sleep(2000);

            var button = WebBrowser.Current.Buttons.Filter(Find.ByClass("btn add-control-measure")).FirstOrDefault();

            Assert.IsNotNull(button, "Could not find add button");

            button.Click();
        }


        [Then(@"the control measure '(.*)' should be saved to the general risk assessment")]
        public void ThenTheControlMeasureShouldBeSavedToTheGeneralRiskAssessment(string controlMeasureText)
        {
            Thread.Sleep(5000);
            var riskAssessmentHazardId = WebBrowser.Current.Links.First(x => x.ClassName == "edit-hazard-description add-edit-link").GetAttributeValue("data-id");
            var sql = new StringBuilder();

            sql.Append("Select * From MultiHazardRiskAssessmentControlMeasure ");
            sql.Append("Where MultiHazardRiskAssessmentHazardId = '" + riskAssessmentHazardId + "' ");
            sql.Append("And ControlMeasure = '" + controlMeasureText + "'");

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

        [Given(@"I highlight the newly created control measure")]
        [When(@"I highlight the newly created control measure")]
        [Then(@"I highlight the newly created control measure")]
        public void ThenIHighlightTheNewlyCreatedControlMeasure()
        {
            var controlMeasureSpan = WebBrowser.Current.Spans.First(x => x.ClassName == "controlCount");

            controlMeasureSpan.Click();

            Thread.Sleep(2000);
        }

        [Then(@"the control measure should be removed from the general risk assessment")]
        public void ThenTheControlMeasureShouldBeRemovedFromTheGeneralRiskAssessment()
        {
            Thread.Sleep(2000);

            // Need to check the general risk assessment hazards table
            var form = WebBrowser.Current.Forms.First();
            var riskAssessmentHazardId = form.Id;
            var sql = new StringBuilder();

            sql.Append("Select * From MultiHazardRiskAssessmentControlMeasure ");
            sql.Append("Where MultiHazardRiskAssessmentHazardId = '" + riskAssessmentHazardId + "' ");
            sql.Append("And ControlMeasure = 'test control measure' ");
            sql.Append("And Deleted = 0");

            using (var conn = new SqlConnection(BusinessSafeConnectionString))
            {
                conn.Open();

                using (var command = new SqlCommand(sql.ToString(), conn))
                {
                    using (var sqlDataReader = command.ExecuteReader())
                    {
                        Assert.False(sqlDataReader.HasRows);
                    }
                }
            }
        }

        [Then(@"the '(.*)' error is displayed")]
        public void ThenThePleaseEnterAControlMeasureErrorIsDisplayed(string errorMessage)
        {
            Thread.Sleep(1000);

            var errorSpan = WebBrowser.Current.Element(Find.ByClass("field-validation-error create-control-measure-error-display"));
            Assert.True(errorSpan.Exists);
            Assert.That(errorSpan.InnerHtml.Contains("<span>" + errorMessage + "</span>"), Is.True);
        }

        [When(@"I press edit hazard description link")]
        public void WhenIPressEditHazardDescriptionLink()
        {
            Thread.Sleep((2000));

            var linkElement = Find.ByName("edit-hazard-description");

            WebBrowser.Current.Link(linkElement).Click();
        }

        [Then(@"the hazard description should be saved")]
        public void ThenTheHazardDescriptionShouldBeSaved()
        {
            Thread.Sleep(2000);

            // Need to check the general risk assessment hazards table
            var form = WebBrowser.Current.Forms.First();
            var riskAssessmentHazardId = form.Id;
            var sql = new StringBuilder();

            sql.Append("Select * From MultiHazardRiskAssessmentHazard ");
            sql.Append("Where Id = '" + riskAssessmentHazardId + "' ");
            sql.Append("And Description = 'test hazard name'");

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

        [Then(@"the hazard title should be saved")]
        public void ThenTheHazardTitleShouldBeSaved()
        {
            Thread.Sleep(2000);

            // Need to check the general risk assessment hazards table
            var form = WebBrowser.Current.Forms.First();
            var riskAssessmentHazardId = form.Id;
            var sql = new StringBuilder();

            sql.Append("Select * From Hazard ");
            sql.Append("Where Name = 'New RA Hazard Name'");

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

        [Then(@"the please enter a description error message should be displayed")]
        public void ThenThePleaseEnterADescriptionErrorMessageShouldBeDisplayed()
        {
            Thread.Sleep(1000);

            var errorSpan = WebBrowser.Current.Span(Find.ByClass("alert alert-error edit-hazard-description-error-display"));

            Assert.True(errorSpan.Exists);
        }
    }
}
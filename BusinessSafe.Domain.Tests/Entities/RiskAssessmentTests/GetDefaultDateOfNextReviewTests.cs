using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class GetDefaultDateOfNextReviewTests
    {
        [Test]
        public void Given_risk_assessment_has_no_assessment_date_When_GetDefaultDateOfNextReview_Then_null_is_returned()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("Test GRA", "GRA01", 123L, new UserForAuditing());

            //When
            var defaultDateOfNextReview = riskAssessment.GetDefaultDateOfNextReview();

            //Then
            Assert.That(!defaultDateOfNextReview.HasValue);
        }

        [Test]
        public void Given_risk_assessment_has_assessment_date_but_no_reviews_When_GetDefaultDateOfNextReview_Then_date_a_year_after_assessment_date_of_ra_Is_returned()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("Test GRA", "GRA01", 123L, new UserForAuditing());
            riskAssessment.AssessmentDate = new DateTime(2050, 03, 17);

            //When
            var defaultDateOfNextReview = riskAssessment.GetDefaultDateOfNextReview();

            //Then
            Assert.That(defaultDateOfNextReview.Value, Is.EqualTo(new DateTime(2051, 03, 17)));
        }

        [Test]
        public void Given_risk_assessment_has_previous_reviews_and_last_review_is_completed_When_GetDefaultDateOfNextReview_Then_date_a_year_after_completed_date_of_last_review_is_returned()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("Test GRA", "GRA01", 123L, new UserForAuditing());
            riskAssessment.AssessmentDate = new DateTime(2050, 03, 17);
            riskAssessment.AddReview(new RiskAssessmentReview { CompletionDueDate = new DateTime(2050, 06, 15), CompletedDate = new DateTime(2050, 06, 03) });
            riskAssessment.AddReview(new RiskAssessmentReview { CompletionDueDate = new DateTime(2051, 06, 15), CompletedDate = new DateTime(2051, 06, 03) });
            riskAssessment.AddReview(new RiskAssessmentReview { CompletionDueDate = new DateTime(2052, 06, 15), CompletedDate = new DateTime(2052, 06, 03) });

            //When
            var defaultDateOfNextReview = riskAssessment.GetDefaultDateOfNextReview();

            //Then
            Assert.That(defaultDateOfNextReview.Value, Is.EqualTo(new DateTime(2053, 06, 03)));
        }

        [Test]
        public void Given_risk_assessment_has_previous_reviews_and_last_review_is_not_completed_When_GetDefaultDateOfNextReview_Then_date_a_year_after_due_date_of_last_review_is_returned()
        {
            //Given
            var riskAssessment = GeneralRiskAssessment.Create("Test GRA", "GRA01", 123L, new UserForAuditing());
            riskAssessment.AssessmentDate = new DateTime(2050, 03, 17);
            riskAssessment.AddReview(new RiskAssessmentReview { CompletionDueDate = new DateTime(2050, 06, 15), CompletedDate = new DateTime(2050, 06, 03) });
            riskAssessment.AddReview(new RiskAssessmentReview { CompletionDueDate = new DateTime(2051, 06, 15), CompletedDate = new DateTime(2051, 06, 03) });
            riskAssessment.AddReview(new RiskAssessmentReview { CompletionDueDate = new DateTime(2052, 06, 15) });

            //When
            var defaultDateOfNextReview = riskAssessment.GetDefaultDateOfNextReview();

            //Then
            Assert.That(defaultDateOfNextReview.Value, Is.EqualTo(new DateTime(2053, 06, 15)));
        }
    }
}

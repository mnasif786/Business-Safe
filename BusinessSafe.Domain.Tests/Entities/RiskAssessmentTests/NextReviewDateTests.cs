using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    public class NextReviewDateTests
    {
        [Test]
        public void Given_a_review_has_been_added_then_the_next_review_date_equals_the_CompletionDueDate_of_the_review_added()
        {
            //GIVEN
            var review = new RiskAssessmentReview();
            review.CompletionDueDate = DateTime.Now.AddDays(10);
            var target = new GeneralRiskAssessment();

            //WHEN
            target.AddReview(review);

            //THEN
            Assert.That(target.NextReviewDate, Is.EqualTo(review.CompletionDueDate));
        }

        [Test]
        public void Given_a_review_is_completed_then_the_next_review_date_equals_the_next_review_date_specified_during_completion()
        {
            //GIVEN
            var expectedNextReviewDate = DateTime.Now.AddDays(50);
            var review = new RiskAssessmentReview();
            review.CompletionDueDate = DateTime.Now.AddDays(10);
            review.RiskAssessmentReviewTask = new RiskAssessmentReviewTask();
            var target = new GeneralRiskAssessment();
            target.AddReview(review);

            //WHEN
            review.Complete("Comments", null, expectedNextReviewDate, false, new List<CreateDocumentParameters>(), new User(){Employee = new Employee()});

            //THEN
            Assert.That(target.NextReviewDate, Is.EqualTo(expectedNextReviewDate));
        }

        [Test]
        public void Given_a_review_is_deleted_then_the_next_review_date_equals_null()
        {
            //GIVEN
            var review = new RiskAssessmentReview();
            review.CompletionDueDate = DateTime.Now.AddDays(10);
            var target = new GeneralRiskAssessment();
            target.AddReview(review);

            //WHEN
            review.MarkForDelete(null);

            //THEN
            Assert.That(target.NextReviewDate, Is.EqualTo(null));
        }

        [Test]
        public void Given_a_reviews_CompletionDueDate_is_changed_then_the_next_review_date_equals_the_new_CompletionDueDate()
        {
            //GIVEN
            var review = new RiskAssessmentReview();
            review.CompletionDueDate = DateTime.Now.AddDays(10);
            review.RiskAssessmentReviewTask = new RiskAssessmentReviewTask();
            var target = new GeneralRiskAssessment();
            target.AddReview(review);

            //WHEN
            review.Edit(null,null,DateTime.Now.AddDays(20)); 

            //THEN
            Assert.That(target.NextReviewDate, Is.EqualTo(review.CompletionDueDate));
        }
    }
}

using System;
using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentReviewTests
{
    [TestFixture]
    [Category("Unit")]
    public class CompleteTests
    {
        [Test]
        public void When_complete_review_without_archive_or_next_review_set_Then_should_throw_correct_exception()
        {
            //Given
            var review = new RiskAssessmentReview();
            review.CompletedDate = new DateTime();
            review.CompletedBy = new Employee();
            var userForAudting = new UserForAuditing();
            var user = new User();

            //When
            //Then

            Assert.Throws<AttemptingToCompleteRiskAssessmentReviewWithoutArchiveOrNextReviewDateSetException>(() => review.Complete("comments", userForAudting, null, false, null, user));
        }

        [Test]
        public void When_complete_review_already_completed_Then_should_throw_correct_exception()
        {
            //Given
            var review = new RiskAssessmentReview();
            review.CompletedDate = new DateTime();
            review.CompletedBy = new Employee();
            var userForAuditing = new UserForAuditing();
            var user = new User();

            //When
            //Then
            Assert.Throws<AttemptingToCompleteRiskAssessmentReviewThatIsCompletedException>(() => review.Complete("comments", userForAuditing, DateTime.Now, false, null, user));
        }

        [Test]
        public void When_complete_Then_should_set_properties_correctly()
        {
            //Given
            var review = new RiskAssessmentReview()
            {
                RiskAssessmentReviewTask = new RiskAssessmentReviewTask(),
                RiskAssessment = new GeneralRiskAssessment()
            };
            var user = new User{ Employee = new Employee() };
            var userForAuditing = new UserForAuditing() ;

            //When
            review.Complete("comments", userForAuditing, DateTime.Now, false, new List<CreateDocumentParameters>(), user);

            //Then
            Assert.That(review.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(review.LastModifiedBy, Is.EqualTo(userForAuditing));
            Assert.That(review.Comments, Is.EqualTo("comments"));
            Assert.That(review.CompletedDate.Value.Date, Is.EqualTo(DateTime.Today.Date));
            Assert.That(review.LastModifiedBy, Is.EqualTo(userForAuditing));
        }

        [Test]
        public void When_complete_Then_should_set_associated_RiskAssessmentReviewTask_to_complete()
        {
            //Given
            var mockReviewTask = new Mock<RiskAssessmentReviewTask>();
            mockReviewTask.Setup(x => x.Complete(It.IsAny<string>(), null, null, It.IsAny<UserForAuditing>(), It.IsAny<User>()));

            var review = new RiskAssessmentReview()
                             {
                                 RiskAssessmentReviewTask = mockReviewTask.Object,
                                 RiskAssessment = new GeneralRiskAssessment()
                             };
            var userForAuditing = new UserForAuditing();
            var user = new User();
            
            //When
            review.Complete("comments", userForAuditing, DateTime.Now, false, null, user);

            //Then
            mockReviewTask.Verify(x => x.Complete("comments", null, new List<long>(), userForAuditing, user), Times.Once());
        }

        [Test]
        public void When_complete_review_already_completed_Then_task_remains_incomplete()
        {
            //Given
            var mockReviewTask = new Mock<RiskAssessmentReviewTask>();
            mockReviewTask.Setup(x => x.Complete(It.IsAny<string>(), null, null, It.IsAny<UserForAuditing>(), It.IsAny<User>()));
            var userForAuditing = new UserForAuditing
                           {
                               Id = Guid.NewGuid()
                           };
            var employee = new Employee();
            var user = new User();
            var review = new RiskAssessmentReview()
            {
                CompletedBy = It.IsAny<Employee>(),
                CompletedDate = It.IsAny<DateTime>(),
                RiskAssessmentReviewTask = mockReviewTask.Object
            };

            //When

            //Then
            Assert.Throws<AttemptingToCompleteRiskAssessmentReviewThatIsCompletedException>(() => review.Complete("comments", userForAuditing, DateTime.Now, false, null, user));
            mockReviewTask.Verify(x => x.Complete("comments", null, null, userForAuditing, user), Times.Never());
        }
    }
}

using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentReviewTaskTests
{
    [TestFixture]
    public class IsTaskCompletedNotificationRequiredTests
    {
        RiskAssessmentReviewTask reviewTask;
        Mock<RiskAssessment> _riskAssessment;
        Mock<RiskAssessor> _riskAssessor;
        Mock<RiskAssessmentReview> _review;
        Mock<Employee> _riskAssessorEmployee;
        string _email;

        [SetUp]
        public void Setup()
        {
            _riskAssessment = new Mock<RiskAssessment>();
            _riskAssessor = new Mock<RiskAssessor>();
            _riskAssessorEmployee = new Mock<Employee>();
            _review = new Mock<RiskAssessmentReview>();
            _email = "risk assessor email";
            
            _riskAssessorEmployee.Setup(x => x.MainContactDetails.Email).Returns(_email);
            _riskAssessor.Setup(x => x.Employee).Returns(_riskAssessorEmployee.Object);
            _riskAssessment.Setup(x => x.RiskAssessor).Returns(_riskAssessor.Object);
            _review.Setup(x => x.RiskAssessment).Returns(_riskAssessment.Object);
        }

        [Test]
        public void Given_task_has_all_requirements_When_IsTaskCompletedNotificationRequired_Then_return_true()
        {
            // Given
            reviewTask = new RiskAssessmentReviewTask() { RiskAssessmentReview = _review.Object, SendTaskCompletedNotification = true };

            // When
            var result = reviewTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_task_SendTaskCompletedNotification_is_null_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            reviewTask = new RiskAssessmentReviewTask() { RiskAssessmentReview = _review.Object };

            // When
            var result = reviewTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_task_SendTaskCompletedNotification_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            reviewTask = new RiskAssessmentReviewTask() { RiskAssessmentReview = _review.Object, SendTaskCompletedNotification = false };

            // When
            var result = reviewTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_does_not_have_contact_details_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessorEmployee.Setup(x => x.MainContactDetails);
            reviewTask = new RiskAssessmentReviewTask() { RiskAssessmentReview = _review.Object, SendTaskCompletedNotification = true };

            // When
            var result = reviewTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_does_not_have_RiskAssessor_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessment.Setup(x => x.RiskAssessor);
            reviewTask = new RiskAssessmentReviewTask() { RiskAssessmentReview = _review.Object, SendTaskCompletedNotification = true };

            // When
            var result = reviewTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_email_is_null_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessorEmployee.Setup(x => x.MainContactDetails.Email);
            reviewTask = new RiskAssessmentReviewTask() { RiskAssessmentReview = _review.Object, SendTaskCompletedNotification = true };

            // When
            var result = reviewTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_email_is_empty_string_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessorEmployee.Setup(x => x.MainContactDetails.Email).Returns(string.Empty);
            reviewTask = new RiskAssessmentReviewTask() { RiskAssessmentReview = _review.Object, SendTaskCompletedNotification = true };

            // When
            var result = reviewTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }
    }
}

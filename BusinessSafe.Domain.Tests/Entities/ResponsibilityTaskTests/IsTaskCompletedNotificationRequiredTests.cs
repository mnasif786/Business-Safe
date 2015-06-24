using System;

using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.ResponsibilityTaskTests
{
    [TestFixture]
    public class IsTaskCompletedNotificationRequiredTests
    {
        ResponsibilityTask responsibilityTask;
        Mock<Responsibility> _responsibility;
        Mock<Employee> _responsibilityOwner;
        string _email;

        [SetUp]
        public void Setup()
        {
            _responsibility = new Mock<Responsibility>();
            _responsibilityOwner = new Mock<Employee>();
            _email = "risk assessor email";

            _responsibilityOwner.Setup(x => x.GetEmail()).Returns(_email);
            _responsibilityOwner.Setup(x => x.HasEmail).Returns(true);
            _responsibility.Setup(x => x.Owner).Returns(_responsibilityOwner.Object);
        }

        [Test]
        public void Given_task_has_all_requirements_When_IsTaskCompletedNotificationRequired_Then_return_true()
        {
            // Given
            responsibilityTask = new ResponsibilityTask() { Responsibility = _responsibility.Object, SendTaskCompletedNotification = true };

            // When
            var result = responsibilityTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_task_SendTaskCompletedNotification_is_null_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            responsibilityTask = new ResponsibilityTask() { Responsibility = _responsibility.Object };

            // When
            var result = responsibilityTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_task_SendTaskCompletedNotification_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            responsibilityTask = new ResponsibilityTask() { Responsibility = _responsibility.Object, SendTaskCompletedNotification = false };

            // When
            var result = responsibilityTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_does_not_have_contact_details_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _responsibilityOwner.Setup(x => x.GetEmail());
            responsibilityTask = new ResponsibilityTask() { Responsibility = _responsibility.Object, SendTaskCompletedNotification = true };

            // When
            var result = responsibilityTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_does_not_have_RiskAssessor_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _responsibility.Setup(x => x.Owner);
            responsibilityTask = new ResponsibilityTask() { Responsibility = _responsibility.Object, SendTaskCompletedNotification = true };

            // When
            var result = responsibilityTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_email_is_null_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _responsibilityOwner.Setup(x => x.HasEmail).Returns(false);
            responsibilityTask = new ResponsibilityTask() { Responsibility = _responsibility.Object, SendTaskCompletedNotification = true };

            // When
            var result = responsibilityTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_email_is_empty_string_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _responsibilityOwner.Setup(x => x.GetEmail()).Returns(string.Empty);
            responsibilityTask = new ResponsibilityTask() { Responsibility = _responsibility.Object, SendTaskCompletedNotification = true };

            // When
            var result = responsibilityTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }
    }

    public class TestableFCMTask : Domain.Entities.FurtherControlMeasureTask
    {
        private readonly RiskAssessment _riskAssessment;

        public TestableFCMTask(RiskAssessment riskAssessment)
        {
            _riskAssessment = riskAssessment;
        }

        public override void MarkAsNoLongerRequired(UserForAuditing user)
        {
            throw new NotImplementedException();
        }

        protected override Task GetBasisForClone()
        {
            throw new NotImplementedException();
        }

        public override RiskAssessment RiskAssessment
        {
            get { return _riskAssessment; }
        }
    }
}

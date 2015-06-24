using System;

using BusinessSafe.Domain.Entities;

using Moq;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FurtherControlMeasureTaskTests
{
    [TestFixture]
    public class IsTaskCompletedNotificationRequiredTests
    {
        TestableFCMTask fcmTask;
        Mock<RiskAssessment> _riskAssessment;
        Mock<RiskAssessor> _riskAssessor;
        Mock<Employee> _riskAssessorEmployee;
        string _email;

        [SetUp]
        public void Setup()
        {
            _riskAssessment = new Mock<RiskAssessment>();
            _riskAssessor = new Mock<RiskAssessor>();
            _riskAssessorEmployee = new Mock<Employee>();
            _email = "risk assessor email";
            
            _riskAssessorEmployee.Setup(x => x.MainContactDetails.Email).Returns(_email);
            _riskAssessor.Setup(x => x.Employee).Returns(_riskAssessorEmployee.Object);
            _riskAssessment.Setup(x => x.RiskAssessor).Returns(_riskAssessor.Object);
        }

        [Test]
        public void Given_task_has_all_requirements_When_IsTaskCompletedNotificationRequired_Then_return_true()
        {
            // Given
            fcmTask = new TestableFCMTask(_riskAssessment.Object) { SendTaskCompletedNotification = true };

            // When
            var result = fcmTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_task_SendTaskCompletedNotification_is_null_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            fcmTask = new TestableFCMTask(_riskAssessment.Object) { };

            // When
            var result = fcmTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_task_SendTaskCompletedNotification_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            fcmTask = new TestableFCMTask(_riskAssessment.Object) { SendTaskCompletedNotification = false };

            // When
            var result = fcmTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_does_not_have_contact_details_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessorEmployee.Setup(x => x.MainContactDetails);
            fcmTask = new TestableFCMTask(_riskAssessment.Object) { SendTaskCompletedNotification = true };

            // When
            var result = fcmTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_does_not_have_RiskAssessor_is_false_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessment.Setup(x => x.RiskAssessor);
            fcmTask = new TestableFCMTask(_riskAssessment.Object) { SendTaskCompletedNotification = true };

            // When
            var result = fcmTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_email_is_null_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessorEmployee.Setup(x => x.MainContactDetails.Email);
            fcmTask = new TestableFCMTask(_riskAssessment.Object) { SendTaskCompletedNotification = true };

            // When
            var result = fcmTask.IsTaskCompletedNotificationRequired();

            // Then
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_tasks_RiskAssessor_email_is_empty_string_When_IsTaskCompletedNotificationRequired_Then_return_false()
        {
            // Given
            _riskAssessorEmployee.Setup(x => x.MainContactDetails.Email).Returns(string.Empty);
            fcmTask = new TestableFCMTask(_riskAssessment.Object) { SendTaskCompletedNotification = true };

            // When
            var result = fcmTask.IsTaskCompletedNotificationRequired();

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries.CompletedTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Data.Tests.Queries.CompletedTaskQueryTests
{
    [TestFixture]
    public class GetCompletedRiskAssessmentReviewTasksForEmployeeQueryTests
    {
        private Mock<IQueryableWrapper<RiskAssessment>> _queryableWrapper;
        private List<RiskAssessment> _riskAssessments;

        [SetUp]
        [Ignore]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<RiskAssessment>>();
            _riskAssessments = new List<RiskAssessment>();

            _queryableWrapper
                .Setup(x => x.Queryable())
                .Returns(() => _riskAssessments.AsQueryable());
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assigned_to_task_but_not_risdk_assessor_then_dont_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var RAEmployee = new Employee() { Id = Guid.NewGuid() };

            var riskAssessor = new RiskAssessor() { Id = 5596871, Employee = RAEmployee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, ReviewAssignedTo = employee };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                TaskAssignedTo = employee,
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(-5),
                TaskStatus = TaskStatus.Completed
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target = new GetCompletedRiskAssessmentReviewTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));

        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assesor_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletedDate = DateTime.Now.AddDays(-5),
                TaskStatus = TaskStatus.Completed
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target = new GetCompletedRiskAssessmentReviewTasksForEmployeeQuery( _queryableWrapper.Object );

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));

        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assessor_and_notification_frequency_is_set_to_weekly_then_return_tasks_since_previous_week()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)System.DayOfWeek.Wednesday };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletedDate = DateTime.Now.AddDays(-5),
                TaskStatus = TaskStatus.Completed
            };
            
            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target = new GetCompletedRiskAssessmentReviewTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}

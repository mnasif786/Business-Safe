using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries.DueTaskQuery;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Data.Tests.Queries.DueTomorrowTaskQueryTests
{
    [TestFixture]
    class GetDueRiskAssessmentReviewTasksForEmployeeQueryTests
    {
        private Mock<IQueryableWrapper<RiskAssessment>> _queryableWrapper;
        private List<RiskAssessment> _riskAssessments;

        [SetUp]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<RiskAssessment>>();
            _riskAssessments = new List<RiskAssessment>();

            _queryableWrapper.Setup(x => x.Queryable())
               .Returns(() => _riskAssessments.AsQueryable());
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assigned_to_task_due_tomorrow_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
           
            var riskAssessement = new HazardousSubstanceRiskAssessment();
            var riskAssessementReview = new RiskAssessmentReview()
                                            {
                                                RiskAssessment = riskAssessement, 
                                                ReviewAssignedTo = employee
                                            };

            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                TaskAssignedTo = employee, 
                RiskAssessmentReview = riskAssessementReview, 
                TaskCompletionDueDate = DateTime.Now.AddDays(1)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target = new GetDueRiskAssessmentReviewTasksForEmployeeQuery( _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));

        }

         [Ignore]
        [Test]
        public void Given_employee_is_risk_assesor_for_task_due_tomorrow_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement,};
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(1)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetDueRiskAssessmentReviewTasksForEmployeeQuery(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_weekly_and_task_is_due_in_less_than_a_week_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly};
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(4)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetDueRiskAssessmentReviewTasksForEmployeeQuery(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_weekly_and_task_is_due_in_more_than_a_week_then_dont_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(14)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetDueRiskAssessmentReviewTasksForEmployeeQuery(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_monthly_and_task_is_due_in_less_than_a_month_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(4)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetDueRiskAssessmentReviewTasksForEmployeeQuery(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_monthly_and_task_is_due_in_more_than_a_month_then_dont_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(44)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetDueRiskAssessmentReviewTasksForEmployeeQuery(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;

namespace BusinessSafe.Data.Tests.Queries.OverdueTaskQueryTests
{
    [TestFixture]
    public class GetOverdueRiskAssessmentReviewTasksForEmployeeQueryTests
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
        public void Given_employee_is_assigned_to_task_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};

           
            var riskAssessement = new HazardousSubstanceRiskAssessment();
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, ReviewAssignedTo = employee};
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                TaskAssignedTo = employee, 
                RiskAssessmentReview = riskAssessementReview, 
                TaskCompletionDueDate = DateTime.Now.AddDays(-5)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueRiskAssessmentReviewTasks(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));

        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assesor_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee};

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement,};
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(-5)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueRiskAssessmentReviewTasks(
                    _queryableWrapper.Object);

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
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)DayOfWeek.Wednesday };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(-5)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueRiskAssessmentReviewTasks(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assignee_and_notification_frequency_is_set_to_weekly_then_return_tasks_since_previous_week()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)DayOfWeek.Wednesday };
            
            var riskAssessement = new HazardousSubstanceRiskAssessment() { };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                TaskAssignedTo = employee,
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletionDueDate = DateTime.Now.AddDays(-5)
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueRiskAssessmentReviewTasks(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assignee_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly, NotificationFrequecy = 26 };

            var riskAssessement = new HazardousSubstanceRiskAssessment() { };
            var riskAssessementReview = new RiskAssessmentReview() { RiskAssessment = riskAssessement, };
            riskAssessementReview.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                TaskAssignedTo = employee,
                RiskAssessmentReview = riskAssessementReview,
                TaskCompletedDate = null,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month,
                Id = 1
            };

            riskAssessement.Reviews.Add(riskAssessementReview);
            _riskAssessments.Add(riskAssessement);


            var riskAssessement1 = new HazardousSubstanceRiskAssessment() { };
            var riskAssessementReview1 = new RiskAssessmentReview() { RiskAssessment = riskAssessement1, };
            riskAssessementReview1.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                TaskAssignedTo = employee,
                RiskAssessmentReview = riskAssessementReview1,
                TaskCompletedDate = null,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month,
                Id = 2
            };

            riskAssessement.Reviews.Add(riskAssessementReview1);
            _riskAssessments.Add(riskAssessement1);

            var riskAssessement2 = new HazardousSubstanceRiskAssessment() { };
            var riskAssessementReview2 = new RiskAssessmentReview() { RiskAssessment = riskAssessement2, };
            riskAssessementReview2.RiskAssessmentReviewTask = new RiskAssessmentReviewTask()
            {
                TaskAssignedTo = employee,
                RiskAssessmentReview = riskAssessementReview2,
                TaskCompletedDate = null,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month,
                Id = 3
            };

            riskAssessement.Reviews.Add(riskAssessementReview2);
            _riskAssessments.Add(riskAssessement2);

            var target =
                new GetOverdueRiskAssessmentReviewTasks(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].RiskAssessmentReview.RiskAssessmentReviewTask.Id, Is.EqualTo(1));
            Assert.That(result[1].RiskAssessmentReview.RiskAssessmentReviewTask.Id, Is.EqualTo(2));

        }
    }
}

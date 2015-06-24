using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries.CompletedTaskQuery;
using BusinessSafe.Data.Queries.DueTaskQuery;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Tests.Queries.DueTomorrowTaskQueryTests
{
    [TestFixture]
    public class GetDueMultiHazardFurtherControlMeasureTasksForEmployeeQueryTests
    {
        private Mock<IBusinessSafeSessionManager> _sessionManager;
        private Mock<ISession> _session;
        private Mock<IQueryableWrapper<GeneralRiskAssessment>> _queryableWrapper;
        private List<GeneralRiskAssessment> _gras;

        [SetUp]
        public void Setup()
        {
            _session = new Mock<ISession>();
            _sessionManager = new Mock<IBusinessSafeSessionManager>();

            _sessionManager
                .Setup(x => x.Session)
                .Returns(() => _session.Object);

            _queryableWrapper = new Mock<IQueryableWrapper<GeneralRiskAssessment>>();

            _gras = new List<GeneralRiskAssessment>();

            _queryableWrapper.Setup(x => x.Queryable())
               .Returns(() => _gras.AsQueryable());
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assigned_to_task_due_tomorrow_and_is_notified_daily_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee, 
                TaskStatus = TaskStatus.Outstanding, 
                TaskCompletionDueDate = DateTime.Now.AddDays(1)
            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        [Ignore]
        public void Given_employee_is_assigned_to_task_due_in_2days_and_is_notified_daily_then_dont_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(2)
            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_weekly_and_task_due_in_less_than_a_week_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly };
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(3)
            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        [Ignore]
        public void Given_notification_set_to_weekly_and_task_due_in_more_than_a_week_then_dont_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly};
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(9)
            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_monthly_and_task_due_in_more_than_a_month_then_dont_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(39)
            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_set_to_monthly_and_task_due_in_less_than_a_month_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(9)
            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetDueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }
    }
}

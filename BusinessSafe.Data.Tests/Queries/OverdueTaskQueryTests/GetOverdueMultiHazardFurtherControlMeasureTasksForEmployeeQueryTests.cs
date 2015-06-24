using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Entities.SafeCheck;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;
using Checklist = BusinessSafe.Domain.Entities.SafeCheck.Checklist;
using BusinessSafe.Data.Queries.OverdueTaskQuery;

namespace BusinessSafe.Data.Tests.Queries.OverdueTaskQueryTests
{
    [TestFixture]
    public class GetOverdueMultiHazardFurtherControlMeasureTasksForEmployeeQueryTests
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

            _sessionManager.Setup(x => x.Session)
                .Returns(() => _session.Object);

            _queryableWrapper = new Mock<IQueryableWrapper<GeneralRiskAssessment>>();

            _gras = new List<GeneralRiskAssessment>();

            _queryableWrapper.Setup(x => x.Queryable())
               .Returns(() => _gras.AsQueryable());

        }

        [Test]
        [Ignore]
        public void Given_employee_is_assigned_to_task_then_return_task()
        {
            //GIVEN
            var employee = new Employee() {Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement,new Hazard(), null);
            hazard.AddFurtherActionTask( new MultiHazardRiskAssessmentFurtherControlMeasureTask() { TaskAssignedTo = employee, TaskCompletionDueDate = DateTime.Now.AddDays(-5)},null );
            riskAssessement.Hazards.Add(hazard);
            
            _gras.Add(riskAssessement);
            var target = new GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count,Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assesor_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var riskAssessor = new RiskAssessor() {Id = 5596870, Employee = employee};
            var riskAssessement = new GeneralRiskAssessment(){RiskAssessor = riskAssessor};
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = new Employee(){Id=Guid.NewGuid()}, 
                TaskCompletionDueDate = DateTime.Now.AddDays(-5)
            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        [Ignore]
        public void Given_risk_assessor_is_null_and_employee_is_not_assigned_task_then_return_empty_list()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var riskAssessement = new GeneralRiskAssessment() { };
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask() { TaskAssignedTo = new Employee() { Id = Guid.NewGuid() } }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);

            var target = new GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_asignee_and_notification_frequency_is_set_to_weekly_then_return_tasks_since_previous_week()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)DayOfWeek.Wednesday };
            var riskAssessement = new GeneralRiskAssessment() { };
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = employee,
                TaskCompletionDueDate = DateTime.Now.AddDays(-2)
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = employee,
                TaskCompletionDueDate = DateTime.Now.AddDays(-3)
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = employee,
                TaskCompletionDueDate = DateTime.Now.AddDays(-12)
            }, null);

            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);

            var target = new GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assessor_and_notification_frequency_is_set_to_weekly_then_return_tasks_since_previous_week()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)DayOfWeek.Wednesday };
            var riskAssessor = new RiskAssessor() {Id = 5596870, Employee = employee};
            var riskAssessement = new GeneralRiskAssessment() { RiskAssessor = riskAssessor };
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = new Employee() { Id = Guid.NewGuid()},
                TaskCompletionDueDate = DateTime.Now.AddDays(-2)
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskCompletionDueDate = DateTime.Now.AddDays(-3)
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskCompletionDueDate = DateTime.Now.AddDays(-12)
            }, null);

            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);

            var target = new GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_asignee_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly, NotificationFrequecy = 26 };
            var riskAssessement = new GeneralRiskAssessment() { };
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = employee,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = employee,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = employee,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month
            }, null);

            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);

            var target = new GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assessor_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly, NotificationFrequecy = 26 };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };
            var riskAssessement = new GeneralRiskAssessment() { RiskAssessor = riskAssessor };
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskAssignedTo = new Employee() { Id = Guid.NewGuid() },
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 111,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month
            }, null);

            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);

            var target = new GetOverdueMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
        }
    }
}

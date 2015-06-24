using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NHibernate;
using NUnit.Framework;

namespace BusinessSafe.Data.Tests.Queries.OverdueTaskQueryTests
{
    [TestFixture]
    public class GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQueryTests
    {

        private Mock<IQueryableWrapper<HazardousSubstanceRiskAssessment>> _queryableWrapper;
        private List<HazardousSubstanceRiskAssessment> _hsRiskAssessments;

        [SetUp]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<HazardousSubstanceRiskAssessment>>();
            _hsRiskAssessments = new List<HazardousSubstanceRiskAssessment>();

            _queryableWrapper.Setup(x => x.Queryable())
               .Returns(() => _hsRiskAssessments.AsQueryable());
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assigned_to_task_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var riskAssessement = new HazardousSubstanceRiskAssessment();
            var hazardousSubstanceRiskAssessmentFurtherControlMeasureTask = 
                new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() {TaskAssignedTo = employee, TaskCompletionDueDate = DateTime.Now.AddDays(-5)};
            riskAssessement.FurtherControlMeasureTasks.Add(hazardousSubstanceRiskAssessmentFurtherControlMeasureTask);

            _hsRiskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
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
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var riskAssessor = new RiskAssessor() {Id = 5596870, Employee = employee};
            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor };
            var hazardousSubstanceRiskAssessmentFurtherControlMeasureTask =
                new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() { HazardousSubstanceRiskAssessment = riskAssessement, TaskCompletionDueDate = DateTime.Now.AddDays(-5) };
            riskAssessement.FurtherControlMeasureTasks.Add(hazardousSubstanceRiskAssessmentFurtherControlMeasureTask);

            _hsRiskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(_queryableWrapper.Object);

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

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id = 111,
                                        HazardousSubstanceRiskAssessment = riskAssessement,
                                        TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                    });

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id= 222,
                                        HazardousSubstanceRiskAssessment = riskAssessement, 
                                        TaskCompletionDueDate = DateTime.Now.AddDays(-5)
                                    });

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id =333,
                                        HazardousSubstanceRiskAssessment = riskAssessement,
                                        TaskCompletionDueDate = DateTime.Now
                                    });

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id = 444,
                                        HazardousSubstanceRiskAssessment = riskAssessement,
                                        TaskCompletionDueDate = DateTime.Now.AddDays(-10)
                                    });

            _hsRiskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(111));
            Assert.That(result[1].Id, Is.EqualTo(222));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_asignee_and_notification_frequency_is_set_to_weekly_then_return_tasks_since_previous_week()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int)System.DayOfWeek.Wednesday };
            
            var riskAssessement = new HazardousSubstanceRiskAssessment() { };

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id = 111,
                                        TaskAssignedTo = employee,
                                        HazardousSubstanceRiskAssessment = riskAssessement,
                                        TaskCompletionDueDate = DateTime.Now.AddDays(-1)
                                    });

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id = 222,
                                        TaskAssignedTo = employee,
                                        HazardousSubstanceRiskAssessment = riskAssessement,
                                        TaskCompletionDueDate = DateTime.Now.AddDays(-5)
                                    });

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id = 333,
                                        TaskAssignedTo = employee,
                                        HazardousSubstanceRiskAssessment = riskAssessement,
                                        TaskCompletionDueDate = DateTime.Now
                                    });

            riskAssessement.FurtherControlMeasureTasks.Add(
                                    new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                                    {
                                        Id = 444,
                                        TaskAssignedTo = employee,
                                        HazardousSubstanceRiskAssessment = riskAssessement,
                                        TaskCompletionDueDate = DateTime.Now.AddDays(-10)
                                    });

            _hsRiskAssessments.Add(riskAssessement);

            var target =
                new GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(111));
            Assert.That(result[1].Id, Is.EqualTo(222));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assignee_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly, NotificationFrequecy = 26 };
           
            var riskAssessement = new HazardousSubstanceRiskAssessment() { };

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month
                TaskStatus = TaskStatus.Outstanding,
                Id = 111,
                TaskAssignedTo = employee
            });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month,
                TaskStatus = TaskStatus.Outstanding,
                Id = 222,
                TaskAssignedTo = employee
            });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month,
                TaskStatus = TaskStatus.Outstanding,
                Id = 333,
                TaskAssignedTo = employee
            });

            _hsRiskAssessments.Add(riskAssessement);

            var target = new GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(111));
            Assert.That(result[1].Id, Is.EqualTo(222));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assessor_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly, NotificationFrequecy = 26 };
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };
            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor, Status = RiskAssessmentStatus.Live };

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month
                TaskStatus = TaskStatus.Outstanding,
                Id = 111,
              
            });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month,
                TaskStatus = TaskStatus.Outstanding,
                Id = 222,
            });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletionDueDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month,
                TaskStatus = TaskStatus.Outstanding,
                Id = 333
            });

            _hsRiskAssessments.Add(riskAssessement);

            var target = new GetOverdueHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(111));
            Assert.That(result[1].Id, Is.EqualTo(222));
        }

    }
}

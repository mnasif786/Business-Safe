using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeNotificationHelperTests
{
    [TestFixture]
    public class CanSendTaskOverdueNotificationTests
    {
        private MultiHazardRiskAssessmentFurtherControlMeasureTask CreateTask()
        {
            var riskAssessement = new GeneralRiskAssessment();
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            return new MultiHazardRiskAssessmentFurtherControlMeasureTask() { MultiHazardRiskAssessmentHazard = hazard, SendTaskOverdueNotification = true };
        }

        [Test]
        public void Given_employee_is_assigned_to_task_when_CanSendTaskOverdueNotification_returns_true()
        {
            //GIVEN
            var employee = new Employee() {Id = Guid.NewGuid()};
            var task = CreateTask();
            task.TaskAssignedTo = employee;

            //WHEN
            var result = EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task,employee);

            //THEN
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_employee_is_not_assigned_to_task_when_CanSendTaskOverdueNotification_returns_false()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid() };
            var task = CreateTask();
            task.TaskAssignedTo = new Employee() {Id = Guid.NewGuid()};

            //WHEN
            var result = EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee);

            //THEN
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_employee_is_risk_assessor_and_task_send_notification_is_false_when_CanSendTaskOverdueNotification_returns_false()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid() };
            var task = CreateTask();
            task.TaskAssignedTo = new Employee() { Id = Guid.NewGuid() }; 
            task.SendTaskOverdueNotification = false;
            task.RiskAssessment.RiskAssessor = new RiskAssessor() { Employee = employee };

            //WHEN
            var result = EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee);

            //THEN
            Assert.IsFalse(result);
        }

        [Test]
        public void Given_employee_is_risk_assessor_and_task_send_notification_is_true_when_CanSendTaskOverdueNotification_returns_true()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid() };
            var riskAssessor = new RiskAssessor() {Employee = employee};
            employee.UpdateRiskAssessorDetails(true,false,false,false,null);

            var task = CreateTask();
            task.TaskAssignedTo = new Employee() { Id = Guid.NewGuid() };
            task.SendTaskOverdueNotification = true;
            task.RiskAssessment.RiskAssessor = riskAssessor;

            //WHEN
            var result = EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee);

            //THEN
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_employee_is_risk_assessor_and_task_send_notification_is_true_and_risk_assessor_wants_to_be_notified_when_CanSendTaskOverdueNotification_returns_true()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid() };
            var riskAssessor = new RiskAssessor() { Employee = employee };
            employee.UpdateRiskAssessorDetails(true, false, false, false, null);

            var task = CreateTask();
            task.TaskAssignedTo = new Employee() { Id = Guid.NewGuid() };
            task.SendTaskOverdueNotification = true;
            task.RiskAssessment.RiskAssessor = riskAssessor;

            //WHEN
            var result = EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee);

            //THEN
            Assert.IsTrue(result);
        }

        [Test]
        public void Given_employee_is_risk_assessor_and_task_send_notification_is_true_and_risk_assessor_doesnt_want_to_be_notified_when_CanSendTaskOverdueNotification_returns_false()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid() };
            var riskAssessor = new RiskAssessor() { Employee = employee };
            employee.UpdateRiskAssessorDetails(true, true, false, false, null);

            var task = CreateTask();
            task.TaskAssignedTo = new Employee() { Id = Guid.NewGuid() };
            task.SendTaskOverdueNotification = true;
            task.RiskAssessment.RiskAssessor = riskAssessor;

            //WHEN
            var result = EmployeeNotificationsHelper.CanSendTaskOverdueNotification(task, employee);

            //THEN
            Assert.IsFalse(result);
        }


    }
}

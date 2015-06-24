using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries.CompletedTaskQuery;
using BusinessSafe.Data.Queries.OverdueTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Tests.Queries.CompletedTaskQueryTests
{
    [TestFixture]
    public class GetCompletedMultiHazardFurtherControlMeasureTasksForEmployeeQueryTests
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
        public void Given_employee_is_risk_assesor_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };
            var riskAssessement = new GeneralRiskAssessment() { RiskAssessor = riskAssessor };
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);
            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
                                            {
                                                TaskAssignedTo = new Employee() { Id = Guid.NewGuid() }, 
                                                TaskCompletedDate = DateTime.Now.AddDays(-5), 
                                                TaskStatus = TaskStatus.Completed
                                            }, null);
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

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
            var riskAssessement = new GeneralRiskAssessment() { RiskAssessor = riskAssessor };
            var hazard = MultiHazardRiskAssessmentHazard.Create(riskAssessement, new Hazard(), null);


            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 123,
                TaskAssignedTo = employee,
                TaskCompletedDate = DateTime.Now,
                TaskStatus = TaskStatus.Completed
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 456,
                TaskAssignedTo = employee,
                TaskCompletedDate = DateTime.Now.AddDays(-5),
                TaskStatus = TaskStatus.Completed
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 678,
                TaskAssignedTo = employee,
                TaskCompletedDate = DateTime.Now.AddDays(-15),
                TaskStatus = TaskStatus.Completed
            }, null);
            
           
            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(123));
            Assert.That(result[1].Id, Is.EqualTo(456));
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
                Id = 123,
                TaskAssignedTo = employee,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month,
                TaskStatus = TaskStatus.Completed
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 456,
                TaskAssignedTo = employee,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //27th of last month,
                TaskStatus = TaskStatus.Completed
            }, null);

            hazard.AddFurtherActionTask(new MultiHazardRiskAssessmentFurtherControlMeasureTask()
            {
                Id = 678,
                TaskAssignedTo = employee,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //27th of last month,
                TaskStatus = TaskStatus.Completed
            }, null);


            riskAssessement.Hazards.Add(hazard);

            _gras.Add(riskAssessement);
            var target = new GetCompletedMultiHazardRiskAssessmentTasksForEmployeeQuery<GeneralRiskAssessment>(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(123));
            Assert.That(result[1].Id, Is.EqualTo(456));
        }
    }
}

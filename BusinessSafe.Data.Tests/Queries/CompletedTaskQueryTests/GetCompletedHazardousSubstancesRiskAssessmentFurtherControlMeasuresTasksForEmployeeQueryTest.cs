using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.CompletedTaskQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Data.Tests.Queries.CompletedTaskQueryTests
{
    [TestFixture]
    public class GetCompletedHazardousSubstancesRiskAssessmentFurtherControlMeasuresTasksForEmployeeQueryTest
    {
        private Mock<IQueryableWrapper<HazardousSubstanceRiskAssessment>> _queryableWrapper;
        private List<HazardousSubstanceRiskAssessment> _hsRiskAssessments;

        [SetUp]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<HazardousSubstanceRiskAssessment>>();
            _hsRiskAssessments = new List<HazardousSubstanceRiskAssessment>();

            _queryableWrapper
                .Setup(x => x.Queryable())
                .Returns(() => _hsRiskAssessments.AsQueryable());
        }


        [Test]
        [Ignore]
        public void Given_employee_is_risk_assessor_then_return_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee }; 
            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor, Status = RiskAssessmentStatus.Live};
            var hazardousSubstanceRiskAssessmentFurtherControlMeasureTask =
                new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask() 
                {   
                    HazardousSubstanceRiskAssessment = riskAssessement, 
                    TaskCompletedDate = DateTime.Now,
                    TaskStatus = TaskStatus.Completed
                };

            riskAssessement.FurtherControlMeasureTasks.Add(hazardousSubstanceRiskAssessmentFurtherControlMeasureTask);

            _hsRiskAssessments.Add(riskAssessement);

            var target = new GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(null);

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
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly, NotificationFrequecy = (int) System.DayOfWeek.Wednesday};
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };
            var riskAssessement = new HazardousSubstanceRiskAssessment() { RiskAssessor = riskAssessor, Status = RiskAssessmentStatus.Live };
            
            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
                {
                    HazardousSubstanceRiskAssessment = riskAssessement,
                    TaskCompletedDate = DateTime.Now,
                    TaskStatus = TaskStatus.Completed,
                    Title = "one"
                });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletedDate = DateTime.Now.AddDays(-10),
                TaskStatus = TaskStatus.Completed,
                Title = "two"
            });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletedDate = DateTime.Now.AddDays(-5),
                TaskStatus = TaskStatus.Completed,
                Title = "three"
            });

            _hsRiskAssessments.Add(riskAssessement);

            var target = new GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(null);

            //WHEN
            var result = target.Execute(employee.Id,null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("one"));
            Assert.That(result[1].Title, Is.EqualTo("three"));
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
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month , 27), //27th of last month
                TaskStatus = TaskStatus.Completed,
                Id = 111,
            });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month,
                TaskStatus = TaskStatus.Completed,
                Id=222,
            });

            riskAssessement.FurtherControlMeasureTasks.Add(new HazardousSubstanceRiskAssessmentFurtherControlMeasureTask()
            {
                HazardousSubstanceRiskAssessment = riskAssessement,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month,
                TaskStatus = TaskStatus.Completed,
                Id = 333
            });

            _hsRiskAssessments.Add(riskAssessement);

            var target = new GetCompletedHazardousSubstanceRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(null);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(111));
            Assert.That(result[1].Id, Is.EqualTo(222));
        }
    }
}

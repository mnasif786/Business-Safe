using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NHibernate;
using NHibernate.Engine;
using NUnit.Framework;
using BusinessSafe.Data.Queries.CompletedTaskQuery;

namespace BusinessSafe.Data.Tests.Queries.CompletedTaskQueryTests
{
    [TestFixture]
    public class GetCompletedFireRiskAssessmentFurtherControlMeasureTaskTest
    {
        private Mock<IQueryableWrapper<FireRiskAssessment>> _queryableWrapper;
        private List<FireRiskAssessment> _fireRiskAssessments;
        private Mock<ISession> _session;
        private Mock<ITransaction> _transaction;

        [SetUp]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<FireRiskAssessment>>();
            _fireRiskAssessments = new List<FireRiskAssessment>();

            _queryableWrapper.Setup(x => x.Queryable())
               .Returns(() => _fireRiskAssessments.AsQueryable());

            _session.Setup(x => x.BeginTransaction())
                .Returns(() => _transaction.Object);

            _transaction = new Mock<ITransaction>();
        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assesor_then_return_completed_task()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            
            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };
            var furthcontrolmersuretasks = new FireRiskAssessmentFurtherControlMeasureTask() { TaskStatus = TaskStatus.Completed, TaskCompletedDate = DateTime.Now.AddDays(-1)};
            
            var significantFinding = new SignificantFinding(){};
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks);
            furthcontrolmersuretasks.SignificantFinding = significantFinding;


            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            riskAssessement.FireRiskAssessmentChecklists[0].FireRiskAssessment.RiskAssessor = riskAssessor;
            
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();
            
            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            fireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor = riskAssessor;

            significantFinding.FireAnswer = fireAnswer;
          
            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() {fireAnswer};

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                new GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(null);

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
            
            var furthcontrolmersuretasks1 = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskStatus = TaskStatus.Completed,
                TaskCompletedDate = DateTime.Now,
                Title = "one"
            };

            var furthcontrolmersuretasks2 = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskStatus = TaskStatus.Completed,
                TaskCompletedDate = DateTime.Now.AddDays(-5),
                Title = "two"
            };

            var furthcontrolmersuretasks3 = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskStatus = TaskStatus.Completed,
                TaskCompletedDate = DateTime.Now.AddDays(-10),
                Title = "three"
            };

            var significantFinding = new SignificantFinding() { };

            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks1);
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks2);
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks3);

            furthcontrolmersuretasks1.SignificantFinding = significantFinding;
            furthcontrolmersuretasks2.SignificantFinding = significantFinding;
            furthcontrolmersuretasks3.SignificantFinding = significantFinding;


            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            riskAssessement.FireRiskAssessmentChecklists[0].FireRiskAssessment.RiskAssessor = riskAssessor;

            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();

            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            fireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor = riskAssessor;

            significantFinding.FireAnswer = fireAnswer;

            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() { fireAnswer };

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                new GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                    null);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("one"));
            Assert.That(result[1].Title, Is.EqualTo("two"));
        }

        [Test]
        [Ignore]
        public void Given_employee_is_risk_assessor_and_notification_frequency_is_set_to_monthly_on_26th_then_return_tasks_since_26th_of_previous_month()
        {
            //GIVEN
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly, NotificationFrequecy = 26 };

            var riskAssessor = new RiskAssessor() { Id = 5596870, Employee = employee };

            var furthcontrolmersuretasks1 = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskStatus = TaskStatus.Completed,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 27), //27th of last month,
                Title = "one"
            };

            var furthcontrolmersuretasks2 = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskStatus = TaskStatus.Completed,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 28), //28th of last month,
                Title = "two"
            };

            var furthcontrolmersuretasks3 = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskStatus = TaskStatus.Completed,
                TaskCompletedDate = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 23), //23th of last month,
                Title = "three"
            };

            var significantFinding = new SignificantFinding() { };

            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks1);
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks2);
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks3);

            furthcontrolmersuretasks1.SignificantFinding = significantFinding;
            furthcontrolmersuretasks2.SignificantFinding = significantFinding;
            furthcontrolmersuretasks3.SignificantFinding = significantFinding;


            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            riskAssessement.FireRiskAssessmentChecklists[0].FireRiskAssessment.RiskAssessor = riskAssessor;

            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();

            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            fireAnswer.FireRiskAssessmentChecklist.FireRiskAssessment.RiskAssessor = riskAssessor;

            significantFinding.FireAnswer = fireAnswer;

            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() { fireAnswer };

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                new GetCompletedFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                    null);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Title, Is.EqualTo("one"));
            Assert.That(result[1].Title, Is.EqualTo("two"));
        }
    }
}

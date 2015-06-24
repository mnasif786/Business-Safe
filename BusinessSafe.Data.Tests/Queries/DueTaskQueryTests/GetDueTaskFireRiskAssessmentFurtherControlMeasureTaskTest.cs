using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NHibernate.Engine;
using NUnit.Framework;
using BusinessSafe.Data.Queries.DueTaskQuery;

namespace BusinessSafe.Data.Tests.Queries.DueTomorrowTaskQueryTests
{
    [TestFixture]
    public class GetDueTaskFireRiskAssessmentFurtherControlMeasureTaskTest
    {
        private Mock<IQueryableWrapper<FireRiskAssessment>> _queryableWrapper;
        private List<FireRiskAssessment> _fireRiskAssessments;

        [SetUp]
        public void Setup()
        {
            _queryableWrapper = new Mock<IQueryableWrapper<FireRiskAssessment>>();
            _fireRiskAssessments = new List<FireRiskAssessment>();

            _queryableWrapper.Setup(x => x.Queryable())
               .Returns(() => _fireRiskAssessments.AsQueryable());
        }

        [Test]
        [Ignore]
        public void Given_employee_is_assignee_then_return_due_tomorrow_task()
        {
            //GIVEN
           
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily};
            var furthcontrolmersuretasks = new FireRiskAssessmentFurtherControlMeasureTask() { TaskAssignedTo = employee, TaskStatus = TaskStatus.Outstanding, TaskCompletionDueDate = DateTime.Now.AddDays(1)};
            var significantFinding = new SignificantFinding(){};
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks);
            furthcontrolmersuretasks.SignificantFinding = significantFinding;
            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });
            
            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();
            
            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            significantFinding.FireAnswer = fireAnswer;
          
            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() {fireAnswer};

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                new GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));

        }

        [Test]
        [Ignore]
        public void Given_employee_is_assignee_then__dont_return__task_due_in_2days()
        {
            //GIVEN

            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Daily };
            var furthcontrolmersuretasks = new FireRiskAssessmentFurtherControlMeasureTask() { TaskAssignedTo = employee, TaskStatus = TaskStatus.Outstanding, TaskCompletionDueDate = DateTime.Now.AddDays(2) };
            var significantFinding = new SignificantFinding() { };
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmersuretasks);
            furthcontrolmersuretasks.SignificantFinding = significantFinding;
            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();

            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            significantFinding.FireAnswer = fireAnswer;

            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() { fireAnswer };

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                new GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                    _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));

        }

        [Test]
        [Ignore]
        public void Given_notification_is_weekly_and_taskDueDate_more_than_week_away_then_dont_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly };

            var furthcontrolmeasuretasks = new FireRiskAssessmentFurtherControlMeasureTask()
                                            {   TaskAssignedTo = employee, 
                                                TaskStatus = TaskStatus.Outstanding, 
                                                TaskCompletionDueDate = DateTime.Now.AddDays(10) 
                                            };

            var significantFinding = new SignificantFinding() { };
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmeasuretasks);

            furthcontrolmeasuretasks.SignificantFinding = significantFinding;

            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();

            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            significantFinding.FireAnswer = fireAnswer;

            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() { fireAnswer };

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                 new GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                     _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_is_weekly_and_taskDueDate_less_than_week_away_then_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Weekly };

            var furthcontrolmeasuretasks = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(3)
            };

            var significantFinding = new SignificantFinding() { };
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmeasuretasks);

            furthcontrolmeasuretasks.SignificantFinding = significantFinding;

            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();

            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            significantFinding.FireAnswer = fireAnswer;

            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() { fireAnswer };

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                 new GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                     _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }


        [Test]
        [Ignore]
        public void Given_notification_is_monthly_and_taskDueDate_more_than_month_away_then_dont_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };

            var furthcontrolmeasuretasks = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(35)
            };

            var significantFinding = new SignificantFinding() { };
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmeasuretasks);

            furthcontrolmeasuretasks.SignificantFinding = significantFinding;

            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();

            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            significantFinding.FireAnswer = fireAnswer;

            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() { fireAnswer };

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                 new GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                     _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        [Ignore]
        public void Given_notification_is_monthly_and_taskDueDate_less_than_month_away_then_return_task()
        {
            var employee = new Employee() { Id = Guid.NewGuid(), NotificationType = NotificationType.Monthly };

            var furthcontrolmeasuretasks = new FireRiskAssessmentFurtherControlMeasureTask()
            {
                TaskAssignedTo = employee,
                TaskStatus = TaskStatus.Outstanding,
                TaskCompletionDueDate = DateTime.Now.AddDays(25)
            };

            var significantFinding = new SignificantFinding() { };
            significantFinding.FurtherControlMeasureTasks.Add(furthcontrolmeasuretasks);

            furthcontrolmeasuretasks.SignificantFinding = significantFinding;

            var riskAssessement = FireRiskAssessment.Create("this is the title", "the ref", 1312, null, new UserForAuditing { Id = Guid.NewGuid() });

            var user = new UserForAuditing();
            var fireRiskAssessmentChecklist = riskAssessement.FireRiskAssessmentChecklists[0];
            var question = new Question();

            var fireAnswer = FireAnswer.Create(fireRiskAssessmentChecklist, question, YesNoNotApplicableEnum.No, "Additional Info", user);
            fireAnswer.SignificantFinding = significantFinding;
            significantFinding.FireAnswer = fireAnswer;

            riskAssessement.FireRiskAssessmentChecklists[0].Answers = new List<FireAnswer>() { fireAnswer };

            _fireRiskAssessments.Add(riskAssessement);

            var target =
                 new GetDueFireRiskAssessmentFurtherControlMeasureTasksForEmployeeQuery(
                     _queryableWrapper.Object);

            //WHEN
            var result = target.Execute(employee.Id, null);

            //THEN
            Assert.That(result.Count, Is.EqualTo(1));
        }






    }
}

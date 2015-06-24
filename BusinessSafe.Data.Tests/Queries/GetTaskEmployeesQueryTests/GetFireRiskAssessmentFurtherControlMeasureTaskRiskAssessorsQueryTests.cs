using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Data.Queries.GetTaskEmployeesQuery;
using BusinessSafe.Data.Queries.SafeCheck;
using BusinessSafe.Domain.Entities;
using Moq;
using NHibernate;
using NUnit.Framework;
using Peninsula.Online.Data.NHibernate.ApplicationServices;

namespace BusinessSafe.Data.Tests.Queries.GetTaskEmployeesQueryTests
{
    [TestFixture]
    class GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQueryTests
    {
        private Mock<IBusinessSafeSessionManager> _sessionManager;
        private Mock<ISession> _session;
        private Mock<IQueryableWrapper<FireRiskAssessmentFurtherControlMeasureTask>> _queryableWrapper;
        private List<FireRiskAssessmentFurtherControlMeasureTask> _fraTasks;

        [SetUp]
        public void Setup()
        {
            //_session = new Mock<ISession>();
            //_sessionManager = new Mock<IBusinessSafeSessionManager>();

            //_sessionManager.Setup(x => x.Session)
            //    .Returns(() => _session.Object);

            _queryableWrapper = new Mock<IQueryableWrapper<FireRiskAssessmentFurtherControlMeasureTask>>();

            _fraTasks = new List<FireRiskAssessmentFurtherControlMeasureTask>();

            _queryableWrapper.Setup(x => x.Queryable())
               .Returns(() => _fraTasks.AsQueryable());

        }

        private static RiskAssessor CreateRiskAssessor()
        {
            var employee = new Employee() {Id = Guid.NewGuid()};
            employee.SetEmail("test@test.com", null);

            return new RiskAssessor() {Id = 123123, Employee = employee};
        }

        private static FireRiskAssessmentFurtherControlMeasureTask CreateFireRiskAssessmentFurtherControlMeasureTask()
        {
            var fireRiskAssessment = new FireRiskAssessment();
            var fireRiskAssessmentChecklist = new FireRiskAssessmentChecklist();
            fireRiskAssessmentChecklist.FireRiskAssessment = fireRiskAssessment;

            var fireAnswer = new FireAnswer();
            fireAnswer.FireRiskAssessmentChecklist = fireRiskAssessmentChecklist;
            fireRiskAssessmentChecklist.Answers.Add(fireAnswer);

            var significantFinding = SignificantFinding.Create(fireAnswer, null);
            var fraTask = new FireRiskAssessmentFurtherControlMeasureTask();
            fraTask.SignificantFinding = significantFinding;
            return fraTask;
        }

        [Test]
        public void Given_FRA_Task_then_returns_risk_assessor_employee()
        {
            //GIVEN
            var fraTask = CreateFireRiskAssessmentFurtherControlMeasureTask();
            fraTask.RiskAssessment.RiskAssessor = CreateRiskAssessor();
            _fraTasks.Add(fraTask);
            
            var target = new GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute();

            //THEN
            Assert.That(result.Count,Is.EqualTo(1));
        }

        [Test]
        public void Given_FRA_Task_and_risk_assessor_doesnt_have_email_then_returns_empty_list()
        {
            //GIVEN
            var riskAssessor = CreateRiskAssessor();
            riskAssessor.Employee.SetEmail("",null);
            var fraTask = CreateFireRiskAssessmentFurtherControlMeasureTask();
            fraTask.RiskAssessment.RiskAssessor = riskAssessor;
            _fraTasks.Add(fraTask);
            
            var target = new GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute();

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_FRA_Task_and_risk_assessment_deleted_then_returns_empty_list()
        {
            //GIVEN
            var fraTask = CreateFireRiskAssessmentFurtherControlMeasureTask();
            fraTask.RiskAssessment.RiskAssessor = CreateRiskAssessor();
            fraTask.RiskAssessment.Deleted = true;
            _fraTasks.Add(fraTask);

            var target = new GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute();

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_deleted_FRA_Task_then_returns_empty_list()
        {
            //GIVEN
            var fraTask = CreateFireRiskAssessmentFurtherControlMeasureTask();
            fraTask.RiskAssessment.RiskAssessor = CreateRiskAssessor();
            fraTask.Deleted = true;
            _fraTasks.Add(fraTask);

            var target = new GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute();

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void Given_FRA_Task_doesnt_have_a_risk_assessor_then_returns_empty_list()
        {
            //GIVEN
            var fraTask = CreateFireRiskAssessmentFurtherControlMeasureTask();
            fraTask.RiskAssessment.RiskAssessor = null;
            _fraTasks.Add(fraTask);

            var target = new GetFireRiskAssessmentFurtherControlMeasureTaskRiskAssessorsQuery(_queryableWrapper.Object);

            //WHEN
            var result = target.Execute();

            //THEN
            Assert.That(result.Count, Is.EqualTo(0));
        }

       


    }
}

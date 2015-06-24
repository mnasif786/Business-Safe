using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Application.Implementations.Employees;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.InfrastructureContracts.Logging;
using BusinessSafe.Domain.RepositoryContracts;
using Moq;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.EmployeeServicesTests
{
    [TestFixture]
    public class GetEmployeeTests
    {
        private Mock<IEmployeeRepository> _employeeRepository;
        private Mock<IPeninsulaLog> _log;
        private RiskAssessor _riskAssessor = null;
        private Employee _employee = null;

        [SetUp]
        public void SetUp()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
            _log = new Mock<IPeninsulaLog>();
            _riskAssessor = new RiskAssessor() {Deleted = false};
            _employee = new Employee();

            _employeeRepository.Setup(x => x.GetByIdAndCompanyId(It.IsAny<Guid>(), It.IsAny<long>()))
                .Returns(() => _employee);

        }

        private EmployeeService GetTarget()
        {
            return new EmployeeService(_employeeRepository.Object, null, null, null, null, null, _log.Object, null, null, null);
        }

        [Test]
        public void given_employee_is_a_risk_assessor_when_getEmployee_then_RiskAssessor_is_not_null()
        {
            //given
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor,Is.Not.Null);
        }

        [Test]
        public void given_employee_is_a_deleted_risk_assessor_when_getEmployee_then_RiskAssessor_is_null()
        {
            //given
            _riskAssessor.Deleted = true;
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor, Is.Null);
        }

        [Test]
        public void given_employee_is_a_risk_assessor_with_DoNotSendTaskOverdueNotifications_when_getEmployee_then_DoNotSendTaskOverdueNotifications_are_true()
        {
            //given
            _riskAssessor.DoNotSendTaskOverdueNotifications = true;
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor.DoNotSendTaskOverdueNotifications, Is.True);
            Assert.That(result.RiskAssessor.DoNotSendTaskCompletedNotifications, Is.False);
            Assert.That(result.RiskAssessor.DoNotSendReviewDueNotification, Is.False);
        }

        [Test]
        public void given_employee_is_a_risk_assessor_with_DoNotSendTaskCompletedNotifications_when_getEmployee_then_DoNotSendTaskCompletedNotifications_are_true()
        {
            //given
            _riskAssessor.DoNotSendTaskCompletedNotifications = true;
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor.DoNotSendTaskOverdueNotifications, Is.False);
            Assert.That(result.RiskAssessor.DoNotSendTaskCompletedNotifications, Is.True);
            Assert.That(result.RiskAssessor.DoNotSendReviewDueNotification, Is.False);
        }

        [Test]
        public void given_employee_is_a_risk_assessor_with_DoNotSendReviewDueNotification_when_getEmployee_then_DoNotSendReviewDueNotification_are_true()
        {
            //given
            _riskAssessor.DoNotSendReviewDueNotification = true;
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor.DoNotSendTaskOverdueNotifications, Is.False);
            Assert.That(result.RiskAssessor.DoNotSendTaskCompletedNotifications, Is.False);
            Assert.That(result.RiskAssessor.DoNotSendReviewDueNotification, Is.True);
        }

        [Test]
        public void given_employee_is_a_risk_assessor_with_site_when_getEmployee_then_site_details_are_returned()
        {
            //given
            _riskAssessor.Site = new Site(){Id = 4324895, Name = "Harrenhall"};
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor.Site.Id, Is.EqualTo(_riskAssessor.Site.Id));
            Assert.That(result.RiskAssessor.Site.Name, Is.EqualTo(_riskAssessor.Site.Name));
        }

        [Test]
        public void given_employee_is_a_risk_assessor_without_a_site_when_getEmployee_then_site_details_are_null()
        {
            //given
            _riskAssessor.Site = null;
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor.Site, Is.Null);
            
        }

        [Test]
        public void given_employee_is_a_risk_assessor_with_access_to_all_sites_when_getEmployee_then_HasAccessToAllSites_equals_true()
        {
            //given
            _riskAssessor.Site = null;
            _riskAssessor.HasAccessToAllSites = true;
            _employee.RiskAssessors.Add(_riskAssessor);

            var target = GetTarget();

            //when
            var result = target.GetEmployee(Guid.NewGuid(), 13);

            //then
            Assert.That(result.RiskAssessor.HasAccessToAllSites, Is.True);

        }
    }


}

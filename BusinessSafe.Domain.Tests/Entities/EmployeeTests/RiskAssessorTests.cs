using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeTests
{
    [TestFixture]
    public class RiskAssessorTests
    {

        [Test]
        public void given_employee_isnt_a_risk_assessor_when_get_RiskAssessor_then_returns_null()
        {
            //GIVEN
            var employee = new Employee();

            //WHEN
            var result = employee.RiskAssessor;

            //THEN
            Assert.IsNull(result);
        }

        [Test]
        public void given_employee_is_a_risk_assessor_when_get_RiskAssessor_then_returns_not_null()
        {
            //GIVEN
            var employee = new Employee();
            employee.RiskAssessors.Add(new RiskAssessor());

            //WHEN
            var result = employee.RiskAssessor;

            //THEN
            Assert.IsNotNull(result);
        }

        [Test]
        public void given_employee_is_a_deleted_risk_assessor_when_get_RiskAssessor_then_returns_null()
        {
            //GIVEN
            var employee = new Employee();
            employee.RiskAssessors.Add(new RiskAssessor(){Deleted = true} );

            //WHEN
            var result = employee.RiskAssessor;

            //THEN
            Assert.IsNull(result);
        }

        [Test]
        public void given_employee_is_becoming_a_risk_assessor_when_set_RiskAssessor_then_get_RiskAssessor_is_not_null()
        {
            //GIVEN
            var employee = new Mock<Employee>() { CallBase = true };

            //WHEN
            employee.Object.UpdateRiskAssessorDetails(true,false,false,false,null);
            var result = employee.Object.RiskAssessor;

            //THEN
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
        }
        
        [Test]
        public void given_employee_is_already_a_risk_assessor_when_update_RiskAssessor_then_get_RiskAssessor_is_not_null()
        {
            //GIVEN
            var employee = new Employee();
            var riskAssessor = Domain.Entities.RiskAssessor.Create(employee, null, true, true, true, false, null);
            employee.RiskAssessors.Add(riskAssessor);

            //WHEN
            var result = employee.RiskAssessor;

            //THEN
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(riskAssessor));
        }
    }
}
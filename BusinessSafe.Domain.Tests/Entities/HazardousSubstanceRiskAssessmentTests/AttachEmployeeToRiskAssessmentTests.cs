using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AttachEmployeeToRiskAssessmentTests
    {
        [Test]
        public void Given_employee_Then_should_add_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);

            var employee = new Employee {Id = Guid.NewGuid()};

            //When
            target.AttachEmployeeToRiskAssessment(employee, user);

            //Then
            Assert.That(target.Employees.Count, Is.EqualTo(1));
            Assert.That(target.LastModifiedBy,Is.EqualTo(user));
        }

        [Test]
        public void Given_employee_already_exists_Then_should_not_add_employee()
        {
            //Given
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            var user = new UserForAuditing();
            var employee = new Employee { Id = Guid.NewGuid() };

            target.AttachEmployeeToRiskAssessment(employee, user);

            //When
            target.AttachEmployeeToRiskAssessment(employee, user);

            //Then
            Assert.That(target.Employees.Count, Is.EqualTo(1));
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class DetachEmployeeFromRiskAssessmentTests
    {
        [Test]
        public void Given_employee_Then_should_detach_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);

            var employee1 = new Employee {Id = Guid.NewGuid()};
            var employee2 = new Employee { Id = Guid.NewGuid() };
            var employee3 = new Employee { Id = Guid.NewGuid() };

            target.AttachEmployeeToRiskAssessment(employee1,user);
            target.AttachEmployeeToRiskAssessment(employee2,user);
            target.AttachEmployeeToRiskAssessment(employee3, user);

            //When
            target.DetachEmployeesFromRiskAssessment(new List<Employee>{ employee1, employee2 }, user);

            //Then
            Assert.That(target.Employees.Count(x => !x.Deleted), Is.EqualTo(1));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
            
        }

        [Test]
        public void Given_employee_does_not_exists_Then_should_list_should_remain_unchanged()
        {
            //Given
            var user = new UserForAuditing();
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            target.Employees.Add(new RiskAssessmentEmployee() { Employee = new Employee() { Id = Guid.NewGuid() } });

            //When
            target.DetachEmployeesFromRiskAssessment(new List<Employee> { new Employee() { Id = Guid.NewGuid() } }, user);
            
            //Then
            Assert.That(target.Employees.Count, Is.EqualTo(1));
        }
    }
}
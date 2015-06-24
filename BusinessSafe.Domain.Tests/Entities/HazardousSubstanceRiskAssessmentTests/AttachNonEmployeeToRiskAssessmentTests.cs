using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AttachNonEmployeeToRiskAssessmentTest
    {
        [Test]
        public void Given_non_employee_Then_should_add_correctly()
        {
            //Given
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            var nonEmployee = new NonEmployee();
            var user = new UserForAuditing();

            //When
            target.AttachNonEmployeeToRiskAssessment(nonEmployee, user);

            //Then
            Assert.That(target.NonEmployees.Count, Is.EqualTo(1));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_non_employee_already_exists_Then_should_not_add_non_employee_to_risk_assessment()
        {
            //Given
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            var nonEmployee = new NonEmployee();
            var user = new UserForAuditing();

            target.AttachNonEmployeeToRiskAssessment(nonEmployee, user);

            //When
            target.AttachNonEmployeeToRiskAssessment(nonEmployee, user);

            //Then
            Assert.That(target.NonEmployees.Count, Is.EqualTo(1));
        }
    }
}

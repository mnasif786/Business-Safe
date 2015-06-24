using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class DetachNonEmployeeToRiskAssessmentTests
    {
        [Test]
        public void Given_non_employee_Then_should_detach_correctly()
        {
            //Given
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            var nonEmployee = new NonEmployee();
            var user = new UserForAuditing();
            
            target.AttachNonEmployeeToRiskAssessment(nonEmployee, user);

            //When
            target.DetachNonEmployeeFromRiskAssessment(nonEmployee, user);

            //Then
            Assert.That(target.NonEmployees.Count(x => !x.Deleted), Is.EqualTo(0));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_non_employee_does_not_exists_Then_should_get_correct_exception()
        {
            //Given
            var target = Domain.Entities.HazardousSubstanceRiskAssessment.Create("", "", default(long), null, null);
            var nonEmployee = new NonEmployee();
            var user = new UserForAuditing();

            //When
            //Then
            Assert.Throws<NonEmployeeNotAttachedToRiskAssessmentException>(() => target.DetachNonEmployeeFromRiskAssessment(nonEmployee, user));
        }
    }
}
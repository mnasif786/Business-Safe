using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeTests
{
    [TestFixture]
    public class MarkForDeleteTests
    {
        [Test]
        public void Given_employee_without_user_When_MarkForDelete_Then_should_mark_for_delete_as_expected()
        {
            // Given
            var employee = new Employee();
            var userForAuditing = new UserForAuditing();

            // When
            employee.MarkForDelete(userForAuditing);

            // Then
            Assert.That(employee.Deleted, Is.True);
            Assert.That(employee.LastModifiedBy, Is.EqualTo(userForAuditing));
            Assert.That(employee.User, Is.Null);
        }

        [Test]
        public void Given_employee_with_user_When_MarkForDelete_Then_should_mark_for_delete_as_expected()
        {
            // Given
            var employee = new Employee
                               {
                                   User = new User() { Id = Guid.NewGuid() }
                               };
            var userForAuditing = new UserForAuditing() { Id = Guid.NewGuid()};

            // When
            employee.MarkForDelete(userForAuditing);

            // Then
            Assert.That(employee.Deleted, Is.True);
            Assert.That(employee.LastModifiedBy, Is.EqualTo(userForAuditing));
            Assert.That(employee.User.Deleted, Is.True);
            Assert.That(employee.User.LastModifiedBy, Is.EqualTo(userForAuditing));
        }
    }
}
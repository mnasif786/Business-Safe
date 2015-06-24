using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.UserTests
{
    [TestFixture]
    [Category("Unit")]
    public class ReinstateFromDeleteTests
    {
        [Test]
        public void When_Delete_Then_should_set_correct_properties()
        {
            // Given
            var user = new User { Id = Guid.NewGuid() };
            var userDeleting = new UserForAuditing();

            user.Delete(userDeleting);

            // When
            user.ReinstateFromDelete(userDeleting);

            // Then
            Assert.That(user.Deleted, Is.False);
            Assert.That(user.LastModifiedBy, Is.EqualTo(userDeleting));
            Assert.That(user.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        }

        [Test]
        public void Given_user_and_employee_deleted_When_ReinstateFromDelete_Then_employee_should_be_restored()
        {
            // Given
            var user = new User
            {
                Id = Guid.NewGuid()
                ,
                Employee = new Employee() { Deleted = true }
                ,
                Deleted = true
            };
            var userDeleting = new UserForAuditing();

            // When
            user.ReinstateFromDelete(userDeleting);

            // Then
            Assert.That(user.Employee.Deleted, Is.False);
            Assert.That(user.Employee.LastModifiedBy, Is.EqualTo(userDeleting));
            Assert.That(user.Employee.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Now.ToShortDateString()));
        }
    }
}
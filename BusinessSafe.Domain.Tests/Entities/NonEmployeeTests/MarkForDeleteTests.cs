using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.NonEmployeeTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {
        [Test]
        public void Given_nonemployee_When_mark_for_delete_Then_nonemployee_properties_should_be_correct()
        {
            //Given
            var nonEmployee = new NonEmployee();
            var user = new UserForAuditing();

            //When
            nonEmployee.MarkForDelete(user);

            //Then
            Assert.That(nonEmployee.Deleted, Is.True);
            Assert.That(nonEmployee.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(nonEmployee.LastModifiedBy, Is.EqualTo(user));
        }

    }
}

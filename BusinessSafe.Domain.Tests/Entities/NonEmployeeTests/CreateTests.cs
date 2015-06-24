using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.NonEmployeeTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_creating_nonemployee_When_create_Then_nonemployee_properties_should_be_correct()
        {
            //Given
            var user = new UserForAuditing();
            
            //When
            var nonEmployee = NonEmployee.Create("name", "position", "companyname", 1, user);

            //Then
            Assert.That(nonEmployee.CreatedBy, Is.EqualTo(user));
            Assert.That(nonEmployee.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));

        }

    }
}
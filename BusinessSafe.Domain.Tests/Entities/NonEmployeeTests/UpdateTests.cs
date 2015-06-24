using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.NonEmployeeTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        [Test]
        public void Given_nonemployee_When_update_Then_nonemployee_properties_should_be_correct()
        {
            //Given
            var nonEmployee = NonEmployee.Create("name","position","companyname", 1, new UserForAuditing());
            var user = new UserForAuditing();

            //When
            nonEmployee.Update("newname", "newposition", "newcompanyname", user);

            //Then
            Assert.That(nonEmployee.Name, Is.EqualTo("newname"));
            Assert.That(nonEmployee.Company, Is.EqualTo("newcompanyname")); 
            Assert.That(nonEmployee.Position, Is.EqualTo("newposition"));
            Assert.That(nonEmployee.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(nonEmployee.LastModifiedBy, Is.EqualTo(user));
        }
    }
}
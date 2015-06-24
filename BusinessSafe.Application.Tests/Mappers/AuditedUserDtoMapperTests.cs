using System;
using BusinessSafe.Application.Mappers;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Mappers
{
    [TestFixture]
    public class AuditedUserDtoMapperTests
    {

        [Test]
        public void Given_a_user_when_mapped_to_AuditedUserDto_then_correct_properties_are_set()
        {
            //Given
            var user = new UserForAuditing
                           {
                               Id = Guid.NewGuid(),
                               Employee =
                                   new EmployeeForAuditing() {Id = Guid.NewGuid(), Forename = "testf", Surname = "testS"}
                           };
            var target = new AuditedUserDtoMapper();

            //When
            var result = target.Map(user);

            //Act
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.Employee.FullName,  result.Name);
        }
    }
}
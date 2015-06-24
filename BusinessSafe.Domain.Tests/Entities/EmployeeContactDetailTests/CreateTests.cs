using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.EmployeeContactDetailTests
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void Given_an_invalid_formatted_email_address_when_Create_Then_Validation_exception_is_thrown()
        {
            var ex = Assert.Throws<InvalidAddUpdateEmployeeContactDetailsParameters>(() => EmployeeContactDetail.Create(new AddUpdateEmployeeContactDetailsParameters {Email = "test"}, new UserForAuditing(), new Employee()));

            Assert.IsTrue(ex.Errors.ToList().Exists(err=> err.PropertyName == "Email" && err.ErrorMessage == "Invalid email"));
        }
    }
}

using System.ComponentModel.DataAnnotations;
using BusinessSafe.Application.Tests.Request.Attributes.OneDropDownSelectedTests;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Request.Attributes.AtLeastOneDropDownSelectedMainSite
{
    [TestFixture]
    public class IsValidTests
    {
        [Test]
        public void Given_that_none_of_the_values_are_selected_Then_validation_return_error()
        {
            //Given
            var target = new Application.Request.Attributes.AtLeastOneDropDownSelectedMainSite("LinkToSiteId");
            var otherObject = new TestClass { LinkToGroupId = null };
            var testContext = new ValidationContext(otherObject, null, null);

            //When
            TestDelegate exceptionResult = () => target.Validate(null, testContext);

            //Then
            var ex = Assert.Throws<ValidationException>(exceptionResult);
            Assert.That(ex.Message, Is.EqualTo("At Least One Should Be Selected"));
        }

        [Test]
        public void Given_that_one_of_the_values_are_selected_Then_validation_return_no_error()
        {
            //Given
            var target = new Application.Request.Attributes.AtLeastOneDropDownSelectedMainSite("LinkToGroupId");
            var otherObject = new TestClass { LinkToGroupId = 10 };
            var testContext = new ValidationContext(otherObject, null, null);

            //When
            TestDelegate exceptionResult = () => target.Validate(null, testContext);

            //Then
            Assert.DoesNotThrow(exceptionResult);
        }

        [Test]
        public void Given_that_main_site_is_selected_Then_validation_return_no_error()
        {
            //Given
            var target = new Application.Request.Attributes.AtLeastOneDropDownSelectedMainSite("LinkToGroupId-IsMainSite");
            var otherObject = new TestClass { LinkToGroupId = null, IsMainSite = true};
            var testContext = new ValidationContext(otherObject, null, null);

            //When
            TestDelegate exceptionResult = () => target.Validate(null, testContext);

            //Then
            Assert.DoesNotThrow(exceptionResult);
        }

        [Test]
        public void Given_that_normal_site_is_selected_Then_validation_return_error()
        {
            //Given
            var target = new Application.Request.Attributes.AtLeastOneDropDownSelectedMainSite("LinkToGroupId-IsMainSite");
            var otherObject = new TestClass { LinkToGroupId = null, IsMainSite = false };
            var testContext = new ValidationContext(otherObject, null, null);

            //When
            TestDelegate exceptionResult = () => target.Validate(null, testContext);

            //Then
            var ex = Assert.Throws<ValidationException>(exceptionResult);
            Assert.That(ex.Message, Is.EqualTo("At Least One Should Be Selected"));
        }
    }
}

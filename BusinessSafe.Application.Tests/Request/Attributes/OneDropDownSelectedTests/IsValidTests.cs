using System.ComponentModel.DataAnnotations;
using BusinessSafe.Application.Request.Attributes;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Request.Attributes.OneDropDownSelectedTests
{
    [TestFixture]
    [Category("Unit")]
    public class IsValidTests
    {
        [Test]
        public void Given_that_two_values_are_selected_Then_Validation_should_return_error()
        {
            //Given
            var target = new OneDropDownSelected("LinkToSiteId");            
            var otherObject = new TestClass {LinkToSiteId = 10};            
            var testContext = new ValidationContext(otherObject, null, null);

            //When
            TestDelegate exceptionResult = () => target.Validate(30, testContext);           

            //Then 
            var exception = Assert.Throws<ValidationException>(exceptionResult);
            Assert.That(exception.Message, Is.EqualTo("Only One Option Is Allowed"));            
        }

        [Test]
        public void Given_that_three_values_are_selected_Then_Validation_should_not_return_error()
        {
            //Given
            var target = new OneDropDownSelected("LinkToSiteId,LinkToGroupId");
            var otherObject = new TestClass { LinkToSiteId = 10, LinkToGroupId = 20};
            var testContext = new ValidationContext(otherObject, null, null);

            //When
            TestDelegate exceptionResult = () => target.Validate(null, testContext);

            //Then
            Assert.DoesNotThrow(exceptionResult);
        }

        [Test]
        public void Given_that_two_values_are_selected_Then_Validation_should_return_error2()
        {
            //Given
            var target = new OneDropDownSelected("LinkToSiteId,LinkToGroupId");
            var otherObject = new TestClass { LinkToSiteId = null, LinkToGroupId = null};
            var testContext = new ValidationContext(otherObject, null, null);

            //When            
            TestDelegate exceptionResult = () => target.Validate(10, testContext);
            
            //Then
            Assert.DoesNotThrow(exceptionResult);
        }
    }

    public class TestClass
    {
        public long? LinkToSiteId { get; set; }
        public long? LinkToGroupId { get; set; }
        public long? AnotherId { get; set; }
        public bool IsMainSite { get; set; }
    }
}

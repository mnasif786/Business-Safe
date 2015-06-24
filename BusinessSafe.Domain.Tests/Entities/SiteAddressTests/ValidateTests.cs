using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using FluentValidation;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.SiteAddressTests
{
    [TestFixture]
    [Category("Unit")]
    public class ValidateTests
    {
        [Test]
        public void Given_that_name_is_duplicated_Then_error_message_is_returned()
        {
            //Given            
            const string duplicateName = "Name";
            var target = Site.Create(default(long), null, default(long), duplicateName, default(string), "", new UserForAuditing());
            var sites = new List<Site> { Site.Create(default(long), null, default(long), duplicateName, default(string), "", new UserForAuditing()) };

            //When
            TestDelegate testDelegate = () => target.Validate(sites);

            //Then
            var exception = Assert.Throws<ValidationException>(testDelegate);
            Assert.That(exception.Message.Contains("Name Already Exists"), Is.True);
        }

        [Test]
        public void Given_that_name_is_duplicated_Then_error_message_is_returned2()
        {
            //Given            
            const string duplicateName = "Name";
            var target = Site.Create(default(long), null, default(long), duplicateName, default(string), "", new UserForAuditing());
            var sites = new List<Site> { Site.Create(default(long), null, default(long), duplicateName, default(string), "", new UserForAuditing()) };

            //When
            TestDelegate testDelegate = () => target.Validate(sites);

            //Then
            var exception = Assert.Throws<ValidationException>(testDelegate);
            Assert.That(exception.Message.Contains("Name Already Exists"), Is.True);
        }
    }
}

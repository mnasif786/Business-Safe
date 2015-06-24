using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessSafe.WebSite.CustomValidators;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.CustomValidators
{
    [TestFixture]
    public class EmailAttributeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // list from http://en.wikipedia.org/wiki/Email_address#Examples
        [TestCase("test@test.com", true)]
        [TestCase("niceandsimple@example.com", true)]
        [TestCase("very.common@example.com", true)]
        [TestCase("a.little.lengthy.but.fine@dept.example.com", true)]
        [TestCase("test@a.com", true)]
        [TestCase("test@a.im", true)] 
        //[TestCase("disposable.style.email.with+symbol@example.com", true)]
        //[TestCase("user@[IPv6:2001:db8:1ff::a0b:dbd0]", true)]
        //[TestCase(@"""much.more unusual""@example.com", true)]
        //[TestCase("\"very.unusual.@.unusual.com\"@example.com", true)]
        //[TestCase("\"very.(),:;<>[]\".VERY.\"very@\\ \"very\".unusual\"@strange.example.com", true)]
        //[TestCase("postbox@com", true)] // (top-level domains are valid hostnames)
        //[TestCase("admin@mailserver1", true)] // (local domain name with no TLD)
        //[TestCase("!#$%&'*+-/=?^_`{}|~@example.org", true)]
        //[TestCase("\"()<>[]:,;@\\\"!#$%&'*+-/=?^_`{}| ~.a\"@example.org", true)]
        //[TestCase(@""" ""@example.org", true)]
        [TestCase("Abc.example.com", false)] //(an @ character must separate the local and domain parts)
        [TestCase("A@b@c@example.com", false)] // (only one @ is allowed outside quotation marks)
        [TestCase(@"a""b(c)d,e:f;g<h>i[j\k]l@example.com", false)] // (none of the special characters in this local part are allowed outside quotation marks)
        [TestCase("just\"not\"right@example.co", false)] //m (quoted strings must be dot separated, or the only element making up the local-part)
        [TestCase("this is\"not\allowed@example.com", false)] // (spaces, quotes, and backslashes may only exist when within quoted strings and preceded by a backslash)
        [TestCase(@" this\ still""not\\allowed@example.com", false)]
        public void Given_email_When_validate_Then_return_expected_result(string email, bool expectedValidationOutcome)
        {
            // Given
            var modelUnderTest = new EmailTestModel() { Email = email };

            // When

            // Then
            Assert.That(ModelIsValid(modelUnderTest), Is.EqualTo(expectedValidationOutcome));
        }

        private bool ModelIsValid(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults.Count == 0;
        }
    }

    public class EmailTestModel
    {
        [Email]
        public string Email { get; set; }
    }
}

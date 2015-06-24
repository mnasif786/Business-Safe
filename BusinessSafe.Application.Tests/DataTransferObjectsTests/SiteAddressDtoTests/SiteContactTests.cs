using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Application.Tests.Builder;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.DataTransferObjectsTests.SiteAddressDtoTests
{
    [TestFixture]
    public class SiteContactTests
    {
        //TODO RM Use the adderess builder
        [Test]
        public void Given_that_contact_name_is_set_in_ContactDto_When_siteContact_property_is_used_Then_value_is_correct()
        {
            //Given
            const string contactNameExpected = "test name";
            var target = SiteAddressDtoBuilder.Create().WithContactName(contactNameExpected).Build();

            //When
            var result = target.SiteContact;

            //Then
            Assert.That(result, Is.EqualTo(contactNameExpected));
        }

        [Test]
        public void Given_that_ContactDto_is_null_When_siteContact_property_is_used_Then_value_is_empty()
        {
            //Given            
            var target2 = new SiteAddressDto(1, default(string), default(string), default(string), default(string),
                                            default(string), default(string), default(string), default(string),
                                            null);

            var target = SiteAddressDtoBuilder.Create().WithSiteContact(null).Build();

            //When
            var result = target.SiteContact;

            //Then
            Assert.That(result, Is.EqualTo(null));
        }
    }
}

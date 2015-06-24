using BusinessSafe.WebSite.Areas.Employees.ViewModels;
using NUnit.Framework;

namespace BusinessSafe.WebSite.Tests.ViewModels.AddressViewModelTests
{
    [TestFixture]
    [Category("Unit")]
    public class PreferedTelephoneNumberTests
    {
        [TestCase(1, "123")]
        [TestCase(2, "456")]
        [TestCase(3, "789")]
        public void Given_that_three_telephone_numbers_exists_When_preferened_telephone_number_is_selected_Then_correct_prefered_number_is_set(int preferedNumber, string expectedTelephoneNumber)
        {
            //Given
            var target = new EmergencyContactViewModel
            {
                WorkTelephone = "123",
                HomeTelephone = "456",
                MobileTelephone = "789",
                PreferredContactNumber = preferedNumber
            };

            //When
            var result = target.GetPreferredContactNumber();

            //Then
            Assert.That(result, Is.EqualTo(expectedTelephoneNumber));
        }
    }
}

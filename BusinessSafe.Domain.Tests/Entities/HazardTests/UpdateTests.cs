using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_update_hazard_updates_all_required_fields()
        {
            //Given
            const string name = "HazardTest";
            const long companyId = 33749;
            var user = new UserForAuditing();
            var result = Hazard.Create("original name", 100, user, null, null);
            var hazardTypes = new[] { new HazardType(), new HazardType(), };

            //When
            result.Update(name, companyId, user, hazardTypes);

            //Then
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.HazardTypes, Is.EqualTo(hazardTypes));
        }

        [Test]
        public void Given_system_default_hazard_When_update_Then_should_throw_the_correct_exception()
        {
            //Given
            var result = new MyHazard();
            result.MarkHazardAsSystemDefault();
            var hazardTypes = new[] { new HazardType(), new HazardType(), };
            
            //When
            //Then
            Assert.Throws<AttemptingToUpdateSystemDefaultException>(() => result.Update("", 1, null, hazardTypes));
        }
    }

    public class MyHazard: Hazard
    {
        public void MarkHazardAsSystemDefault()
        {
            CompanyId = null;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Application.DataTransferObjects;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Application.Tests.Implementations.CompanyDefaultServiceTests.CompanyDefaultMapperTests
{
    [TestFixture]
    [Category("Unit")]
    public class MapTests
    {
        UserForAuditing user = new UserForAuditing();

        [Test]
        public void Given_that_a_list_of_hazards_is_passed_in_Then_returns_list_of_CompnayDefaultDto()
        {
            //Given
            var hazards = new List<Hazard> { Hazard.Create("Fire", 0, user, null, null), Hazard.Create("Flood", 0, user, null, null) };
            
            //When
            var result = CompanyDefaultDto.CreateFrom(hazards);
            //Then
            Assert.That(result.Count(), Is.EqualTo(hazards.Count));
            Assert.That(result.First().Name, Is.EqualTo(hazards[0].Name));
        }

        [Test]
        public void Given_that_a_list_of_peopleatrisk_is_passed_in_Then_returns_list_of_CompnayDefaultDto()
        {
            //Given
            var peopleAtRisk = new List<PeopleAtRisk> { PeopleAtRisk.Create("Visitors", 0, null, new UserForAuditing()), PeopleAtRisk.Create("Customers", 0, null, new UserForAuditing()) };
            //When
            var result = CompanyDefaultDto.CreateFrom(peopleAtRisk);
            //Then
            Assert.That(result.Count(), Is.EqualTo(peopleAtRisk.Count));
            Assert.That(result.First().Name, Is.EqualTo(peopleAtRisk[0].Name));
        }
    }
}

using System;
using BusinessSafe.Domain.Entities;

using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_create_hazard_method_creates_an_object()
        {
            //Given
            const string name = "HazardTest";
            const long companyId = 33749;
            var user = new UserForAuditing();
            var expectedHazardTypes = new[] { new HazardType(), new HazardType(), };

            //When
            var result = Hazard.Create(name, companyId, user, expectedHazardTypes, new GeneralRiskAssessment{ Id = 500});

            //Then
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.HazardTypes.Count, Is.EqualTo(expectedHazardTypes.Length));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(500));
        }
    }
}

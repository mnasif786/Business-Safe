using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireSafetyControlMeasureTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_create_method_creates_an_object()
        {
            //Given
            const string name = "PeopleTest";
            const long companyId = 33749;
            const long riskAssesmentId = 33749;
            var creatingUser = new UserForAuditing();

            //When
            var result = FireSafetyControlMeasure.Create( name, companyId, riskAssesmentId, creatingUser);

            //Then
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssesmentId));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.CreatedBy, Is.EqualTo(creatingUser));
        }
    }
}

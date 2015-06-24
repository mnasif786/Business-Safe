using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceTests
{
    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_felds_are_available_Then_create_HazardousSubstanceRiskAssessment_method_creates_an_object()
        {
            //Given
            const string name = "HS Test";
            const long companyId = 100;
            var user = new UserForAuditing();
            const string reference = "REF 001";
            var supplier = new Supplier() { Id = 1, CompanyId = companyId, Name = "Test Supplier 1" };
            var sdsDate = new DateTime(2012, 11, 1, 10, 30, 0);
            string detailsOfUse = "Test Details of Use";
            //When
            var result = Domain.Entities.HazardousSubstance.Add(
                companyId, 
                user, 
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>()
                {
                    new Pictogram() { Id = 1 },
                    new Pictogram() { Id = 2 },
                    new Pictogram() { Id = 3 }
                }, 
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European, 
				detailsOfUse,
                false);

            //Then
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
            Assert.That(result.Reference, Is.EqualTo(reference));
            Assert.That(result.Supplier, Is.EqualTo(supplier));
            Assert.That(result.SdsDate, Is.EqualTo(sdsDate));
            Assert.That(result.Standard, Is.EqualTo(HazardousSubstanceStandard.European));
            Assert.That(result.HazardousSubstancePictograms.Count, Is.EqualTo(3));
            Assert.That(result.HazardousSubstancePictograms[0].Pictogram.Id, Is.EqualTo(1));
            Assert.That(result.HazardousSubstancePictograms[1].Pictogram.Id, Is.EqualTo(2));
            Assert.That(result.HazardousSubstancePictograms[2].Pictogram.Id, Is.EqualTo(3));
        }
    }
}

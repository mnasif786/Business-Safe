using System;
using System.Collections.Generic;
using System.Linq;

using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.ParameterClasses;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        
        const string name = "HS Test";
        const long companyId = 100;
        readonly UserForAuditing user = new UserForAuditing();
        const string reference = "REF 001";
        readonly Supplier supplier = new Supplier() { Id = 1, CompanyId = companyId, Name = "Test Supplier 1" };
        readonly DateTime sdsDate = new DateTime(2012, 11, 1, 10, 30, 0);
        private const string detailsOfUse = "Test Details of Use";

        [Test]
        public void When_update_pictograms_Then_old_pictograms_not_removed()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().Select(x => x.Pictogram.Id).Contains(1), Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().Select(x => x.Pictogram.Id).Contains(2), Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().Select(x => x.Pictogram.Id).Contains(3), Is.True);
        }

        [Test]
        public void Given_no_pictograms_selected_When_update_pictograms_Then_old_pictograms_removed()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                null,
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().Select(x => x.Pictogram.Id).Contains(1), Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().Select(x => x.Pictogram.Id).Contains(2), Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().Select(x => x.Pictogram.Id).Contains(3), Is.True);
        }

        [Test]
        public void When_update_pictograms_Then_old_pictograms_marked_as_deleted()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().FirstOrDefault(x => x.Pictogram.Id == 1).Deleted, Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().FirstOrDefault(x => x.Pictogram.Id == 2).Deleted, Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().FirstOrDefault(x => x.Pictogram.Id == 3).Deleted, Is.True);
        }

        [Test]
        public void Given_no_pictograms_selected_When_update_pictograms_Then_old_pictograms_marked_as_deleted()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                null,
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().FirstOrDefault(x => x.Pictogram.Id == 1).Deleted, Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().FirstOrDefault(x => x.Pictogram.Id == 2).Deleted, Is.True);
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().FirstOrDefault(x => x.Pictogram.Id == 3).Deleted, Is.True);
        }

        [Test]
        public void When_update_pictograms_Then_new_pictograms_added()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstancePictograms.ToList().Select(x => x.Pictogram.Id).Contains(4));
        }

        [Test]
        public void When_update_Then_removed_safety_phrases_marked_as_deleted()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>()
                {
                    new SafetyPhraseParameters()
                    {
                        Information = "info 2",
                        Phrase = new SafetyPhrase() { Id = 2 }
                    }
                },
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstanceSafetyPhrases.Count(), Is.EqualTo(2));
            Assert.That(hazardousSubstance.HazardousSubstanceSafetyPhrases.FirstOrDefault(x => x.SafetyPhrase.Id == 1).Deleted, Is.True);
        }

        [Test]
        public void When_update_Then_only_add_new_safety_phrases_no_duplicates()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>(),
                new List<SafetyPhraseParameters>()
                {
                    new SafetyPhraseParameters()
                    {
                        Information = "info 1",
                        Phrase = new SafetyPhrase() { Id = 1 }
                    },
                    new SafetyPhraseParameters()
                    {
                        Information = "info 2",
                        Phrase = new SafetyPhrase() { Id = 2 }
                    }
                },
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstanceSafetyPhrases.Count(), Is.EqualTo(2));
            Assert.That(hazardousSubstance.HazardousSubstanceSafetyPhrases.FirstOrDefault(x => x.SafetyPhrase.Id == 1).Deleted, Is.False);
            Assert.That(hazardousSubstance.HazardousSubstanceSafetyPhrases.FirstOrDefault(x => x.SafetyPhrase.Id == 2).Deleted, Is.False);
        }

        [Test]
        public void When_update_Then_removed_risk_phrases_marked_as_deleted()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>()
                {
                    new RiskPhrase()
                    {
                         Id = 2
                    }
                },
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstanceRiskPhrases.Count(), Is.EqualTo(2));
            Assert.That(hazardousSubstance.HazardousSubstanceRiskPhrases.FirstOrDefault(x => x.RiskPhrase.Id == 1).Deleted, Is.True);
        }

        [Test]
        public void When_update_Then_only_add_new_risk_phrases_no_duplicates()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>()
                {
                    new RiskPhrase()
                    {
                         Id = 2
                    }
                },
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstanceRiskPhrases.Count(), Is.EqualTo(2));
            var firstRiskPhrase = hazardousSubstance.HazardousSubstanceRiskPhrases.FirstOrDefault(x => x.RiskPhrase.Id == 1);
            Assert.That(firstRiskPhrase.Deleted, Is.True);
            Assert.That(hazardousSubstance.HazardousSubstanceRiskPhrases.FirstOrDefault(x => x.RiskPhrase.Id == 2).Deleted, Is.False);
        }

        [Test]
        public void When_update_Then_removed_risk_phrases_last_modified_details_set()
        {
            //Given
            var hazardousSubstance = CreateHazardousSubstance();

            //When
            hazardousSubstance.Update(
                user,
                name,
                reference,
                supplier,
                sdsDate,
                new List<Pictogram>() { new Pictogram() { Id = 4 } },
                new List<RiskPhrase>() { new RiskPhrase() { Id = 2 } },
                new List<SafetyPhraseParameters>(),
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);

            //Then
            Assert.That(hazardousSubstance.HazardousSubstanceRiskPhrases.Count(), Is.EqualTo(2));
            Assert.That(hazardousSubstance.HazardousSubstanceRiskPhrases.FirstOrDefault(x => x.RiskPhrase.Id == 1).LastModifiedBy, Is.EqualTo(user));
            Assert.That(hazardousSubstance.HazardousSubstanceRiskPhrases.FirstOrDefault(x => x.RiskPhrase.Id == 1).LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }

        private HazardousSubstance CreateHazardousSubstance()
        {

            return HazardousSubstance.Add(
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
                new List<RiskPhrase>() { new RiskPhrase() { Id = 1 }},
                new List<SafetyPhraseParameters>()
                {
                    new SafetyPhraseParameters()
                    {
                        Information = "info 1",
                        Phrase = new SafetyPhrase() { Id = 1 }
                    }
                },
                HazardousSubstanceStandard.European,
                detailsOfUse,
                false);
        }
    }
}

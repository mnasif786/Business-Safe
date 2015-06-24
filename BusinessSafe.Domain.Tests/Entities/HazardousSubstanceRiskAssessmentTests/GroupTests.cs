using BusinessSafe.Domain.ParameterClasses;

using NUnit.Framework;

using System.Collections.Generic;

using BusinessSafe.Domain.Entities;

using System;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class GroupTests
    {
        [Test]
        public void Given_hazardous_substance_has_many_risk_phrases_When_Group_is_inspected_Then_highest_group_is_returned_1()
        {
            //Given
            const string title = "RA Test";
            const string reference = "RA 002";
            const long companyId = 100;
            var user = new UserForAuditing();
            var pictograms = new List<Pictogram>();
            var hazardousSubstanceGroupA = new HazardousSubstanceGroup { Id = 1, Code = "A" };
            var hazardousSubstanceGroupB = new HazardousSubstanceGroup { Id = 2, Code = "B" };
            var supplier = new Supplier();
            var safetyPhrases = new List<SafetyPhraseParameters>();

            var riskPhrases = new List<RiskPhrase>
                              {
                                  new RiskPhrase { Id = 1, Title = "RX01", Group = hazardousSubstanceGroupA },
                                  new RiskPhrase { Id = 2, Title = "RX02", Group = hazardousSubstanceGroupB },
                                  new RiskPhrase { Id = 3, Title = "RX03", Group = hazardousSubstanceGroupA },
                                  new RiskPhrase { Id = 4, Title = "RX04", Group = hazardousSubstanceGroupB },
                              };

            var hazardousSubstance = Domain.Entities.HazardousSubstance.Add(
                companyId,
                user,
                "Test Substance 01",
                "TS01",
                supplier,
                DateTime.Now,
                pictograms,
                riskPhrases,
                safetyPhrases,
                HazardousSubstanceStandard.European,
                "Details of Use",
                false);

            var hazardousSubstanceRiskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create(title, reference, companyId, user, hazardousSubstance);

            //When
            var hazardousSubstanceRiskAssessmentGroup = hazardousSubstanceRiskAssessment.Group;

            //Then
            Assert.That(hazardousSubstanceRiskAssessmentGroup, Is.EqualTo(hazardousSubstanceGroupB));
        }

        [Test]
        public void Given_hazardous_substance_has_many_risk_phrases_When_Group_is_inspected_Then_highest_group_is_returned_2()
        {
            //Given
            const string title = "RA Test";
            const string reference = "RA 002";
            const long companyId = 100;
            var user = new UserForAuditing();
            var pictograms = new List<Pictogram>();
            var hazardousSubstanceGroupA = new HazardousSubstanceGroup { Id = 1, Code = "A" };
            var hazardousSubstanceGroupB = new HazardousSubstanceGroup { Id = 2, Code = "B" };
            var hazardousSubstanceGroupC = new HazardousSubstanceGroup { Id = 3, Code = "C" };
            var hazardousSubstanceGroupE = new HazardousSubstanceGroup { Id = 5, Code = "E" };

            var supplier = new Supplier();
            var safetyPhrases = new List<SafetyPhraseParameters>();

            var riskPhrases = new List<RiskPhrase>
                              {
                                  new RiskPhrase { Id = 1, Title = "RX01", Group = hazardousSubstanceGroupC },
                                  new RiskPhrase { Id = 2, Title = "RX02", Group = hazardousSubstanceGroupB },
                                  new RiskPhrase { Id = 3, Title = "RX03", Group = hazardousSubstanceGroupA },
                                  new RiskPhrase { Id = 4, Title = "RX04", Group = hazardousSubstanceGroupE },
                                  new RiskPhrase { Id = 5, Title = "RX05", Group = hazardousSubstanceGroupB },
                                  new RiskPhrase { Id = 6, Title = "RX06", Group = hazardousSubstanceGroupA },
                              };

            var hazardousSubstance = Domain.Entities.HazardousSubstance.Add(
                companyId,
                user,
                "Test Substance 01",
                "TS01",
                supplier,
                DateTime.Now,
                pictograms,
                riskPhrases,
                safetyPhrases,
                HazardousSubstanceStandard.European,
                "Details of Use",
                false
                );

            var hazardousSubstanceRiskAssessment = Domain.Entities.HazardousSubstanceRiskAssessment.Create(title, reference, companyId, user, hazardousSubstance);

            //When
            var hazardousSubstanceRiskAssessmentGroup = hazardousSubstanceRiskAssessment.Group;

            //Then
            Assert.That(hazardousSubstanceRiskAssessmentGroup, Is.EqualTo(hazardousSubstanceGroupE));
        }
    }
}

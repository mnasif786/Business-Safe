using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CopyForMultipleSitesTests 
    {
        private long _clientId;
        private UserForAuditing _currentUser;
        private HazardousSubstanceRiskAssessment _hazardousSubstanceRiskAssessment;
        private HazardousSubstance _hazardousSubstance;

        [SetUp]
        public void SetUp()
        {
            _clientId = 3423L;
            _currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            _hazardousSubstance = new HazardousSubstance {Id = 3764L};
            _hazardousSubstanceRiskAssessment = HazardousSubstanceRiskAssessment.Create(
                "Hazardous Substance Risk Assessment 2", "GRA02", _clientId, _currentUser, _hazardousSubstance);
        }

        [Test]
        public void Given_valid_parameters_When_CopyForMultipleSites_is_called_Then_multiple_risk_assessments_are_created()
        {
            var newSites = new List<Site>
                               {
                                   new Site {Id = 727L},
                                   new Site {Id = 927L},
                                   new Site {Id = 1049L},
                               };

            var newGeneralRiskAssessments = _hazardousSubstanceRiskAssessment.CopyForMultipleSites("Copy of Hazardous Substance Risk Assessment 2", newSites, _currentUser)
                .Cast<HazardousSubstanceRiskAssessment>()
                .ToList();

            Assert.That(newGeneralRiskAssessments.Count, Is.EqualTo(3));
            Assert.That(newGeneralRiskAssessments[0].Title, Is.EqualTo("Copy of Hazardous Substance Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[0].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[0].HazardousSubstance, Is.EqualTo(_hazardousSubstance));
            Assert.That(newGeneralRiskAssessments[0].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].RiskAssessmentSite, Is.EqualTo(newSites[0]));
            Assert.That(newGeneralRiskAssessments[1].Title, Is.EqualTo("Copy of Hazardous Substance Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[1].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[0].HazardousSubstance, Is.EqualTo(_hazardousSubstance));
            Assert.That(newGeneralRiskAssessments[1].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].RiskAssessmentSite, Is.EqualTo(newSites[1]));
            Assert.That(newGeneralRiskAssessments[2].Title, Is.EqualTo("Copy of Hazardous Substance Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[2].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[0].HazardousSubstance, Is.EqualTo(_hazardousSubstance));
            Assert.That(newGeneralRiskAssessments[2].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].RiskAssessmentSite, Is.EqualTo(newSites[2]));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CopyForMultipleSitesTests 
    {
        private long _clientId;
        private UserForAuditing _currentUser;
        private FireRiskAssessment _fireRiskAssessment;
        private Checklist _checklist;

        [SetUp]
        public void SetUp()
        {
            _clientId = 3423L;
            _currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            _checklist = new Checklist {Id = 42L};
            _fireRiskAssessment = FireRiskAssessment.Create(
                "Fire Risk Assessment 2", "GRA02", _clientId, _checklist, _currentUser);
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

            var newGeneralRiskAssessments = _fireRiskAssessment.CopyForMultipleSites("Copy of Fire Risk Assessment 2", newSites, _currentUser)
                .Cast<FireRiskAssessment>()
                .ToList();

            Assert.That(newGeneralRiskAssessments.Count, Is.EqualTo(3));
            Assert.That(newGeneralRiskAssessments[0].Title, Is.EqualTo("Copy of Fire Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[0].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[0].FireRiskAssessmentChecklists.Count, Is.EqualTo(1));
            Assert.That(newGeneralRiskAssessments[0].FireRiskAssessmentChecklists[0].Checklist, Is.EqualTo(_checklist));
            Assert.That(newGeneralRiskAssessments[0].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].RiskAssessmentSite, Is.EqualTo(newSites[0]));
            Assert.That(newGeneralRiskAssessments[1].Title, Is.EqualTo("Copy of Fire Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[1].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[1].FireRiskAssessmentChecklists.Count, Is.EqualTo(1));
            Assert.That(newGeneralRiskAssessments[1].FireRiskAssessmentChecklists[0].Checklist, Is.EqualTo(_checklist));
            Assert.That(newGeneralRiskAssessments[1].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].RiskAssessmentSite, Is.EqualTo(newSites[1]));
            Assert.That(newGeneralRiskAssessments[2].Title, Is.EqualTo("Copy of Fire Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[2].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[2].FireRiskAssessmentChecklists.Count, Is.EqualTo(1));
            Assert.That(newGeneralRiskAssessments[2].FireRiskAssessmentChecklists[0].Checklist, Is.EqualTo(_checklist));
            Assert.That(newGeneralRiskAssessments[2].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].RiskAssessmentSite, Is.EqualTo(newSites[2]));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using BusinessSafe.Domain.Entities;

namespace BusinessSafe.Domain.Tests.Entities.GeneralRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class CopyForMultipleSitesTests 
    {
        private long _clientId;
        private UserForAuditing _currentUser;
        private string _location;
        private string _taskProcessDescription;
        private Site _site;
        private DateTime _assessmentDate;
        private RiskAssessor _riskAssessor;
        private List<HazardType> _hazardTypes;
        private GeneralRiskAssessment _generalRiskAssessment;

        [SetUp]
        public void SetUp()
        {
            _clientId = 3423L;
            _currentUser = new UserForAuditing { Id = Guid.NewGuid() };
            _location = "Manchester";
            _taskProcessDescription = "Process";
            _site = new Site { Id = 256L };
            _assessmentDate = new DateTime(2051, 10, 1);
            _riskAssessor = new RiskAssessor { Id = 74747L };

            _hazardTypes = new List<HazardType>
                               {
                                   new HazardType {Id = 25L},
                                   new HazardType {Id = 37L}
                               };

            _generalRiskAssessment = GeneralRiskAssessment.Create(
                "General Risk Assessment 2", "GRA02", _clientId, _currentUser, _location, _taskProcessDescription, _site,
                _assessmentDate, _riskAssessor);
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

            var newGeneralRiskAssessments = _generalRiskAssessment.CopyForMultipleSites("Copy of General Risk Assessment 2", newSites, _currentUser)
                .Cast<GeneralRiskAssessment>()
                .ToList();

            Assert.That(newGeneralRiskAssessments.Count, Is.EqualTo(3));
            Assert.That(newGeneralRiskAssessments[0].Title, Is.EqualTo("Copy of General Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[0].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[0].Location, Is.EqualTo(_location));
            Assert.That(newGeneralRiskAssessments[0].TaskProcessDescription, Is.EqualTo(_taskProcessDescription));
            Assert.That(newGeneralRiskAssessments[0].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[0].RiskAssessmentSite, Is.EqualTo(newSites[0]));
            Assert.That(newGeneralRiskAssessments[1].Title, Is.EqualTo("Copy of General Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[1].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[1].Location, Is.EqualTo(_location));
            Assert.That(newGeneralRiskAssessments[1].TaskProcessDescription, Is.EqualTo(_taskProcessDescription));
            Assert.That(newGeneralRiskAssessments[1].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[1].RiskAssessmentSite, Is.EqualTo(newSites[1]));
            Assert.That(newGeneralRiskAssessments[2].Title, Is.EqualTo("Copy of General Risk Assessment 2"));
            Assert.That(newGeneralRiskAssessments[2].Reference, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].CompanyId, Is.EqualTo(_clientId));
            Assert.That(newGeneralRiskAssessments[2].Location, Is.EqualTo(_location));
            Assert.That(newGeneralRiskAssessments[2].TaskProcessDescription, Is.EqualTo(_taskProcessDescription));
            Assert.That(newGeneralRiskAssessments[2].AssessmentDate, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].RiskAssessor, Is.Null);
            Assert.That(newGeneralRiskAssessments[2].RiskAssessmentSite, Is.EqualTo(newSites[2]));
        }
    }
}

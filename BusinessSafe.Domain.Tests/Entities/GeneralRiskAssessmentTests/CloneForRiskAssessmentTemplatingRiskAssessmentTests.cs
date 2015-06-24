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
    class CloneForRiskAssessmentTemplatingRiskAssessmentTests
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
            _clientId = 234213L;
            _currentUser = new UserForAuditing {Id = Guid.NewGuid()};
            _location = "Manchester";
            _taskProcessDescription = "Process";
            _site = new Site {Id = 4234L};
            _assessmentDate = new DateTime(2050, 10, 1);
            _riskAssessor = new RiskAssessor {Id = 23432L};

            _hazardTypes = new List<HazardType>
                               {
                                   new HazardType {Id = 234L},
                                   new HazardType {Id = 3242L}
                               };

            _generalRiskAssessment = GeneralRiskAssessment.Create(
                "General Risk Assessment 1", "GRA01", _clientId, _currentUser, _location, _taskProcessDescription, _site,
                _assessmentDate, _riskAssessor);
        }

        [Test]
        public void Given_general_risk_assessment_contains_generic_hazard_When_CloneForRiskAssessmentTemplating_called_Then_same_hazard_is_referenced()
        {
            var hazard = new Hazard
                             {
                                 Name = "Generic Hazard",
                                 HazardTypes = _hazardTypes
                             };

            var hazards = new List<Hazard> {hazard};
            _generalRiskAssessment.AttachHazardsToRiskAssessment(hazards, _currentUser);
            var newGeneralRiskAssessment = _generalRiskAssessment.Copy("General Risk Assessment 2", "GRA02", _currentUser) as GeneralRiskAssessment;
            Assert.That(newGeneralRiskAssessment.Hazards.Count, Is.EqualTo(1));
            Assert.That(newGeneralRiskAssessment.Hazards[0].Hazard, Is.SameAs(hazard));
        }

        [Test]
        public void Given_general_risk_assessment_contains_company_wide_hazard_When_CloneForRiskAssessmentTemplating_called_Then_same_hazard_is_referenced()
        {
            var hazard = Hazard.Create(
                "Generic Hazard",
                _clientId,
                _currentUser,
                _hazardTypes,
                null 
                );

            var hazards = new List<Hazard> { hazard };
            _generalRiskAssessment.AttachHazardsToRiskAssessment(hazards, _currentUser);

            var newGeneralRiskAssessment = _generalRiskAssessment.Copy("General Risk Assessment 3", "GRA03", _currentUser) as GeneralRiskAssessment;
            Assert.That(newGeneralRiskAssessment.Hazards.Count, Is.EqualTo(1));
            Assert.That(newGeneralRiskAssessment.Hazards[0].Hazard, Is.SameAs(hazard));
        }

        [Test]
        public void Given_general_risk_assessment_contains_risk_assessment_specific_hazard_When_CloneForRiskAssessmentTemplating_called_Then_same_hazard_is_referenced()
        {
            var hazard = Hazard.Create(
                "Generic Hazard",
                _clientId,
                _currentUser,
                _hazardTypes,
                _generalRiskAssessment
                );

            var hazards = new List<Hazard> { hazard };
            _generalRiskAssessment.AttachHazardsToRiskAssessment(hazards, _currentUser);

            var newGeneralRiskAssessment = _generalRiskAssessment.Copy("General Risk Assessment 3", "GRA03", _currentUser) as GeneralRiskAssessment;
            Assert.That(newGeneralRiskAssessment.Hazards.Count, Is.EqualTo(1));
            Assert.That(newGeneralRiskAssessment.Hazards[0].Hazard, Is.Not.SameAs(hazard));
        }

        [Test]
        public void Given_general_risk_assessment_contains_two_bespoke_hazards_When_CloneForRiskAssessmentTemplating_called_Then_same_hazards_are_referenced()
        {
            var hazard1 = Hazard.Create(
                "Generic Hazard 1",
                _clientId,
                _currentUser,
                _hazardTypes,
                _generalRiskAssessment
                );

            hazard1.Id = 1234;

            var hazard2 = Hazard.Create(
                "Generic Hazard 2",
                _clientId,
                _currentUser,
                _hazardTypes,
                _generalRiskAssessment
                );

            hazard2.Id = 12345;

            _generalRiskAssessment.AttachHazardToRiskAssessment(hazard1, _currentUser);
            _generalRiskAssessment.AttachHazardToRiskAssessment(hazard2, _currentUser);

            var newGeneralRiskAssessment = _generalRiskAssessment.Copy("General Risk Assessment 3", "GRA03", _currentUser) as GeneralRiskAssessment;
            Assert.That(newGeneralRiskAssessment.Hazards.Count, Is.EqualTo(2));
            Assert.That(newGeneralRiskAssessment.Hazards[0].Hazard, Is.Not.SameAs(hazard1));
        }
        
        [Test]
        public void Given_general_risk_assessment_contains_duplicate_hazards_When_CloneForRiskAssessmentTemplating_called_Then_only_one_hazard_entered()
        {
            var hazard =  new Hazard() { Id = 123 };
            var hazard1 = MultiHazardRiskAssessmentHazard.Create(_generalRiskAssessment, hazard, null);
            var hazard2 = MultiHazardRiskAssessmentHazard.Create(_generalRiskAssessment, hazard, null);
            _generalRiskAssessment.Hazards.Add(hazard1);
            _generalRiskAssessment.Hazards.Add(hazard2);

            var newGeneralRiskAssessment = _generalRiskAssessment.Copy("General Risk Assessment 3", "GRA03", _currentUser) as GeneralRiskAssessment;
            Assert.That(newGeneralRiskAssessment.Hazards.Count, Is.EqualTo(1));
            Assert.That(newGeneralRiskAssessment.Hazards[0].Hazard.Id, Is.EqualTo(hazard.Id));
        }


        [Test]
        public void Given_general_risk_assessment_When_AttachHazardToRiskAssessment_for_a_hazard_that_has_already_been_attached_then_no_error_message_and_no_duplicates()
        {
            var hazard = new Hazard() { Id = 123 };
            var hazard1 = MultiHazardRiskAssessmentHazard.Create(_generalRiskAssessment, hazard, null);
            var hazard2 = MultiHazardRiskAssessmentHazard.Create(_generalRiskAssessment, hazard, null);

            _generalRiskAssessment.AttachHazardToRiskAssessment(hazard,null);
            _generalRiskAssessment.AttachHazardToRiskAssessment(hazard, null);

            Assert.That(_generalRiskAssessment.Hazards.Count, Is.EqualTo(1));
            Assert.That(_generalRiskAssessment.Hazards[0].Hazard.Id, Is.EqualTo(hazard.Id));
        }
    }
}

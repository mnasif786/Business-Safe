using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceRiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class RemoveControlMeasureFromRiskAssessmentTest
    {
        private HazardousSubstanceRiskAssessment _riskAssessment;
        private HazardousSubstanceRiskAssessmentControlMeasure _assessmentControlMeasure;
        private UserForAuditing _user;
        private long _controlMeasureId;

        [SetUp]
        public void Setup()
        {
            _user = new UserForAuditing();
            _controlMeasureId = 500;

            _riskAssessment = HazardousSubstanceRiskAssessment.Create("title", "reference", 200, _user, null);
            _assessmentControlMeasure = HazardousSubstanceRiskAssessmentControlMeasure.Create("", _riskAssessment, _user);
            _assessmentControlMeasure.Id = _controlMeasureId;

            _riskAssessment.AddControlMeasure(_assessmentControlMeasure, _user);
        }

        [Test]
        public void Given_controlmeasure_not_attached_to_risk_assessment_Then_should_throw_correct_exception()
        {
            //Given
            //When
            //Then
            Assert.Throws<ControlMeasureDoesNotExistOnRiskAssessmentException>(() => _riskAssessment.RemoveControlMeasure(124312, _user));
        }

        [Test]
        public void Given_remove_controlmeasure_on_risk_assessment_Then_should_set_last_modified()
        {
            //Given

            //When

            //Then
            Assert.That(_riskAssessment.LastModifiedBy, Is.EqualTo(_user));
            Assert.That(_riskAssessment.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Now.Date));
        }

        [Test]
        public void Given_remove_controlmeasure_on_risk_assessment_Then_should_not_remove_control_measure()
        {
            //Given

            //When
            _riskAssessment.RemoveControlMeasure(_controlMeasureId, _user);

            //Then
            Assert.That(_riskAssessment.ControlMeasures.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_remove_controlmeasure_on_risk_assessment_Then_deleted_is_true()
        {
            //Given

            //When
            _riskAssessment.RemoveControlMeasure(_controlMeasureId, _user);

            //Then
            Assert.That(_riskAssessment.ControlMeasures[0].Deleted, Is.True);
        }
    }
}
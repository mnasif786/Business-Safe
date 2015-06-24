using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using System.Linq;
namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class AttachFireSafetyControlMeasuresToRiskAssessmentTests
    {
        [Test]
        public void Given_adding_safety_control_measure_to_add_When_Attach_Then_should_update_last_modified_details()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<FireSafetyControlMeasure> fireSafetControlMeasuresToAttach = new List<FireSafetyControlMeasure>()
                                                                                         {
                                                                                             new FireSafetyControlMeasure()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachFireSafetyControlMeasuresToRiskAssessment(fireSafetControlMeasuresToAttach, user);

            //Then
            Assert.That(fireRiskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(fireRiskAssessment.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Today.ToShortDateString()));
        }

        [Test]
        public void Given_one_safety_control_measure_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<FireSafetyControlMeasure> fireSafetControlMeasuresToAttach = new List<FireSafetyControlMeasure>()
                                                                                         {
                                                                                             new FireSafetyControlMeasure()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachFireSafetyControlMeasuresToRiskAssessment(fireSafetControlMeasuresToAttach, user);

            //Then
            Assert.That(fireRiskAssessment.FireSafetyControlMeasures.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_multiple_safety_control_measure_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<FireSafetyControlMeasure> fireSafetControlMeasuresToAttach = new List<FireSafetyControlMeasure>()
                                                                                         {
                                                                                             new FireSafetyControlMeasure()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                     
                                                                                                 },
                                                                                            new FireSafetyControlMeasure()
                                                                                                 {
                                                                                                     Id = 2,
                                                                                                 }
                                                                                            ,
                                                                                            new FireSafetyControlMeasure()
                                                                                                 {
                                                                                                     Id = 3,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachFireSafetyControlMeasuresToRiskAssessment(fireSafetControlMeasuresToAttach, user);

            //Then
            Assert.That(fireRiskAssessment.FireSafetyControlMeasures.Count(x => x.FireSafetyControlMeasure.Id == 1), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.FireSafetyControlMeasures.Count(x => x.FireSafetyControlMeasure.Id == 2), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.FireSafetyControlMeasures.Count(x => x.FireSafetyControlMeasure.Id == 3), Is.EqualTo(1));
        }

        [Test]
        public void Given_risk_assessment_with_existing_safety_control_measures_but_request_contains_no_control_measures_to_attach_When_Attach_Then_should_be_remove_the_existing_control_measure()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);


            fireRiskAssessment.AttachFireSafetyControlMeasuresToRiskAssessment(new List<FireSafetyControlMeasure>()
                                                                                         {
                                                                                             new FireSafetyControlMeasure()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                     Name = "Some Fire Safety Control Measure"
                                                                                                }
                                                                                         }, user);

            //When
            fireRiskAssessment.AttachFireSafetyControlMeasuresToRiskAssessment(new List<FireSafetyControlMeasure>(), user);

            //Then
            Assert.That(fireRiskAssessment.FireSafetyControlMeasures.Count(x => !x.Deleted), Is.EqualTo(0));
        }


        [Test]
        public void Given_duplicate_safety_control_measure_to_add_When_Attach_Then_should_not_add_duplicate_or_throw_exception()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<FireSafetyControlMeasure> fireSafetControlMeasuresToAttach = new List<FireSafetyControlMeasure>()
                                                                                         {
                                                                                             new FireSafetyControlMeasure(){Id = 1},
                                                                                             new FireSafetyControlMeasure(){Id = 1}
                                                                                         };

            //When
            fireRiskAssessment.AttachFireSafetyControlMeasuresToRiskAssessment(fireSafetControlMeasuresToAttach, user);

            //Then
            Assert.That(fireRiskAssessment.FireSafetyControlMeasures.Count, Is.EqualTo(1));
        }
    }
}
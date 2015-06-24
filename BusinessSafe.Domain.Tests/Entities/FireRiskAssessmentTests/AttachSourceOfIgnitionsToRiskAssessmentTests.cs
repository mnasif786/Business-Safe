using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class AttachSourceOfIgnitionsToRiskAssessmentTests
    {
        [Test]
        public void Given_adding_source_of_ignition_to_add_When_Attach_Then_should_update_last_modified_details()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<SourceOfIgnition> sourceOfIgnitions = new List<SourceOfIgnition>()
                                                                                         {
                                                                                             new SourceOfIgnition()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachSourceOfIgnitionsToRiskAssessment(sourceOfIgnitions, user);

            //Then
            Assert.That(fireRiskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(fireRiskAssessment.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Today.ToShortDateString()));
        }

        [Test]
        public void Given_one_source_of_ignition_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<SourceOfIgnition> sourceOfIgnitions = new List<SourceOfIgnition>()
                                                                                         {
                                                                                             new SourceOfIgnition()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachSourceOfIgnitionsToRiskAssessment(sourceOfIgnitions, user);

            //Then
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfIgnition.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_multiple_sources_of_ignition_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<SourceOfIgnition> sourceOfIgnitions = new List<SourceOfIgnition>()
                                                                                         {
                                                                                             new SourceOfIgnition()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                     
                                                                                                 },
                                                                                             new SourceOfIgnition()
                                                                                                 {
                                                                                                     Id = 2,
                                                                                                 }
                                                                                             ,
                                                                                             new SourceOfIgnition()
                                                                                                 {
                                                                                                     Id = 3,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachSourceOfIgnitionsToRiskAssessment(sourceOfIgnitions, user);

            //Then
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfIgnition.Count(x => x.SourceOfIgnition.Id == 1), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfIgnition.Count(x => x.SourceOfIgnition.Id == 2), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfIgnition.Count(x => x.SourceOfIgnition.Id == 3), Is.EqualTo(1));
        }

        [Test]
        public void Given_risk_assessment_with_existing_source_of_ignitions_but_request_contains_no_control_measures_to_attach_When_Attach_Then_should_be_remove_the_existing_sources_of_ignition()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);


            fireRiskAssessment.AttachSourceOfIgnitionsToRiskAssessment(new List<SourceOfIgnition>()
                                                                                   {
                                                                                       new SourceOfIgnition()
                                                                                           {
                                                                                               Id = 1,
                                                                                               Name = "Some Fire Safety Control Measure"
                                                                                           }
                                                                                   }, user);

            //When
            fireRiskAssessment.AttachSourceOfIgnitionsToRiskAssessment(new List<SourceOfIgnition>(), user);

            //Then
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfIgnition.Count(x => !x.Deleted), Is.EqualTo(0));
        }

    }
}
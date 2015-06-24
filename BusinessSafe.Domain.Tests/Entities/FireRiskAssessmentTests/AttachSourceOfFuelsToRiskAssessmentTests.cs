using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class AttachSourceOfFuelsToRiskAssessmentTests
    {
        [Test]
        public void Given_adding_source_of_fuel_to_add_When_Attach_Then_should_update_last_modified_details()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<SourceOfFuel> sourceOfFuels = new List<SourceOfFuel>()
                                                                  {
                                                                      new SourceOfFuel()
                                                                          {
                                                                              Id = 1,
                                                                          }
                                                                  };

            //When
            fireRiskAssessment.AttachSourceOfFuelsToRiskAssessment(sourceOfFuels, user);

            //Then
            Assert.That(fireRiskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(fireRiskAssessment.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Today.ToShortDateString()));
        }

        [Test]
        public void Given_one_source_of_fuel_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<SourceOfFuel> sourceOfFuels = new List<SourceOfFuel>()
                                                                  {
                                                                      new SourceOfFuel()
                                                                          {
                                                                              Id = 1,
                                                                          }
                                                                  };

            //When
            fireRiskAssessment.AttachSourceOfFuelsToRiskAssessment(sourceOfFuels, user);

            //Then
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfFuel.Count, Is.EqualTo(1));
        }

        [Test]
        public void Given_multiple_sources_of_fuel_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<SourceOfFuel> sourceOfFuels = new List<SourceOfFuel>()
                                                                  {
                                                                      new SourceOfFuel()
                                                                          {
                                                                              Id = 1,
                                                                                                     
                                                                          },
                                                                      new SourceOfFuel()
                                                                          {
                                                                              Id = 2,
                                                                          }
                                                                      ,
                                                                      new SourceOfFuel()
                                                                          {
                                                                              Id = 3,
                                                                          }
                                                                  };

            //When
            fireRiskAssessment.AttachSourceOfFuelsToRiskAssessment(sourceOfFuels, user);

            //Then
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfFuel.Count(x => x.SourceOfFuel.Id == 1), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfFuel.Count(x => x.SourceOfFuel.Id == 2), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfFuel.Count(x => x.SourceOfFuel.Id == 3), Is.EqualTo(1));
        }

        [Test]
        public void Given_risk_assessment_with_existing_source_of_fuels_but_request_contains_none_to_attach_When_Attach_Then_should_be_remove_the_existing_sources_of_fuel()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);


            fireRiskAssessment.AttachSourceOfFuelsToRiskAssessment(new List<SourceOfFuel>()
                                                                           {
                                                                               new SourceOfFuel()
                                                                                   {
                                                                                       Id = 1,
                                                                                       Name = "Some Fire Safety Control Measure"
                                                                                   }
                                                                           }, user);

            //When
            fireRiskAssessment.AttachSourceOfFuelsToRiskAssessment(new List<SourceOfFuel>(), user);

            //Then
            Assert.That(fireRiskAssessment.FireRiskAssessmentSourcesOfFuel.Count(x=> !x.Deleted), Is.EqualTo(0));
        }

    }
}
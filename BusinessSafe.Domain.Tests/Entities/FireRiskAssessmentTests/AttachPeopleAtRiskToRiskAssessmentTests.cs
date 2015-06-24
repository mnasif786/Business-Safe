using System;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class AttachPeopleAtRiskToRiskAssessmentTests
    {
        [Test]
        public void Given_adding_people_at_risk_to_add_When_Attach_Then_should_update_last_modified_details()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<PeopleAtRisk> toAttach = new List<PeopleAtRisk>()
                                                                                         {
                                                                                             new PeopleAtRisk()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachPeopleAtRiskToRiskAssessment(toAttach, user);

            //Then
            Assert.That(fireRiskAssessment.LastModifiedBy, Is.EqualTo(user));
            Assert.That(fireRiskAssessment.LastModifiedOn.Value.ToShortDateString(), Is.EqualTo(DateTime.Today.ToShortDateString()));
        }

        [Test]
        public void Given_one_people_at_risk_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<PeopleAtRisk> toAttach = new List<PeopleAtRisk>()
                                                                                         {
                                                                                             new PeopleAtRisk()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachPeopleAtRiskToRiskAssessment(toAttach, user);

            //Then
            Assert.That(fireRiskAssessment.PeopleAtRisk.Count(x => x.PeopleAtRisk.Id == 1), Is.EqualTo(1));
        }

        [Test]
        public void Given_multiple_people_at_risk_to_add_When_Attach_Then_should_be_added_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);

            IEnumerable<PeopleAtRisk> toAttach = new List<PeopleAtRisk>()
                                                                                         {
                                                                                             new PeopleAtRisk()
                                                                                                 {
                                                                                                     Id = 1,
                                                                                                     
                                                                                                 },
                                                                                             new PeopleAtRisk()
                                                                                                 {
                                                                                                     Id = 2,
                                                                                                 }
                                                                                             ,
                                                                                             new PeopleAtRisk()
                                                                                                 {
                                                                                                     Id = 3,
                                                                                                 }
                                                                                         };

            //When
            fireRiskAssessment.AttachPeopleAtRiskToRiskAssessment(toAttach, user);

            //Then
            Assert.That(fireRiskAssessment.PeopleAtRisk.Count(x => x.PeopleAtRisk.Id == 1), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.PeopleAtRisk.Count(x => x.PeopleAtRisk.Id == 2), Is.EqualTo(1));
            Assert.That(fireRiskAssessment.PeopleAtRisk.Count(x => x.PeopleAtRisk.Id == 3), Is.EqualTo(1));
        }

        [Test]
        public void Given_risk_assessment_with_existing_people_at_risk_but_request_contains_no_people_at_risk_to_attach_When_Attach_Then_should_be_remove_the_existing_people_at_risk()
        {
            //Given
            var user = new UserForAuditing();
            var fireRiskAssessment = FireRiskAssessment.Create("title", "reference", 100, new Checklist(), user);


            fireRiskAssessment.AttachPeopleAtRiskToRiskAssessment(new List<PeopleAtRisk>()
                                                                                   {
                                                                                       new PeopleAtRisk()
                                                                                           {
                                                                                               Id = 1
                                                                                           }
                                                                                   }, user);

            //When
            fireRiskAssessment.AttachPeopleAtRiskToRiskAssessment(new List<PeopleAtRisk>(), user);

            //Then
            Assert.That(fireRiskAssessment.PeopleAtRisk.Count(x => !x.Deleted), Is.EqualTo(0));
        }
    }
}
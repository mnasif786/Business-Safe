using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AttachPeopleAtRiskToRiskAssessmentTests
    {
        [Test]
        public void Given_person_at_risk_Then_should_add_correctly()
        {
            //Given
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var peopleAtRisk = new PeopleAtRisk();
            var user = new UserForAuditing();

            //When
            target.AttachPersonAtRiskToRiskAssessment(peopleAtRisk, user);

            //Then
            Assert.That(target.PeopleAtRisk.Count, Is.EqualTo(1));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_person_at_risk_already_exists_Then_should_get_correct_exception()
        {
            //Given
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var peopleAtRisk = new PeopleAtRisk();
            var user = new UserForAuditing();

            target.AttachPersonAtRiskToRiskAssessment(peopleAtRisk, user);

            //When
            //Then
            Assert.Throws<PersonAtRiskAlreadyAttachedToRiskAssessmentException>(() => target.AttachPersonAtRiskToRiskAssessment(peopleAtRisk, user));
        }

        [Test]
        [Ignore("Not sure this is a valud test?")]
        public void Given_person_is_removed_from_risk_assessment_when_AttachPeopleAtRiskToRiskAssessment_Then_last_modified_by_is_set()
        {
            //Given
            var personAtRiskToRemove = new PeopleAtRisk() { Id = 1, LastModifiedBy = null, LastModifiedOn = null };
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            target.PeopleAtRisk = new List<RiskAssessmentPeopleAtRisk>()
                                   {
                                       new RiskAssessmentPeopleAtRisk { PeopleAtRisk = personAtRiskToRemove }
                                   };

            var peopleAtRisk = new List<PeopleAtRisk>();
            var user = new UserForAuditing();

            //When
            target.AttachPeopleAtRiskToRiskAssessment(peopleAtRisk, user);

            //Then
            Assert.IsNotNull(personAtRiskToRemove.LastModifiedBy);
            Assert.IsNotNull(personAtRiskToRemove.LastModifiedOn);
            Assert.AreEqual(0, target.PeopleAtRisk.Count);
        }
    }
}
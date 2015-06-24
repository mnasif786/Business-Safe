using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class DetachHazardsFromRiskAssessmentTests
    {
        [Test]
        public void Given_hazards_Then_should_detach_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var target = GeneralRiskAssessment.Create("", "", default(long), null);

            var hazardDetaching = new Hazard()
                             {
                                 Id = 1
                             };
            var hazardToRemain = new Hazard()
                             {
                                 Id = 2
                             };
            IEnumerable<Hazard> hazards = new[]
                                              {
                                                  hazardDetaching, 
                                                  hazardToRemain,  
                                              };
            target.AttachHazardsToRiskAssessment(hazards, user);
            
            
            //When
            target.DetachHazardsFromRiskAssessment(new List<Hazard> { hazardDetaching }, user);

            //Then
            Assert.That(target.Hazards.Count(x => !x.Deleted), Is.EqualTo(1));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_hazard_does_not_exists_Then_should_get_correct_exception()
        {
            //Given
            var user = new UserForAuditing();
            var target = GeneralRiskAssessment.Create("", "", default(long), null);

            //When
            //Then
            Assert.Throws<HazardNotAttachedToRiskAssessmentException>(() => target.DetachHazardsFromRiskAssessment(new List<Hazard> { new Hazard() }, user));
        }

        [Test]
        public void Given_a_GRA_has_duplicate_hazards_when_removing_the_duplicate_Then_it_should_be_removed()
        {
            //Given
            var user = new UserForAuditing();
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var duplicateHazard = new Hazard() {Id = 123};

            target.Hazards.Add(MultiHazardRiskAssessmentHazard.Create(target,  new Hazard() {Id = 1}, null));
            target.Hazards.Add(MultiHazardRiskAssessmentHazard.Create(target, duplicateHazard, null));
            target.Hazards.Add(MultiHazardRiskAssessmentHazard.Create(target, duplicateHazard, null));

            //When
            target.DetachHazardsFromRiskAssessment(new List<Hazard> {duplicateHazard}, user);

            //Then
            Assert.That(target.Hazards.Count(x => x.Hazard.Id == duplicateHazard.Id && x.Deleted), Is.EqualTo(2));
            Assert.That(target.Hazards.Count(x => x.Hazard.Id != duplicateHazard.Id && !x.Deleted), Is.EqualTo(1));

        }
    }
}
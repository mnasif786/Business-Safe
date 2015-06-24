using System.Collections.Generic;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
using System.Linq;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class AttachHazardsToRiskAssessmentTests
    {
        [Test]
        public void Given_hazards_Then_should_add_correctly()
        {
            //Given
            var user = new UserForAuditing();
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var hazards = new List<Hazard>()
                              {
                                  new Hazard(){Id = 1},
                                  new Hazard(){Id = 2}
                              };

            //When
            target.AttachHazardsToRiskAssessment(hazards, user);

            //Then
            Assert.That(target.Hazards.Count, Is.EqualTo(hazards.Count));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_hazard_already_exists_Then_should_not_add_hazard_again()
        {
            //Given
            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            var user = new UserForAuditing();
            var hazards = new List<Hazard>()
                              {
                                  new Hazard()
                                      {
                                          Id = 1
                                      }
                              };

            target.AttachHazardsToRiskAssessment(hazards, user);

            //When
            target.AttachHazardsToRiskAssessment(hazards, user);

            //Then
            Assert.That(target.Hazards.Count, Is.EqualTo(hazards.Count));
            Assert.That(target.LastModifiedBy, Is.EqualTo(user));
        }

        [Test]
        public void Given_hazard_is_removed_from_risk_assessment_when_AttachHazardsToRiskAssessment_Then_last_modified_by_is_set()
        {
            //Given
            var hazardToRemove = MultiHazardRiskAssessmentHazard.Create(null, new Hazard(), null);

            hazardToRemove.ControlMeasures.Clear();
            //hazardToRemove.ControlMeasures.Add(new MultiHazardRiskAssessmentControlMeasure() {Id = 17, LastModifiedBy = null, LastModifiedOn = null});

            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            target.Hazards.Clear();
            target.Hazards.Add(hazardToRemove);

            var hazards = new List<Hazard>();
            var user = new UserForAuditing();

            //When
            target.AttachHazardsToRiskAssessment(hazards, user);

            //Then
            Assert.IsNotNull(hazardToRemove.LastModifiedBy);
            Assert.IsNotNull(hazardToRemove.LastModifiedOn);
            Assert.AreEqual(0, target.Hazards.Count(x => !x.Deleted));
            Assert.IsTrue(hazardToRemove.ControlMeasures.All(x => x.LastModifiedBy != null));
            Assert.IsTrue(hazardToRemove.ControlMeasures.All(x => x.LastModifiedOn != null));
        }

        [Test]
        [ExpectedException("System.Exception", ExpectedMessage = "MultiHazardRiskAssessmentHazard with Id 0 cannot be removed from risk assessment because it contains Control Measures or Further Control Measures.")]
        public void Given_hazard_control_measures_When_hazard_removed_error_is_thrown()
        {
            //Given
            var hazardToRemove = MultiHazardRiskAssessmentHazard.Create(null, new Hazard(), null);

            hazardToRemove.ControlMeasures.Clear();
            hazardToRemove.ControlMeasures.Add(new MultiHazardRiskAssessmentControlMeasure() {Id = 17, LastModifiedBy = null, LastModifiedOn = null});

            var target = GeneralRiskAssessment.Create("", "", default(long), null);
            target.Hazards.Clear();
            target.Hazards.Add(hazardToRemove);

            var hazards = new List<Hazard>();
            var user = new UserForAuditing();

            //When
            target.AttachHazardsToRiskAssessment(hazards, user);
        }
    }
}
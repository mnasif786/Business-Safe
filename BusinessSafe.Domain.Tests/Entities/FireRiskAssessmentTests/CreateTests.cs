using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class CreateTests
    {
        [Test]
        public void When_Create_called_Then_it_is_created_as_expected()
        {
            var user = new UserForAuditing {Id = Guid.NewGuid()};
            var checklist = new Checklist {Id = 123L};
            var fireRiskAssessment = FireRiskAssessment.Create("Test title", "TestRef", 234L, checklist, user);
            Assert.That(fireRiskAssessment.CompanyId, Is.EqualTo(234L));
            Assert.That(fireRiskAssessment.Reference, Is.EqualTo("TestRef"));
            Assert.That(fireRiskAssessment.Title, Is.EqualTo("Test title"));
            Assert.That(fireRiskAssessment.CreatedBy, Is.EqualTo(user));
            Assert.That(fireRiskAssessment.CreatedOn, Is.Not.Null);
            Assert.That(fireRiskAssessment.CreatedOn, Is.Not.EqualTo(default(DateTime)));
            Assert.That(fireRiskAssessment.Status, Is.EqualTo(RiskAssessmentStatus.Draft));
            Assert.That(fireRiskAssessment.FireRiskAssessmentChecklists, Is.Not.Null);
            Assert.That(fireRiskAssessment.FireRiskAssessmentChecklists.Count, Is.EqualTo(1));
            Assert.That(fireRiskAssessment.FireRiskAssessmentChecklists[0].Checklist, Is.EqualTo(checklist));
            Assert.That(fireRiskAssessment.LatestFireRiskAssessmentChecklist.Checklist, Is.EqualTo(checklist));
        }
    }
}

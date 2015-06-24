using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BusinessSafe.Domain.Entities;
using BusinessSafe.Domain.Tests.Entities.SiteTests;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.DocumentTests
{
    [TestFixture]
    public class RiskAssessmentDocumentTest
    {
        [Test]
        public void GivenARiskAssessmentWithASiteWhenSiteReferenceIsRequestedOnAnAssociatedRiskAssessmentDocThenSiteNameIsReturned()
        {
            // Given
            var site = new SiteGroup();
            site.Name = "my site";
            var ra = new GeneralRiskAssessment() { RiskAssessmentSite = site };
            var raDoc = new RiskAssessmentDocument() { RiskAssessment = ra };

            // When
            var result = raDoc.SiteReference;

            // Then
            Assert.That(result, Is.EqualTo("my site"));
        }
        [Test]
        public void GivenARiskAssessmentWithNoSiteWhenSiteReferenceIsRequestedOnAnAssociatedRiskAssessmentDocThenStringEmptyIsReturned()
        {
            // Given
            var ra = new GeneralRiskAssessment();
            var raDoc = new RiskAssessmentDocument() { RiskAssessment = ra };

            // When
            var result = raDoc.SiteReference;

            // Then
            Assert.That(result, Is.EqualTo(string.Empty));
        }
    }
}

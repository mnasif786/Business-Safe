using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;
    
namespace BusinessSafe.Domain.Tests.Entities.FireRiskAssessmentTests
{
    [TestFixture]
    public class LatestFireRiskAssessmentChecklistTests
    {

        [Test]
        public void Given_Firechecklist_is_null_When_retrieving_LatestFireRiskAssessmentChecklist_then_return_null()
        {
            //Given
            var fireRa = new FireRiskAssessment();
            fireRa.FireRiskAssessmentChecklists = null;

            //When
            var result = fireRa.LatestFireRiskAssessmentChecklist;

            //Then
            Assert.IsNull(result);
        }

        [Test]
        public void Given_Firechecklist_is_emptylist_When_retrieving_LatestFireRiskAssessmentChecklist_then_return_null()
        {
            //Given
            var fireRa = new FireRiskAssessment();
            fireRa.FireRiskAssessmentChecklists = new List<FireRiskAssessmentChecklist>();

            //When
            var result = fireRa.LatestFireRiskAssessmentChecklist;

            //Then
            Assert.IsNull(result);
        }
    }
}

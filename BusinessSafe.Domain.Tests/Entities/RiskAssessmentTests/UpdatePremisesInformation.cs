using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RiskAssessmentTests
{
    [TestFixture]
    [Category("Unit")]
    public class UpdatePremisesInformation
    {
        [Test]
        public void Given_all_required_felds_are_available_Then_create_RiskAssessment_method_creates_an_object()
        {
            //Given
            const string title = "RA Test";
            const string reference = "RA 001";
            const long clientId = 100;
            var user = new UserForAuditing();
            var result = GeneralRiskAssessment.Create(title, reference, clientId, user);
            
            //When
            result.UpdatePremisesInformation("1","1",user);
            

            //Then
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
        }
    }
}
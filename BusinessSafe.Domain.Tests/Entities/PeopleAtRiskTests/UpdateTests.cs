using System;
using BusinessSafe.Domain.CustomExceptions;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.PeopleAtRiskTests
{
    [TestFixture]
    [Category("Unit")]
    class UpdateTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_update_peopleatrisk_method_updates_all_required_feilds()
        {
            //Given
            const string name = "PeopleTest";
            const long companyId = 33749;
            const long riskAssesmentId = 33749;
            var user = new UserForAuditing();
            var result = PeopleAtRisk.Create(name,companyId, riskAssesmentId, user);

            //When
            result.Update(name, companyId, riskAssesmentId, user);

            //Then
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.RiskAssessmentId, Is.EqualTo(riskAssesmentId));
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.LastModifiedBy, Is.EqualTo(user));
        }

          [Test]
        public void Given_system_default_person_at_risk_When_update_Then_should_throw_the_correct_exception()
        {
            //Given
            var result = new MyPeopleAtRisk();
            result.MarkPeopleAtRiskAsSystemDefault();;
            
            //When
            //Then
            Assert.Throws<AttemptingToUpdateSystemDefaultException>(() => result.Update("", 1, null, null));
        }
    }

    public class MyPeopleAtRisk : PeopleAtRisk
    {
        public void MarkPeopleAtRiskAsSystemDefault()
        {
            CompanyId = null;
        }
    }
    
}

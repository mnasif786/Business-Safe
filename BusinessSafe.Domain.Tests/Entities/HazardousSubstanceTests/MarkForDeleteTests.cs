using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.HazardousSubstanceTests
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {

        [Test]
        public void Given_mark_for_delete_Then_hazard_updates_all_required_fields()
        {
            //Given
            var user = new UserForAuditing();
            var supplier = new Supplier();
            var hazardousSubstance = HazardousSubstance.Add(1, user, "original name", "reference", supplier, DateTime.Now, null, null, null,new HazardousSubstanceStandard(), string.Empty, false);

            //When
            hazardousSubstance.MarkForDelete(user);

            //Then
            Assert.That(hazardousSubstance.Deleted, Is.True);
            Assert.That(hazardousSubstance.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(hazardousSubstance.LastModifiedBy, Is.EqualTo(user));
        }
    }
}
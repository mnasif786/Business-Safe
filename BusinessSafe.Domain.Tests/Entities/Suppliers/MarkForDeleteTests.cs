using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.Suppliers
{
    [TestFixture]
    [Category("Unit")]
    public class MarkForDeleteTests
    {
        UserForAuditing _user = new UserForAuditing();

        [Test]
        public void Given_mark_for_delete_Then_hazard_updates_all_required_fields()
        {
            //Given
            var result = Supplier.Create("original name",100 , new UserForAuditing());
            
            //When
            result.MarkForDelete(_user);

            //Then
            Assert.That(result.Deleted, Is.True);
            Assert.That(result.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.LastModifiedBy, Is.EqualTo(_user));
        }
    }
}
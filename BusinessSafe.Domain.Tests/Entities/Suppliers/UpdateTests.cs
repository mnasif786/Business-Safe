using System;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.Suppliers
{
    [TestFixture]
    [Category("Unit")]
    public class UpdateTests
    {
        [Test]
        public void Given_update_fields_Then_update_supplier_method_creates_an_object()
        {
            //Given
            const string updatedName = "SupplierTest";
            const long companyId = 33749;
            var user = new UserForAuditing();

            var supplier = Supplier.Create("Original Name", companyId, user);

            //When
            supplier.Update(updatedName, user);

            //Then
            Assert.That(supplier.Name, Is.EqualTo(updatedName));
            Assert.That(supplier.CompanyId, Is.EqualTo(companyId));
            Assert.That(supplier.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(supplier.LastModifiedBy, Is.EqualTo(user));
        }
    }
}
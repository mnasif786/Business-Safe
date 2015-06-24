using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RoleTests
{

    [TestFixture]
    [Category("Unit")]
    public class CreateTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_create_hazard_method_creates_an_object()
        {
            //Given
            const string name = "HazardTest";
            const long companyId = 33749;
            var permissions = new List<Permission>() { new Permission() };
            var user = new UserForAuditing();

            //When
            var result = Role.Create(name, companyId, permissions, user);

            //Then
            Assert.That(result.Name, Is.EqualTo(name));
            Assert.That(result.CompanyId, Is.EqualTo(companyId));
            Assert.That(result.Permissions.Count, Is.EqualTo(permissions.Count));
            Assert.That(result.CreatedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(result.CreatedBy, Is.EqualTo(user));
        }
    }
}

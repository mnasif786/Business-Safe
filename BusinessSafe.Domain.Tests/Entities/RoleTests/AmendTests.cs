using System;
using System.Collections.Generic;
using BusinessSafe.Domain.Entities;
using NUnit.Framework;

namespace BusinessSafe.Domain.Tests.Entities.RoleTests
{
    [TestFixture]
    [Category("Unit")]
    public class AmendTests
    {
        [Test]
        public void Given_all_required_fields_are_available_Then_create_hazard_method_creates_an_object()
        {
            //Given
            const string name = "HazardTest";
            var permissions = new List<Permission>() { new Permission() };
            var user = new UserForAuditing();

            var role = new Role();

            //When
            role.Amend(name, permissions, user);

            //Then
            Assert.That(role.Name, Is.EqualTo(name));
            Assert.That(role.Permissions.Count, Is.EqualTo(permissions.Count));
            Assert.That(role.LastModifiedOn.Value.Date, Is.EqualTo(DateTime.Today));
            Assert.That(role.LastModifiedBy, Is.EqualTo(user));
        }
    }
}